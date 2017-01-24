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
using System.Text.RegularExpressions;

namespace IMS_System.Forms
{
    public partial class frmStudentsUpdates : Form
    {
        frmMainPage MainScreen;
        private int _selectedStudentId;
        private float _totalFees=0;
        private readonly List<string> _alreadyRegisterdSubjects;
        private readonly List<string> _alreadyRegisterdBatchess;
        private bool EditEnterSave = false;
        private string editSession = "";
        private string[] currentBarcode = new string[10];
        private bool new_student_mode = false;
            // 0-barcode 1-contact no 2-email
        public frmStudentsUpdates(frmMainPage MainPage, int StudentId,bool EditEnterSave, string editSession,bool new_student_mode)
        {
            InitializeComponent();
            MainScreen = MainPage;
            _selectedStudentId = StudentId;
            _alreadyRegisterdSubjects = new List<string>();
            _alreadyRegisterdBatchess = new List<string>();
            
            AddBatches();
            AddRegisteredSubjects();
            
           
            this.editSession = editSession;
            this.EditEnterSave = EditEnterSave;
            this.new_student_mode = new_student_mode;
            if (new_student_mode == true)
            {
                button2.Text = @"Save";
            }
            else
            {
                button2.Text = @"Update";
                AddDetails();
            }
        }

        private void ClearSelections()
        {
            txtAddress.Clear();
            txtBarcode.Clear();
            txtContactNo.Clear();
            txtEmail.Clear();
            txtFName.Clear();
            txtLName.Clear();
            txtNIC.Clear();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;
        }
        private void EditSessionSelection(string editSession)
        {
            try
            {
                if (editSession.ToUpper().Equals("BARCODE"))
                {
                    txtBarcode.SelectAll();
                    txtBarcode.Focus();
                }
                else if (editSession.ToUpper().Equals("NIC NO"))
                {
                    txtNIC.SelectAll();
                    txtNIC.Focus();
                }
                else if (editSession.ToUpper().Equals("PHONE NO"))
                {
                    txtContactNo.SelectAll();
                    txtContactNo.Focus();
                }
                else if (editSession.ToUpper().Equals("EMAIL"))
                {
                    txtEmail.SelectAll();
                    txtEmail.Focus();
                }
                else
                {
                   // txtFName.Focus();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        private void AddDetails()
        {
            try
            {
                ClearSelections();
                clsDatabase_Connection.Get_Table("select A.StudentFirstName,A.StudentLastName,A.StudentAddress,A.StudentNicNo,A.StudentGender,A.StudentFeesMtd,(Select top 1 C.BarcodeValue from tblStudentBarcode C where C.StudentId='" + _selectedStudentId + "' and C.BarcodeStatus='True') from tblStudentDetails A where A.StudentId='" + _selectedStudentId+"';" +
                    "select top 1 B.StudentPhone,B.StudentEmail  from tblStudentAdditionalDetails B where B.StudentId='" + _selectedStudentId + "' and B.Status='True';");
                if (clsDatabase_Connection.objDataSet.Tables[0].Rows.Count > 0)
                {
                    txtFName.Text = clsDatabase_Connection.objDataSet.Tables[0].Rows[0][0].ToString();
                    txtLName.Text = clsDatabase_Connection.objDataSet.Tables[0].Rows[0][1].ToString();
                    txtAddress.Text = clsDatabase_Connection.objDataSet.Tables[0].Rows[0][2].ToString();
                    txtBarcode.Text = clsDatabase_Connection.objDataSet.Tables[0].Rows[0][6].ToString();
                    txtNIC.Text= clsDatabase_Connection.objDataSet.Tables[0].Rows[0][3].ToString();
                    comboBox1.SelectedIndex =comboBox1.FindStringExact( clsDatabase_Connection.objDataSet.Tables[0].Rows[0][4].ToString());
                    comboBox2.SelectedIndex = comboBox2.FindStringExact(clsDatabase_Connection.objDataSet.Tables[0].Rows[0][5].ToString());
                    currentBarcode[0] = txtBarcode.Text;
                }
                if (clsDatabase_Connection.objDataSet.Tables[1].Rows.Count > 0)
                {
                    txtContactNo.Text = clsDatabase_Connection.objDataSet.Tables[1].Rows[0][0].ToString();
                    txtEmail.Text = clsDatabase_Connection.objDataSet.Tables[1].Rows[0][1].ToString();
                    currentBarcode[1] = txtContactNo.Text;
                    currentBarcode[2] = txtEmail.Text;
                }
            }
            catch (Exception ex) { MessageBox.Show("1" + ex.Message); }
        }

        private void AddBatches()
        {
            try
            {
                _alreadyRegisterdBatchess.Clear();
                dataGridView2.Rows.Clear();
                clsDatabase_Connection.Get_Table("select A.BatchId,case when (select count(StudentBatchId) from tblStudentBatchDetails where BatchId=A.BatchId and StudentId='"+_selectedStudentId+"' and current_status='True')>=1 then 'True' else 'False' end as 'Status',A.BatchName from tblBatch A where A.BatchSatus='True';");
                if (clsDatabase_Connection.objDataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < clsDatabase_Connection.objDataSet.Tables[0].Rows.Count; i++)
                    {
                        if (clsDatabase_Connection.objDataSet.Tables[0].Rows[i][1].ToString().Equals("True"))
                        {
                            dataGridView2.Rows.Add(clsDatabase_Connection.objDataSet.Tables[0].Rows[i][0].ToString(),
                                true,
                                clsDatabase_Connection.objDataSet.Tables[0].Rows[i][2].ToString());
                        }
                        else
                        {
                            dataGridView2.Rows.Add(clsDatabase_Connection.objDataSet.Tables[0].Rows[i][0].ToString(),
                                false,
                                clsDatabase_Connection.objDataSet.Tables[0].Rows[i][2].ToString());
                        }
                        _alreadyRegisterdBatchess.Add(clsDatabase_Connection.objDataSet.Tables[0].Rows[i][1].ToString());
                    }
                    dataGridView2.Rows[0].DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    dataGridView2.Rows.Add("No Registered Batches", "", "");
                    dataGridView2.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
                }
                dataGridView2.ClearSelection();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void AddRegisteredSubjects()
        {
            try
            {
                dataGridView1.Rows.Clear();
                clsDatabase_Connection.Get_Table("select A.SubjectPaymentId,case when (select count(StudentPayId) from tblStudentFees where SubjectPaymentId=A.SubjectPaymentId and StudentId='" + _selectedStudentId + "' and Status='True')>=1 then 'True' else 'False' end as 'Status' , B.SubjectName+' ('+C.BatchName+')',A.Amount,A.AllowToAllBatch,C.BatchId from tblSubjectAmount A inner join tblSubjects B on A.SubjectId=B.SubjectId inner join tblBatch C on C.BatchId=A.BatchId;");
                if (clsDatabase_Connection.objDataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < clsDatabase_Connection.objDataSet.Tables[0].Rows.Count; i++)
                    {
                        if (clsDatabase_Connection.objDataSet.Tables[0].Rows[i][1].ToString().Equals("True"))
                        {
                            dataGridView1.Rows.Add(clsDatabase_Connection.objDataSet.Tables[0].Rows[i][0].ToString(),
                                true,
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][2].ToString(),
                            IMS_System.Properties.Settings.Default.Currency + clsDatabase_Connection.objDataSet.Tables[0].Rows[i][3].ToString(),
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][4].ToString(),
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][3].ToString(),
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][5].ToString());
                        }
                        else
                        {
                            dataGridView1.Rows.Add(clsDatabase_Connection.objDataSet.Tables[0].Rows[i][0].ToString(),
                                   false,
                               clsDatabase_Connection.objDataSet.Tables[0].Rows[i][2].ToString(),
                               IMS_System.Properties.Settings.Default.Currency + clsDatabase_Connection.objDataSet.Tables[0].Rows[i][3].ToString(),
                               clsDatabase_Connection.objDataSet.Tables[0].Rows[i][4].ToString(),
                               clsDatabase_Connection.objDataSet.Tables[0].Rows[i][3].ToString(),
                               clsDatabase_Connection.objDataSet.Tables[0].Rows[i][5].ToString());
                        }
                        _alreadyRegisterdSubjects.Add(clsDatabase_Connection.objDataSet.Tables[0].Rows[i][1].ToString());
                        // MessageBox.Show(clsDatabase_Connection.objDataSet.Tables[0].Rows[i][4].ToString());
                    }
                    dataGridView1.Rows[0].DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    dataGridView1.Rows.Add("No Registered Subjects", "", "");
                    dataGridView1.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
                }
                dgv_RowColorChangeForOtherBatchesSubjects();
                dataGridView1.ClearSelection();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void dgv_RowColorChangeForOtherBatchesSubjects()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[4].Value.ToString().Equals("True"))
                {
                     dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    if (checkSubject_for_SelectedBatch(i) == true)
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Gray;
                        dataGridView1.Rows[i].Cells[1].Value = false;
                    }
                }
            }
  
        }

        private bool checkSubject_for_SelectedBatch(int currentRow)
        {
            try
            {
                for (var i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    if (!dataGridView2.Rows[i].Cells[1].Value.ToString().Equals("True")) continue;
                    if (dataGridView2.Rows[i].Cells[0].Value.Equals(dataGridView1.Rows[currentRow].Cells[6].Value.ToString()))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        public static bool IsValidNic(string email)
        {
            const string pattern = @"\d{9}[V|v|x|X]";
            var check = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            var valid = false;
            valid = !string.IsNullOrEmpty(email) && check.IsMatch(email);
            return valid;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtFName.Text.Trim().Equals(""))
                {
                    if (!txtLName.Text.Trim().Equals(""))
                    {
                        if (comboBox1.SelectedIndex >= 0)
                        {
                            if (comboBox2.SelectedIndex >= 0)
                            {
                                var isEmail = Regex.IsMatch(txtEmail.Text.Trim(), @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                                if (txtEmail.Text.Trim().Equals("") || isEmail == true)
                                {
                                    if (txtContactNo.Text.Trim().Equals("") || txtContactNo.Text.Trim().Length >= 9)
                                    {
                                        SecondStageTesting();
                                    }
                                    else
                                    {
                                        new frmMessageBox("error", "Invaild Contact No", "Invaild Contact No, Please check Contact No", false, MainScreen).ShowDialog();
                                        txtContactNo.SelectAll(); txtContactNo.Focus();
                                    }

                                }
                                else
                                {
                                    new frmMessageBox("error", "Invaild Mail ID", "Invaild Mail ID, Please check mail address", false, MainScreen).ShowDialog();
                                    txtEmail.SelectAll(); txtEmail.Focus();
                                }
                            }
                            else
                            {
                                new frmMessageBox("error", "Invalid", "Please enter Pay Type.", false, MainScreen).ShowDialog();
                                txtLName.Focus();
                            }
                        }
                        else
                        {
                            new frmMessageBox("error", "Invalid", "Please enter Gender.", false, MainScreen).ShowDialog();
                            txtLName.Focus();
                        }
                    }
                    else
                    {
                        new frmMessageBox("error", "Invalid", "Please enter Last Name.", false, MainScreen).ShowDialog();
                        txtLName.Focus();
                    }
                }
                else
                {
                    new frmMessageBox("error", "Invalid", "Please enter First Name.", false, MainScreen).ShowDialog();
                    txtFName.Focus();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void SecondStageTesting()
        {
            if (!txtNIC.Text.Trim().Equals(""))
            {
                if (IsValidNic(txtNIC.Text.Trim()) && checkNicDuplicate(txtNIC.Text.Trim()) == true)
                {
                    if (!txtBarcode.Text.Trim().Equals(""))
                    {
                        if (checkBarcodeDuplicate(txtBarcode.Text.Trim()) == true)
                        {
                            if (CheckBatchSelections() == true)
                            {
                                if (new_student_mode == true)
                                {
                                    InsertNewStudentDetails();
                                }
                                else
                                {
                                    UpdateStudentDetails();
                                }
                            }
                            else
                            {
                                new frmMessageBox("error", "Batches", "Please select batch for the student", false, MainScreen).ShowDialog();
                                txtBarcode.SelectAll();
                            }
                        }
                        else
                        {
                            new frmMessageBox("error", "Duplicate", "Please enter valid Barcode No. It's already exists or Invalid.", false, MainScreen).ShowDialog();
                            txtBarcode.SelectAll();
                        }
                    }
                    else
                    {
                        new frmMessageBox("error", "Empty", "Please enter Barcode No.", false, MainScreen).ShowDialog();
                        txtBarcode.SelectAll();
                    }
                }
                else
                {
                    new frmMessageBox("error", "Duplicate", "Please enter valid NIC No. It's already exists or Invalid.", false, MainScreen).ShowDialog();
                    txtNIC.SelectAll();
                }
            }
            else
            {
                new frmMessageBox("error", "Empty", "Please enter NIC No.", false, MainScreen).ShowDialog();
                txtNIC.SelectAll();
            }
        }

        private bool CheckBatchSelections()
        { 
            try
            {
                for (var i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    if (dataGridView2.Rows[i].Cells[1].Value.ToString().Equals("True"))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return false; }
        }

        private void UpdateStudentDetails()
        {
            try
            {
                if (!txtBarcode.Text.Equals(currentBarcode[0]))
                {
                    UpdateBarcode(txtBarcode.Text.Trim());
                }

                updateOtherDetails();
                UpdateAdditionalDetails();

                SaveSelectedBatches();
                Save_Details();

                MainScreen.SucessMessageShow("Sucessfully Changed", "INFO");
                Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("UP "+ex.Message);
            }
        }

        private void InsertNewStudentDetails()
        {
            try
            {
                updateOtherDetails();
                clsDatabase_Connection.Get_Table("select A.StudentId from tblStudentDetails A where A.StudentNicNo='"+txtNIC.Text.ToString().Trim()+"' ;");
                if (clsDatabase_Connection.objDataSet.Tables[0].Rows.Count > 0)
                {
                    _selectedStudentId = int.Parse(clsDatabase_Connection.objDataSet.Tables[0].Rows[0][0].ToString());
                    UpdateBarcode(txtBarcode.Text.Trim());

                    UpdateAdditionalDetails();

                    SaveSelectedBatches();
                    Save_Details();

                    MainScreen.SucessMessageShow("Sucessfully Inserted", "INFO");
                    Dispose();
                }
                else
                {
                    new frmMessageBox("error", "Error", "Error in saving.", false, MainScreen).ShowDialog();
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("IN "+ex.Message);
            }
        }

        private void frmStudentsUpdates_Load(object sender, EventArgs e)
        {

        }

        private void frmStudentsUpdates_Load_1(object sender, EventArgs e)
        {
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                checkBatchesSelected();
                dgv_RowColorChangeForOtherBatchesSubjects();
            }
            catch (Exception)
            {
                throw;
            }

        }

        private bool checkBatchesSelected()
        {
            try
            {
                if (dataGridView2.Rows.Cast<DataGridViewRow>().Any(row => row.Cells[1].Value.ToString().Equals("True")))
                {
                    return true;
                }
                for (var i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Cells[1].Value = false;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value.ToString().Equals("True"))
                {
                    SelectionChange();
                }
                else
                {
                    if (checkSubject_for_SelectedBatch(dataGridView1.CurrentRow.Index) == true)
                    {
                        SelectionChange();
                    }
                    else
                    {
                        //Calculate_Total(false);
                        label10.Text = @"Subject is not available for selected batches";
                        label10.Font = new Font(label2.Font.Name, 9);
                        label10.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }


        }

        private void SelectionChange()
        {
            try
            {
                if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString().Equals("True"))
                {
                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value = false;
                }
                else
                {
                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value = true;
                }
                label10.Text = "Select Subjects for the Student";
                label10.Font = new Font(label2.Font.Name, 11);
                label10.ForeColor = Color.Black;
            }
            catch (Exception)
            { }
        }

        private void dataGridView2_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
          
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void dataGridView2_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void dataGridView2_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
        
        }

        private void frmStudentsUpdates_Shown(object sender, EventArgs e)
        {
            EditSessionSelection(editSession);
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (!txtBarcode.Text.Trim().Equals(""))
                    {
                        if (!txtBarcode.Text.Equals(currentBarcode[0]))
                        {
                            UpdateBarcode(txtBarcode.Text.Trim());
                            MainScreen.SucessMessageShow("Sucessfully Changed", "INFO");
                            Dispose();
                        }
                        else
                        {
                            Dispose();
                        }
                    }
                    else
                    {
                        Dispose();
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private bool checkBarcodeDuplicate(string barcode)
        {
            try
            {
                if (
                    clsDatabase_Connection.Get_Table("select StudentId from tblStudentBarcode where BarcodeValue='" +
                                                     barcode + "' and (studentId!='" + _selectedStudentId +
                                                     "' or (studentId='" + _selectedStudentId +
                                                     "' and BarcodeStatus='False'));").Tables[0].Rows.Count == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

        private void UpdateBarcode(string barcode)
        {
            try
            {
                if (checkBarcodeDuplicate(barcode) == true)
                {
                    if (new_student_mode == true)
                    {
                        clsDatabase_Connection.ExecuteQuery(
                        "insert into tblStudentBarcode values ('" + _selectedStudentId + "','" + barcode + "','True','" +
                        IMS_System.Properties.Settings.Default.current_staff_id + "',GETDATE())");  
                    }
                    else
                    {
                        clsDatabase_Connection.ExecuteQuery(
                        "update tblStudentBarcode set BarcodeStatus='False' where StudentId='" + _selectedStudentId + "';" +
                        "insert into tblStudentBarcode values ('" + _selectedStudentId + "','" + barcode + "','True','" +
                        IMS_System.Properties.Settings.Default.current_staff_id + "',GETDATE())");
                    }
                }
                else
                {
                    new frmMessageBox("error", "Duplicate", "Please enter valid Barcode. It's already exists.", false, MainScreen).ShowDialog();
                    txtBarcode.SelectAll();
                }
            }
            catch (Exception)
            { 
            }
         
        }

        private void txtNIC_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (!txtNIC.Text.Trim().Equals(""))
                    {
                        updateNIC(txtNIC.Text.Trim().ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool checkNicDuplicate(string nicNo)
        {
            try
            {
                if (
                    clsDatabase_Connection.Get_Table("select StudentId from tblStudentDetails where StudentNicNo='" +
                                                     nicNo + "' and (StudentId!='" + _selectedStudentId +
                                                     "' or (StudentId='" + _selectedStudentId +
                                                     "' and StudentStatus='False'));").Tables[0].Rows.Count == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void updateNIC(string nicNo)
        {
            try
            {
                if (IsValidNic(nicNo) == true && checkNicDuplicate(nicNo)==true)
                {
                    clsDatabase_Connection.ExecuteQuery(
                        "update tblStudentDetails set StudentNicNo='" + nicNo + "'," +
                        "CreatedBy='" + IMS_System.Properties.Settings.Default.current_staff_id + "'," +
                        "CreatedDate=GETDATE() " +
                        " where StudentId='" + _selectedStudentId + "';");

                    MainScreen.SucessMessageShow("Sucessfully Changed", "INFO");
                    Dispose();
                }
                else
                {
                    new frmMessageBox("error", "Duplicate", "Please enter valid NIC No. It's already exists or Invalid.", false, MainScreen).ShowDialog();
                    txtNIC.SelectAll();
                }
            }
            catch (Exception)
            { 
            }
        }

        private void updateOtherDetails()
        {
            try
            {
                if (new_student_mode == true)
                {
                    clsDatabase_Connection.ExecuteQuery(
                            "Insert into tblStudentDetails values ("+
                            "'" + txtFName.Text.Trim() + "'," +
                            "'" + txtLName.Text.Trim() + "'," +
                            "'" + txtAddress.Text.Trim() + "'," +
                            "'" + comboBox1.SelectedItem + "'," +
                            "'" + txtNIC.Text.Trim() + "', " +
                            "'" + comboBox2.SelectedItem + "'," +
                            "GETDATE()," +
                            "'" + IMS_System.Properties.Settings.Default.current_staff_id + "'," +
                            "GETDATE()," + 
                            "'True');");
                }
                else
                {
                    clsDatabase_Connection.ExecuteQuery(
                            "update tblStudentDetails set StudentNicNo='" + txtNIC.Text.Trim() + "'," +
                            "StudentFirstName='" + txtFName.Text.Trim() + "'," +
                            "StudentLastName='" + txtLName.Text.Trim() + "'," +
                            "StudentAddress='" + txtAddress.Text.Trim() + "'," +
                            "StudentGender='" + comboBox1.SelectedItem + "'," +
                            "StudentFeesMtd='" + comboBox2.SelectedItem + "'," +
                            "CreatedBy='" + IMS_System.Properties.Settings.Default.current_staff_id + "'," +
                            "CreatedDate=GETDATE() " +
                            " where StudentId='" + _selectedStudentId + "';");
                }

            
                    
            }
            catch (Exception ex)
            {
                MessageBox.Show("UP_OTHER "+ex.Message);
            }
        }

        private void UpdateAdditionalDetails()
        {
            try
            {
                if (!txtContactNo.Text.Trim().Equals("") || !txtEmail.Text.Trim().Equals(("")))
                {
                    if (!txtContactNo.Text.Trim().Equals(currentBarcode[1]) || !txtEmail.Text.Trim().Equals(currentBarcode[2]))
                    {
                        if (new_student_mode == true)
                        {
                         clsDatabase_Connection.ExecuteQuery(
                        "insert into tblStudentAdditionalDetails values('"+
                            _selectedStudentId+"','"+
                            txtContactNo.Text.Trim()+"','"+
                            txtEmail.Text.Trim()+"','','"+IMS_System.Properties.Settings.Default.current_staff_id + "',GETDATE(),'True');");
   
                        }
                        else
                        {
                            clsDatabase_Connection.ExecuteQuery(
                                "update tblStudentAdditionalDetails set Status='False'" +
                                " where StudentId='" + _selectedStudentId + "';" +
                                "insert into tblStudentAdditionalDetails values('" +
                                _selectedStudentId + "','" +
                                txtContactNo.Text.Trim() + "','" +
                                txtEmail.Text.Trim() + "','','" +
                                IMS_System.Properties.Settings.Default.current_staff_id + "',GETDATE(),'True');");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Save_Details()
        {
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (!dataGridView1.Rows[i].Cells[1].Value.ToString().Equals(_alreadyRegisterdSubjects[i]))
                    {
                        if (dataGridView1.Rows[i].Cells[1].Value.ToString().Equals("True") && _alreadyRegisterdSubjects[i].Equals("False"))
                        { ActivateSubjects(dataGridView1.Rows[i].Cells[0].Value.ToString()); }
                        else if (dataGridView1.Rows[i].Cells[1].Value.ToString().Equals("False") && _alreadyRegisterdSubjects[i].Equals("True"))
                        { DeactivateSubjects(dataGridView1.Rows[i].Cells[0].Value.ToString()); }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void DeactivateSubjects(string id)
        {
            try
            {
                clsDatabase_Connection.ExecuteQuery("update tblStudentFees set Status='False'" +
                        ",CreatedBy='" + IMS_System.Properties.Settings.Default.current_staff_id +
                        "',CreatedDate=GETDATE() where SubjectPaymentId='" + id + "' and StudentId='" + _selectedStudentId + "'");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void ActivateSubjects(string id)
        {
            try
            {
                clsDatabase_Connection.ExecuteQuery("Insert into tblStudentFees values('" + id + "','" + _selectedStudentId + "','" +
                        IMS_System.Properties.Settings.Default.current_staff_id + "',GETDATE(),'True')");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void SaveSelectedBatches()
        {
            try
            {
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    if (!dataGridView2.Rows[i].Cells[1].Value.ToString().Equals(_alreadyRegisterdBatchess[i]))
                    {
                        if (dataGridView2.Rows[i].Cells[1].Value.ToString().Equals("True") && _alreadyRegisterdBatchess[i].Equals("False"))
                        { ActivateBatches(dataGridView2.Rows[i].Cells[0].Value.ToString()); }
                        else if (dataGridView2.Rows[i].Cells[1].Value.ToString().Equals("False") && _alreadyRegisterdBatchess[i].Equals("True"))
                        { DeactivateBatches(dataGridView2.Rows[i].Cells[0].Value.ToString()); }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void DeactivateBatches(string id)
        {
            try
            {
                clsDatabase_Connection.ExecuteQuery("update tblStudentBatchDetails set current_status='False'" +
                        ",CreatedBy='" + IMS_System.Properties.Settings.Default.current_staff_id +
                        "',CreatedDate=GETDATE() where BatchId='" + id + "' and StudentId='" + _selectedStudentId + "'");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void ActivateBatches(string id)
        {
            try
            {
                clsDatabase_Connection.ExecuteQuery("Insert into tblStudentBatchDetails values('" + _selectedStudentId + "','" + id + "','True','" +
                        IMS_System.Properties.Settings.Default.current_staff_id + "',GETDATE())");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar);
        }

        private void txtContactNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar);
        }

        private void txtContactNo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
