using System;
using System.Windows.Forms;
using Suprmrkt.Controllers;
using Suprmrkt.Helpers;
using System.Collections;
using Suprmrkt.Interfaces;
using System.Linq;
using System.Collections.Generic;

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
			switch ((UIAction)e.Button.Tag)
			{
				case UIAction.Login:
					string username = e.Params["username"].ToString();
					string password = e.Params["password"].ToString();
					this.AuthenticateUser(username, password);
					break;
				case UIAction.Quit:
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
					
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// Actions that can be performed by this Controller through it's View.
		/// </summary>
		public enum UIAction
		{
			Login,
			Quit
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
