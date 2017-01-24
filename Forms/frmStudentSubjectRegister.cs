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
using IMS_System.Class;
using System;

namespace IMS_System.Forms
{
    public partial class frmStudentSubjectRegister : Form
    {
        frmMainPage MainScreen;
        private int selectedStudentId;
        private float TotalFees;
        private List<string> alreadyRegisterdIds;
        private List<int> batch_ids;
        public frmStudentSubjectRegister(frmMainPage MainPage, int StudentId,string name,String Batch,List<int> Batchid)
        {
            InitializeComponent();
            MainScreen = MainPage;
            selectedStudentId = StudentId;
            //batch_ids = new List<int>();
            alreadyRegisterdIds = new List<string>();
            label4.Text = @"Name : " + name;
            batch_ids = Batchid;
            label1.Text = Batch;
            AddRegisteredSubjects();
            Calculate_Total(true);
        }

        private void frmStudentSubjectRegister_Load(object sender, EventArgs e)
        {
            
        }

        private void AddRegisteredSubjects()
        {
            try
            {
                alreadyRegisterdIds.Clear();
                dataGridView2.Rows.Clear();
                clsDatabase_Connection.Get_Table("select A.SubjectPaymentId,case when (select count(StudentPayId) from tblStudentFees where SubjectPaymentId=A.SubjectPaymentId and StudentId='"+ selectedStudentId + "' and Status='True')>=1 then 'True' else 'False' end as 'Status' , B.SubjectName+' ('+C.BatchName+')',A.Amount,A.AllowToAllBatch,C.BatchId from tblSubjectAmount A inner join tblSubjects B on A.SubjectId=B.SubjectId inner join tblBatch C on C.BatchId=A.BatchId;");
                if (clsDatabase_Connection.objDataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < clsDatabase_Connection.objDataSet.Tables[0].Rows.Count; i++)
                    {
                        if(clsDatabase_Connection.objDataSet.Tables[0].Rows[i][1].ToString().Equals("True"))
                        {
                            dataGridView2.Rows.Add(clsDatabase_Connection.objDataSet.Tables[0].Rows[i][0].ToString(),
                                true,
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][2].ToString(),
                            IMS_System.Properties.Settings.Default.Currency + clsDatabase_Connection.objDataSet.Tables[0].Rows[i][3].ToString(),
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][4].ToString(),
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][3].ToString(),
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][5].ToString());
                        }
                        else
                        {
                            dataGridView2.Rows.Add(clsDatabase_Connection.objDataSet.Tables[0].Rows[i][0].ToString(),
                                   false,
                               clsDatabase_Connection.objDataSet.Tables[0].Rows[i][2].ToString(),
                               IMS_System.Properties.Settings.Default.Currency + clsDatabase_Connection.objDataSet.Tables[0].Rows[i][3].ToString(),
                               clsDatabase_Connection.objDataSet.Tables[0].Rows[i][4].ToString(),
                               clsDatabase_Connection.objDataSet.Tables[0].Rows[i][3].ToString(),
                               clsDatabase_Connection.objDataSet.Tables[0].Rows[i][5].ToString());
                        }
                        alreadyRegisterdIds.Add(clsDatabase_Connection.objDataSet.Tables[0].Rows[i][1].ToString());
                        // MessageBox.Show(clsDatabase_Connection.objDataSet.Tables[0].Rows[i][4].ToString());

                        dgv_RowColorChangeForOtherBatchesSubjects(i);
                    }
                     dataGridView2.Rows[0].DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    dataGridView2.Rows.Add("No Registered Subjects", "", "");
                    dataGridView2.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
                }
                dataGridView2.ClearSelection();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void dgv_RowColorChangeForOtherBatchesSubjects(int i)
        {
            if (clsDatabase_Connection.objDataSet.Tables[0].Rows[i][4].ToString().Equals("True"))
            { dataGridView2.Rows[i].DefaultCellStyle.ForeColor = Color.Black; }
            else
            {
                if (checkSubject_for_SelectedBatch(i) == true)
                { dataGridView2.Rows[i].DefaultCellStyle.ForeColor = Color.Black; }
                else
                { dataGridView2.Rows[i].DefaultCellStyle.ForeColor = Color.Gray; }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Save_Details();
            Dispose();
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[4].Value.ToString().Equals("True"))
            {
                SelectionChange();
            }
            else
            {
                if (checkSubject_for_SelectedBatch(dataGridView2.CurrentRow.Index) == true)
                {
                    SelectionChange();
                }
                else
                {
                    Calculate_Total(false);
                    label2.Text = "Subject is not available for " + label1.Text + ".";
                    label2.Font = new Font(label2.Font.Name, 12);
                    label2.ForeColor = Color.Red;
                }
            }
        }

        private bool checkSubject_for_SelectedBatch(int currentRow)
        {
            try
            {
                for (int i = 0; i < batch_ids.Count(); i++)
                {
                    if (dataGridView2.Rows[currentRow].Cells[6].Value.ToString().Equals(batch_ids[i].ToString()))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void SelectionChange()
        {
            try
            {
                if (dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[1].Value.ToString().Equals("True"))
                {
                    dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[1].Value = false;
                }
                else
                {
                    dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[1].Value = true;
                }
                Calculate_Total(true);
            }
            catch (Exception)
            { }
        }

       private void Calculate_Total(bool visible)
        {
            try
            {
                TotalFees = 0;
                for (int i=0; i<dataGridView2.Rows.Count;i++)
                {
                    if (dataGridView2.Rows[i].Cells[1].Value.ToString().Equals("True"))
                    {
                        TotalFees += float.Parse(dataGridView2.Rows[i].Cells[5].Value.ToString());
                    }
                }
                if (visible == true)
                {
                    label2.Text = "Total Fees : " + IMS_System.Properties.Settings.Default.Currency + TotalFees + ".";
                    label2.Font = new Font(label2.Font.Name, 13);
                    label2.ForeColor = Color.Black;
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void Save_Details()
        {
            try
            {
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    if (!dataGridView2.Rows[i].Cells[1].Value.ToString().Equals(alreadyRegisterdIds[i]))
                    {
                        if(dataGridView2.Rows[i].Cells[1].Value.ToString().Equals("True") && alreadyRegisterdIds[i].Equals("False"))
                        { ActivateSubjects(dataGridView2.Rows[i].Cells[0].Value.ToString()); }
                        else if (dataGridView2.Rows[i].Cells[1].Value.ToString().Equals("False") && alreadyRegisterdIds[i].Equals("True"))
                        { DeactivateSubjects(dataGridView2.Rows[i].Cells[0].Value.ToString()); }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void DeactivateSubjects(string id)
        {
            try
            {
                clsDatabase_Connection.ExecuteQuery("update tblStudentFees set Status='False'"+
                        ",CreatedBy='" + IMS_System.Properties.Settings.Default.current_staff_id +
                        "',CreatedDate=GETDATE() where SubjectPaymentId='" + id + "' and StudentId='"+selectedStudentId+"'");
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void ActivateSubjects(string id)
        {
            try
            {
                clsDatabase_Connection.ExecuteQuery("Insert into tblStudentFees values('" + id + "','" + selectedStudentId + "','"+
                        IMS_System.Properties.Settings.Default.current_staff_id + "',GETDATE(),'True')");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
