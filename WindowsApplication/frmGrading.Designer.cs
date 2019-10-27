namespace WindowsApplication
{
    partial class frmGrading
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
            this.gbxStudent = new System.Windows.Forms.GroupBox();
            this.gbxGrade = new System.Windows.Forms.GroupBox();
            this.lnkReturn = new System.Windows.Forms.LinkLabel();
            this.lnkUpdate = new System.Windows.Forms.LinkLabel();
            this.lblExisting = new System.Windows.Forms.Label();
            this.gbxGrade.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxStudent
            // 
            this.gbxStudent.Location = new System.Drawing.Point(36, 32);
            this.gbxStudent.Margin = new System.Windows.Forms.Padding(4);
            this.gbxStudent.Name = "gbxStudent";
            this.gbxStudent.Padding = new System.Windows.Forms.Padding(4);
            this.gbxStudent.Size = new System.Drawing.Size(748, 123);
            this.gbxStudent.TabIndex = 0;
            this.gbxStudent.TabStop = false;
            this.gbxStudent.Text = "Student Data";
            // 
            // gbxGrade
            // 
            this.gbxGrade.Controls.Add(this.lnkReturn);
            this.gbxGrade.Controls.Add(this.lnkUpdate);
            this.gbxGrade.Controls.Add(this.lblExisting);
            this.gbxGrade.Location = new System.Drawing.Point(104, 215);
            this.gbxGrade.Margin = new System.Windows.Forms.Padding(4);
            this.gbxGrade.Name = "gbxGrade";
            this.gbxGrade.Padding = new System.Windows.Forms.Padding(4);
            this.gbxGrade.Size = new System.Drawing.Size(612, 279);
            this.gbxGrade.TabIndex = 1;
            this.gbxGrade.TabStop = false;
            this.gbxGrade.Text = "Grade Information";
            // 
            // lnkReturn
            // 
            this.lnkReturn.AutoSize = true;
            this.lnkReturn.Location = new System.Drawing.Point(292, 239);
            this.lnkReturn.Name = "lnkReturn";
            this.lnkReturn.Size = new System.Drawing.Size(154, 17);
            this.lnkReturn.TabIndex = 12;
            this.lnkReturn.TabStop = true;
            this.lnkReturn.Text = "Return to Student Data";
            this.lnkReturn.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkReturn_LinkClicked);
            // 
            // lnkUpdate
            // 
            this.lnkUpdate.AutoSize = true;
            this.lnkUpdate.Location = new System.Drawing.Point(151, 239);
            this.lnkUpdate.Name = "lnkUpdate";
            this.lnkUpdate.Size = new System.Drawing.Size(98, 17);
            this.lnkUpdate.TabIndex = 2;
            this.lnkUpdate.TabStop = true;
            this.lnkUpdate.Text = "Update Grade";
            // 
            // lblExisting
            // 
            this.lblExisting.AutoSize = true;
            this.lblExisting.Location = new System.Drawing.Point(355, 172);
            this.lblExisting.Name = "lblExisting";
            this.lblExisting.Size = new System.Drawing.Size(235, 17);
            this.lblExisting.TabIndex = 10;
            this.lblExisting.Text = "Existing grades cannot be overriden";
            this.lblExisting.Visible = false;
            // 
            // frmGrading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 558);
            this.Controls.Add(this.gbxGrade);
            this.Controls.Add(this.gbxStudent);
            this.Name = "frmGrading";
            this.Text = "Student Grading";
            this.Load += new System.EventHandler(this.frmGrading_Load);
            this.gbxGrade.ResumeLayout(false);
            this.gbxGrade.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxStudent;
        private System.Windows.Forms.GroupBox gbxGrade;
        private System.Windows.Forms.LinkLabel lnkReturn;
        private System.Windows.Forms.LinkLabel lnkUpdate;
        private System.Windows.Forms.Label lblExisting;
    }
}