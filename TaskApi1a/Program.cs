using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TaskApi1a;

/* 1a. Create a console application that take two currency codes and one amount as input. The
amount is in the first currency. The program should calculate the currency amount for the
second currency code (using the latest exchange rates). The program should do the
calculation in process and not utilize any external calculation api. */

namespace TaskApi
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("Base currency: ");
            string baseCurrency = Console.ReadLine().ToUpper();

            Console.Write("Target currency: ");
            string targetCurrency = Console.ReadLine().ToUpper();

            //amount is not price, up to 6 decimal places
            //can be negative, like expences or debt
            Console.Write("Amount: ");
            bool amountValid = Decimal.TryParse(Console.ReadLine().Replace('.', ','), out decimal amount);

            //no spaghetti code
            if (!amountValid || baseCurrency.ToCharArray().Count() != 3
                || targetCurrency.ToCharArray().Count() != 3)
            {
                Console.WriteLine("Please provide valid input data.");
                return;
            }

            if (amount == 0)
            {
                Console.WriteLine("{0} {1} is 0 {2}", amount, baseCurrency, targetCurrency);
                return;
            }

            decimal? convertedAmount = await ConvertAmount(baseCurrency, targetCurrency, amount);

            if (convertedAmount != null)
            { 
                Console.WriteLine("{0:0.######} {1} is {2:N6} {3}", amount, baseCurrency, convertedAmount, targetCurrency);
            }
            
            Console.ReadKey();
        }

        public static async Task<decimal?> ConvertAmount(string baseCurr, string trgtCurr, decimal amount)
        {
            string API_KEY = "3bc595f0904ee949e2866c43f66f224d";
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://data.fixer.io/api/");
                    var response = await client.GetAsync("/latest?access_key=" + API_KEY
                                                            + "&base=" + baseCurr
                                                            + "&symbols=" + trgtCurr);
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ResponseRate>(stringResult);

                    if (result.Success)
                        return result.Rates[trgtCurr] * amount;
                    else
                    {
                        // to log result.Error.Info
                        Console.WriteLine("Error: {0} - {1}", result.Error.Code, result.Error.Type);
                        return null;
                    }
                }
                catch (HttpRequestException hrex)
                {
                    //add to log hrex.StackTrace
                    Console.WriteLine("Error: API calling failed");
                    return null;
                }
                catch (Exception ex)
                {
                    //add to log ex.Message, response.ToString(), DateTime.Now;
                    Console.WriteLine("Error: General API fail");
                    return null;
                }
            }
        }
    }
}
