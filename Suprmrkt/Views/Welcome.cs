using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Suprmrkt.Interfaces;

namespace Suprmrkt.Views
{
	public partial class Welcome : Form, IView
	{
		public Welcome()
		{
			InitializeComponent();
		}

		#region IView Members

		public void ModelChanged(object sender, Suprmrkt.Helpers.ModelChangedEventArgs e)
		{
			throw new NotImplementedException();
		}

		public void InitialiseController()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
