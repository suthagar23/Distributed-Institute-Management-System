namespace IMS_System.Forms.startUp
{
    partial class frmLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.dgw_user_login = new System.Windows.Forms.DataGridView();
            this.login_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.visible = new System.Windows.Forms.DataGridViewImageColumn();
            this.img = new System.Windows.Forms.DataGridViewImageColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status_bool = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnFun1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.button37 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgw_user_login)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // dgw_user_login
            // 
            this.dgw_user_login.AllowUserToAddRows = false;
            this.dgw_user_login.AllowUserToDeleteRows = false;
            this.dgw_user_login.AllowUserToResizeColumns = false;
            this.dgw_user_login.AllowUserToResizeRows = false;
            this.dgw_user_login.BackgroundColor = System.Drawing.Color.White;
            this.dgw_user_login.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgw_user_login.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgw_user_login.ColumnHeadersHeight = 50;
            this.dgw_user_login.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgw_user_login.ColumnHeadersVisible = false;
            this.dgw_user_login.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.login_id,
            this.visible,
            this.img,
            this.name,
            this.status_bool});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgw_user_login.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgw_user_login.Location = new System.Drawing.Point(12, 80);
            this.dgw_user_login.MultiSelect = false;
            this.dgw_user_login.Name = "dgw_user_login";
            this.dgw_user_login.ReadOnly = true;
            this.dgw_user_login.RowHeadersVisible = false;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgw_user_login.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgw_user_login.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgw_user_login.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.dgw_user_login.RowTemplate.Height = 60;
            this.dgw_user_login.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgw_user_login.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgw_user_login.ShowCellErrors = false;
            this.dgw_user_login.ShowCellToolTips = false;
            this.dgw_user_login.ShowEditingIcon = false;
            this.dgw_user_login.ShowRowErrors = false;
            this.dgw_user_login.Size = new System.Drawing.Size(399, 571);
            this.dgw_user_login.TabIndex = 6;
            this.dgw_user_login.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_user_login_CellContentClick);
            this.dgw_user_login.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgw_user_login_CellMouseClick);
            this.dgw_user_login.Paint += new System.Windows.Forms.PaintEventHandler(this.dgw_user_login_Paint);
            // 
            // login_id
            // 
            this.login_id.HeaderText = "Login_id";
            this.login_id.Name = "login_id";
            this.login_id.ReadOnly = true;
            this.login_id.Visible = false;
            this.login_id.Width = 230;
            // 
            // visible
            // 
            this.visible.HeaderText = "visible";
            this.visible.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.visible.Name = "visible";
            this.visible.ReadOnly = true;
            this.visible.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.visible.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.visible.Width = 25;
            // 
            // img
            // 
            this.img.HeaderText = "img";
            this.img.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.img.Name = "img";
            this.img.ReadOnly = true;
            this.img.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.img.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.img.Width = 75;
            // 
            // name
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.name.DefaultCellStyle = dataGridViewCellStyle4;
            this.name.HeaderText = "name";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.name.Width = 300;
            // 
            // status_bool
            // 
            this.status_bool.HeaderText = "Status_bool";
            this.status_bool.Name = "status_bool";
            this.status_bool.ReadOnly = true;
            this.status_bool.Visible = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "green_glow.png");
            this.imageList1.Images.SetKeyName(1, "male2.png");
            this.imageList1.Images.SetKeyName(2, "female3.png");
            this.imageList1.Images.SetKeyName(3, "null_image.png");
            // 
            // btnFun1
            // 
            this.btnFun1.BackColor = System.Drawing.Color.White;
            this.btnFun1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFun1.BackgroundImage")));
            this.btnFun1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnFun1.FlatAppearance.BorderSize = 0;
            this.btnFun1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
            this.btnFun1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.btnFun1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFun1.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFun1.ForeColor = System.Drawing.Color.White;
            this.btnFun1.Location = new System.Drawing.Point(19, 40);
            this.btnFun1.Name = "btnFun1";
            this.btnFun1.Size = new System.Drawing.Size(58, 30);
            this.btnFun1.TabIndex = 8;
            this.btnFun1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFun1.UseVisualStyleBackColor = false;
            this.btnFun1.Click += new System.EventHandler(this.btnFun1_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(14, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(399, 43);
            this.label2.TabIndex = 7;
            this.label2.Text = "Please Select your Login";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Segoe UI Emoji", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(473, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(642, 88);
            this.label1.TabIndex = 28;
            this.label1.Text = "WELCOME";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Visible = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(230, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(413, 43);
            this.label3.TabIndex = 29;
            this.label3.Text = "Log in";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(231, 178);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 39);
            this.label4.TabIndex = 31;
            this.label4.Text = "Password";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Wingdings", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.txtPassword.Location = new System.Drawing.Point(345, 178);
            this.txtPassword.MaxLength = 17;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = 'l';
            this.txtPassword.ShortcutsEnabled = false;
            this.txtPassword.Size = new System.Drawing.Size(274, 39);
            this.txtPassword.TabIndex = 30;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // button37
            // 
            this.button37.BackColor = System.Drawing.Color.RoyalBlue;
            this.button37.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button37.FlatAppearance.BorderSize = 0;
            this.button37.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button37.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button37.ForeColor = System.Drawing.Color.White;
            this.button37.Location = new System.Drawing.Point(492, 261);
            this.button37.Name = "button37";
            this.button37.Size = new System.Drawing.Size(127, 59);
            this.button37.TabIndex = 32;
            this.button37.Text = "Log in";
            this.button37.UseVisualStyleBackColor = false;
            this.button37.Click += new System.EventHandler(this.button37_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.RoyalBlue;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(345, 261);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 59);
            this.button1.TabIndex = 33;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(15, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(204, 194);
            this.pictureBox1.TabIndex = 34;
            this.pictureBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(231, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(392, 30);
            this.label5.TabIndex = 35;
            this.label5.Text = "Waiting";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(231, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(412, 30);
            this.label6.TabIndex = 36;
            this.label6.Text = ":-)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.txtPassword);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.button37);
            this.panel1.Location = new System.Drawing.Point(469, 169);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(646, 361);
            this.panel1.TabIndex = 37;
            this.panel1.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox2.Location = new System.Drawing.Point(912, 570);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(200, 87);
            this.pictureBox2.TabIndex = 38;
            this.pictureBox2.TabStop = false;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(766, 600);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(165, 39);
            this.label7.TabIndex = 39;
            this.label7.Text = "Developed By";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.White;
            this.pictureBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.BackgroundImage")));
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox3.Location = new System.Drawing.Point(484, 156);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(512, 245);
            this.pictureBox3.TabIndex = 40;
            this.pictureBox3.TabStop = false;
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1138, 669);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgw_user_login);
            this.Controls.Add(this.btnFun1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmLogin";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgw_user_login)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView dgw_user_login;
        private System.Windows.Forms.DataGridViewTextBoxColumn login_id;
        private System.Windows.Forms.DataGridViewImageColumn visible;
        private System.Windows.Forms.DataGridViewImageColumn img;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn status_bool;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnFun1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button button37;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}