using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suprmrkt.Models.Users
{
	class Users
	{
		public void GetPasswordForUser(UserType user)
		{
			switch (user)
			{
				case UserType.Advanced:
					
					break;
				case UserType.Standard:
					break;
				default:
					break;
			}
		}
	}

	public enum UserType
	{
		Advanced,
		Standard
	}
}
