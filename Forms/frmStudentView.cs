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
using IMS_System.Class.Codes;

namespace IMS_System.Forms
{
    public partial class frmStudentView : Form
    {
        frmMainPage MainScreen;
        private int selectedStudentId;
        private List<int> batch_ids;
        private decimal totalFees=0;
  
        public frmStudentView(frmMainPage MainPage,int StudentId)
        {
            InitializeComponent();
            MainScreen = MainPage;
            selectedStudentId = StudentId;
            batch_ids=new List<int>();
            AddDataToDetailsPanel();
           
            AddRegisteredSubjects();
            //AddPayments();
        }

        private void AddDataToDetailsPanel()
        {
            try
            {
                dgvCart.Rows.Clear();
                clsDatabase_Connection.Get_Table("select A.StudentId,A.StudentGender,(select top 1 D.BarcodeValue from tblStudentBarcode D where D.StudentId='" + selectedStudentId + "' and D.BarcodeStatus='True'),A.StudentFirstName,A.StudentLastName,A.StudentAddress,A.StudentGender,A.StudentNicNo,A.StudentFeesMtd,A.StudentJoinedDate,A.StudentStatus from tblStudentDetails A where A.StudentId='" + selectedStudentId+"';"+
                    " select top 1 B.StudentPhone,B.StudentEmail,B.StudentPhoto from tblStudentAdditionalDetails B where B.StudentId='"+selectedStudentId+"' and B.Status='True';"+
                    "select B.BatchName,B.BatchId from tblStudentBatchDetails A inner join tblBatch B on A.batchid=B.BatchId where studentid='" + selectedStudentId+"' and A.current_status='True'");
                if (clsDatabase_Connection.objDataSet.Tables[0].Rows.Count > 0)
                {
                    if(clsDatabase_Connection.objDataSet.Tables[0].Rows[0][1].ToString().Equals("Male"))
                        { pictureBox1.BackgroundImage = imageList1.Images[1]; }
                    else { pictureBox1.BackgroundImage = imageList1.Images[2]; }

                    label6.Text = clsDatabase_Connection.objDataSet.Tables[0].Rows[0][3].ToString() + " " + clsDatabase_Connection.objDataSet.Tables[0].Rows[0][4].ToString();
                    label2.Text = clsDatabase_Connection.objDataSet.Tables[0].Rows[0][5].ToString();

                    dgvCart.Rows.Add("Barcode", clsDatabase_Connection.objDataSet.Tables[0].Rows[0][2].ToString());
                    dgvCart.Rows.Add("NIC No", clsDatabase_Connection.objDataSet.Tables[0].Rows[0][7].ToString());
                    dgvCart.Rows.Add("Fees Method", clsDatabase_Connection.objDataSet.Tables[0].Rows[0][8].ToString());
                    dgvCart.Rows.Add("Joined Date", clsDatabase_Connection.objDataSet.Tables[0].Rows[0][9].ToString());
                    if (clsDatabase_Connection.objDataSet.Tables[0].Rows[0][8].ToString().Equals("FREE"))
                    { dgvCart.Rows[2].DefaultCellStyle.ForeColor = Color.Red; }
                    else { dgvCart.Rows[2].DefaultCellStyle.ForeColor = Color.Black; }

                    if (clsDatabase_Connection.objDataSet.Tables[0].Rows[0][10].ToString().Equals("True"))
                    {
                        button4.Text = @"Deactivate";
                    }
                    else
                    {
                        button4.Text = @"Activate";
                    }
                    EnableDisableCheck(clsDatabase_Connection.objDataSet.Tables[0].Rows[0][10].ToString());
                }
               
                if (clsDatabase_Connection.objDataSet.Tables[1].Rows.Count > 0)
                {
                        dgvCart.Rows.Add("Phone No", clsDatabase_Connection.objDataSet.Tables[1].Rows[0][0].ToString()); 

                        dgvCart.Rows.Add("Email", clsDatabase_Connection.objDataSet.Tables[1].Rows[0][1].ToString()); 
                }
                if (clsDatabase_Connection.objDataSet.Tables[2].Rows.Count > 0)
                {
                    label4.Text = "";
                    batch_ids.Clear();
                    for (int i = 0; i < clsDatabase_Connection.objDataSet.Tables[2].Rows.Count; i++)
                    {
                        label4.Text += clsDatabase_Connection.objDataSet.Tables[2].Rows[i][0].ToString() + ", ";
                        batch_ids.Add(int.Parse(clsDatabase_Connection.objDataSet.Tables[2].Rows[i][1].ToString()));
                    }
                    label4.Text = @"Batch : "+ label4.Text.Substring(0, label4.Text.Length-2);
                }
                dgvCart.ClearSelection();
            }
            catch(Exception ex) { MessageBox.Show("1 "+ex.Message); }
        }

        private void AddRegisteredSubjects()
        {
            try
            {
                totalFees = 0;
                dataGridView2.Rows.Clear();
                clsDatabase_Connection.Get_Table("select A.StudentPayId,C.SubjectName+'('+D.BatchName+')',B.Amount from tblStudentFees A inner join tblSubjectAmount B on A.SubjectPaymentId=B.SubjectPaymentId inner join tblSubjects C on C.SubjectId=B.SubjectId inner join tblBatch D on B.BatchId=D.BatchId where A.StudentId='" + selectedStudentId+ "' and B.AmountStatus='True' and A.Status='True' ;");
               if (clsDatabase_Connection.objDataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < clsDatabase_Connection.objDataSet.Tables[0].Rows.Count; i++)
                    {
                        dataGridView2.Rows.Add(clsDatabase_Connection.objDataSet.Tables[0].Rows[i][0].ToString(),
                        clsDatabase_Connection.objDataSet.Tables[0].Rows[i][1].ToString(),
                        IMS_System.Properties.Settings.Default.Currency + clsDatabase_Connection.objDataSet.Tables[0].Rows[i][2].ToString());
                        totalFees += Decimal.Parse(clsDatabase_Connection.objDataSet.Tables[0].Rows[i][2].ToString());
                    }
                    dataGridView2.Rows[0].DefaultCellStyle.ForeColor = Color.Black;
                }
               else
                {
                    dataGridView2.Rows.Add("No Registered Subjects", "","");
                    dataGridView2.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
                    totalFees = 0;
                }
                label5.Text = IMS_System.Properties.Settings.Default.Currency + " "+ totalFees.ToString();
                dataGridView2.ClearSelection();
            }
            catch (Exception ex) { MessageBox.Show("2" + ex.Message); }
        }

        private void AddPayments()
        {
            //try
            //{
            //    dataGridView1.Rows.Clear();
            //    clsDatabase_Connection.Get_Table("select DATENAME(month, MAX(CreatedDate))+' '+CAST(MAX(YEAR(CreatedDate)) as varchar(30)),SUM(Amount) from tblStudentPayments where StudentId='" + selectedStudentId+"' group by MONTH(CreatedDate) order by MONTH(CreatedDate) ;");
            //    if (clsDatabase_Connection.objDataSet.Tables[0].Rows.Count > 0)
            //    {
            //        for (int i = 0; i < clsDatabase_Connection.objDataSet.Tables[0].Rows.Count; i++)
            //        {
            //            dataGridView1.Rows.Add(clsDatabase_Connection.objDataSet.Tables[0].Rows[i][0].ToString(),
            //            IMS_System.Properties.Settings.Default.Currency + clsDatabase_Connection.objDataSet.Tables[0].Rows[i][1].ToString());
            //        }
            //        dataGridView2.Rows[0].DefaultCellStyle.ForeColor = Color.Black;
            //    }
            //    else
            //    {
            //        dataGridView1.Rows.Add("No Payments", "");
            //        dataGridView1.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
            //    }
            //    dataGridView1.ClearSelection();
            //}
            //catch (Exception ex) { MessageBox.Show("3" + ex.Message); }
        }
        private void frmStudentView_Load(object sender, EventArgs e)
        {
  
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditRegisterdSubjects(selectedStudentId);
            AddRegisteredSubjects();
        }

        private void EditRegisterdSubjects(int id)
        {
            if (clsClose_Other_Forms.IMS_IsFormOpen("frmStudentSubjectRegister") == false)
            {
                frmStudentSubjectRegister EditRegisterSubject = new frmStudentSubjectRegister(MainScreen, id,label6.Text,label4.Text,batch_ids);
                EditRegisterSubject.ShowDialog();
                EditRegisterSubject.BringToFront();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text.Equals("Activate"))
            { UpdateActivation("True", selectedStudentId.ToString()); }
            else
            { UpdateActivation("False", selectedStudentId.ToString()); }
        }

        private void UpdateActivation(String Value, string selectedIndex)
        {
            try
            {
                ClsStudents.UpdateActivation(Value, selectedIndex, MainScreen);
                AddDataToDetailsPanel();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void EnableDisableCheck(string value)
        {
            try
            {
                if (value.Equals("True"))
                {
                    dgvCart.Enabled = true;
                    dataGridView2.Enabled = true;
                    button1.Enabled = true;
                    button3.Enabled = true;
                }
                else
                {
                    dgvCart.Enabled = false;
                    dataGridView2.Enabled = false;
                    button1.Enabled = false;
                    button3.Enabled = false;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void dgvCart_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCart.CurrentRow != null)
            {
                var value = dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[0].Value.ToString().ToUpper();

                if (clsClose_Other_Forms.IMS_IsFormOpen("frmStudentsUpdates") == false)
                {
                    frmStudentsUpdates studentUpdate;
                    if (value.Equals("BARCODE") || value.Equals("EMAIL") || value.Equals("PHONE NO") || value.Equals("NIC NO"))
                    {
                        studentUpdate = new frmStudentsUpdates(MainScreen, selectedStudentId,true,value,false);
                    }
                    else
                    {
                        studentUpdate = new frmStudentsUpdates(MainScreen, selectedStudentId,false, value, false);
                    }
                    studentUpdate.ShowDialog();
                    studentUpdate.BringToFront();

                    AddDataToDetailsPanel();
                    AddRegisteredSubjects();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (clsClose_Other_Forms.IMS_IsFormOpen("frmStudentsUpdates") == false)
            {
                frmStudentsUpdates studentUpdate = new frmStudentsUpdates(MainScreen, selectedStudentId, false, "", false);

                studentUpdate.ShowDialog();
                studentUpdate.BringToFront();

                AddDataToDetailsPanel();
                AddRegisteredSubjects();
 
            }
        }
    }
}
