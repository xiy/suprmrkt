using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suprmrkt.Controllers;
using System.Data.SQLite;
using System.Data;

namespace Suprmrkt.Models
{
    class CustomerTypes
    {
        public CustomerTypes(String type)
        {
            this.custType = type;
        }

        public double Patience { get; set; }
        public int Concentration { set; get; }
        public int minItems { set; get; }
        public int maxItems { set; get; }
        public int Dawdling { set; get; }

        public String custType { get; set; }
    }
}
