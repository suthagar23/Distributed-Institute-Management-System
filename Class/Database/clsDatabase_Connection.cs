using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using IMS_System.Class.Database;
using IMS_System.Forms;
using IMS_System.Forms.startUp;
using System.IO;

namespace IMS_System.Class.Database
{
    class clsDatabase_Connection
    {
        //clsCheckConnection Check_Connection = new clsCheckConnection();

        static fileengcode.Class1 FileEncrypt = new fileengcode.Class1();  // Initializing Encrypt DLL 
        public static SqlConnection db_con = new SqlConnection();
        public static SqlDataAdapter objDataAdapter = new SqlDataAdapter();
        public static String sqlConn = "";
        static ListBox Database_conf = new ListBox();
        public static DataSet objDataSet = new DataSet();

        private static string dbname { get; set; }
        private static string dbhost { get; set; }
        private static string user { get; set; }
        private static string pass { get; set; }

        public static void Get_DB_Connection_String()
        {
            try
            {
                if (File.Exists(Directory.GetCurrentDirectory() + @"\Settings\db_conf\db_con.dll") == true)
                {
                    Database_conf.Items.Clear();
                    FileEncrypt.Open_Files(Directory.GetCurrentDirectory() + @"\Settings\db_conf\db_con.dll", ref Database_conf);
                    Database_conf.SetSelected(0, true);
                    Database_conf.SetSelected(1, true);
                    Database_conf.SetSelected(2, true);
                    Database_conf.SetSelected(3, true);

                    dbname = Database_conf.SelectedItems[1].ToString();
                    dbhost = Database_conf.SelectedItems[0].ToString();
                    user = Database_conf.SelectedItems[2].ToString();
                    pass = Database_conf.SelectedItems[3].ToString();

                    IMS_System.Properties.Settings.Default.DBConnectionString = String.Format("server={0}; user id={1}; password={2}; database={3}; connection timeout=1;", dbhost, user, pass, dbname);
                    // MessageBox.Show("ok");
                }
                else
                {// Database Error Show
                    frmDbConnectionLost DB_Connection_Losts = new frmDbConnectionLost();
                    DB_Connection_Losts.ShowDialog();
                }
            }
            catch
            {
                frmDbConnectionLost DB_Connection_Losts = new frmDbConnectionLost();
                DB_Connection_Losts.ShowDialog();
            }
        }

        public static void Start_DB_Connection()
        {
            try
            {
                if (db_con != null)
                { db_con.Close(); }
                db_con.ConnectionString = IMS_System.Properties.Settings.Default.DBConnectionString;
                if (clsCheckConnection.Check_Connection(db_con.DataSource) == true)
                {
                    db_con.Open();
                    //  MessageBox.Show("opened");
                }
                else
                {
                    frmDbConnectionLost DB_Connection_Losts = new frmDbConnectionLost();
                    DB_Connection_Losts.ShowDialog();
                }

            }
            catch
            {
                frmDbConnectionLost DB_Connection_Losts = new frmDbConnectionLost();
                DB_Connection_Losts.ShowDialog();
            }
        }
        public static void Close_DB_Connection()
        {
            try
            {
                db_con.Close();
                //    MessageBox.Show("closed");
            }
            catch { MessageBox.Show("not  close"); }
        }

        public static DataSet Get_Table(String query)
        {
            Clipboard.SetText(query);
            Start_DB_Connection();
            objDataSet.Tables.Clear();
            sqlConn = query;
            objDataAdapter = new SqlDataAdapter(sqlConn, db_con);
            objDataAdapter.Fill(objDataSet, "Item");
            Close_DB_Connection();
             return objDataSet;
        }

        public static Boolean ExecuteQuery(String Query)
        {
            try
            {
                Clipboard.SetText(Query);
                Start_DB_Connection();
                SqlCommand sqlCommands = new SqlCommand();
                sqlCommands.Connection = db_con;
                sqlCommands.CommandText = Query;
                sqlCommands.ExecuteNonQuery();
                Close_DB_Connection(); 
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public static Boolean CheckforDublicate(String Query)
        {
            try
            {
                Clipboard.SetText(Query);
                Start_DB_Connection();
                objDataSet.Tables.Clear();
                sqlConn = Query;
                objDataAdapter = new SqlDataAdapter(sqlConn, db_con);
                objDataAdapter.Fill(objDataSet, "Item");
                Close_DB_Connection();
 
                if (objDataSet.Tables[0].Rows[0][0].ToString().Equals("0"))
                { return true; }
                else
                { return false; }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
