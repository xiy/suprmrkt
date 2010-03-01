using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suprmrkt.Controllers
{
	public sealed class LoginController : IController
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

		#region IController Members

		public Views.IView View
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

		public Models.IModel Model
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
