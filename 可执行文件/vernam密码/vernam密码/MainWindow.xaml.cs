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
using System.Text.RegularExpressions;
namespace vernam密码
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            char[] key = Regex.Replace(密钥.Text, "[^a-zA-Z]", "").ToUpper().ToCharArray();
            char[] input = Regex.Replace(明文.Text, "[^a-zA-Z]", "").ToUpper().ToCharArray();
            jiami(input, key);
        }
       public void jiami(char []a,char [] b)
        {
            密文.Clear();
            for(int i=0;i<a.Length;i++)
            {
                a[i] = (char)(((a[i] - 'A') + (b[i % b.Length] - 'A')+26) % 26+'A');
            }
            密文.AppendText(new string(a));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//解密
        {
            char[] key = Regex.Replace(密钥.Text, "[^a-zA-Z]", "").ToUpper().ToCharArray();
            char[] input = Regex.Replace(密文.Text, "[^a-zA-Z]", "").ToUpper().ToCharArray();
            jiemi(input, key);
        }
        public void jiemi(char []a,char []b)
        {
            明文.Clear();
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = (char)(((a[i] - 'A')-(b[i % b.Length] - 'A') + 52) % 26 + 'A');
            }
            明文.AppendText(new string(a));
        }
    }
}
