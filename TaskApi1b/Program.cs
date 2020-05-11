using Newtonsoft.Json;
using Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TaskApi1b;

/* 1b. Extend the program with an optional input date, do the same calculation as in step 1 but use
the currency rate for the date inputted to the program. You need to find out the url for
retrieving exchange rate a given date using the documentation on fixer.io.*/

namespace TaskApi
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("Base currency: ");
            string baseCurrency = Console.ReadLine().ToUpper();
            bool baseValid = baseCurrency.ToCharArray().Count() == 3;

            Console.Write("Target currency: ");
            string targetCurrency = Console.ReadLine().ToUpper();
            bool targerValid = targetCurrency.ToCharArray().Count() == 3;

            //amount is not price, up to 6 decimal places, can be negative
            Console.Write("Amount: ");
            bool amountValid = Decimal.TryParse(Console.ReadLine().Replace('.', ','), out decimal amount);

            Console.Write("On a date (yyyy-mm-dd). Empty is latest: ");
            string inputDate = Console.ReadLine();

            //is true if parsed or input was empty
            bool dateValid = DateTime.TryParseExact(inputDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parseDate)
                || String.IsNullOrEmpty(inputDate); //&& date > new DateTime(1999,1,1) - api provides rates after this date

            DateTime? mainDate = null;
            if (!String.IsNullOrEmpty(inputDate) && dateValid)
            {
                mainDate = parseDate;
            }

            //no spaghetti code
            if (!amountValid || !baseValid || !targerValid || !dateValid)
            {
                Console.WriteLine("Please provide valid input data.");
                return;
            }

            if (amount == 0)
            {
                Console.WriteLine("{0} {1} is 0 {2}",
                    amount, baseCurrency, targetCurrency);
                return;
            }

            decimal? convertedAmount = await ConvertAmount(baseCurrency, targetCurrency, amount, mainDate);

            if (convertedAmount != null)
            {
                Console.WriteLine("{0:0.######} {1} is {2:N6} {3}",
                        amount, baseCurrency, convertedAmount, targetCurrency);
            }

            Console.ReadKey();
        }

        public static async Task<decimal?> ConvertAmount(string baseCurr, string trgtCurr, decimal amount, DateTime? date)
        {
            var result = await Rates.GetRate(baseCurr, trgtCurr, date);

            if (result != null)
            {
                return result.Rates[trgtCurr] * amount;
            }
            else
            {
                return null;
            }
        }
    }
}

