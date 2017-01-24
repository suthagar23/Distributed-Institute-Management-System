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

namespace IMS_System.Forms.startUp
{
    public partial class frmDbConnectionLost : Form
    {
        //clsCheckConnection clsCheckConnection = new clsCheckConnection();
        //clsDatabase_Connection clsDatabase_Connection = new clsDatabase_Connection();

        Boolean ecancel = false;
        public frmDbConnectionLost()
        {
            InitializeComponent();
        }

        private void frmDbConnectionLost_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                ecancel = true;
                Application.Exit();
            }
            catch { }
        }

        private void btnTryAgain_Click(object sender, EventArgs e)
        {
            clsDatabase_Connection.db_con.ConnectionString = IMS_System.Properties.Settings.Default.DBConnectionString;
            if (clsCheckConnection.Check_Connection(clsDatabase_Connection.db_con.DataSource) == true)
            {
                ecancel = true;
                this.Close();
            }
            else
            {
                // Connection Lost
            }
        }

        private void frmDbConnectionLost_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ecancel == false)
            { e.Cancel = true; }
            else
            { e.Cancel = false; }
        }
    }
}
