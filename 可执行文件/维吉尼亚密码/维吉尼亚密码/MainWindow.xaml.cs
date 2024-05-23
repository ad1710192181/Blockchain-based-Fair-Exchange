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

namespace 维吉尼亚密码
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

        private void Button_Click(object sender, RoutedEventArgs e)//加密按钮
        {
            char[] Input = Regex.Replace(明文.Text, "[^a-zA-Z]", "").ToUpper().ToCharArray();
            char[] miyao = Regex.Replace(密钥.Text, "[^a-zA-Z]", "").ToUpper().ToCharArray();
        
            if (new string(miyao).Length==0)
            {
                MessageBox.Show("请输入密钥");
            }
            else
            {
                jiami(Input, miyao);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//解密按钮
        {
            char[] Input = Regex.Replace(密文.Text, "[^a-zA-Z]", "").ToUpper().ToCharArray();
            char[] miyao = Regex.Replace(密钥.Text, "[^a-zA-Z]", "").ToUpper().ToCharArray();

            if (new string(miyao).Length == 0)
            {
                MessageBox.Show("请输入密钥");
            }
            else
            {
                jiemi(Input, miyao);
            }
        }
        public void jiami(char []a,char []m)
        {
            密文.Clear();
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] >= 'A' && a[i] <= 'Z')
                {
                    a[i] = (char)((((a[i] - 'A'+52) + (m[i%m.Length]-'A'+26)) % 26 + 'A'));
                }
            }
            密文.AppendText(new string(a));
        }
       public void jiemi(char []a,char []m)
        {
            明文.Clear();
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] >= 'A' && a[i] <= 'Z')
                {
                    a[i] = (char)(((a[i] - 'A'+52) - (m[i % m.Length] - 'A'+26)) % 26 + 'A');
                }
            }
            明文.AppendText(new string(a));
        }
    }
}
