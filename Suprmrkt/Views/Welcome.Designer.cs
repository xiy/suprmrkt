namespace Suprmrkt.Views
{
	partial class Welcome
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
			this.cmdlNewSim = new Pyramid.Garnet.Controls.Aero.CommandLink();
			this.cmdlViewResults = new Pyramid.Garnet.Controls.Aero.CommandLink();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cmdlRunSim = new Pyramid.Garnet.Controls.Aero.CommandLink();
			this.cmdlLogout = new Pyramid.Garnet.Controls.Aero.CommandLink();
			this.SuspendLayout();
			// 
			// cmdlNewSim
			// 
			this.cmdlNewSim.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdlNewSim.Location = new System.Drawing.Point(35, 89);
			this.cmdlNewSim.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.cmdlNewSim.Name = "cmdlNewSim";
			this.cmdlNewSim.Note = "Create a new simulation";
			this.cmdlNewSim.ShowShield = true;
			this.cmdlNewSim.Size = new System.Drawing.Size(350, 65);
			this.cmdlNewSim.TabIndex = 7;
			this.cmdlNewSim.Text = "New Simulation";
			this.cmdlNewSim.UseVisualStyleBackColor = true;
			// 
			// cmdlViewResults
			// 
			this.cmdlViewResults.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdlViewResults.Location = new System.Drawing.Point(35, 162);
			this.cmdlViewResults.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.cmdlViewResults.Name = "cmdlViewResults";
			this.cmdlViewResults.Note = "View the results of previous simulation runs";
			this.cmdlViewResults.Size = new System.Drawing.Size(350, 65);
			this.cmdlViewResults.TabIndex = 6;
			this.cmdlViewResults.Text = "View Results";
			this.cmdlViewResults.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(32, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(353, 17);
			this.label1.TabIndex = 8;
			this.label1.Text = "Welcome to the GRPSIX Buyrite Supermarket Simulator!";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(48, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(320, 17);
			this.label2.TabIndex = 9;
			this.label2.Text = "To get started, select an available option from below.";
			// 
			// cmdlRunSim
			// 
			this.cmdlRunSim.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdlRunSim.Location = new System.Drawing.Point(35, 235);
			this.cmdlRunSim.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.cmdlRunSim.Name = "cmdlRunSim";
			this.cmdlRunSim.Note = "Run a previously saved simulation";
			this.cmdlRunSim.Size = new System.Drawing.Size(350, 65);
			this.cmdlRunSim.TabIndex = 6;
			this.cmdlRunSim.Text = "Run a Simulation";
			this.cmdlRunSim.UseVisualStyleBackColor = true;
			this.cmdlRunSim.Click += new System.EventHandler(this.cmdlRunSim_Click);
			// 
			// cmdlLogout
			// 
			this.cmdlLogout.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdlLogout.Location = new System.Drawing.Point(35, 308);
			this.cmdlLogout.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.cmdlLogout.Name = "cmdlLogout";
			this.cmdlLogout.Note = "Log out of the Simulator entirely";
			this.cmdlLogout.Size = new System.Drawing.Size(350, 65);
			this.cmdlLogout.TabIndex = 6;
			this.cmdlLogout.Text = "Log out";
			this.cmdlLogout.UseVisualStyleBackColor = true;
			// 
			// Welcome
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.ClientSize = new System.Drawing.Size(417, 415);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmdlNewSim);
			this.Controls.Add(this.cmdlLogout);
			this.Controls.Add(this.cmdlRunSim);
			this.Controls.Add(this.cmdlViewResults);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Welcome";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "GRPSIX Buyrite Supermarket Simulator - Welcome";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Pyramid.Garnet.Controls.Aero.CommandLink cmdlNewSim;
		private Pyramid.Garnet.Controls.Aero.CommandLink cmdlViewResults;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private Pyramid.Garnet.Controls.Aero.CommandLink cmdlRunSim;
		private Pyramid.Garnet.Controls.Aero.CommandLink cmdlLogout;
	}
}