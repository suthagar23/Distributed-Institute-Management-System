using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IMS_System.Class.Database;
using IMS_System.Forms;

namespace IMS_System.Forms.startUp
{
    public partial class frmSplash : Form
    {
        public frmSplash()
        {
            InitializeComponent();
        }

        private void frmSplash_Load(object sender, EventArgs e)
        {
            Initilize_Database_Connection();
            timer1.Enabled = true;
        }

        private void Initilize_Database_Connection()
        {
            //clsCheckConnection clsCheckConnections = new clsCheckConnection();
            //clsDatabase_Connection clsDatabase_Connections = new clsDatabase_Connection();

            clsDatabase_Connection.Get_DB_Connection_String();

            clsDatabase_Connection.db_con.ConnectionString = IMS_System.Properties.Settings.Default.DBConnectionString;
            if (clsCheckConnection.Check_Connection(clsDatabase_Connection.db_con.DataSource) == true)
            {

            }
            else
            {
                // Connection Lost
                frmDbConnectionLost DB_Connection_Losts = new frmDbConnectionLost();
                DB_Connection_Losts.ShowDialog();
            }

        }


        private void timer1_Tick_1(object sender, EventArgs e)
        {
            this.Hide();
            timer1.Enabled = false;
            frmMainPage Main_Page = new frmMainPage("login");
            Main_Page.Show();
        }
    }
}
