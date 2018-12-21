using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace ColorPicker_By_Gamo097
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            button3.Visible = !true;
            textBox1.Text = "";
            
            //LoadCustumColor();
        }

        
        List<RGB> DSRGB = new List<RGB>();

        RGB chuyendoi(String item)
        {
            String[] temp = item.Split(',');
            RGB color = new RGB(Convert.ToInt32(temp[0]), Convert.ToInt32(temp[1]), Convert.ToInt32(temp[2]));
            return color;
        }
        void LoadCustumColor()
        {
            //try
            //{
                String path1 = new
                        Uri(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
                String path = path1 += @"\data\data.txt";
                FileStream filestream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                List<String> ListColor = new List<string>();
                StreamReader file = new StreamReader(filestream);
                string line = "";
                while ((line = file.ReadLine()) != null)
                {
                     ListColor.Add(line);
                }

                foreach (String item in ListColor)
                {
                        String[] temp = item.Split(',');
                        //MessageBox.Show(temp[0] + "-" + temp[1] + "-" + temp[2]);
                        RGB color = new RGB(Convert.ToInt32(temp[0]), Convert.ToInt32(temp[1]), Convert.ToInt32(temp[2]));
                        DSRGB.Add(color);
                }

            //List<Color> a = new List<Color>();
            //List<int> numberRGB = new List<int>();
            colorDialog1.CustomColors = new int[DSRGB.Count];

            for (int i=0; i<DSRGB.Count; i++)
            {
                colorDialog1.CustomColors[i] = new int();
                colorDialog1.CustomColors[i]=(Convert.ToInt32(ColorTranslator.ToOle(Color.FromArgb(DSRGB[i].R, DSRGB[i].G, DSRGB[i].B))));
            }

            colorDialog1.CustomColors = new int[]{6916092, 15195440, 16107657, 1836924,
                           3758726, 12566463, 7526079, 7405793, 6945974, 241502, 2296476, 5130294,
                           3102017, 7324121, 14993507, 11730944,};

            //{
            //            ColorTranslator.ToOle(Color.FromArgb(DSRGB[0].R, DSRGB[0].G, DSRGB[0].B))
            //        };


            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString(), "Cannot read custum Color!");
            //}
        }
        
        void AddCustomColor(object sender, EventArgs e)
        {
            try
            {
                if (DSRGB.Count < 16)
                {
                    String path1 = new
                        Uri(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
                    String path = path1 += @"\data\data.txt";
                    FileStream filestream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    List<String> ListColor = new List<string>();
                    String color = RGBConverter(colorDialog1.Color);
                    //color = color.Remove(0);
                    StreamWriter file = new StreamWriter(filestream);
                    file.WriteLine(color);
                    MessageBox.Show("Done!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.ToString(), "Cannot add custum Color!");
            }
        }

        private void colorDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.FullOpen=true;
            colorDialog1.ShowDialog();
            ColorChange();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text==null || textBox1.Text=="")
            {
                button1.ForeColor = Color.Black;
                button2.ForeColor = Color.Black;
                return;
            }
            try
            {
                RGB temp = new RGB();
                temp = chuyendoi(textBox1.Text);
                Color a = new Color();
                a = Color.FromArgb(temp.R, temp.G, temp.B);
                textBox1.ForeColor = a;
                button1.ForeColor = a;
                button2.ForeColor = a;
            }
            catch
            {
                try
                {
                    Color a = System.Drawing.ColorTranslator.FromHtml(textBox1.Text);
                    textBox1.ForeColor = a;
                    button1.ForeColor = a;
                    button2.ForeColor = a;
                }
                catch
                { }
            }

        }

        void ColorChange()
        {
            if(radioButton1.Checked)
            textBox1.Text = RGBConverter(colorDialog1.Color);
            else
                textBox1.Text = HexConverter(colorDialog1.Color);
        }

        private String RGBConverter(Color c)
        {
            String rtn = String.Empty;
            try
            {
                //rtn = "RGB(" + c.R.ToString() + "," + c.G.ToString() + "," + c.B.ToString() + ")";

                rtn = "" + c.R.ToString() + "," + c.G.ToString() + "," + c.B.ToString() + "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return rtn;
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(textBox1.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private String HexConverter(Color c)
        {
            String rtn = String.Empty;
            try
            {
                rtn = "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return rtn;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ColorChange();
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            ColorChange();
        }

        private void radioButton1_Enter(object sender, EventArgs e)
        {
            radioButton2.Checked = !true;
        }

        private void radioButton2_Enter(object sender, EventArgs e)
        {
            radioButton1.Checked = !true;
        }
    }
}
