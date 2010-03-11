using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suprmrkt.Models
{
    public class Simulation
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
        private List<Queues> _queues = new List<Queues>();
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
                customersPerMinute = custsEntering/60;
                custsIn = 0;

                while (secondsTimer<3600)
                {
                    if (secondsTimer%60 == 0)
                    {
                        forTheMinute = customersPerMinuteMethod(customersPerMinute, secondsTimer);

                        for(int c=0; c<forTheMinute; c++) // create the customers and add the to the shoppers list
                        {
                            Customer cust = new Customer();
                            cust.EntryTime = dayTimer;
                            Shoppers.Add(cust);
                            custsIn++;
                        }

                        calcEventTimes(dayTimer, Shoppers.Count);
                        SortShoppers();
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
                    }
                    else
                    {
                        float x = customersPerMinute + CustRemainder;
                        customersToEnter = (int)x;
                        float y = (float)customersToEnter;
                        CustRemainder = x - y;
                    }

                    return customersToEnter;
                }
                
                /// <summary>
                /// calculates the event times for every customer in the store
                /// </summary>
                /// <param name="dayTimer"></param>
                /// <param name="custNumber"></param>
                private void calcEventTimes(int dayTimer, int custNumber)
                {
                    int timeDiff = dayTimer-lastCalcTime;

                    foreach(Customer customer in Shoppers)
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