  using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suprmrkt.Controllers;
using System.Data.SQLite;
using System.Data;
using System.Diagnostics;

namespace Suprmrkt.Models
{
	public class Simulator
	{
		/// <summary>
		/// A List that contains all the Customers entering the store.
		/// </summary>
		private List<Customer> _shoppers = new List<Customer>();
		private List<Customer> Shoppers
		{
			get { return _shoppers; }
			set { _shoppers = value; }
		}

		/// <summary>
		/// A List that contains all of the current Queues.
		/// </summary>
		private List<Checkout> _queues = new List<Checkout>();
		private List<Checkout> Queues
		{
			get
			{
				if (_queues == null)
					_queues = new List<Checkout>();
				return _queues;
			}
			set { _queues = value; }
		}

		/// <summary>
		/// Initialization
		/// </summary>
		private static int lastCalcTime = 0; // last time event times were calculated
		private static int nextEventTime = 0;
		private static int dayTimer = 0;
		private static int secondsTimer = 0;
		private static float CustRemainder = 0; // for the customers entering the store method
		private static int custsEntering = 0;
		private static int custsIn = 0;
		private static float customersPerMinute = 0;
		private static int forTheMinute;
		private static int staffCounter = 0;
		private static int staffNumber = 0;
		private Suprmrkt.Models.Checkout.StaffType staffType;
        private Suprmrkt.Models.Customer.CustomerType custType;
        private Dictionary<Suprmrkt.Models.Customer.CustomerType, int> CTypeDict;
		private Dictionary<int, Dictionary<Suprmrkt.Models.Customer.CustomerType, int>> CustDict;
		private Suprmrkt.Models.Customer.CustomerType sebType;

		public Simulator()
		{
			SQLiteResult result = SQLiteController.Instance.Query("SELECT COUNT(Type) from staff");
			staffCounter = Convert.ToInt32(result.Rows[0]["COUNT(Type)"]);

			// create Queues objects with attributes from the Staff db table
			// populate the Queues list with these Queues objects (DONE BELOW)

			// get the number of Staff Members of that type
			for (int k = 0; k < staffCounter; k++)
			{
				result = SQLiteController.Instance.Query("SELECT number FROM staff");
				// make sure this actually gets the number field correctly!
				staffNumber = Convert.ToInt32(result.Rows[0]["number"]);

				// create Queues that have that staff's attributes (speed, maxSpeed)
				for (int r = 0; r < staffNumber; r++)
				{
					result = SQLiteController.Instance.Query("SELECT Type FROM staff");
                    staffType = (Checkout.StaffType)Enum.Parse(typeof(Checkout.StaffType), (string)result.Rows[0]["Type"]);

					// staffType is the type we feed the Queue constructor with
					Queues.Add(new Checkout(staffType));
				}
			}
		}

		/// <summary>
		/// Begins running the simulation.	
		/// </summary>
		public void Run()
		{

			for (int hour = 1; hour <= 12; hour++)
			{
				secondsTimer = 0;

				// Dummy values for now..
				CTypeDict = new Dictionary<Customer.CustomerType, int>();
				CTypeDict.Add(Customer.CustomerType.Family, 15);
				CTypeDict.Add(Customer.CustomerType.Novice, 10);
				CTypeDict.Add(Customer.CustomerType.Pro, 6);
				CTypeDict.Add(Customer.CustomerType.Quick, 20);

				custsEntering = CTypeDict.Count();
				Debug.WriteLine("customersEntering: " + custsEntering);
				customersPerMinute = custsEntering / 60;
				custsIn = 0;

				while (secondsTimer < 3600)
				{
					if (secondsTimer % 60 == 0)
					{
						forTheMinute = customersPerMinuteMethod(customersPerMinute, secondsTimer);

						for (int c = 0; c < forTheMinute; c++) // create the customers and add the to the shoppers list
						{
                            SQLiteResult resultX = SQLiteController.Instance.Query("SELECT Type FROM Customers");
							custType = (Customer.CustomerType)Enum.Parse(typeof(Suprmrkt.Models.Customer.CustomerType), resultX.Rows[0]["Type"].ToString());
                            /**
                            var TypeNumber = from customerType in CTypeDict
                                             where CTypeDict.Contains<custType, int>
                                             select customer.number;
                            */
                            
                            foreach (KeyValuePair<Suprmrkt.Models.Customer.CustomerType, int> entry in CTypeDict)
							{
                                sebType = entry.Key;
                                for (int l = 0; l < entry.Value; l++)
                                {
                                    Customer cust = new Customer(sebType); //new Customer();
                                    cust.EntryTime = dayTimer;
                                    Shoppers.Add(cust);
                                    custsIn++;
                                }
							}

							calcEventTimes(dayTimer, Shoppers.Count);
							SortShoppers();
						}
					}
				}
			}
		}
		/// <summary>
		/// The method takes the number of the customers to enter the store (which is a float number by then),
		/// add the remainder from the previous time and seperates the integer part and the decimal part. 
		/// In the end the integer is returned and the decimal part is stored as the remainder.
		/// </summary>
		private int customersPerMinuteMethod(float customersPerMinute, int secondsTimer)
		{
			int customersToEnter;

			if (secondsTimer == 3540)
			{
				customersToEnter = custsEntering - custsIn;
				Debug.WriteLine("custToEnter: " + customersToEnter);
				customersToEnter = 4;
			}
			else
			{
				float x = customersPerMinute + CustRemainder;
				customersToEnter = (int)x;
				float y = (float)customersToEnter;
				CustRemainder = x - y;
				Debug.WriteLine("custRemainder: " + CustRemainder);
				Debug.WriteLine("custToEnter: " + customersToEnter);
				CustRemainder = 0.4f;
			}
			
			//return customersToEnter;
			return 3;
		}

		/// <summary>
		/// calculates the event times for every customer in the store
		/// </summary>
		/// <param name="dayTimer"></param>
		/// <param name="custNumber"></param>
		private void calcEventTimes(int dayTimer, int custNumber)
		{
			int timeDiff = dayTimer - lastCalcTime;

			foreach (Customer customer in Shoppers)
			{
				if (customer.Shopping)
				{
					customer.timeForEvent = custNumber ^ customer.Concentration + customer.Dawdling; // calculate time for next item (formula)
				}
				else // customer is queueing so the next event is his checkout, whose time is calculated by a different method
				{
					customer.timeForEvent = TimesInQueues(customer.QueueNumber, customer);
				}
			}

			// we have the times for the next event for every customer, we sort the customers by that and then set the nextEventTime
			SortShoppers();
			nextEventTime = dayTimer + Shoppers.ElementAt(0).timeForEvent;
			lastCalcTime = dayTimer;
			Debug.WriteLine("nextEventTime: " + nextEventTime);
			Debug.WriteLine("lastCalcTime: " + lastCalcTime);
		}

		/// <summary>
		/// Sorts the list of shoppers based on the next event time.
		/// </summary>
		private void SortShoppers()
		{
			var shoppers = from customer in Shoppers
						   orderby customer.timeForEvent ascending
						   select customer;
			this.Shoppers = shoppers.ToList<Customer>();
		}

		/// <summary>
		/// calculates the time the customer CUST will spend in the queue
		/// QueueNumber is the number of the Queue customer CUST is at
		/// </summary>
		private int TimesInQueues(int QueueNumber, Customer cust)
		{
			if (Queues.ElementAt(QueueNumber).Customers > 4) // if there are more than 4 customers, Queue becomes faster
			{
				Queues.ElementAt(QueueNumber).Speed = Queues.ElementAt(QueueNumber).MaxSpeed;
			}

			return (cust.Items + Queues.ElementAt(QueueNumber).Items) * Queues.ElementAt(QueueNumber).Speed;
		}
	}
}