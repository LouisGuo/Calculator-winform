using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Calculator.Common;

namespace Calculator
{
    public partial class Calculator : Form
    {
        public Calculator()
        {
            InitializeComponent();
        }

        public int count=0;//统计位数
        public string def="0";//最初显示的文本
        private Operator ope=Operator.nom;//记录操作符
        public string save;//用来储存数据
         enum Operator : int
        {
            nom,add, sub, mul, div, ra, per, rec,sin,mod,a,b
        };//分别对应着加、减、乘、除、开根号、百分数、倒数
        public double firstnum,secondnum;

        //以下是数字键0-9代码
        private void button17_Click(object sender, EventArgs e)
        {
            if (label2.Text == "0") label2.Text = null;
            label2.Text += "6";
        }

        private void Num1_Click(object sender, EventArgs e)
        {
            if (label2.Text == "0") label2.Text = null;
            label2.Text += "1";
        }

        private void Num2_Click(object sender, EventArgs e)
        {
            if (label2.Text == "0") label2.Text = null;
            label2.Text += "2";
        }

        private void Num3_Click(object sender, EventArgs e)
        {
            if (label2.Text == "0" ) label2.Text = null;
            label2.Text += "3";
        }

        private void Num0_Click(object sender, EventArgs e)
        {
            if (label2.Text == "0" ) label2.Text = null;
            label2.Text += "0";
        }

        private void Num4_Click(object sender, EventArgs e)
        {
            if (label2.Text == "0" ) label2.Text = null;
            label2.Text += "4";
        }

        private void Num5_Click(object sender, EventArgs e)
        {
            if (label2.Text == "0" ) label2.Text = null;
            label2.Text += "5";
        }

        private void Num7_Click(object sender, EventArgs e)
        {
            if (label2.Text == "0" ) label2.Text = null;
            label2.Text += "7";
        }

        private void Num8_Click(object sender, EventArgs e)
        {
            if (label2.Text == "0") label2.Text = null;
            label2.Text += "8";
        }

        private void Num9_Click(object sender, EventArgs e)
        {
            if (label2.Text == "0" ) label2.Text = null;
            label2.Text += "9";
        }

        private void C_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            label2.Text = def;
        }//C键功能

        private void Point_Click(object sender, EventArgs e)
        {
            int p;
            if (label2.Text == null)
            {
                label2.Text += "0.";
            }
            else {
                p = label2.Text.IndexOf('.');
                if(p==-1) label2.Text += "."; }
        }//小数点键功能

        private void Plus_Click(object sender, EventArgs e)
        {
            if (label2.Text == null || label2.Text == "0.") label2.Text = "0";
            ope = Operator.add;
            firstnum = Convert.ToDouble(label2.Text);
            label1.Text = label2.Text + "+";
            label2.Text = def;
        }//+键功能

        private void Reduce_Click(object sender, EventArgs e)
        {
            if (label2.Text == null||label2.Text=="0.") label2.Text = "0";
            ope = Operator.sub;
            firstnum = Convert.ToDouble(label2.Text);
            label1.Text = label2.Text + "-";
            label2.Text = def;
        }//减号键功能

        private void Multiply_Click(object sender, EventArgs e)
        {
            if (label2.Text == null || label2.Text == "0.") label2.Text = "0";
            ope = Operator.mul;
            firstnum = Convert.ToDouble(label2.Text);
            label1.Text = label2.Text + "*";
            label2.Text = def;
        }//*键功能

        private void Divide_Click(object sender, EventArgs e)
        {
            if (label2.Text == null || label2.Text == "0.") label2.Text = "0";
            ope = Operator.div;
            firstnum = Convert.ToDouble(label2.Text);
            label1.Text = label2.Text + "/";
            label2.Text = def;
        }//除号键功能

        private void Back_Click(object sender, EventArgs e)
        {
            if (label2.Text != null)
            {
                label2.Text=label2.Text.Remove(label2.Text.Length-1); 
                if (label2.Text.Length == 0) { label2.Text = "0"; }
            }
        }//退格键功能

        private void Plusminus_Click(object sender, EventArgs e)
        {
            int p;
            p = label2.Text.IndexOf('-');
            if (label2.Text != null)
            {
                if (p == -1)
                    label2.Text = label2.Text.Insert(0, "-");
                else label2.Text = label2.Text.Remove(0,1);
            }
        }//负号键功能

        private void Radical_Click(object sender, EventArgs e)
        {
            if (label2.Text == null || label2.Text == "0.") label2.Text = "0";
            ope = Operator.ra;
            firstnum = Convert.ToDouble(label2.Text);
            label1.Text = "sqrt(" + label2.Text + ")";
            label2.Text = "" + Math.Sqrt(firstnum);
        }//求x的2次方根

        private void Result_Click(object sender, EventArgs e)//=键的功能
        {

            if (!textBox1.Text.ToString().Equals("0") && !textBox1.Text.ToString().Equals(""))
            {
                label2.Text = Calculate.GetResult(textBox1.Text.ToString());
                label1.Text = textBox1.Text.ToString();
                label2.Visible = true;

                textBox1.Text = "0";
                textBox1.Visible = false;
                return;
            }

            if (ope == Operator.add)
            {
                secondnum = Convert.ToDouble(label2.Text);
                double sum = firstnum + secondnum;
                label1.Text = ""; label2.Text = "" + sum;
            }//+运算

            if (ope == Operator.sub)
            {
                secondnum = Convert.ToDouble(label2.Text);
                double sum = firstnum - secondnum;
                label1.Text = ""; label2.Text = "" + sum;
            }//-运算

            if (ope == Operator.mul)
            {
                secondnum = Convert.ToDouble(label2.Text);
                double sum = firstnum * secondnum;
                label1.Text = ""; label2.Text = "" + sum;
            }//*运算

            if (ope == Operator.div)
            {
                secondnum = Convert.ToDouble(label2.Text);
                label1.Text = "";
                if (secondnum == 0) label2.Text = "除数不能为零";
                else
                {
                    double sum = firstnum / secondnum;
                    label2.Text = "" + sum;
                }
            }//除运算

            if (ope == Operator.mod)
            {
                secondnum = Convert.ToDouble(label2.Text);
                double sum = firstnum % secondnum;
                label1.Text = ""; label2.Text = "" + sum;
            }//求余运算

            if (ope == Operator.a)
            {
                secondnum = Convert.ToDouble(label2.Text);
                double sum = Math.Pow( firstnum , secondnum);
                label1.Text = ""; label2.Text = "" + sum;
            }//x的y次方运算

            if (ope == Operator.b)
            {
                secondnum = Convert.ToDouble(label2.Text);
                double sum = Math.Pow(firstnum, 1/secondnum);
                label1.Text = ""; label2.Text = "" + sum;
            }//x的y次方根运算

             label1.Text = ""; 
        }

        private void Reciprocal_Click(object sender, EventArgs e)
        {
            if (label2.Text == null || label2.Text == "0.") label2.Text = "0";
            ope = Operator.rec;
            firstnum = Convert.ToDouble(label2.Text);
            label1.Text = "recipros(" + label2.Text + ")";
            label2.Text = "" + 1/firstnum;
        }//求x 的倒数

        private void CE_Click(object sender, EventArgs e)
        {
            label2.Text = "0";
        }//CE键的功能

        private void Percent_Click(object sender, EventArgs e)
        {
            secondnum = Convert.ToDouble(label2.Text);
            secondnum = firstnum * secondnum / 100;
            if (ope == Operator.add) label1.Text = "" + firstnum + "+" + secondnum;
            if (ope == Operator.sub) label1.Text = "" + firstnum + "-" + secondnum;
            if (ope == Operator.mul) label1.Text = "" + firstnum + "*" + secondnum;
            if (ope == Operator.div) label1.Text = "" + firstnum + "/" + secondnum;
            label2.Text = ""+secondnum;
        }//%键的功能

        private void MS_Click(object sender, EventArgs e)
        {
            label3.Text = "M";
            save = label2.Text;
        }//MS键的功能

        private void MR_Click(object sender, EventArgs e)
        {
            label2.Text = save;
        }//MR键的功能

        private void MC_Click(object sender, EventArgs e)
        {
            save ="0"; label3.Text="";
        }//MC键的功能

        private void MPlus_Click(object sender, EventArgs e)
        {
            double num1, num2,sum;
            num1 = Convert.ToDouble(save); num2 = Convert.ToDouble(label2.Text);
            sum = num1 + num2;
            save = sum.ToString();
        }//M+键的功能

        private void MReduce_Click(object sender, EventArgs e)
        {
            double num1, num2, r;
            num1 = Convert.ToDouble(save); num2 = Convert.ToDouble(label2.Text);
            r= num1 - num2;
            save = r.ToString();
        }//M-键的功能

        private void 关于计算器AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f=new Form2();f.Show();
        }


        private void 复制CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(label2.Text, true, 1, 10);
            MessageBox.Show("成功复制："+label2.Text);
        }

        private void 粘贴PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label2.Text = Clipboard.GetText();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (label2.Text == null || label2.Text == "0.") label2.Text = "0";
            ope = Operator.nom;
            firstnum = Convert.ToDouble(label2.Text);
            label1.Text = "tan(" + label2.Text + ")";
            label2.Text = "" + Math.Tan(firstnum / 180 * Math.PI);
        }//求x 的正切

        private void button16_Click(object sender, EventArgs e)
        {
            if (label2.Text == null || label2.Text == "0.") label2.Text = "0";
            ope = Operator.nom;
            firstnum = Convert.ToDouble(label2.Text);
            label1.Text = "sin(" + label2.Text + ")";
            label2.Text = "" + Math.Sin(firstnum/180*Math.PI);
        }//求x的正弦

        private void button8_Click(object sender, EventArgs e)
        {
            label2.Text = Math.PI.ToString();
        }//求π的值

        private void button11_Click(object sender, EventArgs e)
        {
            if (label2.Text == null || label2.Text == "0.") label2.Text = "0";
            ope = Operator.nom;
            firstnum = Convert.ToDouble(label2.Text);
            label1.Text = "cos(" + label2.Text + ")";
            label2.Text = "" + Math.Cos(firstnum / 180 * Math.PI);
        }//求x的余弦

        private void button17_Click_1(object sender, EventArgs e)
        {
            if (label2.Text == null || label2.Text == "0.") label2.Text = "0";
            ope = Operator.nom;
            firstnum = Convert.ToDouble(label2.Text);
            label1.Text = "sinh(" + label2.Text + ")";
            label2.Text = "" + Math.Sinh(firstnum);
        }//求x的双曲正弦

        private void button12_Click(object sender, EventArgs e)
        {
            if (label2.Text == null || label2.Text == "0.") label2.Text = "0";
            ope = Operator.nom;
            firstnum = Convert.ToDouble(label2.Text);
            label1.Text = "cosh(" + label2.Text + ")";
            label2.Text = "" + Math.Cosh(firstnum );
        }//求x的双曲余弦

        private void button7_Click(object sender, EventArgs e)
        {
            if (label2.Text == null || label2.Text == "0.") label2.Text = "0";
            ope = Operator.nom;
            firstnum = Convert.ToDouble(label2.Text);
            label1.Text = "tanh(" + label2.Text + ")";
            label2.Text = "" + Math.Tanh(firstnum);
        }//求x的双曲正切

        private void button21_Click(object sender, EventArgs e)
        {
            if (label2.Text == null || label2.Text == "0.") label2.Text = "0";
            ope = Operator.nom;
            firstnum = Convert.ToDouble(label2.Text);
            label1.Text = "ln(" + label2.Text + ")";
            label2.Text = "" + Math.Log(firstnum, Math.E);
        }//求lnx的值

        private void button18_Click(object sender, EventArgs e)
        {
            if (label2.Text == null || label2.Text == "0.") label2.Text = "0";
            ope = Operator.nom;
            firstnum = Convert.ToDouble(label2.Text);
            label1.Text = "Int(" + label2.Text + ")";
            label2.Text = "" + (firstnum-firstnum%1);
        }//取该数的整数部分

        private void button3_Click(object sender, EventArgs e)
        {
            if (label2.Text == null || label2.Text == "0.") label2.Text = "0";
            ope = Operator.mod;
            firstnum = Convert.ToDouble(label2.Text);
            label1.Text = label2.Text + " Mod";
            label2.Text = def;
        }//求余

        private void button2_Click(object sender, EventArgs e)
        {
            if (label2.Text == null || label2.Text == "0.") label2.Text = "0";
            ope = Operator.nom;
            firstnum = Convert.ToDouble(label2.Text);
            label1.Text = "log(" + label2.Text + ")";
            label2.Text = "" + Math.Log10(firstnum);
        }//求以10为底的logx

        private void button5_Click(object sender, EventArgs e)
        {
            if (label2.Text == null || label2.Text == "0.") label2.Text = "0";
            ope = Operator.nom;
            firstnum = Convert.ToDouble(label2.Text);
            label1.Text = "cube(" + label2.Text + ")";
            label2.Text = "" + Math.Pow (firstnum,3);
        }//求x的3次方

        private void button10_Click(object sender, EventArgs e)
        {
            if (label2.Text == null || label2.Text == "0.") label2.Text = "0";
            ope = Operator.a;
            firstnum = Convert.ToDouble(label2.Text);
            label1.Text = label2.Text + " ^";
            label2.Text = def;
        }//求x的y次方

        private void button15_Click(object sender, EventArgs e)
        {
            if (label2.Text == null || label2.Text == "0.") label2.Text = "0";
            ope = Operator.nom;
            firstnum = Convert.ToDouble(label2.Text);
            label1.Text = "sqr(" + label2.Text + ")";
            label2.Text = "" + Math.Pow(firstnum, 2);
        }//求x的2次方根

        private void button14_Click(object sender, EventArgs e)
        {
            if (label2.Text == null || label2.Text == "0.") label2.Text = "0";
            ope = Operator.nom;
            firstnum = Convert.ToDouble(label2.Text);
            label1.Text = "fact(" + label2.Text + ")";
            double s=firstnum;
            for (int i = 1; i < firstnum; i++)
                s = s * i;
                label2.Text = "" + s;
        }//求x的阶乘

        private void button9_Click(object sender, EventArgs e)
        {
            if (label2.Text == null || label2.Text == "0.") label2.Text = "0";
            ope = Operator.b;
            firstnum = Convert.ToDouble(label2.Text);
            label1.Text = label2.Text + " yroot";
            label2.Text = def;
        }//求x的y次方根

        private void button24_Click(object sender, EventArgs e)
        {
            if (label2.Text == null || label2.Text == "0.") label2.Text = "0";
            ope = Operator.nom;
            firstnum = Convert.ToDouble(label2.Text);
            label1.Text = "cuberoot(" + label2.Text + ")";
            label2.Text = "" + Math.Pow(firstnum, 1.0/3);
        }//求x的3次方根

        private void button1_Click(object sender, EventArgs e)
        {
            if (label2.Text == null || label2.Text == "0.") label2.Text = "0";
            ope = Operator.nom;
            firstnum = Convert.ToDouble(label2.Text);
            label1.Text = "powten(" + label2.Text + ")";
            label2.Text = "" + Math.Pow(10, firstnum);
        }//求10的x次方

        private void 标准型ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            科学型TAlt1ToolStripMenuItem.Checked = false;
            标准型ToolStripMenuItem.Checked = true;
            this.Width = 220; panel1.Width = 190; panel2.Width = 190;
            label2.Width = 166; label1.Width = 190;
            panel2.Left = 12;
            panel3.Left = 250;
            panel3.Visible = true;
        }

        private void 科学型TAlt1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            科学型TAlt1ToolStripMenuItem.Checked = true;
            标准型ToolStripMenuItem.Checked = false;
            this.Width = 420; panel1.Width = 390; panel2.Width = 390;
            label2.Width = 366; label1.Width = 390;
            panel2.Left = 213;
            panel3.Left = 12;
            panel3.Visible = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            label2.Visible = false;
            label1.Text = "";

            textBox1.Text = "0";
            textBox1.SelectAll();
            textBox1.Visible = !textBox1.Visible;
        }

        private void Calculator_Load(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            label2.Visible = true;
            label2.Text = "0";
        }

        private void Calculator_Click(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            label2.Visible = true;
            label2.Text = "0";
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                label2.Text = Calculate.GetResult(textBox1.Text.ToString());
                label1.Text = textBox1.Text.ToString();
                label2.Visible = true;

                textBox1.Text = "0";
                textBox1.Visible = false;
            }
        }




    }
}
