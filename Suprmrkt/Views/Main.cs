using System;
using System.Windows.Forms;
using Suprmrkt.Helpers;
using Suprmrkt.Interfaces;
using Suprmrkt.Controllers;

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
			foreach (Control item in this.garnetTabStrip1.Controls)
			{
				item.Enabled = disable;
			}
		}

		#region IView Members

		public void ModelChanged(object sender, ModelChangedEventArgs e)
		{
			throw new NotImplementedException();
		}

		public void InitialiseController()
		{
			this.FormClosed += new FormClosedEventHandler(MainController.Instance.ButtonClickHandler);
		}

		public MainController Controller
		{
			get
			{
				return MainController.Instance;
			}
		}

		#endregion

	}
}
