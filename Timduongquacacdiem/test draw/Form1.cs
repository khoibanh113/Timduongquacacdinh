using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_draw
{
    public partial class Form1 : Form
    {
        Graphics g, gg;
        Pen p, pVe;
        Point cursor;
        int k = 0, stt = 0;
        int z = 0;
        Point[] points = new Point[50];
        int[] dodai = new int[50];
        int[,] luuTru = new int[50, 50];
        string[] mang = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20" };
        string[] luu = new string[50];
        
        public Form1()
        {
            InitializeComponent();
            g = this.CreateGraphics();
            p = new Pen(Color.Red);
            pVe = new Pen(Color.Black,3);
            gg = pictureBox1.CreateGraphics();
            
            
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (checkBox1.Checked)
            {
                cursor = this.PointToClient(Cursor.Position);
                mouseStatus.Text = "X: " + cursor.X + " Y: " + cursor.Y; 
            }
            
        }
        private void Form1_Click(object sender, EventArgs e)
        {
            
            if (checkBox1.Checked)
            {

                Label lb = new Label();
                try
                {
                    lb.Text = (z + 1).ToString();
                }
                catch (Exception)
                {
                    MessageBox.Show("Quá nhiều đỉnh ","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                z++;
                lb.BackColor = Color.LightBlue;
                lb.Location = new Point(cursor.X-2, cursor.Y-2);
                lb.AutoSize = true;
                lb.Cursor = Cursors.No;
              
                this.Controls.Add(lb);
                cmbDinhbatdau.Items.Add(lb.Text);cmbDinhbatdau.SelectedIndex = 0;
                cmbDinhketthuc.Items.Add(lb.Text);

                Pen p = new Pen(Color.LightBlue);
                Rectangle R = new Rectangle(cursor.X - 5, cursor.Y - 5, 20, 20);
                g.DrawEllipse(p, cursor.X -5 , cursor.Y -5, 20, 20);
                g.FillEllipse(Brushes.LightBlue, R);
                points[k++] = new Point(cursor.X, cursor.Y);
                listBox1.Items.Add("X: " + cursor.X + " Y: " + cursor.Y);
                
            }
            
           
        }
        private void btnVe_Click(object sender, EventArgs e)
        {
            
            try
            {
                var sobd = cmbDinhbatdau.SelectedIndex;
                var sokt = cmbDinhketthuc.SelectedIndex;
                Point dinhbd = points[sobd];
                Point dinhkt = points[sokt];
                if (radioButton2.Checked)
                {
                    //tinh do dai
                    double a = Math.Pow((dinhbd.X - dinhkt.X), 2);
                    double b = Math.Pow((dinhbd.Y - dinhkt.Y), 2);
                    double dodai2diem = Math.Round(Math.Sqrt(a + b));
                    int kqq = int.Parse(dodai2diem.ToString());
                 //   MessageBox.Show("Khoảng cách từ đỉnh: " + cmbDinhbatdau.Text + " đến đỉnh: " + cmbDinhketthuc.Text + " là: " + kqq, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    numericDodaicanh.Value = kqq;
                }


                if (pictureBox1.Visible==false) //chua hien ban do
                {
                    if (dinhbd != dinhkt)
                    {
                        g.DrawLine(pVe, dinhbd, dinhkt);
                        Label lb = new Label();
                        lb.Text = numericDodaicanh.Value.ToString();
                        lb.Location = new Point((dinhbd.X + dinhkt.X) / 2, (dinhbd.Y + dinhkt.Y) / 2 - 20);

                        lb.AutoSize = true;
                        this.Controls.Add(lb);
                        //    dodai[stt] =int.Parse( numericDodaicanh.Value.ToString());



                    }
                    else
                    {
                        Point dinhbandauve = new Point(dinhbd.X - 33, dinhbd.Y - 25);
                        g.DrawArc(pVe, dinhbandauve.X, dinhbandauve.Y, 30, 30, 50, 310);
                        Label lb = new Label();
                        lb.Text = numericDodaicanh.Value.ToString();
                        lb.Location = new Point(dinhbd.X - 33 - 12, dinhbd.Y - 25 - 10);
                        lb.Size = new Size(16, 16);
                        this.Controls.Add(lb);


                    } 
                }
                else//phan ung dung
                {
                    if (dinhbd != dinhkt)
                    {
                        gg.DrawLine(pVe, dinhbd.X-300+40,dinhbd.Y-30+18, dinhkt.X-300+40,dinhkt.Y-30+18);
                    }
                    else
                    {
                        Point dinhbandauve = new Point(dinhbd.X - 33, dinhbd.Y - 25);
                    }
                }

                string dau = cmbDinhbatdau.Text;
                string cuoi = cmbDinhketthuc.Text;
                int n = 0, m = 0;
                for (int i = 0; i < 26; i++)
                {
                    if (mang[i] == dau)
                    {
                        n = i + 1;
                        break;
                    }
                }
                for (int i = 0; i < 26; i++)
                {
                    if (mang[i] == cuoi)
                    {
                        m = i + 1;
                        break;
                    }
                }
                if (n < m)
                {
                    luu[stt] = n + "," + m + "," + int.Parse(numericDodaicanh.Text);
                }
                if (n > m)
                {
                    luu[stt] = m + "," + n + "," + int.Parse(numericDodaicanh.Text);
                }
                stt++;
                
            }
            catch (Exception)
            {
                MessageBox.Show("Hãy chọn đỉnh cần vẽ","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
           
           

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            radioButton2.Checked = true;
           
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            

             using (StreamWriter sw = new StreamWriter("textfile.txt"))
             {
                 foreach (string s in luu)
                 {
                     sw.WriteLine(s);
                 }
             }
            ReadGraphFromFile(@"C:\Users\ADMIN\Documents\visual studio 2015\Projects\test draw\test draw\bin\Debug\textfile.txt");
            richTextBox1.Clear();
            PrintGraph();
            var mang = "";
            if (Graph.Count > 0)
            {

                List<Edge> Tree = Prim();
                richTextBox1.Text += "Cay khung co trong luong nho nhat : \n";
                foreach (Edge edge in Tree)
                {
                    richTextBox1.Text += "(" + edge.s + " , " + edge.e + " )= " + edge.w + "\n";
                    mang += edge.s +" "+edge.e+",";
                }
                richTextBox1.Text += "Trong luong cay: " + Tree.Sum(p => p.w);

                //lay toa do
                richTextBox2.Text = mang;
                string[] kq, kq1;
                kq = mang.Split(',');
                for(int i = 0; i < kq.Length; i++)
                {
                    try
                    {
                        kq1 = kq[i].Split(' ');
                        Point p1 = points[(int.Parse(kq1[0]) - 1)];
                        Point p2 = points[(int.Parse(kq1[1]) - 1)];
                        
                        if (pictureBox1.Visible==false)
                        {
                            Pen pVelai = new Pen(Color.Red, 5);
                            g.DrawLine(pVelai, p1, p2);

                        }
                        else
                        {
                            Pen pVelai2 = new Pen(Color.Yellow, 5);
                            gg.DrawLine(pVelai2, p1.X-300+40,p1.Y-30+18, p2.X-300+40,p2.Y-30+18);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                using (StreamWriter sw2 = new StreamWriter("test2.txt"))
                {
                    foreach (char s2 in mang.ToString())
                    {
                        sw2.WriteLine(s2);
                    }
                }

            }

           
        }
       

        public struct Edge
        {
            public string s;
            public string e;
            public int w;
            public Edge(int _s, int _e, int _w)
            {
                s = _s.ToString();
                e = _e.ToString();
                w = _w;
            }
            public Edge(string _s, string _e, string _w)
            {
                s = _s;
                e = _e;
                w = int.Parse(_w);
            }
        }
        //tao Do thi = danh sach cac canh
        public List<Edge> Graph;
        int SoDinh;

        public List<Edge> Prim()
        {

            List<Edge> Tree = new List<Edge>();
            List<string> DanhDau = new List<string>();
          
            DanhDau.Add(Graph[0].s);
            while (DanhDau.Count < SoDinh)
            {
                var Edges = Graph.Where(p => (DanhDau.Contains(p.s) && !DanhDau.Contains(p.e)) || (DanhDau.Contains(p.e) && !DanhDau.Contains(p.s)));
                try
                {
                    var minw = Edges.Min(p => p.w);
                    var minEdge = Edges.Where(p => p.w == minw).First();
                    Tree.Add(minEdge);
                    Graph.Remove(minEdge);

                    if (!DanhDau.Contains(minEdge.s)) DanhDau.Add(minEdge.s);
                    if (!DanhDau.Contains(minEdge.e)) DanhDau.Add(minEdge.e);

                }
                catch (Exception)
                {
                }
            
            } 
            return Tree;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
                numericDodaicanh.Enabled = true;
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                numericDodaicanh.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ( (MessageBox.Show("Bạn có muốn khởi động lại chương trình???", "Khởi động lại", MessageBoxButtons.YesNo, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes))
            {
                Application.Restart();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Visible == false)//ko thay hinh =>nhan vao la thay hinh
            {
                radioButton1.Enabled = false;
                pictureBox1.Visible = true;
                richTextBox1.Visible = false;
            }
            else
            {
              //  pictureBox1.Visible = false;
              //  radioButton1.Enabled = true;
              //  richTextBox1.Visible = true;
            }
        }

        

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Bạn có muốn thoát chương trình???", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes))
            {
                this.Close();
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            btnVe_Click(sender, e);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
           
            button2_Click(sender, e);
            
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            cursor = this.PointToClient(Cursor.Position);

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            btnRun_Click(sender, e);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Label lb = new Label();
                try
                {
                    lb.Text = "" + mang[z];
                }
                catch (Exception)
                {
                    MessageBox.Show("Quá nhiều đỉnh ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                z++;
                lb.BackColor = Color.Red;
                lb.Location = new Point(cursor.X - 300 + 39, cursor.Y - 30 + 14);
                lb.AutoSize = true;
                lb.Cursor = Cursors.No;
                pictureBox1.Controls.Add(lb);
                cmbDinhbatdau.Items.Add(lb.Text); cmbDinhbatdau.SelectedIndex = 0;
                cmbDinhketthuc.Items.Add(lb.Text);

                Pen p = new Pen(Color.Red);
                Rectangle R = new Rectangle(cursor.X - 300+38, cursor.Y - 30+13, 15, 15);
                gg.DrawEllipse(p, R);
                gg.FillEllipse(Brushes.Red, R);
                points[k++] = new Point(cursor.X, cursor.Y);
                listBox1.Items.Add("X: " + cursor.X + " Y: " + cursor.Y); 
            }
        }

        public void ReadGraphFromFile(string fileName)
        {
            var Lines = System.IO.File.ReadAllLines(fileName);
            Graph = new List<Edge>();
            foreach (var Line in Lines)
            {
                var p = Line.Split(',');
                try
                {
                    Graph.Add(new Edge(p[0], p[1], p[2]));
                }
                catch (Exception)
                {
                    
                }
            }
           
            List<string> dsDinh = new List<string>();
            foreach (Edge edge in Graph)
            {
                if (!dsDinh.Contains(edge.s)) dsDinh.Add(edge.s);
                if (!dsDinh.Contains(edge.e)) dsDinh.Add(edge.e);
            }
            SoDinh = dsDinh.Count;

           
        }

        public void PrintGraph()
        {
            richTextBox1.Text += "\n Do thi ban dau";
            foreach (Edge edge in Graph)
            {
                richTextBox1.Text += "(" + edge.s + "," + edge.e + ")= " + edge.w + "\n";
            }
            richTextBox1.Text += "--------------------------------\n";
        }
    }
    
}
