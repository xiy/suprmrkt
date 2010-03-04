using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suprmrkt.Interfaces
{
	/// <summary>
	/// The base class for all Customer types. Must inherit.
	/// </summary>
    public abstract class CustomerBase : IComparable<CustomerBase>
    {
        Random random = new Random();

        public CustomerBase(int desiredItems, int concentration, int dawdle, double patience,
							int promoResponse, int nextItemSearchTime)
        {
			// Default base parameters are for the most common
			// customer type, which I guess would be Family?

			// Override in the derived class.
			this.DesiredItems = desiredItems;
			this.Concentration = concentration;
			this.Dawdle = dawdle;
			this.Patience = patience;
			this.PromoResponse = promoResponse;
			//this.NextItemSearchTime;
        }

        #region Self Setting Properties
        public int CurrentBag { get; set; }
        public int Stage { get; set; }
        public int DesiredItems { get; set; }
        public int Concentration { get; set; }
        public int Dawdle { get; set; }
        public int EntryTime { get; set; }
        public int LeavingTime { get; set; }
        public int TotalItemSearchTime { get; set; }
        public int NextItemSearchTime { get; set; }
        public double Patience { get; set; }
        public float QueueingTime { get; set; }
        public float Happiness { get; set; }
        public float PromoResponse { get; set; }
        #endregion

        public enum CustomerType
        {
            Novice,
            Quick,
            Family,
            Pro
        };

        #region IComparable<Customer> Members

        int IComparable<CustomerBase>.CompareTo(CustomerBase other)
        {
			throw new NotImplementedException();
        }

        #endregion
    }
}
