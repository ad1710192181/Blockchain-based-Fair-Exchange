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
namespace Playfair加解密
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
            char[] miyao = Regex.Replace(密钥.Text, "[^a-zA-Z]", "").ToUpper().ToCharArray();
            char[] Input = Regex.Replace(明文.Text, "[^a-zA-Z]", "").ToUpper().ToCharArray();
            jiami(Input, Table(miyao));
        }
        public char [] Table(char [] key)
        {
           char[] alpha= new char [26];
            for(int i=0;i<26;i++)
            {
                alpha[i] = (char)('A' + i);
            }
            alpha[9] = '0';
            for(int i=0;i<key.Length;i++)
            {
                if (key[i] == 'J')
                {
                    key[i] = 'I';
                }
            }
            for(int i=0;i<key.Length;i++)
            {
                for(int k=i+1;k<key.Length;k++)
                {
                    if(key[i]==key[k])
                    {
                        key[k] ='0';
                    }
                }
            }

            for (int i = 0; i < key.Length; i++)
            {
                for (int p = 0; p < alpha.Length; p++)
                {
                    if (alpha[p] == key[i])
                    {
                        alpha[p] = '0';
                    }
                }
            }
            string kk = Regex.Replace(new string(key), "[0]", "");
            string s = Regex.Replace(new string(alpha), "[0]", "");
            string cipher =kk+ s;
            return cipher.ToCharArray();
        }
        public int[] weizhi(char a,char[]b)
        {
            int[] x = new int[2];
            for(int i=0;i<b.Length;i++)
            {
                if(a==b[i])
                {
                    x[0] = i / 5;
                    x[1] = i % 5;
                }
            }
            return x;
        }
        public void jiami(char[] A, char[] b)
        {//a是明文 ，b是密文表
            string a = A.ToString();
            if ((a.Length) % 2 != 0)
            {
                a = a + 'Q';
            }
            A = a.ToCharArray();
            //密文.AppendText(new string(b));
            char[,] k = new char[5, 5];
            for(int i=0;i<5;i++)
            {
                for(int j=0;j<5;j++)
                {
                    k[i, j] = b[i * 5 + j];
                }
            }

        }
    }
}
