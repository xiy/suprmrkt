using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suprmrkt.Models;

namespace Suprmrkt.Models.Simulator
{
	public class Simulation
	{
		private List<Customer> _shoppers = new List<Customer>();
		/// <summary>
		/// A List that contains all the Customers entering the store.
		/// </summary>
		private  List<Customer> Shoppers
		{
			get { return _shoppers; }
			set { _shoppers = value; }
		}

		private List<Queues> _queues = new List<Queues>();
		/// <summary>
		/// A List that contains all of the current Queues.
		/// </summary>
		private List<Queues> Queues
		{
			get 
			{
				if (_queues == null)
					_queues = new List<Queues>();
				return _queues; 
			}
			set { _queues = value; }
		}

		// last time event times were calculated
		private static int lastCalcTime = 0;
		private static int nextEventTime = 0;
		private static int dayTimer = 0;
		private static int secondsTimer = 0;
		// for the customers entering the store method
		private static float custRemainder = 0; 
		private static int custsEntering = 0;
		private static int custsPerMinute = 0;

		public Simulation()
		{
			// create a simulation table (copying the sim table) [DB]

			// create Queues objects with attributes from the Staff db table
			// populate the Queues list with these Queues objects
		}

		/// <summary>
		/// Begins running the simulation.	
		/// </summary>
		public void Run()
		{
			for (int hour=1; hour<=12; hour++)
            {
                secondsTimer = 0;
                // get customer number for that hour [DB] : custsEntering

                while (secondsTimer < 3600)
                {
                    if (secondsTimer % 60 == 0)
                    {
                        custsPerMinute = customersPerMinute(custsEntering);

                        for(int c=0; c<custsPerMinute; c++) // create the customers and add the to the shoppers list
                        {
                            Customer cust = new Customer();
                            cust.EntryTime = dayTimer;
                            _shoppers.Add(cust);
                        }

                        calcEventTimes(dayTimer, _shoppers.Count);
                        SortShoppers();
                    }
                }
			}
		}

		private int customersPerMinute(int custsEntering)
		{
			throw new NotImplementedException();
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
		/// Calculates the event times for every customer in the store
		/// </summary>
		/// <param name="dayTimer"></param>
		/// <param name="custNumber"></param>
		private void calcEventTimes(int dayTimer, int custNumber)
		{
			int timeDiff = dayTimer - lastCalcTime;

			foreach (Customer cust in _shoppers)
			{
				if (cust.Shopping)
				{
					cust.timeForEvent = custNumber ^ cust.Concentration + cust.Dawdling; // calculate time for next item (formula)
				}
				else // customer is queueing so the next event is his checkout, whose time is calculated by a different method
				{
					cust.timeForEvent = timesInQueues(cust.QueueNumber, cust);
				}
			}

			// we have the times for the next event for every customer, we sort the customers by that and then set the nextEventTime
			SortShoppers();
			nextEventTime = dayTimer + _shoppers.ElementAt(0).timeForEvent;
			lastCalcTime = dayTimer;
		}

		// calculates the time the customer CUST will spend in the queue
		private int timesInQueues(int QueueNumber, Customer cust) // QueueNumber is the number of the Queue customer CUST is at
		{
			if (_queues.ElementAt(QueueNumber).Customers > 4) // if there are more than 4 customers, Queue becomes faster
			{
				_queues.ElementAt(QueueNumber).Speed = _queues.ElementAt(QueueNumber).MaxSpeed;
			}

			return (cust.Items + _queues.ElementAt(QueueNumber).Items) * _queues.ElementAt(QueueNumber).Speed;
		}
	}
}