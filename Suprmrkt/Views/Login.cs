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
			this.btnLogin.Tag = LoginActions.Login;
			this.btnQuit.Tag = LoginActions.Quit;
		}

		void ActionHandlerRedirect(object sender, EventArgs e)
		{
			ButtonActionEventArgs baInfo = new ButtonActionEventArgs();
			baInfo.Button = (Button)sender;

			switch ((LoginActions)baInfo.Button.Tag)
			{
				case LoginActions.Login:
					// Validation checks need to moved from here to the model?
					if (this.cmbUserType.SelectedItem == null)
					{
						errorProvider.SetError(cmbUserType, "No User Type selected!");
						this.labelValidationError.Visible = true;
						this.labelValidationError.Text = "No User Type selected!";
						return;
					}
					baInfo.Params.Add("username", this.cmbUserType.SelectedItem.ToString());
					baInfo.Params.Add("password", this.txtPassword.Text);
					baInfo.Params.Add("view", this);
					baInfo.TypeOfButton = ButtonActionEventArgs.ButtonType.Button;
					break;
				case LoginActions.Quit:
					Application.Exit();
					break;
				default:
					break;
			}

			Controller.ButtonActionHandler(baInfo);
		}

		#endregion
	}
}
