using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suprmrkt.Models.Users
{
	class AdvancedUser : UserBase
	{
		public AdvancedUser(string username, string plaintextPassword)
			: base(username, plaintextPassword)
		{
			
		}
	}
}
