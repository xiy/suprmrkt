using System;
using System.Windows.Forms;
using Suprmrkt.Helpers;
using System.Collections;
using Suprmrkt.Interfaces;
using Suprmrkt.Controllers;

namespace Suprmrkt.Views
{
	public partial class Login : Form, IView
	{
		public Login()
		{
			InitializeComponent();
			InitialiseController();
		}

		#region IView Members

		public void ModelChanged(object sender, ModelChangedEventArgs e)
		{
			throw new NotImplementedException();
		}

		public LoginController Controller
		{
			get
			{
				return LoginController.Instance;
			}
		}

		public void InitialiseController()
		{
			// Hook View
			this.Controller.RegisterView(this);

			// Hook Events
			this.btnLogin.Click += new EventHandler(ActionHandlerRedirect);
			this.btnQuit.Click += new EventHandler(ActionHandlerRedirect);
			this.btnLogin.Tag = LoginController.UIAction.Login;
			this.btnQuit.Tag = LoginController.UIAction.Quit;
		}

		void ActionHandlerRedirect(object sender, EventArgs e)
		{
			ButtonActionEventArgs baInfo = new ButtonActionEventArgs();
			baInfo.Button = (Button)sender;

			switch ((LoginController.UIAction)baInfo.Button.Tag)
			{
				case LoginController.UIAction.Login:
					baInfo.Params.Add("username", this.cmbUserType.SelectedValue.ToString());
					baInfo.Params.Add("password", this.txtPassword.Text);
					baInfo.Type = ButtonActionEventArgs.ButtonType.Button;
					break;
				case LoginController.UIAction.Quit:
					
					break;
				default:
					break;
			}

			Controller.ButtonActionHandler(baInfo);
		}

		#endregion
	}
}
