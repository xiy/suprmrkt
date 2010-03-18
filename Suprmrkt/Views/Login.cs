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
			switch ((LoginActions)e.ActionReference)
			{
				case LoginActions.Login:
					if (e.Params.ContainsKey("Fail"))
					{
						this.labelValidationError.Text = e.Params["Fail"].ToString();
						this.labelValidationError.Visible = true;
					}
					break;
				case LoginActions.Quit:
					break;
				default:
					break;
			}
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
			this.btnLogin.Click += new EventHandler(ButtonActionHandlerRedirect);
			this.btnQuit.Click += new EventHandler(ButtonActionHandlerRedirect);
			this.btnLogin.Tag = LoginActions.Login;
			this.btnQuit.Tag = LoginActions.Quit;
		}

		private void ButtonActionHandlerRedirect(object sender, EventArgs e)
		{
			ButtonActionEventArgs baInfo = new ButtonActionEventArgs();
			baInfo.Button = (Button)sender;

			switch ((LoginActions)baInfo.Button.Tag)
			{
				case LoginActions.Login:
					// Validation checks need to moved from here to the model or controller?
					if (this.cmbUserType.SelectedItem == null)
					{
						errorProvider.SetError(cmbUserType, "No User Type selected!");
						this.labelValidationError.Visible = true;
						this.labelValidationError.Text = "No User Type selected!";
						return;
					}
					else if (this.txtPassword.Text == string.Empty)
					{
						errorProvider.SetError(txtPassword, "No password entered!");
						this.labelValidationError.Visible = true;
						this.labelValidationError.Text = "No password entered!";
						return;
					}
					else
					{
						baInfo.Params.Add("username", this.cmbUserType.SelectedItem.ToString());
						baInfo.Params.Add("password", this.txtPassword.Text);
						baInfo.Params.Add("view", this);
						baInfo.TypeOfButton = ButtonActionEventArgs.ButtonType.Button;
						this.txtPassword.Clear();
						errorProvider.Clear();
						labelValidationError.Hide();
					}
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
