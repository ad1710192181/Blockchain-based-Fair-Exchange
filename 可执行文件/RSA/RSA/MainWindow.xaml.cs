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

namespace RSA
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
            long p, q;
            //获取p,q的值
            p = long.Parse(textBox1.Text);
            q = long.Parse(textBox2.Text);

            //判断p，q是否为素数，若为真进行计算，则否弹出提示
            if (isPrim(p) && isPrim(q))
            {
                long n = p * q;
                long m = (p - 1) * (q - 1);
                textBox3.Text = n.ToString();
                textBox4.Text = m.ToString();
            }
            else
            {
                MessageBox.Show("请按要求,重新输入p,q");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox1.Focus();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
                     //生成随机数工具
              Random random = new Random();
              long p, q;
              
              //设置判断标志
              Boolean flag = false;
              while (!flag)
              {
                  p = random.Next(595530);
                  q = random.Next(585530);
                  
                  //若生成的两个随机数都为素数，设定更新标志，并进行显示
                  if (isPrim(p) && isPrim(q))
                  {
                      flag = true;
                      textBox1.Text = p.ToString();
                      textBox2.Text = q.ToString();
                      textBox3.Clear();
                      textBox4.Clear();
                  }
  
               }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
             //获取d,m的值
              long d = long.Parse(textBox5.Text);
              long m = long.Parse(textBox4.Text);
  
              //判断输入的d是否符合条件
              if (d > 1 && d < m && gcd(d, m) == 1 && isPrim(d))
              {
                  //使用逆元函数，计算d,m的逆元
                  long result = niyuan(d, m);
                  textBox6.Text = result.ToString();
              }
              else
              {
                  MessageBox.Show("输入的 e 不符合要求，请按要求,重新输入公钥 e ！");
                  textBox5.Clear();
                  textBox5.Focus();
 
             }

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
             long d = 0;
             
             //获取m的值，用以计算e
             long m = long.Parse(textBox4.Text);
 
             //生成随机数工具，并设置随机是否完成标志
             Random radom = new Random();
             Boolean flag = false;
             while(!flag)
             {
                  d = radom.Next(655300);
                  if (d > 1 && d < m && gcd(d, m) == 1 && isPrim(d))
                  {
                       flag = true;
                  }
             }

             //利用逆元函数计算公钥
             long result = niyuan(d, m);
 
             textBox5.Text = d.ToString();
             textBox6.Text = result.ToString();

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //设置标志，判断是否已经输入明文
             Boolean flag = false;
             if (!string.IsNullOrWhiteSpace(textBox7.Text))
                flag = true;

             if(flag == false)
             {
                 MessageBox.Show("加密前,请先输入明文！");
                 textBox7.Focus();
             }
             else
             {
                 long n = long.Parse(textBox3.Text);
                 long m = long.Parse(textBox7.Text);
                 long E = long.Parse(textBox5.Text);
 
                 //利用快速指数模运算函数生成密文
                 long C = getMod(E, m, n);
                 textBox8.Text = C.ToString();
             }

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            //设置标志，判断是否已经输入密文
             Boolean flag = false;
             if (!string.IsNullOrWhiteSpace(textBox9.Text))
                 flag = true;
             if(flag == false)
             {
                 MessageBox.Show("解密前，请先输入密文！");
                 textBox9.Focus();
             }
             else
             {
                 long C = long.Parse(textBox9.Text);
                 long n = long.Parse(textBox3.Text);
                 long d = long.Parse(textBox6.Text);
 
                 //利用快速指数模运算函数解密密文
                 long M = getMod(d, C, n);
                 textBox10.Text = M.ToString();
             }

        }
        private bool isPrim(long num)
         {
             //两个较小数另外处理
             if (num == 2 || num == 3)
                 return true;
            //不在6的倍数两侧的一定不是质数
             if (num % 6 != 1 && num % 6 != 5)
                 return false;
             long tmp = (long)Math.Sqrt(num);
            //在6的倍数两侧的也可能不是质数
             for (long i = 5; i <= tmp; i += 6)
                 if (num % i == 0 || num % (i + 2) == 0)
                     return false;
             //排除所有，剩余的是质数
             return true;
         }
 
         //****************************************
         //采用递归的形式，判断两个数是否互质
        //****************************************
         private long gcd(long x, long y)
         {
             return y != 0 ? gcd(y, x % y) : x;
         }
 
        //****************************************
         //利用欧几里得算法计算m，d的逆元
         //****************************************
         private long niyuan(long number1, long number3)
         {
             long x1 = 1, x2 = 0, x3 = number3, y1 = 0, y2 = 1, y3 = number1;
            long q;
             long number4 = 0;
             long t1, t2, t3;
             while (y3 != 0)
             {
                 if (y3 == 1)
                 {
                    number4 = y2;
                     break;
                 }
                 else
                 {
                     q = (x3 / y3);
                     t1 = x1 - q* y1;
                     t2 = x2 - q* y2;
                     t3 = x3 - q* y3;
                     x1 = y1; x2 = y2; x3 = y3;
                     y1 = t1; y2 = t2; y3 = t3;
                }
             }
            if (number4< 0)
                 number4 = number4 + number3;
             return number4;
         }

         //****************************************
        //利用快速指数模运算，计算m^e mod n
         //****************************************
         private long getMod(long a, long b, long c)
         {
             //指数 e --> a  底数 m --> b   模数 n --> c
             long number3 = 1;
             while (a != 0)
             {
                 if (a % 2 == 1)
                 {
                     a = a - 1;
                     number3 = (number3* b) % c;
                 }
                 else
                 {
                     a = (a / 2);
                     b = (b* b) % c;
                 }
             }
             return number3;

         }

    }
}
