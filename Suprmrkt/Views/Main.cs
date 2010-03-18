using System;
using System.Windows.Forms;
using Suprmrkt.Helpers;
using Suprmrkt.Interfaces;
using Suprmrkt.Controllers;
using Pyramid.Garnet.Controls.Tabs;
using Suprmrkt.Models;
using System.Threading;
using System.Data.SQLite;
using System.Data;

namespace Suprmrkt.Views
{
	public partial class Main : Form, IView
	{
		public Main()
		{
			InitializeComponent();
			InitialiseController();
			DisableAllControls(true);
            SQLiteConnection conn = new SQLiteConnection("Data Source=DatabaseServer;Initial Catalog=buyrite.s3db;");
            SQLiteDataReader rdr = null;
        }

		/// <summary>
		/// Disables all the controls on tab pages for standard users.
		/// </summary>
		/// <param name="disable"></param>
		private void DisableAllControls(bool disable)
		{
			foreach (GarnetTabStripItem tab in this.tabstripMainTabs.Tabs)
			{
				tab.Enabled = disable;
			}
		}

		#region IView Members

		public void ModelChanged(object sender, ModelChangedEventArgs e)
		{
			switch ((MainActions)e.ActionReference)
			{
				case MainActions.RunSimulation:
					break;
				case MainActions.NewSimulation:
					break;
				case MainActions.LoadSimulation:
					break;
				case MainActions.ViewResults:
					break;
				case MainActions.Logout:
					break;
				case MainActions.GetCustomerTypes:
					this.cmbCustomersCustomerTypes.Items.Clear();
					// this.cmbPricingCustomerTypes.Items.Clear();
					// this.cmbActivityCustomerTypes.Items.Clear();
					this.cmbCustomersCustomerTypes.Items.AddRange((string[])e.Params["Customer Types"]);
					// this.cmbPricingCustomerTypes.Items.AddRange((string[])e.Params["Customer Types"]);
					// this.cmbActivityCustomerTypes.Items.AddRange((string[])e.Params["Customer Types"]);
					break;
				case MainActions.GetStaffTypes:
					this.comboBox1.Items.Clear();
					this.comboBox1.Items.AddRange((string[])e.Params["Staff Types"]);
					break;
				case MainActions.GetPromotionTypes:
					break;
				case MainActions.Quit:
					break;
				default:
					break;
			}
		}

		public void InitialiseController()
		{
			this.Controller.RegisterView(this);

			this.FormClosed += new FormClosedEventHandler(Main_FormClosed);
			this.cmdlRunSimulation.Click += new EventHandler(Controller.ButtonActionHandler);
			this.cmbCustomersCustomerTypes.Click += new EventHandler(Controller.ButtonActionHandler);
			// this.cmbPricingCustomerTypes.Click += new EventHandler(Controller.ButtonActionHandler);
			// this.cmbActivityCustomerTypes.Click += new EventHandler(Controller.ButtonActionHandler);
			this.comboBox1.Click += new EventHandler(Controller.ButtonActionHandler);

			this.cmdlRunSimulation.Tag = MainActions.RunSimulation;
			this.cmbCustomersCustomerTypes.Tag = MainActions.GetCustomerTypes;
			// this.cmbPricingCustomerTypes.Tag = MainActions.GetCustomerTypes;
			// this.cmbActivityCustomerTypes.Tag = MainActions.GetCustomerTypes;
			this.comboBox1.Tag = MainActions.GetStaffTypes;
		}

		void Main_FormClosed(object sender, FormClosedEventArgs e)
		{
			Application.Exit();
		}

		/// <summary>
		/// Redirects standard EventHandler driven form events to the custom ButtonClick
		/// handler inside the controller.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ButtonActionHandlerRedirect(object sender, EventArgs e)
		{
			ButtonActionEventArgs baInfo = new ButtonActionEventArgs();
			baInfo.Button = (Button)sender;
			Controller.ButtonActionHandler(sender, baInfo);
		}

		public MainController Controller
		{
			get
			{
				return MainController.Instance;
			}
		}

		#endregion

		private void cmdlNewSimulation_Click(object sender, EventArgs e)
		{
			this.txtSimTitle.Clear();
			this.tabstripMainTabs.SelectedItem = this.tabCustomers;
		}

		private void cmdlLoadSimulation_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Title = "Choose a Simulation to load..";
			ofd.ShowDialog();
		}

		private void cmdlViewResults_Click(object sender, EventArgs e)
		{
			Results r = new Results();
			r.ShowDialog(this);
		}

		private void cmdlLogout_Click(object sender, EventArgs e)
		{
			Login loginView = (Login)Application.OpenForms["Login"];
			loginView.Show();
			this.Hide();
		}

		private void cmdlRunSimulation_Click(object sender, EventArgs e)
		{
			Simulator s = new Simulator();
			Thread t = new Thread(new ThreadStart(s.Run));
			t.IsBackground = true;
			t.Start();
			cmdlRunSimulation.Note = "Running Simulation..";
		}

        private void cmbCustomersCustomerTypes_Click(object sender, EventArgs e)
        {
            this.Tag = MainActions.GetCustomerTypes;
        }

        private void MinItems_TextChanged(object sender, EventArgs e)
        {
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            this.Tag = MainActions.GetStaffTypes;
        }

        private void cmbCustomersCustomerTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = cmbCustomersCustomerTypes.Text;
            SQLiteResult result0 = SQLiteController.Instance.Query("SELECT minItems FROM customers WHERE (Type = '" + type + "')");
            minItems.Text = result0.Rows[0]["minItems"].ToString();
            SQLiteResult result1 = SQLiteController.Instance.Query("SELECT maxItems FROM customers WHERE (Type = '" + type + "')");
            maxItems.Text = result1.Rows[0]["maxItems"].ToString();
            SQLiteResult result2 = SQLiteController.Instance.Query("SELECT dawdling FROM customers WHERE (Type = '" + type + "')");
            dawdling.Text = result2.Rows[0]["dawdling"].ToString();
            SQLiteResult result3 = SQLiteController.Instance.Query("SELECT patience FROM customers WHERE (Type = '" + type + "')");
            patience.Text = result3.Rows[0]["patience"].ToString();
            SQLiteResult result4 = SQLiteController.Instance.Query("SELECT promoResponse FROM customers WHERE (Type = '" + type + "')");
            promoResponse.Text = result4.Rows[0]["promoResponse"].ToString();
            SQLiteResult result5 = SQLiteController.Instance.Query("SELECT concentration FROM customers WHERE (Type = '" + type + "')");
            concentration.Text = result5.Rows[0]["concentration"].ToString();
            SQLiteResult result6 = SQLiteController.Instance.Query("SELECT hour1, hour2, hour3, hour4, hour5, hour6, hour7, hour8, hour9, hour10, hour11, hour12 FROM customers WHERE (Type = '" + type + "')");
            hour1.Text = result6.Rows[0]["hour1"].ToString();
            hour2.Text = result6.Rows[0]["hour2"].ToString();
            hour3.Text = result6.Rows[0]["hour3"].ToString();
            hour4.Text = result6.Rows[0]["hour4"].ToString();
            hour5.Text = result6.Rows[0]["hour5"].ToString();
            hour6.Text = result6.Rows[0]["hour6"].ToString();
            hour7.Text = result6.Rows[0]["hour7"].ToString();
            hour8.Text = result6.Rows[0]["hour8"].ToString();
            hour9.Text = result6.Rows[0]["hour9"].ToString();
            hour10.Text = result6.Rows[0]["hour10"].ToString();
            hour11.Text = result6.Rows[0]["hour11"].ToString();
            hour12.Text = result6.Rows[0]["hour12"].ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = comboBox1.Text;
            SQLiteResult result0 = SQLiteController.Instance.Query("SELECT normSpeed FROM staff WHERE (Type = '" + type + "')");
            normSpeed.Text = result0.Rows[0]["normSpeed"].ToString();
            SQLiteResult result1 = SQLiteController.Instance.Query("SELECT maxSpeed FROM staff WHERE (Type = '" + type + "')");
            maxSpeed.Text = result1.Rows[0]["maxSpeed"].ToString();
            SQLiteResult result2 = SQLiteController.Instance.Query("SELECT working FROM staff WHERE (Type = '" + type + "')");
            working.Text = result2.Rows[0]["working"].ToString();
        }
	}
}
