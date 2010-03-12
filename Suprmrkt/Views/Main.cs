using System;
using System.Windows.Forms;
using Suprmrkt.Helpers;
using Suprmrkt.Interfaces;
using Suprmrkt.Controllers;
using Pyramid.Garnet.Controls.Tabs;
using Suprmrkt.Models;
using System.Threading;

namespace Suprmrkt.Views
{
	public partial class Main : Form, IView
	{
		public Main()
		{
			InitializeComponent();
			InitialiseController();
			DisableAllControls(true);
		}

		/// <summary>
		/// Disables all the controls on tab pages for standard users.
		/// </summary>
		/// <param name="disable"></param>
		private void DisableAllControls(bool disable)
		{
			foreach (GarnetTabStripItem tab in this.tabstripMainTabs.Items)
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
					this.cmbPricingCustomerTypes.Items.Clear();
					this.cmbActivityCustomerTypes.Items.Clear();
					this.cmbCustomersCustomerTypes.Items.AddRange((string[])e.Params["Customer Types"]);
					this.cmbPricingCustomerTypes.Items.AddRange((string[])e.Params["Customer Types"]);
					this.cmbActivityCustomerTypes.Items.AddRange((string[])e.Params["Customer Types"]);
					break;
				case MainActions.GetStaffTypes:
					this.cmbStaffTypes.Items.Clear();
					this.cmbStaffTypes.Items.AddRange((string[])e.Params["Staff Types"]);
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
			this.cmbPricingCustomerTypes.Click += new EventHandler(Controller.ButtonActionHandler);
			this.cmbActivityCustomerTypes.Click += new EventHandler(Controller.ButtonActionHandler);
			this.cmbStaffTypes.Click += new EventHandler(Controller.ButtonActionHandler);

			this.cmdlRunSimulation.Tag = MainActions.RunSimulation;
			this.cmbCustomersCustomerTypes.Tag = MainActions.GetCustomerTypes;
			this.cmbPricingCustomerTypes.Tag = MainActions.GetCustomerTypes;
			this.cmbActivityCustomerTypes.Tag = MainActions.GetCustomerTypes;
			this.cmbStaffTypes.Tag = MainActions.GetStaffTypes;
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

		private void button15_Click(object sender, EventArgs e)
		{
			Simulator s = new Simulator();
			Thread t = new Thread(new ThreadStart(s.Run));
			t.IsBackground = true;
			t.Start();
		}

	}
}
