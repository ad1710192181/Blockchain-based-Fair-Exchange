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
namespace 标准字头密码
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
            char[] a = new char[26];
            char[] miyao = Regex.Replace(密钥.Text, "[^a-zA-Z]", "").ToUpper().ToCharArray();
            char[] Input = Regex.Replace(密文.Text, "[^a-zA-Z]", "").ToUpper().ToCharArray();

            if (new string(miyao).Length == 0)
            {
                MessageBox.Show("请输入密钥");
            }
            else if (!again(miyao))
            {
                MessageBox.Show("请输入非重复字母密钥");
                密钥.Clear();
            }
            else
            {
                jiemi(Input, ciphertable(minwentablecreate(a), miyao), minwentablecreate(a));
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            char[] a=new char [26];
            char[] miyao = Regex.Replace(密钥.Text, "[^a-zA-Z]", "").ToUpper().ToCharArray();
            char[] Input = Regex.Replace(明文.Text, "[^a-zA-Z]", "").ToUpper().ToCharArray();

            if (new string(miyao).Length==0)
            {
                MessageBox.Show("请输入密钥");
            }
            else if(!again(miyao))
            {
                MessageBox.Show("请输入非重复字母密钥");
                密钥.Clear();
            }
            else
            {
                jiami(Input, ciphertable(minwentablecreate(a), miyao));
            }

           
        }
        public char[] minwentablecreate(char []minwentable)
        {
            
            for(int i=0;i<26;i++)
            {
                minwentable[i] = (char)('A'+i);
                
            }
            
            return minwentable;
        }
        public char [] ciphertable(char [] alpha,char[] key)
        {
            char[] cipher = new char[key.Length];
            for(int i=0;i<key.Length;i++)
            {
                cipher[i] = key[i];
            }
            for(int i=0;i<key.Length;i++)
            {
                for(int p=0;p<alpha.Length;p++)
                {
                    if(alpha[p]==key[i])
                    {
                        alpha[p] = '0';
                    }
                }
            }
            string s=Regex.Replace(new string(alpha), "[0]", "");
            string cipher1 = new string(cipher) + s;
            cipher = cipher1.ToCharArray();
            return cipher;
        }
       
        public bool again(char [] a)
        {
            for(int i=0;i<a.Length;i++)
            {
                for(int l=i+1;l<a.Length;l++)
                {
                    if(a[i]==a[l])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public void jiami(char [] a,char []c)
        {
            密文.Clear();
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] >= 'A' && a[i] <= 'Z')
                {
                    a[i] = c[a[i] - 'A'];
                }
            }
            密文.AppendText(new string(a));
        }
        public void jiemi(char []a,char []c,char []b)
        {    //a[]是密文 ,b[]是原字母表，c[]是密文表
            明文.Clear();
            for (int i = 0; i < a.Length; i++)
            {
               int k = 0;
               while(a[i]!=c[k])
                {
                    k++;
                }
                a[i] = b[k];
            }
            明文.AppendText(new string(a));
        }
    }
}
