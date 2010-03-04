using System;
using System.Windows.Forms;
using Suprmrkt.Controllers;
using Suprmrkt.Helpers;
using System.Collections;

namespace Suprmrkt.Interfaces
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
		/// Handles all button click events sent from the Main form.
		/// </summary>
		/// <param name="sender">The control that sent the Click event.</param>
		/// <param name="e">The EventArgs related to the sending Control.</param>
		public void ActionHandler(object sender, ActionEventArgs e)
		{
			// OMG RAILS MVC FTW
			Control sendingControl = (Control)sender;
			switch ((UIAction)sendingControl.Tag)
			{
				case UIAction.Login:
					// TODO: Validate, authenticate.
					break;
				case UIAction.Quit:
					MessageBox.Show("Quit clicked!");
					break;
				default:
					break;
			}
		}

		private void AuthenticateUser(string username, string password)
		{
			throw new NotImplementedException();
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
