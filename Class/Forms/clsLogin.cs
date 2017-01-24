
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
    class clsLogin
    {
        //clsDatabase_Connection clsDatabase_Connections = new clsDatabase_Connection();
        //clsEncrypt clsEncrypts = new clsEncrypt();
        //clsNetwork clsNetworks = new clsNetwork();

        public static Boolean Check_Clicked_User_Status(int User_id)
        {
            try 
            { 
                clsDatabase_Connection.Start_DB_Connection();
                clsDatabase_Connection.objDataSet.Tables.Clear();
                clsDatabase_Connection.sqlConn = "select UserStatus from tblUser where USERID='"+User_id+"'";
                clsDatabase_Connection.objDataAdapter = new SqlDataAdapter(clsDatabase_Connection.sqlConn, clsDatabase_Connection.db_con);
                clsDatabase_Connection.objDataAdapter.Fill(clsDatabase_Connection.objDataSet, "Login");
                clsDatabase_Connection.Close_DB_Connection();
                if (clsDatabase_Connection.objDataSet.Tables[0].Rows.Count > 0)
                {
                    if(clsDatabase_Connection.objDataSet.Tables[0].Rows[0][0].ToString()=="True")
                    {  return true;  }
                    else
                    { return false;  }
                }
                else { return false; }
            }
            catch { return false;}
        }

        public static Boolean Check_Clicked_Login_Status(int User_id)
        {
            try
            {
                clsDatabase_Connection.Start_DB_Connection();
                clsDatabase_Connection.objDataSet.Tables.Clear();
                clsDatabase_Connection.sqlConn = "select LoginStatus,Login_PC_Id from tblUser where USERID='" + User_id + "'";
                clsDatabase_Connection.objDataAdapter = new SqlDataAdapter(clsDatabase_Connection.sqlConn, clsDatabase_Connection.db_con);
                clsDatabase_Connection.objDataAdapter.Fill(clsDatabase_Connection.objDataSet, "Login");
                clsDatabase_Connection.Close_DB_Connection();
                if (clsDatabase_Connection.objDataSet.Tables[0].Rows.Count > 0)
                {
                    if (clsDatabase_Connection.objDataSet.Tables[0].Rows[0][0].ToString() == "False")  // no login found 
                    { return true; }
                    else
                    {
                        if (clsDatabase_Connection.objDataSet.Tables[0].Rows[0][1].ToString() == clsNetwork.SystemHDD_Id())  
                        {
                            return true; // same pc
                        }
                        else
                        {
                            return false; // another pc
                        }
                    }
                }
                else { return false; }
            }
            catch { return false; }
        }

        public static Boolean Check_Entered_PIN_code(int User_id,String PINCode)
        {
            try
            {
                clsDatabase_Connection.Start_DB_Connection();
                clsDatabase_Connection.objDataSet.Tables.Clear();
                clsDatabase_Connection.sqlConn = "select Password from tblUser where USERID='" + User_id + "'";
                clsDatabase_Connection.objDataAdapter = new SqlDataAdapter(clsDatabase_Connection.sqlConn, clsDatabase_Connection.db_con);
                clsDatabase_Connection.objDataAdapter.Fill(clsDatabase_Connection.objDataSet, "Login");
                clsDatabase_Connection.Close_DB_Connection();
                if (clsDatabase_Connection.objDataSet.Tables[0].Rows.Count > 0)
                { 
                    if (clsDatabase_Connection.objDataSet.Tables[0].Rows[0][0].ToString() == clsEncrypt.Encrypt_Data(PINCode))
                    { return true; }  // sucess
                    else
                    { return false; } // wrong pincode
                }
                else { return false; }  // could not find the user or password
            }
            catch { return false; } // error
        }

        static String PC_Id = "";
        public static Boolean Sucess_Full_Login(int User_Id)
        {
            try 
            {
                clsDatabase_Connection.Start_DB_Connection();
                clsDatabase_Connection.objDataSet.Tables.Clear();
                clsDatabase_Connection.sqlConn = "select B.StaffId,B.StafLastName,A.UserType from tblUser A inner join tblStaffDetails B on A.StaffId=B.StaffId where A.USERID='" + User_Id + "'";
                clsDatabase_Connection.objDataAdapter = new SqlDataAdapter(clsDatabase_Connection.sqlConn, clsDatabase_Connection.db_con);
                clsDatabase_Connection.objDataAdapter.Fill(clsDatabase_Connection.objDataSet, "Login");
                clsDatabase_Connection.Close_DB_Connection();
                if (clsDatabase_Connection.objDataSet.Tables[0].Rows.Count > 0)
                {
                    IMS_System.Properties.Settings.Default.current_user_id = User_Id;
                    IMS_System.Properties.Settings.Default.current_staff_id = Int32.Parse(clsDatabase_Connection.objDataSet.Tables[0].Rows[0][0].ToString());
                    IMS_System.Properties.Settings.Default.current_staff_name = clsDatabase_Connection.objDataSet.Tables[0].Rows[0][1].ToString();
                    IMS_System.Properties.Settings.Default.staffType = clsDatabase_Connection.objDataSet.Tables[0].Rows[0][2].ToString();

                    PC_Id = clsNetwork.SystemHDD_Id();
                    clsDatabase_Connection.Start_DB_Connection();
                    SqlCommand sqlCommands = new SqlCommand();
                    sqlCommands.Connection = clsDatabase_Connection.db_con;
                    sqlCommands.CommandText = "insert into tblLoginHistory values('" + User_Id + "',GETDATE(),'" + clsNetwork.LocalIPAddress() + "','" + PC_Id + "');";
                    sqlCommands.CommandText = sqlCommands.CommandText + "update tblUser set LoginStatus=1,LoginPC_IP='" + clsNetwork.LocalIPAddress() + "',Login_PC_Id='" + PC_Id + "' where UserId='" + User_Id + "';";
                    sqlCommands.ExecuteNonQuery();
                    clsDatabase_Connection.Close_DB_Connection();

                    return true;
                }
                else { return false; }
            }
            catch {return false; }
        }
    }
}
