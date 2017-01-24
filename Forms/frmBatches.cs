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
    public partial class frmBatches : Form
    {
        frmMainPage MainScreen;
       // private String Query = "";
        private List<int> batch_id;
        private string DgvQuery;
        public frmBatches(frmMainPage MainPage)
        {
            InitializeComponent();
            MainScreen = MainPage;
            batch_id = new List<int>();

            Add_Batches_to_Combobox("select BatchId, BatchName from tblBatch where BatchSatus='True'");
            DgvQuery= " select B.ClassId,(select SubjectName from tblSubjects where SubjectId = B.SubjectId),A.BatchName,(SELECT DATENAME(DW, CAST(B.ClassDay AS INT))),B.StartTime,B.EndTime,(select COUNT(C.studentid) from tblStudentBatchDetails C where C.batchid = A.BatchId and C.current_status = 'True' ),B.AutoGenerate,B.AutoAttendance,B.BatchId,B.SubjectId,B.TimeStatus from tblBatch A inner join tblClassTimes B on A.BatchId = B.BatchId where A.BatchSatus = 'True' order by TimeStatus desc,ClassId desc";
            Add_Details_to_Datagridview(DgvQuery);
        }

        private void frmBatches_Load(object sender, EventArgs e)
        {

        }

        private void label6_Paint(object sender, PaintEventArgs e)
        {
            clsLines.Draw_Horizontal_lines(e, Color.Black, label6);
        }


        private void Add_Batches_to_Combobox(String Query)
        {
            try
            {
                batch_id.Clear();
                comboBox1.Items.Clear();
                batch_id.Add(0);
                comboBox1.Items.Add("All Batches");
                clsDatabase_Connection.Get_Table(Query);
                if (clsDatabase_Connection.objDataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < clsDatabase_Connection.objDataSet.Tables[0].Rows.Count; i++)
                    {
                        batch_id.Add((int)clsDatabase_Connection.objDataSet.Tables[0].Rows[i][0]);
                        comboBox1.Items.Add("Batch " +clsDatabase_Connection.objDataSet.Tables[0].Rows[i][1].ToString());
                    }
                    comboBox1.SelectedIndex = 0;
                }
                else
                { MessageBox.Show("please add batches"); }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        public void Add_Details_to_Datagridview(String Query)
        {
            try
            {
                dgvCart.Rows.Clear();
                clsDatabase_Connection.Get_Table(Query);
                if (clsDatabase_Connection.objDataSet.Tables[0].Rows.Count > 0)
                {
                    dgvCart.Visible = true;
                    for (int i = 0; i < clsDatabase_Connection.objDataSet.Tables[0].Rows.Count; i++)
                    {
                        dgvCart.Rows.Add(clsDatabase_Connection.objDataSet.Tables[0].Rows[i][0].ToString(),
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][1].ToString(),
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][2].ToString(),
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][3].ToString(),
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][4].ToString(),
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][5].ToString(),
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][6].ToString(),
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][7].ToString(),
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][8].ToString(),
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][9].ToString(),
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][10].ToString(),
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][11].ToString()
                            );
                        if (clsDatabase_Connection.objDataSet.Tables[0].Rows[i][11].ToString().Equals("True"))
                        { dgvCart.Rows[i].DefaultCellStyle.ForeColor = Color.Black; }
                        else { dgvCart.Rows[i].DefaultCellStyle.ForeColor = Color.Red; }

                        dgvCart.ClearSelection();
                      
                    }
                }
                else
                {
                    //dgvCart.Visible = false;
                    // label2.Text = "There are no Details";
                    //  label2.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_DockChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                setDgvQuery();
                Add_Details_to_Datagridview(DgvQuery);
                //if (comboBox1.SelectedIndex==0)
                //{
                //    Add_Details_to_Datagridview(DgvQuery);
                //}
                //else
                //{
                //    DgvQuery= DgvQuery+ " and A.BatchId=" + batch_id[comboBox1.SelectedIndex];
                //    Add_Details_to_Datagridview(DgvQuery); 
                //}
              }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void setDgvQuery()
        {
            DgvQuery = " select B.ClassId,(select SubjectName from tblSubjects where SubjectId = B.SubjectId),A.BatchName,(SELECT DATENAME(DW, CAST(B.ClassDay AS INT))),B.StartTime,B.EndTime,(select COUNT(C.studentid) from tblStudentBatchDetails C where C.batchid = A.BatchId and C.current_status = 'True' ),B.AutoGenerate,B.AutoAttendance,B.BatchId,B.SubjectId,B.TimeStatus from tblBatch A inner join tblClassTimes B on A.BatchId = B.BatchId where A.BatchSatus = 'True' ";
            if (checkBox2.Checked == true)
            { DgvQuery = DgvQuery + " and TimeStatus='True' "; }
            if (comboBox1.SelectedIndex > 0)
            { DgvQuery = DgvQuery + " and A.BatchId=" + batch_id[comboBox1.SelectedIndex]; }
            if (checkBox1.Checked==true)
            { DgvQuery = DgvQuery + " order by B.ClassDay asc,ClassId desc"; }
            else { DgvQuery = DgvQuery + " order by B.TimeStatus desc,ClassId desc"; }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (clsClose_Other_Forms.IMS_IsFormOpen("frmBatchDetails") == false)
            {
                frmBatchDetails BatchDetails = new frmBatchDetails(MainScreen);
               // BatchDetails.MdiParent = MainScreen;
                BatchDetails.ShowDialog();
                BatchDetails.BringToFront();

                Add_Batches_to_Combobox("select BatchId, BatchName from tblBatch where BatchSatus='True'");
                Add_Details_to_Datagridview(DgvQuery);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (clsClose_Other_Forms.IMS_IsFormOpen("frmSubjects") == false)
            {
                frmSubjects Subjects = new frmSubjects(MainScreen);
                Subjects.ShowDialog();
                Subjects.BringToFront();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (clsClose_Other_Forms.IMS_IsFormOpen("frmAddBatchTime") == false)
            {
                frmAddBatchTime AddBatchTime = new frmAddBatchTime(MainScreen);
                AddBatchTime.ShowDialog();
                AddBatchTime.BringToFront();

                Add_Batches_to_Combobox("select BatchId, BatchName from tblBatch where BatchSatus='True'");
                Add_Details_to_Datagridview(DgvQuery);

            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            editSelectedData();
        }

        private void editSelectedData()
        {
            try
            {
            if(clsClose_Other_Forms.IMS_IsFormOpen("frmAddBatchTime") == false)
            {
                frmAddBatchTime AddBatchTime = new frmAddBatchTime(MainScreen);
                AddBatchTime.Edit_AddDatatoCombo(dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[0].Value.ToString(),
                    dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[10].Value.ToString(),
                    dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[9].Value.ToString(),
                    clsDayConvert.getWeeDay_Number(dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[3].Value.ToString()),
                    dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[4].Value.ToString(),
                    dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[5].Value.ToString(),
                    dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[7].Value.ToString(),
                    dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[8].Value.ToString());

                AddBatchTime.ShowDialog();
                AddBatchTime.BringToFront();

                Add_Batches_to_Combobox("select BatchId, BatchName from tblBatch where BatchSatus='True'");
                Add_Details_to_Datagridview(DgvQuery);
            }
            }
            catch { }
        }

       

        private void dgvCart_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(btnEdit.Enabled==true) { editSelectedData(); }
            
        }

        private void dgvCart_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                button1.Enabled = true;
               // selectedIndex = dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[0].Value.ToString();
                if (dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[11].Value.Equals("True"))
                {
                    button1.Text = "Deactivate";
                    btnEdit.Enabled = true;
                }
                else
                {
                    button1.Text = "Activate";
                    btnEdit.Enabled = false;
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCart.SelectedRows.Count >= 1)
                {
                   // selectedIndex = dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[0].Value.ToString();
                    if (button1.Text.Equals("Activate"))
                    { UpdateActivation("True", dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[0].Value.ToString()); }
                    else
                    { UpdateActivation("False", dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[0].Value.ToString()); }
                }
                else
                {
                    new frmMessageBox("error", "Empty", "Please Select Time Details!", false, MainScreen).ShowDialog();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void UpdateActivation(String Value,string selectedIndex)
        {
            try
            {
                if (Value.Equals("True"))
                {
                    if (clsDatabase_Connection.ExecuteQuery("update tblClassTimes set TimeStatus='" + Value +
                        "',CreatedBy='" + IMS_System.Properties.Settings.Default.current_staff_id +
                        "',CreatedDate=GETDATE() where ClassId='" + selectedIndex + "'") == true)
                    { MainScreen.SucessMessageShow("Sucessfully Activated", "INFO"); }
                    else
                    { new frmMessageBox("error", "Activate", "You can not Activate this. Some errors occurred!", false, MainScreen).ShowDialog(); }
                }
                else
                {
                    if (clsDatabase_Connection.ExecuteQuery("update tblClassTimes set TimeStatus='" + Value +
                        "',CreatedBy='" + IMS_System.Properties.Settings.Default.current_staff_id +
                        "',CreatedDate=GETDATE() where ClassId='" + selectedIndex + "'") == true)
                    { MainScreen.SucessMessageShow("Sucessfully Deactivated", "INFO"); }
                    else
                    { new frmMessageBox("error", "Deactivate", "You can not Deactivate this. Some errors occurred!", false, MainScreen).ShowDialog(); }

                }
                Add_Details_to_Datagridview(DgvQuery);
                button1.Enabled = false;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            setDgvQuery();
            Add_Details_to_Datagridview(DgvQuery);
        }

        private void checkBox2_CheckStateChanged(object sender, EventArgs e)
        {
            setDgvQuery();
            Add_Details_to_Datagridview(DgvQuery);
        }
    }
}
