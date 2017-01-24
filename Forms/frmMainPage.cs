using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IMS_System.Forms.startUp;
using IMS_System.Class.Forms;
using IMS_System.Class.Database;
using IMS_System.Class.GUI;
namespace IMS_System.Forms
{
    public partial class frmMainPage : Form
    {

        frmLogin LoginForm;

        //clsClose_Other_Forms clsCloseOtherForms;
       // clsLogout clsLogouts;

        public void SucessMessageShow(String Message,String Type)
        {

        }
        public frmMainPage(string code)
        {
            InitializeComponent();
            InitilizeObjects();
            InitialActions(code);
        }

        private void InitilizeObjects()
        {
           // clsCloseOtherForms = new clsClose_Other_Forms();
           // clsLogouts = new clsLogout();
          

            foreach (Control control in this.Controls)
            {
                MdiClient client = control as MdiClient;
                if (!(client == null))
                {
                    client.BackColor = Color.White;
                    break;
                }
            }
        }

        private void frmMainPage_Load(object sender, EventArgs e)
        {
            try
            {
                this.KeyPreview = true;
               
            }
            catch { }
        }

        private void InitialActions(string code)
        {
            ShowLoginForm();

        }

        private void ShowLoginForm()
        {
            // clsClose_Other_Formss.Dispose_other_forms("frmLoginProiles");
            if (clsClose_Other_Forms.IMS_IsFormOpen("frmLogin") == false)
            {
                LoginForm = new frmLogin(this);
                LoginForm.MdiParent = this;
                LoginForm.Show();
                LoginForm.BringToFront();
                LoginForm.Dock = DockStyle.Left;
                LoginForm.Left = 0;// panel2.Left;
                LoginForm.Top = panel1.Height + 10;// panel2.Top;
            }
        }

        public void Login_Sucessfull()
        {
            //btnLogout.Enabled = true; btnLogout.Visible = true;
            btnLogout.Text = "Logout";
            panel2.Visible = true;
            autoAttendance();
            OpenHomePage();

 //          MessageBox.Show(IMS_System.Properties.Settings.Default.staffType);
            if (IMS_System.Properties.Settings.Default.staffType.ToUpper().Equals("ADMIN"))
            {
                button4.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
            }
        }

        private void autoAttendance()
        {
            panel3.Visible = true;
        }

        public void Log_Out()
        {
            try
            {
                panel3.Visible = false;
                panel2.Visible = false;
                clsLogout.Sucess_Full_Logout("MANUAL");
                Logout_button_hide_works();
                ShowLoginForm();
            }
            catch { }
        }
        private void Logout_button_hide_works()
        {
            try
            {
               // btnLogout.Enabled = false; btnLogout.Visible = false;
                btnLogout.Text = "CLOSE";
            }
            catch (Exception ex) { }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if(btnLogout.Text.ToUpper().Equals("CLOSE"))
            {
                Application.Exit();
            }
            else { Log_Out(); }
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void frmMainPage_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void label2_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void label6_ParentChanged(object sender, EventArgs e)
        {

        }

        private void label6_Paint(object sender, PaintEventArgs e)
        {
            clsLines.Draw_Horizontal_lines(e, Color.Black, label6);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (clsClose_Other_Forms.IMS_IsFormOpen("frmBatches") == false)
            {
                clsClose_Other_Forms.Dispose_other_forms("");
                frmBatches batches = new frmBatches(this);
                batches.MdiParent = this;
                batches.Show();
                batches.BringToFront();
                batches.Left = 0;
                batches.Top = 0;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (clsClose_Other_Forms.IMS_IsFormOpen("frmStudents") == false)
            {
                clsClose_Other_Forms.Dispose_other_forms("");
                frmStudents students = new frmStudents(this);
                students.MdiParent = this;
                students.Show();
                students.BringToFront();
                students.Left = 0;
                students.Top = 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (clsClose_Other_Forms.IMS_IsFormOpen("frmExams") == false)
            {
                clsClose_Other_Forms.Dispose_other_forms("");
                frmExams exams = new frmExams(this);
                exams.MdiParent = this;
                exams.Show();
                exams.BringToFront();
                exams.Left = 0;
                exams.Top = 0;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (clsClose_Other_Forms.IMS_IsFormOpen("frmAccounts") == false)
            {
                clsClose_Other_Forms.Dispose_other_forms("");
                frmAccounts Accounts = new frmAccounts(this);
                Accounts.MdiParent = this;
                Accounts.Show();
                Accounts.BringToFront();
                Accounts.Left = 0;
                Accounts.Top = 0;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenHomePage();
        }

        private void OpenHomePage()
        {
            if (clsClose_Other_Forms.IMS_IsFormOpen("frmHome") == false)
            {
                clsClose_Other_Forms.Dispose_other_forms("");
                frmHome home = new frmHome(this);
                home.MdiParent = this;
                home.Show();
                home.BringToFront();
                home.Left = 0;
                home.Top = 0;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (clsClose_Other_Forms.IMS_IsFormOpen("frmReports") == false)
            {
                clsClose_Other_Forms.Dispose_other_forms("");
                frmReports Reports = new frmReports(this);
                Reports.MdiParent = this;
                Reports.Show();
                Reports.BringToFront();
                Reports.Left = 0;
                Reports.Top = 0;
            }
        }
    }
}
