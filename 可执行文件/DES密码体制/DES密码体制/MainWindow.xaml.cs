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
using System.Security.Cryptography;
using System.IO;
using System.Security;
using System.Text.RegularExpressions;

namespace DES密码体制
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Structure_S();

        }
        private readonly byte[][,] S = new byte[8][,];
        private byte[] IP = { 58, 50, 42, 34, 26, 18, 10, 2, 60, 52,
            44, 36, 28, 20, 12, 4, 62, 54, 46, 38, 30, 22, 14, 6, 64, 56, 48,
            40, 32, 24, 16, 8, 57, 49, 41, 33, 25, 17, 9, 1, 59, 51, 43, 35,
            27, 19, 11, 3, 61, 53, 45, 37, 29, 21, 13, 5, 63, 55, 47, 39, 31,
            23, 15, 7 }; // 64
        private byte[] IP_1 = { 40, 8, 48, 16, 56, 24, 64, 32, 39, 7,
            47, 15, 55, 23, 63, 31, 38, 6, 46, 14, 54, 22, 62, 30, 37, 5, 45,
            13, 53, 21, 61, 29, 36, 4, 44, 12, 52, 20, 60, 28, 35, 3, 43, 11,
            51, 19, 59, 27, 34, 2, 42, 10, 50, 18, 58, 26, 33, 1, 41, 9, 49,
            17, 57, 25 }; // 64
        private byte[] P = { 16, 7, 20, 21, 29, 12, 28, 17,
                             1, 15, 23, 26, 5, 18, 31, 10,
                             2, 8, 24, 14,  32, 27, 3, 9,
                             19, 13, 30, 6, 22, 11, 4, 25 };

        private byte[] PC_1 = { 57, 49, 41, 33, 25, 17, 9, 1, 58, 50, 42, 34, 26, 18,
                                10, 2, 59, 51, 43, 35, 27, 19, 11, 3, 60, 52, 44, 36,
                                63, 55, 47, 39, 31, 26, 15, 7, 60, 54, 46, 38, 30, 22,
                                14, 6, 61, 53, 45, 37, 29, 21, 13, 5, 28, 20, 12, 4 };
        private byte[] PC_2 = { 14, 17, 11, 24, 1, 5, 3, 28, 15, 6, 21, 10,
                                23, 19, 12, 4, 26, 8, 16, 7, 27, 20, 13, 2,
                                41, 52, 31, 37, 47, 55, 30, 40, 51, 45, 33, 48,
                                44, 49, 39, 56, 34, 53, 46, 42, 50, 36, 29, 32 };
        private void Structure_S()
        {
            S[0] = new byte[,]{ { 14, 4, 15, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7 },
                                { 0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8 },
                                { 4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0 },
                                { 15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13 } };

            S[1] = new byte[,]{ { 15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10 },
                                { 3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5 },
                                { 0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15 },
                                { 13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9 } };

            S[2] = new byte[,]{ { 10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8 },
                                { 13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1 },
                                { 13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7 },
                                { 1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12 } };

            S[3] = new byte[,]{ { 7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15 },
                                { 13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9 },
                                { 10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4 },
                                { 3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14} };

            S[4] = new byte[,]{ { 2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9 },
                                { 14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6 },
                                { 4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14 },
                                { 11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3 } };

            S[5] = new byte[,]{ { 12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11 },
                                { 10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8 },
                                { 9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6 },
                                { 4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13 } };

            S[6] = new byte[,]{ { 4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1 },
                                { 13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6 },
                                { 1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2 },
                                { 6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12 } };

            S[7] = new byte[,]{ { 13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7 },
                                { 1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2 },
                                { 7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8 },
                                { 2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11 } };
        }
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="str">需要加密的</param>
        /// <param name="sKey">密匙</param>
        /// <returns></returns>
        private string Encrypt(string str, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(str);
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);// 密匙
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);// 初始化向量
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            var retB = Convert.ToBase64String(ms.ToArray());
            return retB;
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="pToDecrypt">需要解密的</param>
        /// <param name="sKey">密匙</param>
        /// <returns></returns>
        private string Decrypt(string pToDecrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Convert.FromBase64String(pToDecrypt);
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            // 如果两次密匙不一样，这一步可能会引发异常
            cs.FlushFinalBlock();
            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }




        public void Encryption(byte[] data, byte[] key)//编码
        {
            int numD = data.Length >> 3;
            int numK = key.Length >> 3;
            for (int i = 0; i < numD; i++)
            {
                byte[] segment = new byte[64];
                for (int j = 0; j < 8; j++)
                    for (int b = 0; b < 8; b++)
                        segment[j << 3 | b] = (byte)((data[i << 3 | j] & 1 << b) >> b);

                byte[] keyS = new byte[64];
                for (int j = 0; j < 8; j++)
                {
                    byte temp = key[(i % numK) << 3 | j];
                    for (int b = 0; b < 8; b++)
                        keyS[j << 3 | b] = (byte)((temp & 1 << b) >> b);
                }
                DES_64(segment, keyS, true);
                for (int j = 0; j < 8; j++)
                {
                    data[i * 8 + j] = 0;
                    for (int b = 0; b < 8; b++)
                        data[i * 8 + j] = (byte)(data[i * 8 + j] + (segment[j * 8 + b] << b));
                }
            }
        }//加密

        public void Decryption(byte[] data, byte[] key)//解密
        {
            int numD = data.Length >> 3;
            int numK = key.Length >> 3;
            for (int i = 0; i < numD; i++)
            {
                byte[] segment = new byte[64];
                for (int j = 0; j < 8; j++)
                    for (int b = 0; b < 8; b++)
                        segment[j << 3 | b] = (byte)((data[i << 3 | j] & 1 << b) >> b);

                byte[] keyS = new byte[64];
                for (int j = 0; j < 8; j++)
                {
                    byte temp = key[(i % numK) << 3 | j];
                    for (int b = 0; b < 8; b++)
                        keyS[j << 3 | b] = (byte)((temp & 1 << b) >> b);
                }
                DES_64(segment, keyS, false);
                for (int j = 0; j < 8; j++)
                {
                    data[i * 8 + j] = 0;
                    for (int b = 0; b < 8; b++)
                        data[i << 3 | j] = (byte)(data[i << 3 | j] + (segment[j << 3 | b] << b));
                }
            }
        }

        private void DES_64(byte[] data, byte[] key, bool isEncry)
        {
            Console.WriteLine("原文：");
            this.过程框1.Text += "原文:";
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(data[i * 8 + j] + " ");
                    this.过程框1.Text += data[i * 8 + j] + " ";
                }
                this.过程框1.Text += "\n";
                Console.WriteLine();
            }

            byte[] temp = new byte[64];
            for (int i = 0; i < 64; i++)        // IP 置换
                temp[i] = data[IP[i] - 1];
            for (int i = 0; i < 64; i++)
                data[i] = temp[i];

            Console.WriteLine(" IP 置换：");
            this.过程框1.Text += " IP 置换：";
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(data[i * 8 + j] + " ");
                    this.过程框1.Text += data[i * 8 + j] + " ";
                }
                Console.WriteLine();
                this.过程框1.Text += "\n";
            }



            byte[][] subkey = GenerateSubkey(key);
            for (int count = 0; count < 16; count++) // 16 轮加密
            {
                byte[] left = new byte[32];
                byte[] right = new byte[32];
                for (int j = 0; j < 32; j++)            // 分半
                {
                    left[j] = data[j];
                    right[j] = data[j + 32];
                }
                byte[] xor_t;
                if (isEncry)
                    xor_t = F(right, subkey[count]);  // f 函数
                else
                    xor_t = F(right, subkey[count]);

                for (int j = 0; j < 32; j++)    // 异或运算
                    left[j] = (byte)(xor_t[j] ^ left[j]);

                Console.WriteLine("与左一半异或：");
                this.过程框1.Text += "与左一半异或：";
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        Console.Write(left[i * 4 + j] + " ");
                        this.过程框1.Text += left[i * 4 + j] + " ";
                    }
                    Console.WriteLine();
                    this.过程框1.Text += "\n";
                }


                if (count < 15)
                    for (int j = 0; j < 32; j++)   // 前 15 轮左右交换
                    {
                        data[j] = right[j];
                        data[j + 32] = left[j];
                    }
                else
                    for (int j = 0; j < 32; j++)    // 最后一轮不交换
                    {
                        data[j] = left[j];
                        data[j + 32] = right[j];
                    }
            }
            for (int i = 0; i < 64; i++)        // IP 逆置换
                temp[i] = data[IP_1[i] - 1];
            for (int i = 0; i < 64; i++)
                data[i] = temp[i];

            Console.WriteLine(" IP 逆置换：");
            this.过程框1.Text += " IP 逆置换：";
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(data[i * 8 + j] + " ");
                    this.过程框1.Text += data[i * 8 + j] + " ";
                }
                Console.WriteLine();
                this.过程框1.Text += "\n";
            }
        }
        private byte[][] GenerateSubkey(byte[] key)
        {
            byte[][] subkey = new byte[16][];
            byte[] Cn = new byte[28], Dn = new byte[28];
            int i;
            for (i = 0; i < 28; i++)        // 选择置换 PC_1
            {
                Cn[i] = key[PC_1[i] - 1];
                Dn[i] = key[PC_1[i + 28] - 1];
            }
            for (i = 0; i < 16; i++)        // 生成 16 个子密钥
            {
                byte[] tempLS_C = new byte[28];         // C(i + 1) = LS(C(i))
                byte[] tempLS_D = new byte[28];         // D(i + 1) = LS(D(i))
                Console.WriteLine("第" + i + "个子密钥:");
                this.过程框1.Text += "第" + i + "个子密钥:";
                if (i < 2 || i == 8 || i == 15)
                    for (int j = 0; j < 28; j++)        // 循环左移 1 位
                    {
                        tempLS_C[(j + 1) % 28] = Cn[j];
                        tempLS_D[(j + 1) % 28] = Dn[j];
                    }
                else
                    for (int j = 0; j < 28; j++)        // 循环左移 2 位
                    {
                        tempLS_C[(j + 2) % 28] = Cn[j];
                        tempLS_D[(j + 2) % 28] = Dn[j];
                    }
                for (int j = 0; j < 28; j++)
                {
                    Cn[j] = tempLS_C[j];
                    key[j] = Cn[j];
                    Dn[j] = tempLS_D[j];
                    key[j + 28] = Dn[j];
                }

                subkey[i] = new byte[48];
                for (int j = 0; j < 48; j++)        // 选择置换 PC_2
                {
                    Console.Write(key[PC_2[j] - 1]);
                    this.过程框1.Text += key[PC_2[j] - 1];
                    subkey[i][j] = key[PC_2[j] - 1];
                }
                Console.WriteLine();
                this.过程框1.Text += "\n";

            }
            return subkey;
        }
        private byte[] F(byte[] right, byte[] key)
        {
            Console.WriteLine("右一半：");
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                    Console.Write(right[i * 4 + j] + " ");
                Console.WriteLine();
            }

            byte[] res = new byte[32];
            for (int i = 0; i < 32; i++)            // 备份
                res[i] = right[i];

            byte[] extansion = Extend(res);         // E 盒扩展

            Console.WriteLine("子密钥：");
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 6; j++)
                    Console.Write(key[i * 6 + j] + " ");
                Console.WriteLine();
            }

            Console.WriteLine("E 盒扩展：");
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 6; j++)
                    Console.Write(extansion[i * 6 + j] + " ");
                Console.WriteLine();
            }

            for (int i = 0; i < 48; i++)            // 与子密钥异或
                extansion[i] = (byte)(extansion[i] ^ key[i]);

            Console.WriteLine("与子密钥异或：");
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 6; j++)
                    Console.Write(extansion[i * 6 + j] + " ");
                Console.WriteLine();
            }

            byte[] s_t = S_Conversion(extansion);   // S 盒压缩

            Console.WriteLine("S 盒置换：");
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                    Console.Write(s_t[i * 4 + j] + " ");
                Console.WriteLine();
            }

            res = P_Conversion(s_t);                // P 盒变换

            Console.WriteLine("P 盒置换：");
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                    Console.Write(res[i * 4 + j] + " ");
                Console.WriteLine();
            }


            return res;
        }
        private byte[] Extend(byte[] data)
        {
            byte[] res = new byte[48];
            for (int r = 0; r < 8; r++)
            {
                res[r * 6] = data[((r + 7) % 8) << 2 | 3];
                res[r * 6 + 5] = data[((r + 1) % 8) << 2];
                for (int c = 0; c < 4; c++)
                    res[r * 6 + c + 1] = data[r << 2 | c];
            }
            return res;
        }
        private byte[] S_Conversion(byte[] data)
        {
            byte[] res = new byte[32];
            for (int i = 0; i < 8; i++)
            {
                int r = data[i * 6] | data[i * 6 + 5] << 1;
                int c = data[i * 6 + 1] | data[i * 6 + 2] << 1 | data[i * 6 + 3] << 2 | data[i * 6 + 4] << 3;
                int s = S[i][r, c];
                for (int j = 0; j < 4; j++)
                    res[i << 2 | j] = (byte)((s & 1 << 3 - j) >> 3 - j);
            }
            return res;
        }
        private byte[] P_Conversion(byte[] data)
        {
            byte[] res = new byte[32];
            for (int i = 0; i < 32; i++)
                res[i] = data[P[i] - 1];
            return res;
        }
        private byte[] Encoding16string(string MW)
        {
            byte[] m = System.Text.Encoding.ASCII.GetBytes(MW);
            for (int i = 0; i < m.Length; i++)
            {
                m[i] -= 48;
            }
            byte[] v = new byte[8];
            for (int i = 0; i < 16; i += 2)
            {
                v[i / 2] = (byte)(m[i] + m[i + 1] * 16);
            }
            return v;
        }

        private void 加密_Click(object sender, RoutedEventArgs e)
        {
            密文.Clear();
            string s3 = Encrypt(明文.Text, 密钥.Text);
            密文.AppendText(s3);
            过程框1.Clear();
            string MINGWEN = 明文.Text;
            byte[] DATE = Encoding16string(MINGWEN);
            string MIYAO = 密钥.Text;
            byte[] KEY = System.Text.Encoding.ASCII.GetBytes(MIYAO);
            Encryption(DATE, KEY);
            Decryption(DATE, KEY);
        }

        private void 解密_Click(object sender, RoutedEventArgs e)
        {
            明文.Clear();
            string s4 = Decrypt(密文.Text, 密钥.Text);
            明文.AppendText(s4);
            过程框1.Clear();
            string MINGWEN = 密文.Text;
            byte[] DATE = Encoding16string(MINGWEN);
            string MIYAO = 密钥.Text;
            byte[] KEY = System.Text.Encoding.ASCII.GetBytes(MIYAO);
            Encryption(DATE, KEY);
            Decryption(DATE, KEY);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            过程框1.Clear();
            明文.Clear();
            密文.Clear();
            密钥.Clear();
        }
    }
}

