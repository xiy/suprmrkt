using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Suprmrkt.Views;
using Suprmrkt.Models;
using System.Diagnostics;

namespace Suprmrkt.Controllers
{
	public class MainController : IController
	{
		#region Singleton
		// Lazy loading implementation of the singleton pattern
		private MainController() { }
		private static readonly MainController _instance = new MainController();

		public static MainController Instance
		{
			get
			{
				return NestedSingleton._instance;
			}
		}

		/// <summary>
		/// Nested static singleton to ensure thread safety.
		/// </summary>
		class NestedSingleton
		{
			static NestedSingleton() { }
			internal static MainController _instance = new MainController();
		} 
		#endregion

		/// <summary>
		/// Handles all button click events sent from the Main form.
		/// </summary>
		/// <param name="sender">The control that sent the Click event.</param>
		/// <param name="e">The EventArgs related to the sending Control.</param>
		public void ButtonClickHandler(object sender, EventArgs e)
		{
			Button sendingButton = (Button)sender;
			switch (sendingButton.Text)
			{
				case "New Simulation":
					Login l = new Login();
					l.ShowDialog(sendingButton.FindForm());
					break;
				default:
					break;
			}
		}

		#region IController Members

		public IView View
		{
			get;
			set;
		}

		public IModel Model
		{
			get;
			set;
		}

		#endregion
	}
}
