namespace Suprmrkt.Views
{
	partial class Results
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
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Christmas Eve Model",
            "24/21/2010",
            "12/07/2010",
            "Advanced User"}, -1);
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "Mondays",
            "15/04/2010",
            "10/04/2010",
            "Standard User"}, -1);
			this.panel1 = new System.Windows.Forms.Panel();
			this.label4 = new System.Windows.Forms.Label();
			this.listView1 = new Pyramid.Garnet.Controls.Aero.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.tlpCommandLinkPanel = new System.Windows.Forms.TableLayoutPanel();
			this.commandLink1 = new Pyramid.Garnet.Controls.Aero.CommandLink();
			this.commandLink3 = new Pyramid.Garnet.Controls.Aero.CommandLink();
			this.commandLink2 = new Pyramid.Garnet.Controls.Aero.CommandLink();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.panel1.SuspendLayout();
			this.tlpCommandLinkPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.label4);
			this.panel1.Location = new System.Drawing.Point(15, 302);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(683, 307);
			this.panel1.TabIndex = 2;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(269, 115);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(144, 15);
			this.label4.TabIndex = 0;
			this.label4.Text = "[Result Chart Placeholder]";
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
			this.listView1.FullRowSelect = true;
			this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
			this.listView1.Location = new System.Drawing.Point(15, 85);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(683, 151);
			this.listView1.TabIndex = 3;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Simulation Name";
			this.columnHeader1.Width = 196;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Simulation Date";
			this.columnHeader2.Width = 165;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Actual Run Date";
			this.columnHeader3.Width = 181;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Run By";
			this.columnHeader4.Width = 134;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(12, 259);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(122, 17);
			this.label1.TabIndex = 5;
			this.label1.Text = "Select a Timeframe:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(191, 259);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(23, 17);
			this.label2.TabIndex = 6;
			this.label2.Text = "To";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(314, 261);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(140, 17);
			this.label3.TabIndex = 5;
			this.label3.Text = "Performance Indicator:";
			// 
			// tlpCommandLinkPanel
			// 
			this.tlpCommandLinkPanel.AutoSize = true;
			this.tlpCommandLinkPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.tlpCommandLinkPanel.ColumnCount = 3;
			this.tlpCommandLinkPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.95968F));
			this.tlpCommandLinkPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.04032F));
			this.tlpCommandLinkPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 241F));
			this.tlpCommandLinkPanel.Controls.Add(this.commandLink1, 0, 0);
			this.tlpCommandLinkPanel.Controls.Add(this.commandLink3, 2, 0);
			this.tlpCommandLinkPanel.Controls.Add(this.commandLink2, 1, 0);
			this.tlpCommandLinkPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.tlpCommandLinkPanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns;
			this.tlpCommandLinkPanel.Location = new System.Drawing.Point(0, 0);
			this.tlpCommandLinkPanel.Name = "tlpCommandLinkPanel";
			this.tlpCommandLinkPanel.RowCount = 1;
			this.tlpCommandLinkPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tlpCommandLinkPanel.Size = new System.Drawing.Size(710, 68);
			this.tlpCommandLinkPanel.TabIndex = 8;
			// 
			// commandLink1
			// 
			this.commandLink1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.commandLink1.Location = new System.Drawing.Point(3, 3);
			this.commandLink1.Name = "commandLink1";
			this.commandLink1.Note = "Load a simulation result file";
			this.commandLink1.Size = new System.Drawing.Size(193, 62);
			this.commandLink1.TabIndex = 5;
			this.commandLink1.Text = "Import";
			this.commandLink1.UseVisualStyleBackColor = true;
			// 
			// commandLink3
			// 
			this.commandLink3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.commandLink3.Location = new System.Drawing.Point(471, 3);
			this.commandLink3.Name = "commandLink3";
			this.commandLink3.Note = "Save the results to a file";
			this.commandLink3.Size = new System.Drawing.Size(236, 62);
			this.commandLink3.TabIndex = 3;
			this.commandLink3.Text = "Export";
			this.commandLink3.UseVisualStyleBackColor = true;
			// 
			// commandLink2
			// 
			this.commandLink2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.commandLink2.Location = new System.Drawing.Point(213, 3);
			this.commandLink2.Name = "commandLink2";
			this.commandLink2.Note = "Save the selected results";
			this.commandLink2.Size = new System.Drawing.Size(252, 62);
			this.commandLink2.TabIndex = 6;
			this.commandLink2.Text = "Save";
			this.commandLink2.UseVisualStyleBackColor = true;
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numericUpDown1.Location = new System.Drawing.Point(140, 256);
			this.numericUpDown1.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
			this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(45, 25);
			this.numericUpDown1.TabIndex = 9;
			this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// numericUpDown2
			// 
			this.numericUpDown2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numericUpDown2.Location = new System.Drawing.Point(220, 256);
			this.numericUpDown2.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
			this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.Size = new System.Drawing.Size(45, 25);
			this.numericUpDown2.TabIndex = 9;
			this.numericUpDown2.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[] {
            "Happiness"});
			this.comboBox1.Location = new System.Drawing.Point(460, 258);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(238, 25);
			this.comboBox1.TabIndex = 10;
			// 
			// Results
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(710, 621);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.numericUpDown2);
			this.Controls.Add(this.numericUpDown1);
			this.Controls.Add(this.tlpCommandLinkPanel);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Results";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "GRPSIX Buyrite Supermarket Simulator - Results";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.tlpCommandLinkPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private Pyramid.Garnet.Controls.Aero.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.TableLayoutPanel tlpCommandLinkPanel;
		private Pyramid.Garnet.Controls.Aero.CommandLink commandLink1;
		private Pyramid.Garnet.Controls.Aero.CommandLink commandLink3;
		private Pyramid.Garnet.Controls.Aero.CommandLink commandLink2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.NumericUpDown numericUpDown2;
		private System.Windows.Forms.ComboBox comboBox1;
	}
}