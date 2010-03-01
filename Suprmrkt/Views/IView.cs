using Suprmrkt.Controllers;
using Suprmrkt.Models;

namespace Suprmrkt.Views
{
	public interface IView
	{
		void ModelChanged(object sender, ModelChangedEventArgs e);
		void RegisterHandlersWithController();
	}
}