using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suprmrkt.Models
{
	/// <summary>
	/// An Interface for a Model, which represents Business Models such as
	/// database tables or objects in code.
	/// </summary>
	public interface IModel
	{
		/* Model Validation routines for validating input on a Model.
		* These are a bit of a steal from Ruby/Rails, but I love the format.
		* The parameters are also just objects so that we can use the methods
		* ambiguously, without having to rewrite a new function for each object
		* type that we might be validating in the future. Also as this is an
		* Interface, we can leave the type choice completely with the implementer. */

		/// <summary>
		/// Validates the length of an object, usually a <see cref="System.String"/>
		/// or integer.
		/// </summary>
		/// <param name="thisObject">The object to validate the length of.</param>
		/// <returns>True if the object validates correctly, False otherwise.</returns>
		bool ValidatesLengthOf(object thisObject);
		
		/// <summary>
		/// Validates the existence of a particular field, usually to determine
		/// wether the user has input the required fields or not.
		/// </summary>
		/// <param name="thisObject">The object to validate the existence of.</param>
		/// <returns>True if the object validates correctly, False otherwise.</returns>
		bool ValidatesExistenceOf(object thisObject);
		/// <summary>
		/// Validates the format of an object, e.g. a date or string.
		/// </summary>
		/// <param name="thisObject">The object to validate the format of.</param>
		/// <returns>True if the object validates correctly, False otherwise.</returns>
		bool ValidatesFormatOf(object thisObject);
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
