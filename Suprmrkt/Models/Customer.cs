using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suprmrkt.Models
{
    class Customer : IComparable<Customer>
    {
        Random random = new Random();

        public Customer(CustomerType type)
        {
           try 
	       {	        
		     switch (type)
                {
                    case CustomerType.Novice:
                        DesiredItems = random.Next(5, 20);
                        Concentration = 2;
                        Dawdle = 120;
                        Patience = 0.5f;
                        PromoResponse = 1;
                        break;
                    case CustomerType.Quick:
                        DesiredItems = random.Next(6, 10);
                        Concentration = 1;
                        Dawdle = 30;
                        Patience = 1.2f;
                        PromoResponse = 0.2f;
                        break;
                    case CustomerType.Family:
                        DesiredItems = random.Next(40, 60);
                        Concentration = 1;
                        Dawdle = 30;
                        Patience = 1;
                        PromoResponse = 0.5f;
                        break;
                 case CustomerType.Pro:
                        DesiredItems = 17;
                        Concentration = 0;
                        Dawdle = 30;
                        Patience = 2;
                        PromoResponse = 0;
                        NextItemSearchTime = 2;
                        break;
                    default:
                        break;
                }   
	        }
	        catch (NullReferenceException nullEx)
	        {
                //TODO: expand.
		        throw nullEx;
	        }
        }

        #region Properties
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

        int IComparable<Customer>.CompareTo(Customer other)
        {
			throw new NotImplementedException();
        }

        #endregion
    }
}
