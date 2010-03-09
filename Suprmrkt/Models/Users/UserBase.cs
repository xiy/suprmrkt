using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Suprmrkt.Interfaces;

namespace Suprmrkt.Models.Users
{
	public abstract class UserBase : IModel
	{
		/// <summary>
		/// Create a new Advanced User with full parameters.
		/// </summary>
		/// <param name="fullName">The real full name of the user.</param>
		/// <param name="username">The username of the advanced user.</param>
		/// <param name="plaintextPassword">The password of the advanced user, as plain-text.</param>
		public UserBase(string username, string plaintextPassword) 
		{
			this.Username = username;
			this.PasswordHashed = HashPassword(plaintextPassword);
		}

		/// <summary>
		/// The real full name of the user, for identification purposes.
		/// </summary>
		public string FullName { get; set; }
		/// <summary>
		/// The username of the user on the system.
		/// </summary>
		public string Username { get; set; }
		/// <summary>
		/// The hashed string of the user's password.
		/// </summary>
		public string PasswordHashed { get; set; }

		/// <summary>
		/// Hash a <see cref="System.String"/> using the SHA256 algorithm.
		/// This ensures the password of any user can never be obtained as it is stored,
		/// either in the database or through code.
		/// </summary>
		/// <param name="plaintextPassword">The plain-text password of the user to hash.</param>
		/// <returns>A <see cref="System.String"/> hashed using the SHA256 algorithm.</returns>
		public string HashPassword(string plaintextPassword)
		{
			// This probably doesn't need to be used as it's managed?
			using (SHA256Managed sha = new SHA256Managed())
			{
				sha.ComputeHash(UTF8Encoding.UTF8.GetBytes(plaintextPassword));
				return UTF8Encoding.UTF8.GetString(sha.Hash);
			}
		}

		#region IModel Members

		public bool ValidatesLengthOf(object thisObject)
		{
			throw new NotImplementedException();
		}

		public bool ValidatesExistenceOf(object thisObject)
		{
			throw new NotImplementedException();
		}

		public bool ValidatesFormatOf(object thisObject)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
