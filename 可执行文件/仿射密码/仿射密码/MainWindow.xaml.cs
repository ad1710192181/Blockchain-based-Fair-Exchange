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
namespace 仿射密码
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

   
  

    
        public void jiami(char [] a,int k1,int k2)
        {
            for(int i=0;i<a.Length;i++)
            {
                if (a[i] >='A'  && a[i]<='Z')
                {
                    a[i] = (char)((k1 + k2 * (a[i] - 'A')) % 26+'A');
                }
            }
            密文.Clear();
            密文.AppendText(new string(a));
        }
        public void jiemi(char [] a,int k1,int k2)
        {
            int K2 = 1;
            while(K2*k2%26!=1)
            {
                K2++;
            }
            for(int i=0;i<a.Length;i++)
            {
                if(a[i]>='A' && a[i]<='Z')
                {
                    a[i] = (char)(K2 * ((a[i] - 'A') - k1 +26)% 26 + 'A');
                }
            }
            明文.Clear();
            明文.AppendText(new string(a));
        }
        public void pojie(char [] a,int k1,int k2)
        {
            int K2 = 1;
            while (K2 * k2 % 26 != 1)
            {
                K2++;
            }
            for (int i=0;i<a.Length;i++)
            {
                if(a[i]>='A' && a[i]<='Z')
                {
                    a[i] = (char)(K2 * ((a[i] - 'A') - k1+26) % 26 + 'A');
                }
            }
            破解框.AppendText(new string(a));
        }

       

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
            char[] Input = Regex.Replace(明文.Text, "[^a-zA-Z]", "").ToUpper().ToCharArray();
            string M1 = 密钥1.Text;
            string M2 = 密钥2.Text;
            if (M1.Length == 0 || M2.Length == 0)
            {
                MessageBox.Show("请输入正确的密钥");
            }
            else
            {
                int K1 = int.Parse(密钥1.Text);
                int K2 = int.Parse(密钥2.Text);
                jiami(Input, K1, K2);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            char[] Input = Regex.Replace(密文.Text, "[^a-zA-Z]", "").ToUpper().ToCharArray();
            string M1 = 密钥1.Text;
            string M2 = 密钥2.Text;
            if (M1.Length == 0 || M2.Length == 0)
            {
                MessageBox.Show("请输入正确的密钥");
            }
            else
            {
                int K1 = int.Parse(密钥1.Text);
                int K2 = int.Parse(密钥2.Text);
                jiemi(Input, K1, K2);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            破解框.Clear();
            
            char[] Input = Regex.Replace(密文.Text, "[^a-zA-Z]", "").ToUpper().ToCharArray();
            for(int i=0;i<26;i++)
            {
                for(int x=0;x<26;x++)
                {
                    if(x%2!=0 && x%13!=0)
                    {
                        破解框.AppendText("密钥1=" + i + "   密钥2=" + x + "\n");
                        pojie(Input, i, x);
                        破解框.AppendText("\n\n");
                    }
                }
            }
        }
    }
}
