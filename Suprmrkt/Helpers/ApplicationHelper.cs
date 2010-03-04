using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

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
	
	public class ModelChangedEventArgs
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
