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
using IMS_System.Class.Forms;
using IMS_System.Class.Database;
using System.Data.SqlClient;
using IMS_System.Class.Codes;

namespace IMS_System.Forms
{
    public partial class frmReports : Form
    {
        frmMainPage MainScreen;
        // private String Query = "";

        public frmReports(frmMainPage MainPage)
        {
            InitializeComponent();
            MainScreen = MainPage;

        }

        private void frmBatches_Load(object sender, EventArgs e)
        {

        }

        private void label6_Paint(object sender, PaintEventArgs e)
        {
            clsLines.Draw_Horizontal_lines(e, Color.Black, label6);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
