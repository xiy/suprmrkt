using System;
using System.Windows.Forms;
using Suprmrkt.Controllers;
using Suprmrkt.Helpers;
using System.Collections;
using Suprmrkt.Interfaces;
using System.Linq;
using System.Collections.Generic;
using Pyramid.Garnet.Controls.Dialogs;
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
					this.AuthenticateUser(username, password);
					loginView.Hide();
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
