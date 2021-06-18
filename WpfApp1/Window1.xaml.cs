using LiteEntity;
using LiteEntity.Tables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        private readonly LiteDB liteDB = new LiteDB();
        public Window1()
        {
            InitializeComponent();
            Web.Navigate(new Uri(Directory.GetCurrentDirectory() + "/Html/Html1.html"));


            MediaPlayer md = new MediaPlayer();
            try
            {
                md.Open(new Uri("E:\\ossbackup\\fd0621e8b3fcfffde1822b1235074cd1.mp4"));
                var vc = md.NaturalDuration.TimeSpan;

                var vvv = vc;
            }
            catch { }
            finally
            {
                md.Close();
            }

        }

        int show = 0;
        private void btnShowHide_Click(object sender, RoutedEventArgs e)
        {
            Web.InvokeScript("jsShowHide", show);
            if (show == 0) show = 1;
            else show = 0;
        }
        private void btnPushData_Click(object sender, RoutedEventArgs e)
        {
            List<KP> kPs = liteDB.Set<KP>().ToList();
            List<KPR> kPRs = liteDB.Set<KPR>().ToList();

            var nodes = kPs.Select(l => new Node
            {
                id = l.ID,
                name = l.Name,
            }).ToArray();

            var links = kPRs.Select(l => new Link
            {
                id = l.ID,
                source = l.OriginID,
                target = l.TargetID,
            }).ToArray();

            var obj = new Chart { nodes = nodes, links = links };


            var vvv = Web.InvokeScript("jsPushData", obj);

        }

        public class Chart
        {
            public Node[] nodes { get; set; }

            public Link[] links { get; set; }
        }

        public class Node
        {
            public string id { get; set; }

            public string name { get; set; }
        }

        public class Link
        {
            public string id { get; set; }

            public string source { get; set; }

            public string target { get; set; }
        }
    }
}
