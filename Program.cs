using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("请输入(不能省略运算符、括号需要是英文模式下)例如1+2+3*(2*3) ：");
                try
                {
                    string num = Console.ReadLine();
                    //先算括号里面的
                    while (num.Contains("(") && num.Contains(")"))
                    {
                        var index1 = num.IndexOf("(");
                        var index2 = num.IndexOf(")");
                        var brackets = num.Substring(index1 + 1, index2 - index1 - 1);
                        var val = Four(brackets);
                        var zfc = num.Substring(index1, index2 + 1 - index1);
                        num = num.Replace(zfc, val.ToString());
                    }
                    Console.WriteLine("计算结果是："+Four(num));
                }
                catch (Exception)
                {
                    Console.WriteLine("运算出错,不支持此算法");
                }
            }
        }
        /// <summary>
        /// 运算
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static double Four(string str)
        {
            var num = 0.0;
            var a = 0.0;
            var b = 0;
           // Console.WriteLine(str);
            if (double.TryParse(str, out a)) //判断是否可以转换为整型
            {
                num = a;
                //Console.WriteLine("最后结果为：" + str);
                return a;
            }
            if (int.TryParse(str, out b))
            {
                num = b;
                //Console.WriteLine("最后结果为：" + str);
                return b;
            }
            //获取下标
            var rideIndex = str.IndexOf("*");
            var exceptIndex = str.IndexOf("/");
            var plusIndex = str.IndexOf("+");
            var reduceIndex = str.IndexOf("-");
            if (reduceIndex == 0)
            {
                reduceIndex = str.TrimStart('-').IndexOf("-");
                reduceIndex = reduceIndex != -1 ? reduceIndex + 1 : reduceIndex;
            }
            if ((rideIndex < exceptIndex && rideIndex != -1) || (rideIndex != -1 && exceptIndex == -1))
            {
                str = Formula(rideIndex, "*", str);
                return Four(str);
            }
            else if ((rideIndex > exceptIndex && exceptIndex != -1) || (exceptIndex != -1 && rideIndex == -1))
            {
                str = Formula(exceptIndex, "/", str);
                return Four(str);
            }
            else if ((plusIndex < reduceIndex && plusIndex != -1) || (plusIndex != -1 && reduceIndex == -1))
            {
                str = Formula(plusIndex, "+", str);
                return Four(str);
            }
            else if ((plusIndex > reduceIndex && reduceIndex != -1) || (reduceIndex != -1 && plusIndex == -1))
            {
                str = Formula(reduceIndex, "-", str);
                return Four(str);
            }
            return num;
        }
        public static string Formula(int index, string type, string str)
        {
            var index1 = GetIndex(str.Substring(0, index), true);
            var num1 = str.Substring(index1 + 1, index - index1 - 1);
            var index2 = GetIndex(str.Substring(index + 1, str.Length - index - 1), false);
            var num2 = str.Substring(index + 1, index2);
            var zfc = "";
            if (index1 == -1 || index2 == -1)
            {
                zfc = str.Substring(index1 + 1, index + index2 + 1);
            }
            else
            {
                zfc = str.Substring(index1 + 1, index + index2 - index1);
            }
            var jieguo = 0.0;
            if (type == "*")
            {
                jieguo = (double.Parse(num1) * double.Parse(num2));
            }
            else if (type == "/")
            {
                jieguo = (double.Parse(num1) / double.Parse(num2));
            }
            else if (type == "-")
            {
                jieguo = (double.Parse(num1) - double.Parse(num2));
            }
            else if (type == "+")
            {
                jieguo = (double.Parse(num1) + double.Parse(num2));
            }
            str = str.Replace(zfc, jieguo.ToString());
            return str;
        }
        //计算结果
        public static int GetIndex(string str, bool direction)
        {
            List<int> list = new List<int>();
            var ride = direction ? str.LastIndexOf("*") : str.IndexOf("*");
            if (ride > -1)
            {
                list.Add(ride);
            }
            var except = direction ? str.LastIndexOf("/") : str.IndexOf("/");
            if (except > -1)
            {
                list.Add(except);
            }
            var reduce = direction ? str.LastIndexOf("-") : str.IndexOf("-");
            if (direction)
            {
                reduce = str.LastIndexOf("-") == 0 ? -1 : str.LastIndexOf("-");
            }
            else
            {
                reduce = str.IndexOf("-") == 0 ? -1 : str.IndexOf("-");
            }

            if (reduce > -1)
            {
                list.Add(reduce);
            }
            var plus = direction ? str.LastIndexOf("+") : str.IndexOf("+");
            if (plus > -1)
            {
                list.Add(plus);
            }
            if (list.Count == 0)
            {
                if (direction) return -1;
                else return str.Length;

            }
            return list.Min(a => a);
        }
    }
}
