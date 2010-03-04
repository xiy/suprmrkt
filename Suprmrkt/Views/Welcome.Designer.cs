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
			this.commandLink1 = new Pyramid.Garnet.Controls.Aero.CommandLink();
			this.commandLink3 = new Pyramid.Garnet.Controls.Aero.CommandLink();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.commandLink2 = new Pyramid.Garnet.Controls.Aero.CommandLink();
			this.commandLink4 = new Pyramid.Garnet.Controls.Aero.CommandLink();
			this.SuspendLayout();
			// 
			// commandLink1
			// 
			this.commandLink1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.commandLink1.Location = new System.Drawing.Point(35, 89);
			this.commandLink1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.commandLink1.Name = "commandLink1";
			this.commandLink1.Note = "Create a new simulation";
			this.commandLink1.ShowShield = true;
			this.commandLink1.Size = new System.Drawing.Size(350, 65);
			this.commandLink1.TabIndex = 7;
			this.commandLink1.Text = "New Simulation";
			this.commandLink1.UseVisualStyleBackColor = true;
			// 
			// commandLink3
			// 
			this.commandLink3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.commandLink3.Location = new System.Drawing.Point(35, 162);
			this.commandLink3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.commandLink3.Name = "commandLink3";
			this.commandLink3.Note = "View the results of previous simulation runs";
			this.commandLink3.Size = new System.Drawing.Size(350, 65);
			this.commandLink3.TabIndex = 6;
			this.commandLink3.Text = "View Results";
			this.commandLink3.UseVisualStyleBackColor = true;
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
			// commandLink2
			// 
			this.commandLink2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.commandLink2.Location = new System.Drawing.Point(35, 235);
			this.commandLink2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.commandLink2.Name = "commandLink2";
			this.commandLink2.Note = "Run a previously saved simulation";
			this.commandLink2.Size = new System.Drawing.Size(350, 65);
			this.commandLink2.TabIndex = 6;
			this.commandLink2.Text = "Run a Simulation";
			this.commandLink2.UseVisualStyleBackColor = true;
			// 
			// commandLink4
			// 
			this.commandLink4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.commandLink4.Location = new System.Drawing.Point(35, 308);
			this.commandLink4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.commandLink4.Name = "commandLink4";
			this.commandLink4.Note = "Log out of the Simulator entirely";
			this.commandLink4.Size = new System.Drawing.Size(350, 65);
			this.commandLink4.TabIndex = 6;
			this.commandLink4.Text = "Log out";
			this.commandLink4.UseVisualStyleBackColor = true;
			// 
			// Welcome
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.ClientSize = new System.Drawing.Size(417, 415);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.commandLink1);
			this.Controls.Add(this.commandLink4);
			this.Controls.Add(this.commandLink2);
			this.Controls.Add(this.commandLink3);
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

		private Pyramid.Garnet.Controls.Aero.CommandLink commandLink1;
		private Pyramid.Garnet.Controls.Aero.CommandLink commandLink3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private Pyramid.Garnet.Controls.Aero.CommandLink commandLink2;
		private Pyramid.Garnet.Controls.Aero.CommandLink commandLink4;
	}
}