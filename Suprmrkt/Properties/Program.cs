using System;
using System.Windows.Forms;
using Suprmrkt.Interfaces;
using Suprmrkt.Views;

namespace Suprmrkt
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Main());
		}
	}
}
