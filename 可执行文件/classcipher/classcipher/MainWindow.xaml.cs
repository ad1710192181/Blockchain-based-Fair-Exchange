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

namespace classcipher
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

        static int mode = 0;//密码模式
        public void ks(object sender, RoutedEventArgs e)
        {
            mode = 1;
        }
        public void bz(object sender, RoutedEventArgs e)
        {
            mode = 2;
        }
        public void pl(object sender, RoutedEventArgs e)
        {
            mode = 3;
        }
        public void ver(object sender, RoutedEventArgs e)
        {
            mode = 4;
        }
        public void hill(object sender, RoutedEventArgs e)
        {
            mode = 5;
        }
        public void vz(object sender, RoutedEventArgs e)
        {
            mode = 6;
        }

        private void g(object sender, RoutedEventArgs e)
        {
            if (mode == 0)
            {
                MessageBox.Show("请确定加密模式");
            }
            if (mode == 1)
            {
                密文.Clear();
                char[] ks = Regex.Replace(明文.Text, "[^a-zA-Z]", "").ToUpper().ToCharArray();
                for (int i = 0; i < ks.Length; i++)
                {
                    if (ks[i] >= 'A' && ks[i] <= 'Z')
                    {
                        ks[i] = (char)(((ks[i] - 'A') + 3) % 26 + 'A');
                    }
                }
                密文.AppendText(new string(ks));
            }
            if (mode == 2)
            {//标准字头
                char[] ab = new char[26];
                char[] bzmiyao = Regex.Replace(密钥.Text, "[^a-zA-Z]", "").ToUpper().ToCharArray();
                char[] bzInput = Regex.Replace(明文.Text, "[^a-zA-Z]", "").ToUpper().ToCharArray();

                if (new string(bzmiyao).Length == 0)
                {
                    MessageBox.Show("请输入密钥");
                }
                else if (!again(bzmiyao))
                {
                    MessageBox.Show("请输入非重复字母密钥");
                    密钥.Clear();
                }
                else
                {
                    bzjiami(bzInput, ciphertable(minwentablecreate(ab), bzmiyao));
                }
                bool again(char[] aaa)
                {
                    for (int i = 0; i < aaa.Length; i++)
                    {
                        for (int l = i + 1; l < aaa.Length; l++)
                        {
                            if (aaa[i] == aaa[l])
                            {
                                return false;
                            }
                        }
                    }
                    return true;
                }
                char[] minwentablecreate(char[] minwentable)
                {

                    for (int i = 0; i < 26; i++)
                    {
                        minwentable[i] = (char)('A' + i);

                    }

                    return minwentable;
                }
                char[] ciphertable(char[] alpha, char[] key)
                {
                    char[] cipher = new char[key.Length];
                    for (int i = 0; i < key.Length; i++)
                    {
                        cipher[i] = key[i];
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
                    string s = Regex.Replace(new string(alpha), "[0]", "");
                    string cipher1 = new string(cipher) + s;
                    cipher = cipher1.ToCharArray();
                    return cipher;
                }
                void bzjiami(char[] a, char[] c)
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
            }
            if (mode == 3)
            {//playfair
               
                PlayFair_E(Regex.Replace(明文.Text, "[^a-zA-Z]", "").ToUpper(), Regex.Replace(密钥.Text, "[^a-zA-Z]", "").ToUpper());
                bool PlayFair_E(string message, string key)
                {
                    char[] Temp = Regex.Replace(Regex.Replace(key, "[^a-zA-Z]", "").ToUpper(), "[J]", "I").ToCharArray();
                    if (Temp.Length != key.Length)
                        return false;
                    char[] Key = new char[25];
                    byte[] sigh = new byte[25];
                    short i, j, k;
                    for (i = 0; i < 25; i++)
                        sigh[i] = 0;
                    for (i = 0, j = 0; i < Temp.Length && j < 25; i++)
                    {
                        k = (short)(Temp[i] > 'I' ? Temp[i] - 'B' : Temp[i] - 'A');
                        if (sigh[k] == 0)
                        {
                            Key[j] = Temp[i];
                            sigh[k] = 1;
                            j++;
                        }
                    }
                    for (i = 0; j < 25; j++)
                    {
                        while (sigh[i] == 1)
                            i++;
                        Key[j] = (char)(i > 'I' - 'A' ? i + 'B' : i + 'A');
                        i++;
                    }

                    key = Regex.Replace(Regex.Replace(message, "[^a-zA-Z]", "").ToUpper(), "[J]", "I");
                    char[] Message;
                    if (key.Length % 2 == 1)
                    {
                        short rd = (short)(new Random()).Next(0, key.Length);
                        Message = (key.Substring(0, rd) + "Q" + key.Substring(rd)).ToCharArray();
                    }
                    else
                        Message = key.ToCharArray();
                    Console.WriteLine(key + "\n" + new string(Message));
                    for (i = 0; i < Message.Length; i += 2)
                    {
                        j = 0; k = 0;
                        while (Key[j] != Message[i])
                            j++;
                        while (Key[k] != Message[i + 1])
                            k++;
                        if (j / 5 == k / 5)
                        {
                            Message[i] = Key[(j / 5) * 5 + (j % 5 + 1) % 5];
                            Message[i + 1] = Key[(k / 5) * 5 + (k % 5 + 1) % 5];
                        }
                        else if (j % 5 == k % 5)
                        {
                            Message[i] = Key[(j / 5 + 1) % 5 * 5 + j % 5];
                            Message[i + 1] = Key[(k / 5 + 1) % 5 * 5 + k % 5];
                        }
                        else
                        {
                            Message[i] = Key[(j / 5) * 5 + k % 5];
                            Message[i + 1] = Key[(k / 5) * 5 + j % 5];
                        }
                    }
                   密文.AppendText(new string(Message)) ;
                    return true;
                }
            }
            if (mode == 4)
            {//vernam
                char[] verkey = Regex.Replace(密钥.Text, "[^a-zA-Z]", "").ToUpper().ToCharArray();
                char[] verinput = Regex.Replace(密文.Text, "[^a-zA-Z]", "").ToUpper().ToCharArray();
                verjiami(verinput, verkey);
                void verjiami(char[] a, char[] b)
                {
                    密文.Clear();
                    for (int i = 0; i < a.Length; i++)
                    {
                        a[i] = (char)(((a[i] - 'A') + (b[i % b.Length] - 'A') + 26) % 26 + 'A');
                    }
                    密文.AppendText(new string(a));
                }
            }
            if (mode == 5)
            {//hill

            }
            if (mode == 6)
            {//维吉尼亚
                char[] Input = Regex.Replace(明文.Text, "[^a-zA-Z]", "").ToUpper().ToCharArray();
                char[] miyao = Regex.Replace(密钥.Text, "[^a-zA-Z]", "").ToUpper().ToCharArray();

                if (new string(miyao).Length == 0)
                {
                    MessageBox.Show("请输入密钥");
                }
                else
                {
                    vzjiami(Input, miyao);
                }
                void vzjiami(char[] a, char[] m)
                {
                    密文.Clear();
                    for (int i = 0; i < a.Length; i++)
                    {
                        if (a[i] >= 'A' && a[i] <= 'Z')
                        {
                            a[i] = (char)((((a[i] - 'A' + 52) + (m[i % m.Length] - 'A' + 26)) % 26 + 'A'));
                        }
                    }
                    密文.AppendText(new string(a));
                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mode == 0)
            {
                MessageBox.Show("请确定加密模式");
            }
            if (mode == 1)
            {//kaisa
                char[] ksInput = Regex.Replace(密文.Text, "[^a-zA-Z]", "").ToUpper().ToCharArray();
                ksjiemi(ksInput, 3);
                void ksjiemi(char[] a, int k)
                {
                    for (int i = 0; i < a.Length; i++)
                    {
                        if (a[i] >= 'A' && a[i] <= 'Z')
                        {
                            a[i] = (char)(((a[i] - 'A') - k + 26) % 26 + 'A');
                        }
                    }
                    明文.Clear();
                    明文.AppendText(new string(a));
                }
            }

            if (mode == 2)
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
                        bzjiemi(Input, ciphertable(minwentablecreate(a), miyao), minwentablecreate(a));
                    }
                    char[] minwentablecreate(char[] minwentable)
                    {

                        for (int i = 0; i < 26; i++)
                        {
                            minwentable[i] = (char)('A' + i);

                        }

                        return minwentable;
                    }
                    char[] ciphertable(char[] alpha, char[] key)
                    {
                        char[] cipher = new char[key.Length];
                        for (int i = 0; i < key.Length; i++)
                        {
                            cipher[i] = key[i];
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
                        string s = Regex.Replace(new string(alpha), "[0]", "");
                        string cipher1 = new string(cipher) + s;
                        cipher = cipher1.ToCharArray();
                        return cipher;
                    }
                    bool again(char[] ag)
                    {
                        for (int i = 0; i < ag.Length; i++)
                        {
                            for (int l = i + 1; l < ag.Length; l++)
                            {
                                if (ag[i] == ag[l])
                                {
                                    return false;
                                }
                            }
                        }
                        return true;
                    }
                    void bzjiemi(char[] fa, char[] c, char[] b)
                    {    //a[]是密文 ,b[]是原字母表，c[]是密文表
                        明文.Clear();
                        for (int i = 0; i < fa.Length; i++)
                        {
                            int k = 0;
                            while (fa[i] != c[k])
                            {
                                k++;
                            }
                            fa[i] = b[k];
                        }
                        明文.AppendText(new string(fa));
                    }
             }
            if (mode == 3)
                {
                PlayFair_D(Regex.Replace(密文.Text, "[^a-zA-Z]", "").ToUpper(), Regex.Replace(密钥.Text, "[^a-zA-Z]", "").ToUpper());
                 bool PlayFair_D(string message, string key)
                {
                    char[] Temp = Regex.Replace(Regex.Replace(key, "[^a-zA-Z]", "").ToUpper(), "[J]", "I").ToCharArray();
                    if (Temp.Length != key.Length)
                        return false;
                    char[] Key = new char[25];
                    byte[] sigh = new byte[25];
                    short i, j, k;
                    for (i = 0; i < 25; i++)
                        sigh[i] = 0;
                    for (i = 0, j = 0; i < Temp.Length && j < 25; i++)
                    {
                        k = (short)(Temp[i] > 'I' ? Temp[i] - 'B' : Temp[i] - 'A');
                        if (sigh[k] == 0)
                        {
                            Key[j] = Temp[i];
                            sigh[k] = 1;
                            j++;
                        }
                    }
                    for (i = 0; j < 25; j++)
                    {
                        while (sigh[i] == 1)
                            i++;
                        Key[j] = (char)(i > 'I' - 'A' ? i + 'B' : i + 'A');
                        i++;
                    }

                    key = Regex.Replace(Regex.Replace(message, "[^a-zA-Z]", "").ToUpper(), "[J]", "I");
                    char[] Message;
                    if (key.Length % 2 == 1)
                    {
                        short rd = (short)(new Random()).Next(0, key.Length);
                        Message = (key.Substring(0, rd) + "Q" + key.Substring(rd)).ToCharArray();
                    }
                    else
                        Message = key.ToCharArray();
                    Console.WriteLine(key + "\n" + new string(Message));
                    for (i = 0; i < Message.Length; i += 2)
                    {
                        j = 0; k = 0;
                        while (Key[j] != Message[i])
                            j++;
                        while (Key[k] != Message[i + 1])
                            k++;
                        if (j / 5 == k / 5)
                        {
                            Message[i] = Key[(j / 5) * 5 + (j % 5 + 4) % 5];
                            Message[i + 1] = Key[(k / 5) * 5 + (k % 5 + 4) % 5];
                        }
                        else if (j % 5 == k % 5)
                        {
                            Message[i] = Key[(j / 5 + 4) % 5 * 5 + j % 5];
                            Message[i + 1] = Key[(k / 5 + 4) % 5 * 5 + k % 5];
                        }
                        else
                        {
                            Message[i] = Key[(j / 5) * 5 + k % 5];
                            Message[i + 1] = Key[(k / 5) * 5 + j % 5];
                        }
                    }
                    明文.Clear();
                    明文.AppendText(new string(Message));
                    return true;
                }
            }
            if (mode == 4)
                {
                    char[] verkey = Regex.Replace(密钥.Text, "[^a-zA-Z]", "").ToUpper().ToCharArray();
                    char[] verinput = Regex.Replace(密文.Text, "[^a-zA-Z]", "").ToUpper().ToCharArray();
                    verjiemi(verinput, verkey);
                    void verjiemi(char[] a, char[] b)
                    {
                        明文.Clear();
                        for (int i = 0; i < a.Length; i++)
                        {
                            a[i] = (char)(((a[i] - 'A') - (b[i % b.Length] - 'A') + 52) % 26 + 'A');
                        }
                        明文.AppendText(new string(a));
                    }
             }
            if (mode == 5)
                {

                }
            if (mode == 6)
             {
                    char[] vzInput = Regex.Replace(密文.Text, "[^a-zA-Z]", "").ToUpper().ToCharArray();
                    char[] miyao = Regex.Replace(密钥.Text, "[^a-zA-Z]", "").ToUpper().ToCharArray();

                    if (new string(miyao).Length == 0)
                    {
                        MessageBox.Show("请输入密钥");
                    }
                    else
                    {
                        vzjiemi(vzInput, miyao);
                    }
                    void vzjiemi(char[] a, char[] m)
                    {
                        明文.Clear();
                        for (int i = 0; i < a.Length; i++)
                        {
                            if (a[i] >= 'A' && a[i] <= 'Z')
                            {
                                a[i] = (char)(((a[i] - 'A' + 52) - (m[i % m.Length] - 'A' + 26)) % 26 + 'A');
                            }
                        }
                        明文.AppendText(new string(a));
                    }
             }
            }
        }
    }

