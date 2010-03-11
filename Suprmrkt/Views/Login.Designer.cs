namespace Suprmrkt.Views
{
	partial class Login
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
			this.labelHeader = new System.Windows.Forms.Label();
			this.panelTop = new System.Windows.Forms.Panel();
			this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.labelValidationError = new System.Windows.Forms.Label();
			this.btnLogin = new System.Windows.Forms.Button();
			this.btnQuit = new System.Windows.Forms.Button();
			this.cmbUserType = new Pyramid.Garnet.Controls.Aero.ComboBox();
			this.txtPassword = new Pyramid.Garnet.Controls.Aero.TextBox();
			this.panelTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// labelHeader
			// 
			this.labelHeader.AutoSize = true;
			this.labelHeader.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelHeader.Location = new System.Drawing.Point(85, 16);
			this.labelHeader.Name = "labelHeader";
			this.labelHeader.Size = new System.Drawing.Size(189, 20);
			this.labelHeader.TabIndex = 0;
			this.labelHeader.Text = "GRPSIX Buyrite Simulator";
			// 
			// panelTop
			// 
			this.panelTop.BackColor = System.Drawing.Color.White;
			this.panelTop.Controls.Add(this.labelHeader);
			this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelTop.Location = new System.Drawing.Point(0, 0);
			this.panelTop.Name = "panelTop";
			this.panelTop.Size = new System.Drawing.Size(359, 51);
			this.panelTop.TabIndex = 5;
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			// 
			// labelValidationError
			// 
			this.labelValidationError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.labelValidationError.ForeColor = System.Drawing.Color.Red;
			this.errorProvider.SetIconAlignment(this.labelValidationError, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
			this.errorProvider.SetIconPadding(this.labelValidationError, 5);
			this.labelValidationError.Location = new System.Drawing.Point(27, 161);
			this.labelValidationError.Name = "labelValidationError";
			this.labelValidationError.Size = new System.Drawing.Size(306, 15);
			this.labelValidationError.TabIndex = 7;
			this.labelValidationError.Text = "Validation Error";
			this.labelValidationError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.labelValidationError.Visible = false;
			// 
			// btnLogin
			// 
			this.btnLogin.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnLogin.Location = new System.Drawing.Point(27, 198);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size(75, 23);
			this.btnLogin.TabIndex = 6;
			this.btnLogin.Tag = "Actions.PerformLogin";
			this.btnLogin.Text = "&Login";
			this.btnLogin.UseVisualStyleBackColor = true;
			// 
			// btnQuit
			// 
			this.btnQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnQuit.Location = new System.Drawing.Point(258, 198);
			this.btnQuit.Name = "btnQuit";
			this.btnQuit.Size = new System.Drawing.Size(75, 23);
			this.btnQuit.TabIndex = 6;
			this.btnQuit.Text = "&Quit";
			this.btnQuit.UseVisualStyleBackColor = true;
			// 
			// cmbUserType
			// 
			this.cmbUserType.CueBannerText = "Select your User Type...";
			this.cmbUserType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbUserType.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmbUserType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbUserType.Items.AddRange(new object[] {
            "Advanced",
            "Standard"});
			this.cmbUserType.Location = new System.Drawing.Point(27, 79);
			this.cmbUserType.Name = "cmbUserType";
			this.cmbUserType.Size = new System.Drawing.Size(306, 23);
			this.cmbUserType.TabIndex = 0;
			// 
			// txtPassword
			// 
			this.txtPassword.CueBannerText = "Password";
			this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.errorProvider.SetIconAlignment(this.txtPassword, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
			this.errorProvider.SetIconPadding(this.txtPassword, 5);
			this.txtPassword.Location = new System.Drawing.Point(27, 117);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '•';
			this.txtPassword.Size = new System.Drawing.Size(306, 23);
			this.txtPassword.TabIndex = 1;
			// 
			// Login
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(359, 237);
			this.Controls.Add(this.labelValidationError);
			this.Controls.Add(this.btnQuit);
			this.Controls.Add(this.btnLogin);
			this.Controls.Add(this.panelTop);
			this.Controls.Add(this.cmbUserType);
			this.Controls.Add(this.txtPassword);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "Login";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "GRPSIX Buyrite Simulator - Login";
			this.TopMost = true;
			this.panelTop.ResumeLayout(false);
			this.panelTop.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Pyramid.Garnet.Controls.Aero.TextBox txtPassword;
		private Pyramid.Garnet.Controls.Aero.ComboBox cmbUserType;
		private System.Windows.Forms.Label labelHeader;
		private System.Windows.Forms.Panel panelTop;
		private System.Windows.Forms.ErrorProvider errorProvider;
		private System.Windows.Forms.Label labelValidationError;
		private System.Windows.Forms.Button btnQuit;
		private System.Windows.Forms.Button btnLogin;
	}
}