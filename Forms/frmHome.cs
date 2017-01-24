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
    public partial class frmHome : Form
    {
        frmMainPage MainScreen;
        
        public frmHome(frmMainPage MainPage)
        {
            InitializeComponent();
            MainScreen = MainPage;
            
        }

        private void frmBatches_Load(object sender, EventArgs e)
        {
           // MessageBox.Show(IMS_System.Properties.Settings.Default.)
        }

        private void label6_Paint(object sender, PaintEventArgs e)
        {
            clsLines.Draw_Horizontal_lines(e, Color.Black, label6);
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {

            if (clsClose_Other_Forms.IMS_IsFormOpen("frmSubjects") == false)
            {
                frmSubjects Subjects = new frmSubjects(MainScreen);
                Subjects.ShowDialog();
                Subjects.BringToFront();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (clsClose_Other_Forms.IMS_IsFormOpen("frmBatchDetails") == false)
            {
                frmBatchDetails BatchDetails = new frmBatchDetails(MainScreen);
                // BatchDetails.MdiParent = MainScreen;
                BatchDetails.ShowDialog();
                BatchDetails.BringToFront();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (clsClose_Other_Forms.IMS_IsFormOpen("frmSubjectFees") == false)
            {
                frmSubjectFees SubjectFees = new frmSubjectFees(MainScreen);
                SubjectFees.ShowDialog();
                SubjectFees.BringToFront();

            }
        }
    }
}
