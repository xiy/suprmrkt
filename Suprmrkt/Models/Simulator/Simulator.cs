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
		/// A List that contains all the Customers being "simulated".
		/// </summary>
        private List<Customer> Shoppers = new List<Customer>();

        /// <summary>
        /// A List that contains all the "hours".
        /// </summary>
        private List<Hour> Hours = new List<Hour>();

        		/// <summary>
		/// A List that contains all the customer types entering the store in that hour.
		/// </summary>
        private List<CustomerTypes> HourlyCust = new List<CustomerTypes>();

		/// <summary>
		/// A List that contains all of the current Queues.
		/// </summary>
		private List<Checkout> Checkouts = new List<Checkout>();

		/// <summary>
		/// Initialization
		/// </summary>
		private static int lastCalcTime = 0; // last time event times were calculated
		private static int nextEventTime = 0;
		private static int dayTimer = 0;
		private static int secondsTimer = 0;
		private static double CustRemainder ; // for the customers entering the store method
		private static int custsEntering = 0;
		private static int custsIn = 0;
        private static int customersToEnter = 0;
		private static int forTheMinute;
		private static int staffCounter = 0;
        private static int custTypeCounter = 0;
		private static int staffNumber = 0;
        private static int totalCustForHour = 0;

        /// <summary>
        /// parameters to temporarily store customer type parameters
        /// </summary>
        private static int minItems = 0;
        private static int maxItems = 0;
        private static int Concentration = 0;
        private static int Dawdling = 0;
        private static int Patience = 0;

        private static int[] customerDistribution = new int[60];

        private static Random random = new Random();
		private String staffType;

		public Simulator()
		{
			SQLiteResult result = SQLiteController.Instance.Query("SELECT COUNT(Type) FROM staff");
			staffCounter = Convert.ToInt32(result.Rows[0]["COUNT(Type)"]);

			// create Queues objects with attributes from the Staff db table
			// populate the Queues list with these Queues objects (DONE BELOW)

			// get the number of Staff Members of that type
			for (int k = 0; k < staffCounter; k++)
			{
				result = SQLiteController.Instance.Query("SELECT number FROM staff");
				// make sure this actually gets the number field correctly!
				staffNumber = Convert.ToInt32(result.Rows[k]["number"]);

                // get the staff type
                result = SQLiteController.Instance.Query("SELECT Type FROM staff");
                staffType = (string)result.Rows[k]["Type"];

				// create Queues that have that staff type's attributes (speed, maxSpeed)
				for (int r = 0; r < staffNumber; r++)
				{
					// staffType is the type we feed the Queues constructor with
					Checkouts.Add(new Checkout(staffType));
				}
			}

            SQLiteResult resultY = SQLiteController.Instance.Query("SELECT COUNT(Type) FROM Customers");
            custTypeCounter = Convert.ToInt32(resultY.Rows[0]["COUNT(Type)"]);

            for (int n = 1; n <= 12; n++)
            {
                totalCustForHour = 0;

                for (int m = 0; m < custTypeCounter; m++)
                {
                    resultY = SQLiteController.Instance.Query("SELECT hour" + n + " FROM Customers");
                    totalCustForHour = totalCustForHour + Convert.ToInt32(resultY.Rows[m]["hour" + n]);
                }

                Hours.Add(new Hour(totalCustForHour, custTypeCounter));
            }
		}

        /// <summary>
        /// Begins running the simulation.	
        /// </summary>
        public void Run()
		{
			for (int hour = 0; hour < 12; hour++)
			{
                int f = hour+1;
				custsEntering = Hours.ElementAt(hour).totalCustomers;
                customerDistribution = customersPerMinuteMethod(custsEntering);

                for (int t = 0; t < Hours.ElementAt(hour).customerTypeCounter; t++)
                {
                    SQLiteResult resultT = SQLiteController.Instance.Query("SELECT hour" + f + " FROM Customers");
                    int totalCustOfType = Convert.ToInt32(resultT.Rows[t]["hour" + f]);

                    resultT = SQLiteController.Instance.Query("SELECT Type FROM Customers");
                    String custType = (String)resultT.Rows[t]["Type"];

                    SQLiteResult resultZ = SQLiteController.Instance.Query("SELECT * FROM Customers WHERE (type = '" + custType + "')");

                    minItems = Convert.ToInt32(resultZ.Rows[0]["minItems"]);
                    maxItems = Convert.ToInt32(resultZ.Rows[0]["maxItems"]);
                    Concentration = Convert.ToInt32(resultZ.Rows[0]["concentration"]);
                    Dawdling = Convert.ToInt32(resultZ.Rows[0]["dawdling"]);
                    Patience = Convert.ToInt32(resultZ.Rows[0]["patience"]);

                    for (int h = 0; h < totalCustOfType; h++)
                    {
                        HourlyCust.Add(new CustomerTypes(custType));

                        HourlyCust.ElementAt(HourlyCust.Count - 1).minItems = minItems;
                        HourlyCust.ElementAt(HourlyCust.Count - 1).maxItems = maxItems;
                        HourlyCust.ElementAt(HourlyCust.Count - 1).Concentration = Concentration;
                        HourlyCust.ElementAt(HourlyCust.Count - 1).Dawdling = Dawdling;
                        HourlyCust.ElementAt(HourlyCust.Count - 1).Patience = Patience;
                    }
                }

                shuffleList(HourlyCust);

                for (secondsTimer = 0; secondsTimer < 3600; secondsTimer++)
                {
                    if (secondsTimer % 60 == 0)
                    {
                        forTheMinute = customerDistribution[secondsTimer / 60];

                        for (int w = 0; w < forTheMinute; w++)
                        {
                            Customer cust = new Customer(HourlyCust.ElementAt(0).custType, HourlyCust.ElementAt(0).minItems, HourlyCust.ElementAt(0).maxItems);
                            cust.EntryTime = dayTimer;

                            cust.Concentration = HourlyCust.ElementAt(0).Concentration;
                            cust.Dawdling = HourlyCust.ElementAt(0).Dawdling;
                            cust.Patience = HourlyCust.ElementAt(0).Patience;

                            Shoppers.Add(cust);

                            HourlyCust.RemoveAt(0);
                        }

                        calcEventTimes(dayTimer, Shoppers.Count);
                    }

                    if (nextEventTime == dayTimer)
                    {
                        Customer cust2 = Shoppers.ElementAt(0);
                        int itemSearchTime = dayTimer - cust2.EntryTime;

                        if (Shoppers.ElementAt(0).Shopping)
                        {
                            int basket = Shoppers.ElementAt(0).Basket + 1;
                            Shoppers.ElementAt(0).Basket = basket;

                            if (Shoppers.ElementAt(0).Basket == Shoppers.ElementAt(0).Items)
                            {
                                int shortestQueueIndex = 0;
                                int queueLength = TimesInQueues(0, cust2); ;

                                for (int d = 1; d < Checkouts.Count; d++)
                                {
                                    if (queueLength > TimesInQueues(d, cust2))
                                    {
                                        shortestQueueIndex = d;
                                        queueLength = TimesInQueues(d, cust2);
                                    }
                                }

                                double timePreparedToQueue = (double)itemSearchTime / cust2.Patience;

                                if (timePreparedToQueue > queueLength)
                                {
                                    cust2.Shopping = false;
                                    Checkouts.ElementAt(shortestQueueIndex).Customers++;
                                    Checkouts.ElementAt(shortestQueueIndex).Items = Checkouts.ElementAt(shortestQueueIndex).Items + cust2.Items;
                                }
                                else
                                {
                                    cust2.LeavingTime = dayTimer;
                                    Shoppers.RemoveAt(0);
                                    Hours.ElementAt(hour).customersLeaving++;
                                    Hours.ElementAt(hour).customersWalkedOut++;
                                    Hours.ElementAt(hour).customerTimeSpentShopping = Hours.ElementAt(hour).customerTimeSpentShopping + itemSearchTime;
                                    Hours.ElementAt(hour).customerTotalHappiness = Hours.ElementAt(hour).customerTotalHappiness + 0;
                                }
                            }
                        }
                        else
                        {
                            cust2.LeavingTime = dayTimer;
                            double happiness = itemSearchTime / (dayTimer - cust2.EntryTime);
                            Shoppers.RemoveAt(0);
                            Hours.ElementAt(hour).customersLeaving++;
                            Hours.ElementAt(hour).customerTimeSpentShopping = Hours.ElementAt(hour).customerTimeSpentShopping + itemSearchTime;
                            Hours.ElementAt(hour).customerTotalHappiness = Hours.ElementAt(hour).customerTotalHappiness + happiness;
                        }

                        calcEventTimes(dayTimer, Shoppers.Count);
                    }

                    dayTimer++;
                }
            }
        }

		/// <summary>
		/// The method takes the number of the customers to enter the store (which is a double number by then),
		/// add the remainder from the previous time and seperates the integer part and the decimal part. 
		/// In the end the integer is returned and the decimal part is stored as the remainder.
		/// </summary>
        private int[] customersPerMinuteMethod(int custsEntering)
		{
            int[] customersPerMin = new int[60];
            CustRemainder = 0;
            custsIn = 0;

            double xCust = custsEntering / 60;
            xCust = Math.Round(xCust, 2);

            for (int min = 0; min < 60; min++)
            {
                if (min == 59)
                {
                    customersToEnter = custsEntering - custsIn;
                    customersPerMin[min] = customersToEnter;
                    custsIn = custsIn + customersToEnter;
                }
                else
                {
                    xCust = xCust + CustRemainder;

                    customersToEnter = (int)xCust;
                    customersPerMin[min] = customersToEnter;
                    custsIn = custsIn + customersToEnter;

                    double yCust = (double)customersToEnter;

                    CustRemainder = xCust - yCust;
                    CustRemainder = Math.Round(CustRemainder, 2);
                }
            }

            return customersPerMin;
		}

		/// <summary>
		/// calculates the event times for every customer in the store
		/// </summary>
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

        private static void shuffleList(List<CustomerTypes> customerTypes)
        {
            if (customerTypes.Count > 1)
            {
                for (int z = customerTypes.Count - 1; z >= 0; z--)
                {
                    CustomerTypes tmp = customerTypes[z];
                    int randomIndex = random.Next(z + 1);

                    customerTypes[z] = customerTypes[randomIndex];
                    customerTypes[randomIndex] = tmp;
                }
            }
        }

		/// <summary>
		/// calculates the time the customer CUST will spend in the queue
		/// QueueNumber is the number of the Queue customer CUST is at
		/// </summary>
		private int TimesInQueues(int QueueNumber, Customer cust)
		{
			if (Checkouts.ElementAt(QueueNumber).Customers > 4) // if there are more than 4 customers, Queue becomes faster
			{
				Checkouts.ElementAt(QueueNumber).AssignedStaffMember.Speed = Checkouts.ElementAt(QueueNumber).AssignedStaffMember.MaxSpeed;
			}

			return (cust.Items + Checkouts.ElementAt(QueueNumber).Items) * Checkouts.ElementAt(QueueNumber).AssignedStaffMember.Speed;
		}
	}
}