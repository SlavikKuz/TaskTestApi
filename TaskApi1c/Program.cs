using Newtonsoft.Json;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TaskApi1c;

/* 1c. Create a program that will be executed once a day. The program should retrieve the latest
exchange rate and store it in a database. Using SqlServer and Entity framework is preferred
but not mandatory. This task involves design a suitable database structure.*/

namespace TaskApi
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var context = new TaskDbContext())
            {
                try
                {
                    int lastDbTimestamp = context.ExchangeRates.Select(x => x.Timestamp).Max();

                    if (lastDbTimestamp < GetLastTimestampYesterday())
                    {
                        ExchangeRate currentRate = MapExchangeRate(await Rates.GetRate(
                            Constants.BaseCurrency, Constants.TargetCurrency, null));

                        if (currentRate != null)
                        {
                            context.ExchangeRates.Add(currentRate);
                            context.SaveChanges();
                            Console.WriteLine("Database was updated.");
                        }
                        else
                        {
                            Console.WriteLine("Database was not updated.");
                        }
                    }
                    else
                        Console.WriteLine("Database is up to date.");
                }
                catch (Exception ex)
                {
                    //add to log ex.Message
                    Console.WriteLine("Error: DB Update failed");
                }
            }
        }

        public static ExchangeRate MapExchangeRate(ResponseRate rate)
        {
            return new ExchangeRate()
            {
                Timestamp = rate.Timestamp,
                Rate = rate.Rates[Constants.TargetCurrency],
                BaseCurrency = Constants.BaseCurrency,
                TargetCurrency = Constants.TargetCurrency
            };
        }

        /*The Fixer API delivers EOD / End of Day historical exchange rates, 
         * which become available at 00:05am GMT for the previous day 
         * and are time stamped at one second before midnight.*/
        public static int GetLastTimestampYesterday()
        {            
            int timestamp = (int)(DateTime.UtcNow.Date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            return timestamp + 5;
        }

    }
}
