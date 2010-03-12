using System;
using System.Windows.Forms;
using Suprmrkt.Helpers;
using Suprmrkt.Interfaces;
using Suprmrkt.Controllers;
using Pyramid.Garnet.Controls.Tabs;
using Suprmrkt.Models;

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
			throw new NotImplementedException();
		}

		public void InitialiseController()
		{
			this.Controller.RegisterView(this);

			this.FormClosed += new FormClosedEventHandler(Main_FormClosed);
			this.cmdlRunSimulation.Click += new EventHandler(ButtonActionHandlerRedirect);
			this.cmbCustomersCustomerTypes.Click += new EventHandler(ButtonActionHandlerRedirect);

			this.cmdlRunSimulation.Tag = MainActions.RunSimulation;
			this.cmbCustomersCustomerTypes.Tag = MainActions.GetCustomerTypes;
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
			s.Run();
		}

	}
}
