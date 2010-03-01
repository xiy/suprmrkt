using System;
using System.Windows.Forms;
using Suprmrkt.Controllers;
using Suprmrkt.Models;
using Pyramid.Garnet.Controls.Dialogs;

namespace Suprmrkt.Views
{
	public partial class Login : Form, IView
	{
		public Login()
		{
			InitializeComponent();
			this.Controller = LoginController.Instance;
		}

		#region IView Members

		public void ModelChanged(object sender, ModelChangedEventArgs e)
		{
			/*TODO: Validate Login:
			 * Send input via EventHandler to Controler->
			 * ->Controller validates against Users database->
			 * <-Model sends back result to Controller-<
			 * <-Controller returns result to View-<
			 * View updates based on Controller action
			 */
			throw new NotImplementedException();
		}

		public IController Controller
		{
			get;
			set;
		}

		public void RegisterHandlersWithController()
		{
			
		}

		#endregion

		#region IView Members


		public void AssignController()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
