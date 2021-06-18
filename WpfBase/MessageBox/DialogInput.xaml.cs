using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
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
    /// DialogInput.xaml 的交互逻辑
    /// </summary>
    public partial class DialogInput : UserControl
    {
        public DialogInput()
        {
            InitializeComponent();
        }

        public async static Task<string> Show(string msg, string input = null, string target = "RootDialog")
        {
            var sampleMessageDialog = new DialogInput
            {
                Message = { Text = msg },
                Input = { Text = input },
            };

            bool? ret = (await DialogHost.Show(sampleMessageDialog, target)) as bool?;

            return ret == true ? sampleMessageDialog.Input.Text : null;
        }
    }
}