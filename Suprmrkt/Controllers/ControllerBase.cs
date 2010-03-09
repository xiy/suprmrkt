using Suprmrkt.Helpers;
using Suprmrkt.Views;
using Suprmrkt.Interfaces;

namespace Suprmrkt.Controllers
{
	public abstract class ControllerBase
	{
		public delegate void ModelChangedDelegate(object sender, ModelChangedEventArgs e);
		public event ModelChangedDelegate ModelChanged;

		public void RegisterView(IView view)
		{
			this.ModelChanged += new ModelChangedDelegate(view.ModelChanged);
		}

		public void UnregisterView(IView view)
		{
			this.ModelChanged -= new ModelChangedDelegate(view.ModelChanged);
		}

		public void RaiseModelChange(object sender, ModelChangedEventArgs e)
		{
			if (ModelChanged != null)
				ModelChanged(sender, e);
		}
	}
}
