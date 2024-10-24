namespace Host_Info_Retriever
{
	partial class HostInfoRetriever
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
			this.lblHost = new System.Windows.Forms.Label();
			this.txtHost = new System.Windows.Forms.TextBox();
			this.lblHostResults = new System.Windows.Forms.Label();
			this.txtHostResults = new System.Windows.Forms.TextBox();
			this.btnGetInfo = new System.Windows.Forms.Button();
			this.lblUsername = new System.Windows.Forms.Label();
			this.lblPassword = new System.Windows.Forms.Label();
			this.txtUsername = new System.Windows.Forms.TextBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.btnCopy = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.ckbGetInstalledSoftware = new System.Windows.Forms.CheckBox();
			this.ckbGetHardware = new System.Windows.Forms.CheckBox();
			this.ckbGetOtherInfo = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// lblHost
			// 
			this.lblHost.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblHost.Location = new System.Drawing.Point(24, 8);
			this.lblHost.Name = "lblHost";
			this.lblHost.Size = new System.Drawing.Size(128, 23);
			this.lblHost.TabIndex = 1;
			this.lblHost.Text = "Hostname or Address:";
			this.lblHost.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtHost
			// 
			this.txtHost.Location = new System.Drawing.Point(152, 8);
			this.txtHost.Name = "txtHost";
			this.txtHost.Size = new System.Drawing.Size(136, 20);
			this.txtHost.TabIndex = 2;
			this.txtHost.Text = ".";
			// 
			// lblHostResults
			// 
			this.lblHostResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblHostResults.Location = new System.Drawing.Point(8, 136);
			this.lblHostResults.Name = "lblHostResults";
			this.lblHostResults.Size = new System.Drawing.Size(80, 23);
			this.lblHostResults.TabIndex = 10;
			this.lblHostResults.Text = "Host Resutls:";
			this.lblHostResults.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtHostResults
			// 
			this.txtHostResults.Location = new System.Drawing.Point(8, 168);
			this.txtHostResults.Multiline = true;
			this.txtHostResults.Name = "txtHostResults";
			this.txtHostResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtHostResults.Size = new System.Drawing.Size(520, 320);
			this.txtHostResults.TabIndex = 11;
			this.txtHostResults.Text = "";
			// 
			// btnGetInfo
			// 
			this.btnGetInfo.Location = new System.Drawing.Point(296, 72);
			this.btnGetInfo.Name = "btnGetInfo";
			this.btnGetInfo.Size = new System.Drawing.Size(88, 23);
			this.btnGetInfo.TabIndex = 4;
			this.btnGetInfo.Text = "Get Host Info";
			this.btnGetInfo.Click += new System.EventHandler(this.btnGetInfo_Click);
			// 
			// lblUsername
			// 
			this.lblUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblUsername.Location = new System.Drawing.Point(24, 40);
			this.lblUsername.Name = "lblUsername";
			this.lblUsername.Size = new System.Drawing.Size(128, 23);
			this.lblUsername.TabIndex = 3;
			this.lblUsername.Text = "Username:";
			this.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblPassword
			// 
			this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblPassword.Location = new System.Drawing.Point(24, 72);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(128, 23);
			this.lblPassword.TabIndex = 5;
			this.lblPassword.Text = "Password:";
			this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtUsername
			// 
			this.txtUsername.Location = new System.Drawing.Point(152, 40);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(136, 20);
			this.txtUsername.TabIndex = 4;
			this.txtUsername.Text = "";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(152, 72);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(136, 20);
			this.txtPassword.TabIndex = 6;
			this.txtPassword.Text = "";
			// 
			// btnCopy
			// 
			this.btnCopy.Location = new System.Drawing.Point(171, 496);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.TabIndex = 12;
			this.btnCopy.Text = "Copy";
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(291, 496);
			this.btnClear.Name = "btnClear";
			this.btnClear.TabIndex = 13;
			this.btnClear.Text = "Clear";
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// ckbGetInstalledSoftware
			// 
			this.ckbGetInstalledSoftware.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.ckbGetInstalledSoftware.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ckbGetInstalledSoftware.Location = new System.Drawing.Point(8, 104);
			this.ckbGetInstalledSoftware.Name = "ckbGetInstalledSoftware";
			this.ckbGetInstalledSoftware.Size = new System.Drawing.Size(160, 24);
			this.ckbGetInstalledSoftware.TabIndex = 7;
			this.ckbGetInstalledSoftware.Text = "Show Installed Software:";
			this.ckbGetInstalledSoftware.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ckbGetHardware
			// 
			this.ckbGetHardware.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.ckbGetHardware.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ckbGetHardware.Location = new System.Drawing.Point(184, 104);
			this.ckbGetHardware.Name = "ckbGetHardware";
			this.ckbGetHardware.Size = new System.Drawing.Size(136, 24);
			this.ckbGetHardware.TabIndex = 8;
			this.ckbGetHardware.Text = "Show Hardware Info:";
			this.ckbGetHardware.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ckbGetOtherInfo
			// 
			this.ckbGetOtherInfo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.ckbGetOtherInfo.Checked = true;
			this.ckbGetOtherInfo.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ckbGetOtherInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ckbGetOtherInfo.Location = new System.Drawing.Point(336, 104);
			this.ckbGetOtherInfo.Name = "ckbGetOtherInfo";
			this.ckbGetOtherInfo.Size = new System.Drawing.Size(168, 24);
			this.ckbGetOtherInfo.TabIndex = 9;
			this.ckbGetOtherInfo.Text = "Show Other Windows Info:";
			this.ckbGetOtherInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(536, 526);
			this.Controls.Add(this.ckbGetOtherInfo);
			this.Controls.Add(this.ckbGetHardware);
			this.Controls.Add(this.ckbGetInstalledSoftware);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnCopy);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.txtUsername);
			this.Controls.Add(this.lblPassword);
			this.Controls.Add(this.lblUsername);
			this.Controls.Add(this.btnGetInfo);
			this.Controls.Add(this.txtHostResults);
			this.Controls.Add(this.lblHostResults);
			this.Controls.Add(this.txtHost);
			this.Controls.Add(this.lblHost);
			this.Name = "Form1";
			this.Text = "Host Info Retriever";
			this.ResumeLayout(false);

		}
		#endregion
	}
}