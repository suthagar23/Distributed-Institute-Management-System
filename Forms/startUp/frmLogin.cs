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
using System.Data.SqlClient;
using IMS_System.Class.GUI;
using IMS_System.Class.Forms;

namespace IMS_System.Forms.startUp
{
    
    public partial class frmLogin : Form
    {
        frmMainPage MainScreen;
        //clsDatabase_Connection clsDatabase_Connection;
        //clsLogin clsLogin;
        private int Current_User_Id;
        public frmLogin(frmMainPage MainPage)
        {
            InitializeComponent();
           // clsDatabase_Connection = new clsDatabase_Connection();
           // clsLogin = new clsLogin();
            MainScreen = MainPage;

            this.Width = MainScreen.Width-20;
            pictureBox2.Left = this.Width - pictureBox2.Width - 10;
            label7.Left = pictureBox2.Left - label7.Width;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            IMS_System.Properties.Settings.Default.Active_Session = "LOG_IN";
            Add_user_logins_to_panel();
        }

        private void Add_user_logins_to_panel()
        {
            try
            {
                label1.Text = "WELCOME";
                panel1.Visible = false;
                dgw_user_login.Rows.Clear();
                clsDatabase_Connection.Start_DB_Connection();
                clsDatabase_Connection.objDataSet.Tables.Clear();
                clsDatabase_Connection.sqlConn = "select A.UserId,A.LoginStatus,B.StaffPhoto,B.StafFirstName,B.StaffGender from tblUser A inner join tblStaffDetails B on A.StaffId=B.StaffId where A.UserStatus=1";
                clsDatabase_Connection.objDataAdapter = new SqlDataAdapter(clsDatabase_Connection.sqlConn, clsDatabase_Connection.db_con);
                clsDatabase_Connection.objDataAdapter.Fill(clsDatabase_Connection.objDataSet, "Item");
                clsDatabase_Connection.Close_DB_Connection();
                if (clsDatabase_Connection.objDataSet.Tables[0].Rows.Count > 0)
                {
                    dgw_user_login.Visible = true;

                    label2.Text = "Please Select your Login";
                    label2.ForeColor = Color.Black;
                    for (int i = 0; i < clsDatabase_Connection.objDataSet.Tables[0].Rows.Count; i++)
                    {
                        if (clsDatabase_Connection.objDataSet.Tables[0].Rows[i][4].ToString() == "Male")
                        {
                            if (clsDatabase_Connection.objDataSet.Tables[0].Rows[i][1].ToString() == "True")
                            {
                                dgw_user_login.Rows.Add(clsDatabase_Connection.objDataSet.Tables[0].Rows[i][0].ToString(),
                                    imageList1.Images[0],
                                    imageList1.Images[1],
                                    clsDatabase_Connection.objDataSet.Tables[0].Rows[i][3].ToString(),
                                    clsDatabase_Connection.objDataSet.Tables[0].Rows[i][1].ToString());
                            }
                            else
                            {
                                dgw_user_login.Rows.Add(clsDatabase_Connection.objDataSet.Tables[0].Rows[i][0].ToString(),
                                    imageList1.Images[3],
                                    imageList1.Images[1],
                                    clsDatabase_Connection.objDataSet.Tables[0].Rows[i][3].ToString(),
                                    clsDatabase_Connection.objDataSet.Tables[0].Rows[i][1].ToString());
                            }
                        }
                        else
                        {
                            if (clsDatabase_Connection.objDataSet.Tables[0].Rows[i][1].ToString() == "True")
                            {
                                dgw_user_login.Rows.Add(clsDatabase_Connection.objDataSet.Tables[0].Rows[i][0].ToString(),
                                    imageList1.Images[0],
                                    imageList1.Images[2],
                                    clsDatabase_Connection.objDataSet.Tables[0].Rows[i][3].ToString(),
                                    clsDatabase_Connection.objDataSet.Tables[0].Rows[i][1].ToString());
                            }
                            else
                            {
                                dgw_user_login.Rows.Add(clsDatabase_Connection.objDataSet.Tables[0].Rows[i][0].ToString(),
                                   imageList1.Images[3],
                                    imageList1.Images[2],
                                    clsDatabase_Connection.objDataSet.Tables[0].Rows[i][3].ToString(),
                                    clsDatabase_Connection.objDataSet.Tables[0].Rows[i][1].ToString());
                            }
                        }
                        dgw_user_login.ClearSelection();
                        //  dataGridView1.Rows.Add(0, clsDatabase_Connection.objDataSet.Tables[0].Rows[i][0].ToString(), clsDatabase_Connection.objDataSet.Tables[0].Rows[i][1].ToString());
                    }
                }
                else
                {
                    dgw_user_login.Visible = false;
                    label2.Text = "There are no Users";
                    label2.ForeColor = Color.Red;
                }
            }
            catch
            { }
        }

        private void dgw_user_login_Paint(object sender, PaintEventArgs e)
        {
            clsLines.Draw_Vertical_full_lines(e, Color.Black, dgw_user_login.Width,dgw_user_login.Height);
        }

        private void dgw_user_login_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                panel1.Visible = true;
                label1.Visible = true;
                User_selection();
            }
            catch(Exception ex)
            { }
        }

        private void User_selection()
        {
            try
            {
                if (dgw_user_login.Rows.Count > 0 & dgw_user_login.CurrentRow.Index >= 0)
                {
                    Login_User_Clicked( 
                        dgw_user_login.Rows[dgw_user_login.CurrentRow.Index].Cells[3].Value.ToString(),
                        (Image)dgw_user_login.Rows[dgw_user_login.CurrentRow.Index].Cells[2].Value,
                        dgw_user_login.Rows[dgw_user_login.CurrentRow.Index].Cells[4].Value.ToString(),
                        dgw_user_login.Rows[dgw_user_login.CurrentRow.Index].Cells[0].Value.ToString());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Login_User_Clicked( String Firstname, Image Photo, String Active, String User_Id)
        {
            try
            { 
                label1.Text= "Hi " + Firstname + "!";
                label3.Text ="Waiting for Password " + Firstname ;
                if (Active == "True")
                {
                    label5.Text = "Active Now";
                }
                else
                {
                    label5.Text = "Offline";
                }
                Get_Last_Login_Date(User_Id);
                pictureBox1.BackgroundImage = (Photo);
                Current_User_Id = Int32.Parse(User_Id);
                txtPassword.Text = "";
                this.ActiveControl = txtPassword;
                txtPassword.Focus();
            }
            catch (Exception ex) {  }
        }

        private void Get_Last_Login_Date(String User_Id)
        {
            try
            {
                clsDatabase_Connection.Start_DB_Connection();
                clsDatabase_Connection.objDataSet.Tables.Clear();
                clsDatabase_Connection.sqlConn = "select top 1 left(Convert(dateTime,logindate,106),20),datediff(HOUR,logindate,GETDATE()),datediff(MINUTE,logindate,GETDATE()) from tblLoginHistory where UserId='" + User_Id + "' order by LoginId DESC";
                clsDatabase_Connection.objDataAdapter = new SqlDataAdapter(clsDatabase_Connection.sqlConn, clsDatabase_Connection.db_con);
                clsDatabase_Connection.objDataAdapter.Fill(clsDatabase_Connection.objDataSet, "Item");
                clsDatabase_Connection.Close_DB_Connection();
                if (clsDatabase_Connection.objDataSet.Tables[0].Rows.Count > 0)
                {
                    if ((int)clsDatabase_Connection.objDataSet.Tables[0].Rows[0][1] < 24) // below 1 day
                    {
                        if ((int)clsDatabase_Connection.objDataSet.Tables[0].Rows[0][1] == 0) // below 1 hour
                        {
                            label5.Text = label5.Text + @" for last " + clsDatabase_Connection.objDataSet.Tables[0].Rows[0][2].ToString() + @" Minutes";
                        }
                        else
                        {
                            label5.Text = label5.Text + @" for last " + clsDatabase_Connection.objDataSet.Tables[0].Rows[0][1].ToString() + @" Hours";
                        }
                    }
                    else
                    {
                        label5.Text = label5.Text + @" for last " + ((int)clsDatabase_Connection.objDataSet.Tables[0].Rows[0][1] / 24) + @" Days";
                    }
                    label6.Text = "Last Loged in : " + clsDatabase_Connection.objDataSet.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    label6.Text = "";
                }
            }
            catch(Exception ex) {  }
        }

        private void loginClick()
        {
            try
            {
                    if (clsLogin.Check_Clicked_User_Status(Current_User_Id) == true)
                    {
                        if (clsLogin.Check_Clicked_Login_Status(Current_User_Id) == true)
                        {
                            if (txtPassword.Text.Trim() != "")
                            {
                                if (clsLogin.Check_Entered_PIN_code(Current_User_Id, txtPassword.Text.Trim()) == true)
                                {
                                    txtPassword.Text = "";
                                    txtPassword.Enabled = false;
                                    label1.Text = "Please Wait...";
                                    if (clsLogin.Sucess_Full_Login(Current_User_Id) == true)
                                    {
                                    // Sucess
                                        txtPassword.Enabled = true;
                                        MainScreen.Login_Sucessfull();
                                        this.Dispose();
                                    }
                                else
                                    {
                                        txtPassword.Enabled = true;
                                        label1.Text = "Please enter your PIN Code";
                                        new frmMessageBox("error", "Login failed", "Could not get the Details. Unexpected User found!", false, MainScreen).ShowDialog();
                                    }
                                }
                                else
                                { new frmMessageBox("error", "Invaild Login", "Wrong Login PIN Code", false, MainScreen).ShowDialog(); txtPassword.Text = ""; }
                            }
                            else
                            { new frmMessageBox("error", "Login", "Please  enter Login PIN Code", false, MainScreen).ShowDialog(); }
                        }
                        else
                        { new frmMessageBox("error", "Duplicate Login", "You can not Login Now Here!, You have already Loged in to this system.", false, MainScreen).ShowDialog(); }
                    }
                    else
                    { new frmMessageBox("error", "Invalid Login", "You can not Login Now!, Your Login was deactivated.", false, MainScreen).ShowDialog(); }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button37_Click(object sender, EventArgs e)
        {
            loginClick();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter) { loginClick(); }
            }
            catch(Exception ex) { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnFun1_Click(object sender, EventArgs e)
        {
            try
            {
                Add_user_logins_to_panel();
            }
            catch
            { }
        }

        private void dgw_user_login_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                panel1.Visible = true;
                label1.Visible = true;
                User_selection();
            }
            catch (Exception ex)
            { }
        }
    }
}
