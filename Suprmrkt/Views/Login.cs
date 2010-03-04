using System;
using System.Windows.Forms;
using Suprmrkt.Helpers;
using System.Collections;

namespace Suprmrkt.Interfaces
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

			this.btnLogin.Tag = LoginController.UIAction.Login;
			this.btnCancel.Tag = LoginController.UIAction.Quit;

			// Hook Events
			this.btnLogin.Click += new EventHandler(btnLogin_Click);
			this.btnCancel.Click += new EventHandler(btnCancel_Click);
		}

		void btnCancel_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		void btnLogin_Click(object sender, EventArgs e)
		{
			// Temporary for Prototyping
			this.Close();
		}

		public delegate void ActionEventHandler(object sender, ActionEventArgs e);

		void ActionHandlerRedirect(object sender, EventArgs e)
		{
			ActionEventArgs ae = new ActionEventArgs();
			ae.Params.Add(this.txtPassword.Text);
			Controller.ActionHandler(sender, ae);
		}

		#endregion
	}
}
