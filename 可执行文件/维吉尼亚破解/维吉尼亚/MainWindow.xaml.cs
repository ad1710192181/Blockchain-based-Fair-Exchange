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

namespace 维吉尼亚
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        string message;
        //用于存储密文
        string[] repetition;
        string[] divideStr;
        public string[] keyPossible;
        public string Result;
        public string[] mutual;
        private int[] coresidual;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string MIWEN_TXT = 密文.Text;
            //将密文从密文框中存到MIWEN_TXT字符串中
            char[] MIWEN = Regex.Replace(MIWEN_TXT, "[^a-zA-Z]", "").ToUpper().ToCharArray();
            //将MIWEN_TXT字符串存到字符串数组MIWEN中（已全部转换为大写）
            Kasiski(MIWEN_TXT);

            for (int i = 0; i < repetition.Length; i++)
            {
               卡西斯基.Text = this.卡西斯基.Text + repetition[i];
            }
            MutualCoincidence();
            for (int i = 0; i < mutual.Length; i++)
                交互.Text += mutual[i];
            KeyGenerator();
            for (int i = 0; i < keyPossible.Length; i++)
            {
               可能密钥.Text += "密钥 " + (i + 1) + " : " + keyPossible[i] + "\n";
                // keypossible.Text = this.keypossible.Text + keyPossible[i];
            }
            PlaintextRecovery();
            破解结果.Text = Result;
        }
        public int Change(int i, char[] txt)//将字符转换为数字
        {
            char b = txt[i];       //获取密文第i位的内容
            int t = (char)b;       //转换为数字类型 
            t = t - 65;            //转换为0-25
            return t;
        }

        public int Compare(List<int> position, char[] subesquence, char[] MIWEN)//计算子序列出现的次数，位置
        {

            int number = 0;
            //记录子序列出现次数
            int S_len = subesquence.Length;
            //将子序列长度储存于S_len中 

            for (int t = 0; t < MIWEN.Length; t++)
            {
                int test = 0;
                //用于测试字符串是否相同
                for (int h = 0; h < S_len; h++)
                {
                    if (t + h < MIWEN.Length)
                    {
                        if (subesquence[h] != MIWEN[t + h])
                        {
                            test = 1;
                            break;
                            //字符串不相同。test值改变，退出循环
                        }
                    }

                }
                if (test == 0)
                {
                    number++;
                    //如果全部相同，个数加一
                    position.Add(t);
                    //将地址存到position中
                }
            }

            return number;
        }
        public double[] Statistics(string message)
        {
            int i;
            double[] sdr = new double[26];
            for (i = 0; i < 26; i++)
                sdr[i] = 0;
            char[] m = Regex.Replace(message, "[^a-zA-Z]", "").ToUpper().ToCharArray();
            for (i = 0; i < m.Length; i++)
                sdr[m[i] - 'A'] += 1;
            for (i = 0; i < 26; i++)
                sdr[i] /= m.Length;

            return sdr;
        }
        public int gcd(int a, int b)//求两个数的公因数
        {
            int t;
            if (a < b)//保证a为大的数
            {
                t = a;
                a = b;
                b = t;
            }
            while (b != 0)
            {
                t = a % b;
                a = b;
                b = t;
            }
            //  Console.WriteLine("这是A 啊：");
            // Console.WriteLine(a);
            return a;
        }
        public void Kasiski(string Message)
        {
            message = Message;
            short num = 2;
            short max_num = 5;
            // 最大公因数统计表
            List<int>[] gcd_sta = new List<int>[2];
            gcd_sta[0] = new List<int>();
            gcd_sta[1] = new List<int>();
            int g_l = 0;

            repetition = new string[max_num - num + 1];
            while (num <= max_num)  // 子序列长度从 2 到 5 查找
            {   // 子序列的出现次数、出现位置
                List<List<int>> record = new List<List<int>>();
                List<int> gcds = new List<int>();   // 相同子序列的相对位置的最大公因数
                int length = 0;
                // 以子序列在密文中的位置逐一查找子序列
                for (int i = 0; i < message.Length - num; i++)
                {
                    int j = 0;
                    while (j < length)  // 在 record 表中查看当前子序列是否已被记录
                    {   // 若已存在记录
                        if (message.Substring(i, num).Equals(message.Substring(record[j][1], num)))
                        {
                            record[j][0]++;     // 该子序列的出现次数加一
                            record[j].Add(i);   // 记录当前位置
                            gcds[j] = gcd(gcds[j], i - record[j][1]);// 迭代计算相对位置的最大公因数
                            break;
                        }
                        j++;
                    }
                    if (j == length)        // 若该子序列未被记录
                    {
                        record.Add(new List<int>());//增加一列记录
                        record[j].Add(1);   // 初始化该子序列出现次数为 1
                        record[j].Add(i);   // 记录当前位置
                        gcds.Add(0);        // 初始化该子序列的相对位置的最大公因数为 0
                        length++;
                    }
                }
                repetition[num - 2] = "";

                for (int i = 0; i < length; i++)
                {   // 记录所有子序列的信息，并输出到界面
                    int j = 0;
                    repetition[num - 2] += "子序列长度 = " + num + "  子序列： " + message.Substring(record[i][1], num) + "  出现位置： [";
                    while (j++ < record[i][0])
                        repetition[num - 2] += " " + record[i][j] + " ";
                    repetition[num - 2] += "]  最大公约数： " + gcds[i] + "\n";
                    j = 0;
                    // 统计最大公因数的出现次数
                    while (j < g_l)
                    {
                        if (gcds[i] == gcd_sta[0][j])
                        {
                            gcd_sta[1][j]++;
                            break;
                        }
                        j++;
                    }
                    if (j == g_l && gcds[i] > 2 && record[i][0] > 2)    // 最大公因数需要大于 1，对应的子序列的出现次数需要大于 2
                    {
                        gcd_sta[0].Add(gcds[i]);
                        gcd_sta[1].Add(1);
                        g_l++;
                    }
                }
                num++;
            }
            // 根据统计结果得出密钥长度
            int divideLen = gcd_sta[1][0], sigh = 0;
            for (int i = 1; i < g_l; i++)
            {
                if (divideLen < gcd_sta[1][i])
                {
                    divideLen = gcd_sta[1][i];
                    sigh = i;
                }
            }
            // 对密文进行分堆处理
            divideLen = gcd_sta[0][sigh];
            divideStr = new string[divideLen];
            for (int i = 0; i < message.Length; i++)
                divideStr[i % divideLen] += message.Substring(i, 1);

        }
        public void MutualCoincidence()
        {
            double[][] sta_char = new double[divideStr.Length][];
            for (int i = 0; i < divideStr.Length; i++)  // 统计每一堆的字母频率
                sta_char[i] = Statistics(divideStr[i]);
            int count = 0;
            mutual = new string[divideStr.Length * (divideStr.Length - 1) / 2];
            double[,] m_C = new double[divideStr.Length, divideStr.Length]; // 两两不同的接近 0.065 的交互重合指数
            int[,] m_CIndex = new int[divideStr.Length, divideStr.Length];  // 接近 0.065 的交互重合指数对应的位移量
            // 计算每两堆的交互重合指数
            for (int i = 0; i < divideStr.Length; i++)
            {
                m_C[i, i] = 0;    // 相同的两堆的重合指数不计算
                m_CIndex[i, i] = 0;
                for (int j = i + 1; j < divideStr.Length; j++)
                {
                    double min = 0;
                    int o_flag = 0;
                    mutual[count] = "\n" + (i + 1).ToString() + " -- " + (j + 1).ToString() + "\n";
                    for (int offset = 0; offset < 26; offset++)
                    {
                        double temp = 0;
                        for (int n = 0; n < 26; n++)    // 计算位移量为 offset 的交互重合指数
                            temp += sta_char[i][n] * sta_char[j][(n - offset + 26) % 26];
                        mutual[count] += temp.ToString("0.000") + "  ";
                        if (0.060 < temp && temp < 0.082)   // 判断是否接近 0.065
                        {
                            min = temp;
                            o_flag = offset;
                        }
                    }
                    m_C[i, j] = min;    // 记录当前两堆的最接近 0.065 的交互重合指数与 0.065 的差
                    m_C[j, i] = min;
                    m_CIndex[i, j] = o_flag;    // 记录当前两堆的最接近 0.065 的交互重合指数对应的位移量
                    m_CIndex[j, i] = 26 - o_flag;
                    count++;
                }
            }
            coresidual = new int[divideStr.Length];
            // 通过交互重合指数对应的位移量确定具体密钥
            for (int i = 0; i < divideStr.Length; i++)
                coresidual[i] = 0;
            // 在交互重合指数表中选用可行的同余关系
            for (int i = 1; i < divideStr.Length; i++)
            {
                int j;
                for (j = 0; j < i; j++) // 在左下角中选择可行的同余关系
                {
                    if (0.061 < m_C[i, j] && m_C[i, j] < 0.082)
                        break;
                }
                if (j < i)
                    m_CIndex[i, j] = -m_CIndex[i, j];   // 标记选用的同余关系
                else
                {
                    for (j = i + 1; j < divideStr.Length; j++)  // 在右上角选用可行的同余关系
                    {
                        if (0.061 < m_C[i, j] && m_C[i, j] < 0.082)
                        {
                            m_CIndex[i, j] = -m_CIndex[i, j];
                            break;
                        }
                    }
                }
            }
            for (int i = 1; i < divideStr.Length; i++)
            {
                if (m_CIndex[i, 0] < 0) // 若 i 与 0 的同余关系被选中
                    coresidual[i] = -m_CIndex[i, 0];// 则第 i 个密钥为 0 + Gi
                else
                {   // 否则，在交互重合指数表中找出第 i 行中被选中的同余关系，迭代出第 i 个密钥
                    int num = 0, before = i, temp = 0;
                    while (num++ < divideStr.Length - 1)
                    {
                        for (int j = 1; j < divideStr.Length; j++)
                        {
                            if (m_CIndex[before, j] < 0)
                            {
                                temp = (temp + Math.Abs(m_CIndex[before, j])) % 26;
                                before = j;
                                break;
                            }
                        }
                        if (m_CIndex[before, 0] < 0)
                        {
                            temp = (temp + Math.Abs(m_CIndex[before, 0])) % 26;
                            break;
                        }
                    }
                    coresidual[i] = temp;
                }
            }
        }
        /// <summary>
        /// 利用同余方程组的解生成 26 种可能的密钥
        /// </summary>
        public void KeyGenerator()
        {
            if (coresidual == null)
                return;
            keyPossible = new string[26];
            for (int i = 0; i < 26; i++)
            {
                keyPossible[i] = "";
                for (int j = 0; j < coresidual.Length; j++)
                    keyPossible[i] += (char)((i + coresidual[j] + 26) % 26 + 'A');
            }
        }
        /// <summary>
        /// Vigenere 密码解密
        /// </summary>
        /// <param name="message">密文</param>
        /// <param name="key">密钥</param>
        /// <returns>解密结果</returns>
        private string Vigenere_D(string message, string key)
        {
            char[] Key = Regex.Replace(key, "[^a-zA-Z]", "").ToUpper().ToCharArray();

            char[] Message = Regex.Replace(message, "[^a-zA-Z]", "").ToUpper().ToCharArray();
            for (int i = 0; i < Message.Length; i++)
                Message[i] = (char)((Message[i] - Key[i % Key.Length] + 26) % 26 + 'A');

            return new string(Message);
        }
        /// <summary>
        /// 在可能的 26 种可能的密钥中查找出最可能的密钥
        /// </summary>
        public void PlaintextRecovery()
        {
            string[] ss = { "TH", "HE", "IN", "ER", "AN", "RE" }; // 自然语言出现次数最多的 6 个长度为 2 的序列
            string[] plantextPossible = new string[26];     // 破解的 26 种可能
            for (int i = 0; i < 26; i++)
            {
                plantextPossible[i] = Vigenere_D(message, keyPossible[i]);  // 用第 i 个密钥解密
                List<int>[] sta_string = new List<int>[2];  // 解密结果中长度为 2 的序列的统计表
                sta_string[0] = new List<int>();    // 序列出现次数
                sta_string[1] = new List<int>();    // 序列在原文中的第一个出现的位置
                int len = 0;    // 统计表的长度
                for (int j = 0; j < plantextPossible[i].Length - ss[0].Length; j++) // 统计出结果中长度为 2 的子序列的出现次数和出现位置
                {
                    int k = 0;
                    while (k < len)
                    {
                        if (plantextPossible[i].Substring(j, ss[0].Length).Equals(plantextPossible[i].Substring(sta_string[1][k], ss[0].Length)))
                        {
                            sta_string[0][k]++;
                            break;
                        }
                        k++;
                    }
                    if (k == len)
                    {
                        sta_string[0].Add(1);
                        sta_string[1].Add(j);
                        len++;
                    }
                }
                int ind = 0;
                for (int j = 1; j < len; j++)   // 找出出现次数最多的子序列
                {
                    if (sta_string[0][ind] < sta_string[0][j])
                        ind = j;
                }
                for (len = 0; len < ss.Length; len++)   // 比较该子序列是否出现在自然语言表中
                {
                    if (ss[len].Equals(plantextPossible[i].Substring(sta_string[1][ind], ss[0].Length)))
                        break;
                }
                if (len < ss.Length)    // 若存在，则当前密钥为最可能的密钥，保存结果并返回
                {
                    Result = "密钥是：" + keyPossible[i] + "\n" + plantextPossible[i];
                    return;
                }
            }
            // 未找到可能的密钥，保存结果并返回
            Result = "密钥未知！！\n" + Vigenere_D(message, keyPossible[0]);
        }


    }
}
