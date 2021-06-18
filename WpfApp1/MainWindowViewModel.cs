using LiteEntity;
using LiteEntity.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Util;
using Util.Helper;
using WpfBase.MessageBox;
using WpfBase.Mvvm;

namespace WpfApp1
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly LiteDB liteDB = new LiteDB();
        private ObservableCollection<Entity> entities;
        private Entity entity;
        private EKP eKP;
        private KP kP;
        private Visibility isShow = Visibility.Hidden;
        private Action clearLine;

        public ObservableCollection<Entity> Entities { get => entities; set { entities = value; PCEH(); } }

        public Entity Entity { get => entity; set { entity = value; PCEH(); } }

        public EKP EKP { get => eKP; set { eKP = value; PCEH(); } }

        public KP KP
        {
            get => kP; set
            {
                KP kP1 = kP;
                kP = value;
                PCEH();
                IsShow = kP == null ? Visibility.Hidden : Visibility.Visible;
                if (kP1 != kP) clearLine?.Invoke();
            }
        }

        public Visibility IsShow { get => isShow; set { isShow = value; PCEH(); } }


        public ICommand AddEntityCommand => new CommandBase(async o =>
        {
            if (Entities == null) return;
            string str = await DialogInput.Show("添加实体");
            if (str == null) return;
            if (Entities.Select(l => l.Name).Contains(str))
            {
                await DialogOK.Show("实体已存在");
                return;
            }
            Entity = new Entity { Name = str };
            Entities.Add(Entity);
            liteDB.Add(Entity);
            await liteDB.SaveChangesAsync();
        });

        public ICommand EditEntityCommand => new CommandBase(async o =>
        {
            if (Entity == null) return;
            string str = await DialogInput.Show("修改实体", Entity.Name);
            if (str == null) return;
            if (str == Entity.Name) return;
            if (Entities.Select(l => l.Name).Contains(str))
            {
                await DialogOK.Show("实体已存在");
                return;
            }
            Entity.Name = str;
            await liteDB.SaveChangesAsync();
        });

        public ICommand DeleteEntityCommand => new CommandBase(async o =>
        {
            if (Entity == null) return;
            if (Entity.EKPs.Count > 0)
            {
                await DialogOK.Show("该类下有知识点,无法深处");
                return;
            }
            if (!await DialogYesNo.Show($"确定删除 {Entity.Name} 吗?")) return;
            liteDB.Remove(Entity);
            Entities.Remove(Entity);
            await liteDB.SaveChangesAsync();
        });

        public ICommand AddKPCommand => new CommandBase(async o =>
        {
            if (Entity == null) return;
            if (Entity.EKPs == null) Entity.EKPs = new ObservableCollection<EKP>();
            string str = await DialogInput.Show($"{Entity.Name} 添加知识点");
            if (str == null) return;
            if (entities.SelectMany(l => l.EKPs?.Select(k => k.KP.Name)).Contains(str))
            {
                await DialogOK.Show("知识点已存在");
                return;
            }
            EKP = new EKP { KP = new KP { Name = str } };
            Entity.EKPs.Add(EKP);
            await liteDB.SaveChangesAsync();
        });

        public ICommand EditKPCommand => new CommandBase(async o =>
        {
            if (EKP == null) return;
            string str = await DialogInput.Show("修改知识点", EKP.KP.Name);
            if (str == null) return;
            if (str == EKP.KP.Name) return;
            if (entities.SelectMany(l => l.EKPs?.Select(k => k.KP.Name)).Contains(str))
            {
                await DialogOK.Show("知识点已存在");
                return;
            }
            EKP.KP.Name = str;
            await liteDB.SaveChangesAsync();
        });

        public ICommand DeleteKPCommand => new CommandBase(async o =>
        {
            if (EKP == null) return;
            if (!await DialogYesNo.Show($"确定要在 {Entity.Name} 下删除 {EKP.KP.Name} 吗?")) return;
            liteDB.Remove(EKP);
            if (EKP.KP.EKPs.Count == 1)
            {
                liteDB.Remove(EKP.KP);
                if (EKP.KP == KP) KP = null;
                liteDB.RemoveRange(EKP.KP.Extends);
                liteDB.RemoveRange(EKP.KP.Preconditions);
            }
            EKP = null;
            await liteDB.SaveChangesAsync();
        });

        public ICommand AddPrecondition => new CommandBase(async o =>
        {
            KP nkp = o as KP;
            if (KP == null) return;
            if (KP.Name == nkp.Name) return;

            KPR kPR = KP.Preconditions.FirstOrDefault(l => l.Origin == nkp);
            if (kPR == null)
            {
                kPR = KP.Extends.FirstOrDefault(l => l.Target == nkp);
                if (kPR != null) liteDB.Remove(kPR);
                kPR = new KPR { Origin = nkp, Target = KP };
                liteDB.Add(kPR);
            }
            else
            {
                liteDB.Remove(kPR);
            }
            await liteDB.SaveChangesAsync();
            ReSelect();
        });

        public ICommand AddExtend => new CommandBase(async o =>
        {
            KP nkp = o as KP;
            if (KP == null) return;
            if (KP.Name == nkp.Name) return;

            KPR kPR = KP.Extends.FirstOrDefault(l => l.Target == nkp);
            if (kPR == null)
            {
                kPR = KP.Preconditions.FirstOrDefault(l => l.Origin == nkp);
                if (kPR != null) liteDB.Remove(kPR);
                kPR = new KPR { Target = nkp, Origin = KP };
                liteDB.Add(kPR);
            }
            else
            {
                liteDB.Remove(kPR);
            }
            await liteDB.SaveChangesAsync();
            ReSelect();
        });

        private void ReSelect()
        {
            KP kP = KP;
            KP = null;
            KP = kP;
        }

        public ICommand SelectKP => new CommandBase(o => KP = o as KP);


        public MainWindowViewModel()
        {
            Init();
        }

        public MainWindowViewModel(Action clearLine) : this()
        {
            this.clearLine = clearLine;
        }

        public async void Init()
        {
            Entities = new ObservableCollection<Entity>();
            List<Entity> entities = await liteDB.Set<Entity>().ToListAsync();
            entities.ForEach(l => Entities.Add(l));
        }
    }
}
