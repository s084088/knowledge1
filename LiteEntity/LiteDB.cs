using LiteEntity.Tables;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LiteEntity
{
    public class LiteDB : DbContext
    {
        /// <summary>
        /// 取表中非删除的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IQueryable<T> Sets<T>() where T : DbBase => Set<T>().Where(l => l.DeleteFlag == 0);

        /// <summary>
        /// 数据库配置
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies()
                .UseMySql("Server=47.103.142.91;Port=3306;Database=zhishi1; User=root;Password=UuTPdVdwgWrGMSQsSbS45oakyDawgT3X;CharSet=utf8;Convert Zero Datetime=True;Allow Zero Datetime=True");
        }

        /// <summary>
        /// 模型建立
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<KP>(entity =>
            {
                entity.HasMany(l => l.Preconditions).WithOne(l => l.Target).HasForeignKey(l => l.TargetID);
                entity.HasMany(l => l.Extends).WithOne(l => l.Origin).HasForeignKey(l => l.OriginID);
            });
        }
    }
}