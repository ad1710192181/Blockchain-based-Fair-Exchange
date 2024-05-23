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

namespace Hill密码
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
            char[] Input = Regex.Replace(明文.Text, "[^a-zA-Z]", "").ToUpper().ToCharArray();
            char[] key = Regex.Replace(密钥.Text, "[^a-zA-Z]", "").ToUpper().ToCharArray();
            jiami(Input, juzhen(key));
        }
        public int [,] juzhen(char []a)
        {
            int[,] jz = new int[2, 2];
            jz[0, 0] = a[0]-'A';
            jz[0, 1] = a[1]-'A';
            jz[1, 0] = a[2]-'A';
            jz[1, 1] = a[3]-'A';
           
            return jz;
        }
        public void jiami(char []a,int [,]jz)
        {
            if(a.Length%2!=0)
            {
                string s = a.ToString();
                s = s + 'A';
                a = s.ToCharArray();
            }
            int i;
            for(i=0;i<a.Length;i+=2)
            {
                a[i] = (char)(((a[i] - 'A') * jz[0, 0] + (a[i + 1] - 'A') * jz[1, 0]) % 26+'A');
                a[i + 1] = (char)(((a[i] - 'A') * jz[0, 1] + (a[i + 1] - 'A') * jz[1, 1]) % 26+'A');
                //密文.AppendText(a[i].ToString());
                //密文.AppendText(" a[i]1");
            }
            //密文.AppendText(" a[i]2");
            密文.AppendText(new string(a));
        }
    }
}
