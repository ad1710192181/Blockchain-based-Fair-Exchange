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

namespace 加密算法
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
            解密.Clear();
            string input = 明文.Text;
            char[] Input = Regex.Replace(input, "[^a-zA-Z]", "").ToUpper().ToCharArray();

            string M = 密钥.Text;
            if(M.Length==0)
            {
                MessageBox.Show("请输入密钥");
            }
            else
            {
                int K = int.Parse(密钥.Text);
                jiami(Input, K);
            }
           
        }
        public void jiami(char [] a,int k)
        {
           for(int i=0;i<a.Length;i++)
            {
                if(a[i]>='A' && a[i]<='Z')
                {
                    a[i] = (char)(((a[i] - 'A') +k) % 26 + 'A');
                }
                else if(a[i]>='a' && a[i]<='z')
                {
                    a[i] = (char)(((a[i] - 'a') + k) % 26 + 'a');
                }
               
            }

            
            解密.AppendText(new string(a));

        }
        public void jiemi(char [] a,int k)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] >= 'A' && a[i] <= 'Z')
                {
                    a[i] = (char)(((a[i] - 'A') - k+26) % 26 + 'A');
                }
                else if (a[i] >= 'a' && a[i] <= 'z')
                {
                    a[i] = (char)(((a[i] - 'a') - k+26) % 26 + 'a');
                }
               
            }
            
            明文.AppendText(new string(a));
        }
        public void jiemi2(char[] a, int k)
        {
            
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] >= 'A' && a[i] <= 'Z') 
                {
                    a[i] = (char)(((a[i] - 'A') - k+26) % 26 + 'A');
                }
                else if (a[i] >= 'a' && a[i] <= 'z')
                {
                    a[i] = (char)(((a[i] - 'a') - k+26) % 26 + 'a');
                }
               
            }
          
            结果.AppendText(new string(a));
        }
       

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            明文.Clear();
            string input = 解密.Text;
            string M = 密钥.Text;
            char[] Input = Regex.Replace(input, "[^a-zA-Z]", "").ToUpper().ToCharArray();

            if (M.Length!=0)
            {
                int K = int.Parse(密钥.Text);
                jiemi(Input, K);
            }
            else
            {
                结果.Clear();
                for(int i=0;i<26;i++)
                {
                    结果.AppendText("密钥="+i+"\n");
                    jiemi2(input.ToCharArray(), i);
                    结果.AppendText("\n");
                }
            }
        }

        private void 破解_Click(object sender, RoutedEventArgs e)
        {
            string input = 解密.Text;
            string M = 密钥.Text;
            char[] Input = Regex.Replace(input, "[^a-zA-Z]", "").ToUpper().ToCharArray();
            结果.Clear();
            for (int i = 0; i < 26; i++)
            {
                结果.AppendText("密钥=" + i + "\n");
                jiemi2(Input, i);
                结果.AppendText("\n");
            }
        }
    }
}