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

namespace 乘法密码
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

        private void 加密_Click(object sender, RoutedEventArgs e)
        {
            string input = 明文.Text;
            char[] Input= Regex.Replace(input, "[^a-zA-Z]", "").ToUpper().ToCharArray();
            string M = 密钥.Text;
            if(M.Length==0)
            {
                MessageBox.Show("请输入密钥");
            }
            else
            {
                int k = int.Parse(密钥.Text);
                jiami(Input, k);
            }

        }

        private void 解密_Click(object sender, RoutedEventArgs e)
        {
            //string input = 密文.Text;
            char[] Input = Regex.Replace(密文.Text, "[^a-zA-Z]", "").ToUpper().ToCharArray();
            string M = 密钥.Text;
            if (M.Length == 0)
            {
                MessageBox.Show("请输入密钥");
            }
            else
            {
                int k = int.Parse(密钥.Text);
                jiemi(Input, k);
            }
        }

        private void 破解_Click(object sender, RoutedEventArgs e)
        {
            破解框.Clear();
            string input = Regex.Replace(密文.Text, "[^a-zA-Z]", "").ToUpper();
            for (int i=1;i<26;i++)
            {
                if(i%2!=0 && i%13!=0 )
                {
                    破解框.AppendText("密钥=" + i + "\n");
                    pojie(input.ToCharArray(), i);
                    破解框.AppendText("\n\n");
                }
            }

        }
        public void jiami(char [] a,int k)
        {

            for(int i=0;i<a.Length;i++)
            {
                if(a[i]>='A' && a[i]<='Z')
                {
                    a[i] = (char)(((a[i] - 'A') * k) % 26 + 'A');
                }
                else if(a[i]>='a' && a[i]<='z')
                {
                    a[i] = (char)(((a[i] - 'a') * k) % 26 + 'a');
                }
            }
            密文.Clear();
            密文.AppendText(new string(a));
        }
        public void jiemi(char [] a,int k)
        {
            int k1=1;
            while(k1*k%26!=1)
            {
                k1++;
            }                                                                                                                                                                                                     
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] >= 'A' && a[i] <= 'Z')
                {
                    a[i] = (char)(((a[i] - 'A') * k1) % 26 + 'A');
                }
                else if (a[i] >= 'a' && a[i] <= 'z')
                {
                    a[i] = (char)(((a[i] - 'a') * k1) % 26 + 'a');
                }
            }
            明文.Clear();
            明文.AppendText(new string(a));
        }
        public void pojie(char [] a,int k)
        {
            int k1 = 1;
            while (k1 * k%26 != 1)
            {
                k1++;
            }
            //Console.WriteLine(k + "  " + k1);
            for (int i = 0; i < a.Length; i++)
            {
                //Console.Write(a[i] + "   ");
                if (a[i] >= 'A' && a[i] <= 'Z')
                {
                    a[i] = (char)(((a[i] - 'A') * k1) % 26 + 'A');
                }
                else if (a[i] >= 'a' && a[i] <= 'z')
                {
                    a[i] = (char)(((a[i] - 'a') * k1) % 26 + 'a');
                }
                //Console.WriteLine(a[i]);
            }
            破解框.AppendText(new string(a));
        }
    }
}
