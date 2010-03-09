using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suprmrkt.Models
{
    class Supermarket
    {
        private static List<Customer> Shoppers = new List<Customer>();
        private static int lastCalcTime = 0;
        private static int timeForNextItem = 0;

        public void RunSimulation()
        {
            Random random = new Random();
            int second, custOutside, custNumber;
            // full timer to keep track of total time passed
            int timer = 0;
            //TODO: pull from database
			// Getting warning: assigned but never used.
            const int totalCustomers = 1200; 

            //Customer[] Customers = new Customer[totalCustomers]; // create an empty array for customers - only a dummy for now

            for (int hour = 0; hour < 12; hour++)
            {
                // reset seconds to zero for every hour
                second = 0;
                // cust_DB.length(); // number of customers left for this hour // pull from database
                custOutside = 60;

                // 3600 intervals for each of the 3600 seconds in an hour
                while (second < 3600)
                {
                    // let new customers into the store every 60 seconds
                    if (second % 60 == 0)
                    {
                        // the number of customers that enter the store that minute
                        custNumber = random.Next(custOutside + 1);
                        custOutside = custOutside - custNumber;

                        for (int j = 0; j < custNumber; j++)
                        {
                            // pull a customer from cust_DB and add it to ArrayList Shoppers
                        }
                        
                        // update action times
                        calculateEventTimes(timer);
                        // sort the shopper array list
                        linqSortShoppers();
                    }

                    // if next event time is up
                    if (timeForNextItem == timer)	
                    {
                        // get the first shopper from the list
                        Customer thisCustomer = Shoppers[0];
                        // determine what stage the shopper is in
                        int stage = thisCustomer.Stage;
                        if (stage == 1)	// if stage one (searching)
                        {
                            // mark: this is the same as what you had defined in the setter in the java code,
                            // but now it's not hardcoded, just incase we want to abuse the property differently later on!
                            thisCustomer.CurrentBag++;

                            if (thisCustomer.CurrentBag == thisCustomer.DesiredItems)	// if the shoppers bag is full
                            {
                                // do the bit after shopping: calculate queuing time, pick a queue to join and change "stage"
                                thisCustomer.Stage = 2;
                            }
                        }
                        else if (stage == 2)	// if stage two (queuing)
                        {
                            //TODO: do the bit after paying: calculate total time spent shopping, happiness, remove from shoppers list
                            Shoppers.Remove(thisCustomer);
                        }

                        calculateEventTimes(timer);
                        linqSortShoppers();
                    }

                    second++;
                    // increment timer
                    timer++;
                }
            }
        }

        /// <summary>
        /// Sort the shopper list according to time left until next event
        /// </summary>
        //private static void sortShoppers()
        //{
        //    for (int pass = 1; pass < Shoppers.Count(); pass++)
        //    {
        //        for (int k = 0; k < Shoppers.Count() - pass; k++)
        //        {
        //            Customer cust = Shoppers[k];
        //            Customer cust2 = Shoppers[k + 1];

        //            if (cust.NextItemSearchTime > cust2.NextItemSearchTime)
        //            {
        //                Collections.swap(Shoppers, k, k + 1);
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// Sorts the list of Shoppers based on the NextItemSearchTime property
        /// using LINQ. Nice.
        /// </summary>
        /// <returns></returns>
        public static void linqSortShoppers()
        {
            // Order the shoppers by NextItemSearchTime in ascending order,
			// and place the results in a var (as IOrderedList<Customer> collection).
            var searchTimes = from customer in Shoppers
                              orderby customer.NextItemSearchTime ascending
                              select customer;
            // Copy the sorted list back to the Shoppers list.
            Shoppers = searchTimes.ToList();
        }

        /// <summary>
        /// Calculate how much time every customer has left until next event happens.
        /// </summary>
        /// <param name="time">The total time passed.</param>
        public static void calculateEventTimes(int time)
        {
            // how much time has passed since last calculation
            int time_difference = time - lastCalcTime;
            for (int j = 0; j < Shoppers.Count; j++)
            {
                Customer cust = Shoppers[j];

                if (cust.Stage == 1)	// if the customer is searching for items, do these actions
                {
                    timeForNextItem = (Shoppers.Count() ^ cust.Concentration + cust.Dawdle) - time_difference;
                    cust.NextItemSearchTime = timeForNextItem;
                }
                else	// else if the customer is queuing, calculate the time left until he reaches the end of the queue
                {
                    // time_for_next = XXX - time_difference;
                    // cust.setTimeForNext(time_for_next);
                }
            }

            // set new last calculation time
            lastCalcTime = time;

            Customer firstCustomer = Shoppers[0];			// get the first customer from the list
            timeForNextItem = time + firstCustomer.NextItemSearchTime; // update the static instance variable time_for_next event
        }
    }
}
