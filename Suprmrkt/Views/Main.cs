using System;
using System.Windows.Forms;
using Suprmrkt.Controllers;
using Suprmrkt.Models;
using Pyramid.Garnet.Controls.Dialogs;

namespace Suprmrkt.Views
{
	public partial class Main : Form, IView
	{
		MainController Controller;

		public Main()
		{
			InitializeComponent();
			AssignController();
		}

		#region IView Members

		public void ModelChanged(object sender, ModelChangedEventArgs e)
		{
			throw new NotImplementedException();
		}

		public void RegisterHandlersWithController()
		{
			//this.button1.Click += new EventHandler(Controller.ButtonClickHandler);
		}

		public void AssignController()
		{
			this.Controller = MainController.Instance;
			RegisterHandlersWithController();
		}

		#endregion
	}
}
