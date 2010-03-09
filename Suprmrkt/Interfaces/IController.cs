using System;
namespace Suprmrkt.Interfaces
{
	public interface IController
	{
		void RegisterView(IView view);
		void UnregisterView(IView model);
		IView View { get; set; }
		IModel Model { get; set; }
	}
}
