namespace Full_Real_Project_DrivingAndVehicleLicenseDepartment_DVLD_
{
    partial class UCPersonInfoWithFilterBy
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbFilter = new Guna.UI2.WinForms.Guna2GroupBox();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.btnAddNewPerson = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnSearchPerson = new Guna.UI2.WinForms.Guna2GradientButton();
            this.txtFilterBy = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ucPersonInfo1 = new Full_Real_Project_DrivingAndVehicleLicenseDepartment_DVLD_.UCPersonInfo();
            this.ucPersonInfo2 = new Full_Real_Project_DrivingAndVehicleLicenseDepartment_DVLD_.UCPersonInfo();
            this.gbFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbFilter
            // 
            this.gbFilter.BorderColor = System.Drawing.Color.Teal;
            this.gbFilter.Controls.Add(this.cbFilterBy);
            this.gbFilter.Controls.Add(this.btnAddNewPerson);
            this.gbFilter.Controls.Add(this.btnSearchPerson);
            this.gbFilter.Controls.Add(this.txtFilterBy);
            this.gbFilter.Controls.Add(this.label2);
            this.gbFilter.CustomBorderColor = System.Drawing.Color.DarkSlateGray;
            this.gbFilter.FillColor = System.Drawing.Color.Teal;
            this.gbFilter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gbFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.gbFilter.Location = new System.Drawing.Point(2, 3);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(961, 100);
            this.gbFilter.TabIndex = 3;
            this.gbFilter.Text = "Filter";
            this.gbFilter.Click += new System.EventHandler(this.gbFilter_Click);
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.BackColor = System.Drawing.Color.LightSeaGreen;
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbFilterBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.ImeMode = System.Windows.Forms.ImeMode.On;
            this.cbFilterBy.Items.AddRange(new object[] {
            "None",
            "Person ID",
            "National No",
            "Phone",
            "Email"});
            this.cbFilterBy.Location = new System.Drawing.Point(127, 57);
            this.cbFilterBy.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(223, 28);
            this.cbFilterBy.TabIndex = 23;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // btnAddNewPerson
            // 
            this.btnAddNewPerson.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAddNewPerson.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAddNewPerson.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAddNewPerson.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAddNewPerson.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAddNewPerson.FillColor = System.Drawing.Color.PaleTurquoise;
            this.btnAddNewPerson.FillColor2 = System.Drawing.Color.Silver;
            this.btnAddNewPerson.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAddNewPerson.ForeColor = System.Drawing.Color.White;
            this.btnAddNewPerson.Image = global::Full_Real_Project_DrivingAndVehicleLicenseDepartment_DVLD_.Properties.Resources.Add_Person_72;
            this.btnAddNewPerson.ImageSize = new System.Drawing.Size(40, 40);
            this.btnAddNewPerson.Location = new System.Drawing.Point(682, 47);
            this.btnAddNewPerson.Name = "btnAddNewPerson";
            this.btnAddNewPerson.Size = new System.Drawing.Size(51, 45);
            this.btnAddNewPerson.TabIndex = 22;
            this.btnAddNewPerson.Click += new System.EventHandler(this.btnAddNewPerson_Click);
            // 
            // btnSearchPerson
            // 
            this.btnSearchPerson.BackgroundImage = global::Full_Real_Project_DrivingAndVehicleLicenseDepartment_DVLD_.Properties.Resources.SearchPerson;
            this.btnSearchPerson.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearchPerson.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSearchPerson.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSearchPerson.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSearchPerson.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSearchPerson.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSearchPerson.FillColor = System.Drawing.Color.PaleTurquoise;
            this.btnSearchPerson.FillColor2 = System.Drawing.Color.PeachPuff;
            this.btnSearchPerson.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSearchPerson.ForeColor = System.Drawing.Color.White;
            this.btnSearchPerson.Image = global::Full_Real_Project_DrivingAndVehicleLicenseDepartment_DVLD_.Properties.Resources.SearchPerson;
            this.btnSearchPerson.ImageSize = new System.Drawing.Size(38, 38);
            this.btnSearchPerson.Location = new System.Drawing.Point(611, 47);
            this.btnSearchPerson.Name = "btnSearchPerson";
            this.btnSearchPerson.Size = new System.Drawing.Size(51, 45);
            this.btnSearchPerson.TabIndex = 21;
            this.btnSearchPerson.Click += new System.EventHandler(this.btnSearchPerson_Click);
            // 
            // txtFilterBy
            // 
            this.txtFilterBy.BackColor = System.Drawing.Color.Silver;
            this.txtFilterBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilterBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilterBy.Location = new System.Drawing.Point(365, 58);
            this.txtFilterBy.Name = "txtFilterBy";
            this.txtFilterBy.Size = new System.Drawing.Size(222, 26);
            this.txtFilterBy.TabIndex = 20;
            this.txtFilterBy.Visible = false;
            this.txtFilterBy.TextChanged += new System.EventHandler(this.txtFilterBy_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Teal;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(27, 58);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 24);
            this.label2.TabIndex = 18;
            this.label2.Text = "Filter By ";
            // 
            // ucPersonInfo1
            // 
            this.ucPersonInfo1.Location = new System.Drawing.Point(2, 109);
            this.ucPersonInfo1.Name = "ucPersonInfo1";
            this.ucPersonInfo1.Size = new System.Drawing.Size(961, 288);
            this.ucPersonInfo1.TabIndex = 2;
            // 
            // ucPersonInfo2
            // 
            this.ucPersonInfo2.BackColor = System.Drawing.Color.Teal;
            this.ucPersonInfo2.Location = new System.Drawing.Point(3, 109);
            this.ucPersonInfo2.Name = "ucPersonInfo2";
            this.ucPersonInfo2.Size = new System.Drawing.Size(955, 300);
            this.ucPersonInfo2.TabIndex = 24;
            // 
            // UCPersonInfoWithFilterBy
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Teal;
            this.Controls.Add(this.ucPersonInfo2);
            this.Controls.Add(this.gbFilter);
            this.Name = "UCPersonInfoWithFilterBy";
            this.Size = new System.Drawing.Size(965, 414);
            this.Load += new System.EventHandler(this.UCFillterBy_Load);
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2GroupBox gbFilter;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private Guna.UI2.WinForms.Guna2GradientButton btnAddNewPerson;
        private Guna.UI2.WinForms.Guna2GradientButton btnSearchPerson;
        private System.Windows.Forms.TextBox txtFilterBy;
        private System.Windows.Forms.Label label2;
        private UCPersonInfo ucPersonInfo1;
        private UCPersonInfo ucPersonInfo2;
    }
}
