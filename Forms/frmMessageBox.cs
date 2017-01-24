using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IMS_System.Class.GUI;

namespace IMS_System.Forms
{
    public partial class frmMessageBox : Form
    {
       // clsLines clsLiness = new clsLines();
        frmMainPage MainScreen;

        public frmMessageBox(String Type, String Title, String Message, Boolean Yes, frmMainPage MainPage)
        {
            InitializeComponent();
            MainScreen = MainPage;
            label2.Text = Title;
            label1.Text = Message;
            if (Yes == true)
            {
                button1.Visible = true;
                button1.Enabled = true;
                button2.Text = "No";
            }
            else
            {
                button1.Visible = false;
                button1.Enabled = false;
                button2.Text = "OK";
            }

            if (Type == "error")
            {
                panel2.BackColor = Color.Red;
                label2.BackColor = Color.Red;
                pictureBox1.BackgroundImage = imageList1.Images[3];
            }
            else if (Type == "alert")
            {
                panel2.BackColor = Color.RoyalBlue;
                label2.BackColor = Color.RoyalBlue;
                pictureBox1.BackgroundImage = imageList1.Images[0];
            }
            else if (Type == "question")
            {
                panel2.BackColor = Color.RoyalBlue;
                label2.BackColor = Color.RoyalBlue;
                pictureBox1.BackgroundImage = imageList1.Images[1];
            }
            else
            {
                panel2.BackColor = Color.RoyalBlue;
                label2.BackColor = Color.RoyalBlue;
                pictureBox1.BackgroundImage = imageList1.Images[2];
            }

            if (Message.Length >= 66)
            {
                label1.Font = new Font(label1.Font.Name,10);
            }
            else
            {
                label1.Font = new Font(label1.Font.Name, 11);
            }
        }

 
        private void label1_Paint(object sender, PaintEventArgs e)
        {
            try { clsLines.Draw_Horizontal_lines(e, Color.Black, label1); }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                IMS_System.Properties.Settings.Default.MessageBoxResults = button2.Text;
                this.Dispose();
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Visible = false;
                IMS_System.Properties.Settings.Default.MessageBoxResults = button1.Text;
                
                this.Dispose();
            }
            catch { }
        }

        private void frmMessageBox_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.BringToFront();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
