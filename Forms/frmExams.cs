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
    public partial class frmExams : Form
    {
        frmMainPage MainScreen;
       // private String Query = "";
        private List<int> batch_id;
        private string DgvQuery;
        public frmExams(frmMainPage MainPage)
        {
            InitializeComponent();
            MainScreen = MainPage;
            batch_id = new List<int>();

            Add_Subjects_to_Combobox("select SubjectId, SubjectName from tblSubjects where SubjectStatus='True';");
            DgvQuery= " select A.ExamId,(select SubjectName from tblSubjects where SubjectId = A.SubjectId),A.ExamPart,B.BatchName,A.ExamDay,A.StartTime,A.Duration,A.ExamStatus,A.BatchId,A.SubjectId,A.Status  from tblExamDetails A inner join tblBatch B on A.BatchId=B.BatchId where B.BatchSatus ='True' order by A.Status desc,A.ExamId desc";
            Add_Details_to_Datagridview(DgvQuery);
        }

        private void frmBatches_Load(object sender, EventArgs e)
        {

        }

        private void label6_Paint(object sender, PaintEventArgs e)
        {
            clsLines.Draw_Horizontal_lines(e, Color.Black, label6);
        }


        private void Add_Subjects_to_Combobox(String Query)
        {
            try
            {
                batch_id.Clear();
                comboBox1.Items.Clear();
                batch_id.Add(0);
                comboBox1.Items.Add("All Subjects");
                clsDatabase_Connection.Get_Table(Query);
                if (clsDatabase_Connection.objDataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < clsDatabase_Connection.objDataSet.Tables[0].Rows.Count; i++)
                    {
                        batch_id.Add((int)clsDatabase_Connection.objDataSet.Tables[0].Rows[i][0]);
                        comboBox1.Items.Add(clsDatabase_Connection.objDataSet.Tables[0].Rows[i][1].ToString());
                    }
                    comboBox1.SelectedIndex = 0;
                }
                else
                { MessageBox.Show(@"please add Subjects"); }
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
                            clsExamDetails.returnExamParts( clsDatabase_Connection.objDataSet.Tables[0].Rows[i][2].ToString()),
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][3].ToString(),
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][4].ToString(),
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][5].ToString(),
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][6].ToString(),
                            clsExamDetails.returnExamStatus(clsDatabase_Connection.objDataSet.Tables[0].Rows[i][7].ToString()),
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][8].ToString(),
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][9].ToString(),
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][10].ToString()
                            );
                        if (clsDatabase_Connection.objDataSet.Tables[0].Rows[i][10].ToString().Equals("True"))
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
            DgvQuery = " select A.ExamId,(select SubjectName from tblSubjects where SubjectId = A.SubjectId),A.ExamPart,B.BatchName,A.ExamDay,A.StartTime,A.Duration,A.ExamStatus,A.BatchId,A.SubjectId,A.Status  from tblExamDetails A inner join tblBatch B on A.BatchId=B.BatchId where B.BatchSatus = 'True'";

            if (checkBox2.Checked == true)
            { DgvQuery = DgvQuery + " and A.Status='True' "; }
            if (comboBox1.SelectedIndex > 0)
            { DgvQuery = DgvQuery + " and A.SubjectId=" + batch_id[comboBox1.SelectedIndex]; }
            if (checkBox1.Checked==true)
            { DgvQuery = DgvQuery + " order by A.ExamDay asc,A.ExamId desc"; }
            else { DgvQuery = DgvQuery + " order by A.Status desc,A.ExamId desc"; }
        }
        private void button2_Click(object sender, EventArgs e)
        {
    
        }

        private void button4_Click(object sender, EventArgs e)
        {
       
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (clsClose_Other_Forms.IMS_IsFormOpen("frmAddExamTime") == false)
            {
                frmAddExamTime AddExamTime = new frmAddExamTime(MainScreen);
                AddExamTime.ShowDialog();
                AddExamTime.BringToFront();

                Add_Subjects_to_Combobox("select SubjectId, SubjectName from tblSubjects where SubjectStatus='True';");
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
                if (clsClose_Other_Forms.IMS_IsFormOpen("frmAddExamTime") == false)
                {
                   // MessageBox.Show(dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[4].Value.ToString());

                    frmAddExamTime AddExamTime = new frmAddExamTime(MainScreen);
                    AddExamTime.Edit_AddDatatoCombo(
                        dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[0].Value.ToString(),
                    dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[9].Value.ToString(),
                    dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[8].Value.ToString(),
                    dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[4].Value.ToString(),
                    dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[5].Value.ToString(),
                    dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[6].Value.ToString(),
                    clsExamDetails.returnExamPartIndex( dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[2].Value.ToString()),
                    clsExamDetails.returnExamStatusIndex(dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[7].Value.ToString()));

                    AddExamTime.ShowDialog();
                    AddExamTime.BringToFront();

                    Add_Subjects_to_Combobox("select SubjectId, SubjectName from tblSubjects where SubjectStatus='True';");
                    Add_Details_to_Datagridview(DgvQuery);
                }
            }
            catch (Exception ex)
            { MessageBox.Show("1"+ex.Message); }
        }



        private void dgvCart_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnEdit.Enabled == true) { editSelectedData(); }

        }

        private void dgvCart_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                button1.Enabled = true;
               // selectedIndex = dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[0].Value.ToString();
                if (dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[10].Value.Equals("True"))
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
                    new frmMessageBox("error", "Empty", "Please Select Exam Details!", false, MainScreen).ShowDialog();
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
                    if (clsDatabase_Connection.ExecuteQuery("update tblExamDetails set Status='" + Value +
                        "',CreatedBy='" + IMS_System.Properties.Settings.Default.current_staff_id +
                        "',CreatedDate=GETDATE() where ExamId='" + selectedIndex + "'") == true)
                    { MainScreen.SucessMessageShow("Sucessfully Activated", "INFO"); }
                    else
                    { new frmMessageBox("error", "Activate", "You can not Activate this. Some errors occurred!", false, MainScreen).ShowDialog(); }
                }
                else
                {
                    if (clsDatabase_Connection.ExecuteQuery("update tblExamDetails set Status='" + Value +
                        "',CreatedBy='" + IMS_System.Properties.Settings.Default.current_staff_id +
                        "',CreatedDate=GETDATE() where ExamId='" + selectedIndex + "'") == true)
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
