using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IMS_System.Class.Database;
using IMS_System.Forms;

namespace IMS_System.Class.Codes
{
    class ClsStudents
    {
        public static void UpdateActivation(String value, string selectedIndex,frmMainPage mainScreen)
        {
            try
            {
                if (value.Equals("True"))
                {
                    if (clsDatabase_Connection.ExecuteQuery("update tblStudentDetails set StudentStatus='" + value +
                        "',CreatedBy='" + IMS_System.Properties.Settings.Default.current_staff_id +
                        "',CreatedDate=GETDATE() where StudentId='" + selectedIndex + "'") == true)
                    {
                        if (clsDatabase_Connection.ExecuteQuery("update tblStudentBatchDetails set current_status='" + value +
                        "',CreatedBy='" + IMS_System.Properties.Settings.Default.current_staff_id +
                        "',CreatedDate=GETDATE() where StudentId='" + selectedIndex + "' and studentbatchid=(select max(studentbatchid)" +
                        " from tblStudentBatchDetails where studentid='" + selectedIndex + "')") == false)
                        {
                            SystemLogFile.WriteSystemLog("Seudent Batch Details Deactivation Error", "Student Deactivation");
                        }
                        mainScreen.SucessMessageShow("Sucessfully Activated", "INFO");
                    }
                    else
                    { new frmMessageBox("error", "Activate", "You can not Activate this. Some errors occurred!", false, mainScreen).ShowDialog(); }
                }
                else
                {
                    clsDatabase_Connection.Get_Table("select * from tblStudentDetails A inner join tblStudentBatchDetails B on A.StudentId=B.studentid where A.StudentId='" + selectedIndex + "'");
                    if (clsDatabase_Connection.objDataSet.Tables[0].Rows.Count > 1) { }
                    new frmMessageBox("question", "Batches", "Selected Students is studying for more batches, would you like to Deactivate all?", true, mainScreen).ShowDialog();
                    if (IMS_System.Properties.Settings.Default.MessageBoxResults.Equals("Yes"))
                    {

                        if (clsDatabase_Connection.ExecuteQuery("update tblStudentDetails set StudentStatus='" + value +
                   "',CreatedBy='" + IMS_System.Properties.Settings.Default.current_staff_id +
                   "',CreatedDate=GETDATE() where StudentId='" + selectedIndex + "'") == true)
                        {
                            if (clsDatabase_Connection.ExecuteQuery("update tblStudentBatchDetails set current_status='" + value +
                        "',CreatedBy='" + IMS_System.Properties.Settings.Default.current_staff_id +
                        "',CreatedDate=GETDATE() where StudentId='" + selectedIndex + "'") == false)
                            {
                                SystemLogFile.WriteSystemLog("Seudent Batch Details Deactivation Error - FULL", "Student Deactivation");
                            }
                            mainScreen.SucessMessageShow("Sucessfully Deactivated", "INFO");
                        }
                        else
                        { new frmMessageBox("error", "Deactivate", "You can not Deactivate this. Some errors occurred!", false, mainScreen).ShowDialog(); }
                    }
                    else { /** no need of deactivation **/ }
                }
               
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
    }
}
