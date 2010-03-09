using Suprmrkt.Helpers;

namespace Suprmrkt.Interfaces
{
	public interface IView
	{
		void ModelChanged(object sender, ModelChangedEventArgs e);
		void InitialiseController();
	}
}