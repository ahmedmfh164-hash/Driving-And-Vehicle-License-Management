namespace Full_Real_Project_DrivingAndVehicleLicenseDepartment_DVLD_
{
    partial class frmAddEditPerson
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblPersonID = new System.Windows.Forms.Label();
            this.lblAddEditPerson = new System.Windows.Forms.Label();
            this.ucAddEditPerson1 = new Full_Real_Project_DrivingAndVehicleLicenseDepartment_DVLD_.UCAddEditPerson();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(39, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "PersonID: ";
            // 
            // lblPersonID
            // 
            this.lblPersonID.AutoSize = true;
            this.lblPersonID.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPersonID.Location = new System.Drawing.Point(193, 99);
            this.lblPersonID.Name = "lblPersonID";
            this.lblPersonID.Size = new System.Drawing.Size(42, 24);
            this.lblPersonID.TabIndex = 2;
            this.lblPersonID.Text = "N/A";
            // 
            // lblAddEditPerson
            // 
            this.lblAddEditPerson.AutoSize = true;
            this.lblAddEditPerson.Font = new System.Drawing.Font("Microsoft Tai Le", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddEditPerson.Location = new System.Drawing.Point(428, 29);
            this.lblAddEditPerson.Name = "lblAddEditPerson";
            this.lblAddEditPerson.Size = new System.Drawing.Size(270, 41);
            this.lblAddEditPerson.TabIndex = 3;
            this.lblAddEditPerson.Text = "Add New Person";
            // 
            // ucAddEditPerson1
            // 
            this.ucAddEditPerson1.BackColor = System.Drawing.Color.Teal;
            this.ucAddEditPerson1.Location = new System.Drawing.Point(30, 126);
            this.ucAddEditPerson1.Name = "ucAddEditPerson1";
            this.ucAddEditPerson1.Size = new System.Drawing.Size(1075, 408);
            this.ucAddEditPerson1.TabIndex = 4;
            this.ucAddEditPerson1.Tag = "Default";
            this.ucAddEditPerson1.OnSaveComplete += new System.Action<int>(this.ucAddEditPerson1_OnSaveComplete);
            this.ucAddEditPerson1.OnCloseClick += new System.Action(this.ucAddEditPerson1_OnCloseClick);
            this.ucAddEditPerson1.Load += new System.EventHandler(this.ucAddEditPerson1_Load);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Full_Real_Project_DrivingAndVehicleLicenseDepartment_DVLD_.Properties.Resources.Number_32;
            this.pictureBox1.Location = new System.Drawing.Point(141, 91);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(38, 34);
            this.pictureBox1.TabIndex = 35;
            this.pictureBox1.TabStop = false;
            // 
            // frmAddEditPerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(1131, 553);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ucAddEditPerson1);
            this.Controls.Add(this.lblAddEditPerson);
            this.Controls.Add(this.lblPersonID);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmAddEditPerson";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmAddEditPerson_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPersonID;
        private System.Windows.Forms.Label lblAddEditPerson;
        private UCAddEditPerson ucAddEditPerson1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}