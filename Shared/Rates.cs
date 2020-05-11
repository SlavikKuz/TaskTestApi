using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shared
{
    public class Rates
    {
        public static async Task<ResponseRate> GetRate(string baseCurr, string trgtCurr, DateTime? date)
        {
            string optionalDate = date.HasValue ? date.Value.ToString("yyyy-MM-dd") : "latest";

            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://data.fixer.io/api/");

                    var response = await client.GetAsync("/" + optionalDate
                                                              + "?access_key=" + Constants.API_KEY
                                                              + "&base=" + baseCurr
                                                              + "&symbols=" + trgtCurr);

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ResponseRate>(stringResult);

                    if (result.Success)
                    {
                        return result;
                    }
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
