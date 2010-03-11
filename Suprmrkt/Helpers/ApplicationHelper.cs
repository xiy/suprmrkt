using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using Pyramid.Garnet.Controls.Tabs;

namespace Suprmrkt.Helpers
{
	class ApplicationHelper
	{
	}

	public class ActionEventArgs : EventArgs
	{
		ArrayList _params;
		public ArrayList Params 
		{
			get
			{
				if (this._params == null)
					this._params = new ArrayList();
				return this._params;
			}
			set { this._params = value; }
		}
	}

	public class ButtonActionEventArgs : EventArgs
	{
		Dictionary<string, object> _params;
		public Button Button { get; set; }
		public Dictionary<string, object> Params
		{
			get
			{
				if (this._params == null)
					this._params = new Dictionary<string, object>();
				return this._params;
			}
		}
		
		public ButtonType TypeOfButton { get; set; }

		public enum ButtonType
		{
			Button,
			CommandLink
		};
	}

	public class TabActionEventArgs : EventArgs
	{
		Queue _params;
		public GarnetTabStripItem Tab { get; set; }
		public Queue Params
		{
			get
			{
				if (this._params == null)
					this._params = new Queue();
				return this._params;
			}
		}
	}
	
	public class ModelChangedEventArgs : EventArgs
	{
		int customerIndex;
		private ModelChangedEventArgs() { }
		public ModelChangedEventArgs(int customerIndex)
		{
			// push some event details here
			this.customerIndex = customerIndex;
		}
	}

}
