using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suprmrkt.Models
{
	[AttributeUsage(AttributeTargets.Class)]
	class ORMTableAttribute : Attribute
	{
		public ORMTableAttribute(string tableName)
		{
			this.Name = tableName;
		}

		public string Name { get; set; }
	}
	
	[AttributeUsage(AttributeTargets.Property)]
	class ORMColumnAttribute : Attribute
	{
		public ORMColumnAttribute(string columnName)
		{
			this.Name = columnName;
		}

		public string Name { get; set; }
	}
}
