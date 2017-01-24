using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS_System.Class.Database;
using System.Data.SqlClient;
using IMS_System.Class.Forms;
using System.Windows.Forms;

namespace IMS_System.Class.Forms
{
    class clsLogout
    {
        // clsDatabase_Connection clsDatabase_Connections = new clsDatabase_Connection();
        // clsNetwork clsNetworks = new clsNetwork();

        static String PC_Id = "";
        public static void Sucess_Full_Logout(String Type)
        {
            try
            {
                    PC_Id = clsNetwork.SystemHDD_Id();
                    clsDatabase_Connection.Start_DB_Connection();
                    SqlCommand sqlCommands = new SqlCommand();
                    sqlCommands.Connection = clsDatabase_Connection.db_con;
                    sqlCommands.CommandText = "insert into tblLogoutHistory values((select top 1 LoginId from tblLoginHistory where Login_PC_Id='" + PC_Id + "' and UserId='" + IMS_System.Properties.Settings.Default.current_user_id + "' order by LoginId desc),GETDATE(),'" + clsNetwork.LocalIPAddress() + "','" + Type + "','" + PC_Id + "');";
                    sqlCommands.CommandText = sqlCommands.CommandText + "update tblUser set LoginStatus=0 where UserId='" + IMS_System.Properties.Settings.Default.current_user_id + "';";
                    Clipboard.SetText(sqlCommands.CommandText);
    
                sqlCommands.ExecuteNonQuery();
                    clsDatabase_Connection.Close_DB_Connection();
                   // return true;
            }
            catch 
            { //return false; 
            }
        }
    }
}
