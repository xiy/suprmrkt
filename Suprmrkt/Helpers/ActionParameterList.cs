using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Suprmrkt.Helpers
{
	class ActionParameterList : ArrayList
	{
		public enum ParameterType
		{
			UIAction,
			String,
			Integer,
			Raw
		}	
	}
}
