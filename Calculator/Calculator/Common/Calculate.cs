using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Calculator.Common
{
    public class Calculate
    {

        public static string GetResult(string str)
        {
            str = str + "+";
            char[] expArry = str.ToCharArray();
            string operations = "()+-*/";
            Hashtable signs = new Hashtable();
            string[] numArry = str.Split('(', ')', '+', '-', '*', '/'); //分割字符串，提取表达式中的数字
            //定义操作符的优先级并存入哈希表
            signs.Add("(", 0);
            signs.Add(")", 0);
            signs.Add("+", 1);
            signs.Add("-", 1);
            signs.Add("*", 2);
            signs.Add("/", 2);
            string[] resultArry = new string[99];
            int resultIndex = 0;
            int numIndex = 0;
            for (int i = 0; i < str.Length - 1; i++)
            {
                if (operations.Contains(expArry[i]))
                {
                    //遇到符号直接存入resultArry
                    resultArry[resultIndex] = expArry[i].ToString();
                    resultIndex++;
                }
                else if (operations.Contains(expArry[i + 1]))
                {
                    //连续非符号字符，从numArry取出一个元素放入resultArry
                    while (numArry[numIndex] == "")
                    //去除numArry中的空元素
                    {
                        numIndex++;
                    }
                    resultArry[resultIndex] = numArry[numIndex];
                    numIndex++;
                    resultIndex++;
                }
            }
            Stack<char> sign_stack = new Stack<char>();//定义符号栈
            Stack<string> num_stack = new Stack<string>();//定义操作数栈
            char calc_sign;
            try
            {
                for (int i = 0; i < resultIndex + 1; i++)
                {
                    if (i == resultIndex)
                    {
                        if (sign_stack.Count != 0)
                        {
                            calc_sign = sign_stack.Pop();
                            Calc(ref sign_stack, ref num_stack, ref resultArry[i], ref calc_sign);
                            while (num_stack.Count() > 1)
                            //计算到栈中只剩一个操作数为止
                            {
                                calc_sign = sign_stack.Pop();
                                Calc(ref sign_stack, ref num_stack, ref resultArry[i], ref calc_sign);
                            }
                        }
                    }
                    else if (operations.Contains(resultArry[i]))
                    {
                        if (resultArry[i] != "(")
                        {
                            if (sign_stack.Count() == 0 || Convert.ToInt32(signs[resultArry[i]]) > Convert.ToInt32(signs[sign_stack.Peek().ToString()]))
                            //第一个操作符或者当前操作符优先级大于符号栈栈顶元素时，当前操作符入栈
                            {
                                sign_stack.Push(resultArry[i].ToCharArray()[0]);
                            }
                            else
                            {
                                //否则，符号栈出栈操作符，数字栈出栈两个操作数进行运算
                                calc_sign = sign_stack.Pop();
                                Calc(ref sign_stack, ref num_stack, ref resultArry[i], ref calc_sign);
                            }
                        }
                        else
                        {
                            sign_stack.Push(resultArry[i].ToCharArray()[0]);
                        }
                    }
                    else
                    {
                        num_stack.Push(resultArry[i]);
                    }
                }
                //结果出栈
                return num_stack.Pop();
            }
            catch (Exception e)
            {
                MessageBox.Show("表达式格式不正确，请检查！ " + e.Message);
                return null;
            }
        }
        public static string CalculateResult(string num1, string num2, char sign)
        {
            double result = 0;
            double oper_num1 = Convert.ToDouble(num1);
            double oper_num2 = Convert.ToDouble(num2);
            if (sign.ToString() == "+")
            {
                result = oper_num1 + oper_num2;
            }
            if (sign.ToString() == "-")
            {
                result = oper_num1 - oper_num2;
            }
            if (sign.ToString() == "*")
            {
                result = oper_num1 * oper_num2;
            }
            if (sign.ToString() == "/")
            {
                if (oper_num2 != 0)
                    result = oper_num1 / oper_num2;
                else
                    MessageBox.Show("表达式中有除0操作，请检查！");
            }
            return result.ToString();
        }
        public static void Calc(ref Stack<char> sign, ref Stack<string> num, ref string str, ref char calc_sign)
        {
            string num1 = "";
            string num2 = "";
            if (str == ")")
            {
                while (calc_sign != '(')
                {
                    num2 = num.Pop();
                    num1 = num.Pop();
                    num.Push(CalculateResult(num1, num2, calc_sign));
                    calc_sign = sign.Pop();
                }
            }
            else
            {
                num2 = num.Pop();
                num1 = num.Pop();
                num.Push(CalculateResult(num1, num2, calc_sign));
                if (str != null && "()+-*/".Contains(str))
                {
                    sign.Push(str.ToCharArray()[0]);
                }
            }

        }

    }
}
