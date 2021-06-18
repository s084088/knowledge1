using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfBase.MessageBox
{
    /// <summary>
    /// DialogYesNo.xaml 的交互逻辑
    /// </summary>
    public partial class DialogYesNo : UserControl
    {
        public DialogYesNo()
        {
            InitializeComponent();
        }

        public async static Task<bool> Show(string msg, string target = "RootDialog")
        {
            var sampleMessageDialog = new DialogYesNo { Message = { Text = msg } };

            bool? ret = (await DialogHost.Show(sampleMessageDialog, target)) as bool?;

            return ret == true;
        }
    }
}
