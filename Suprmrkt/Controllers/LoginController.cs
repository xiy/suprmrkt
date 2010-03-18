using System;
using System.Windows.Forms;
using Suprmrkt.Helpers;
using Suprmrkt.Interfaces;
using Suprmrkt.Models.Users;
using Suprmrkt.Views;

namespace Suprmrkt.Controllers
{
	public sealed class LoginController : ControllerBase, IController
	{
		#region Singleton
		// Lazy loading implementation of the singleton pattern
		LoginController() { }
		static readonly LoginController _instance = new LoginController();

		public static LoginController Instance
		{
			get
			{
				return NestedSingleton._instance;
			}
		}

		class NestedSingleton
		{
			static NestedSingleton() { }
			internal static LoginController _instance = new LoginController();
		} 
		#endregion

		/// <summary>
		/// Handles all button click events sent from the Login form.
		/// </summary>
		/// <param name="sender">The control that sent the Click event.</param>
		/// <param name="e">The EventArgs related to the sending Control.</param>
		public void ButtonActionHandler(ButtonActionEventArgs e)
		{
			switch ((LoginActions)e.Button.Tag)
			{
				case LoginActions.Login:
					string username = e.Params["username"].ToString();
					string password = e.Params["password"].ToString();
					Login loginView = (Login)e.Params["view"];
					//this.View = loginView;
					this.AuthenticateUser(username, password);
					break;
				case LoginActions.Quit:
					Application.Exit();
					break;
				default:
					break;
			}
		}

		private void AuthenticateUser(string username, string password)
		{
			switch (username)
			{
				case "Advanced":
					string checkPassword = User.Instance.GetPasswordForUser(UserType.Advanced);
					if (checkPassword == password) {
						Main mainForm = new Main();
						mainForm.Show();
						Application.OpenForms["Login"].Hide();
					}
					else if (checkPassword == string.Empty || checkPassword != password)
					{
						// notify view!
						ModelChangedEventArgs m = new ModelChangedEventArgs();
						m.ActionReference = LoginActions.Login;
						m.Params.Add("Fail", "Password was invalid!");
						RaiseModelChange(this, m);
					}
					break;
				default:
					break;
			}
		}

		#region IController Members


		public IView View
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public IModel Model
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		#endregion
	}
}
