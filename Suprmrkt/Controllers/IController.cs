using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suprmrkt.Views;
using Suprmrkt.Models;

namespace Suprmrkt.Controllers
{
	public interface IController
	{
		IView View { get; set; }
		IModel Model { get; set; }
	}
}
