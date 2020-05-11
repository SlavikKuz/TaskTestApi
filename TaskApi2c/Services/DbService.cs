using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApi2c.DbLayer;

namespace TaskApi2c.Services
{
    public class DbService : IDbService
    {
        private readonly IRatesRepository _ratesRepository;

        public DbService(IRatesRepository rateRepository)
        {
            _ratesRepository = rateRepository;
        }

        public List<ExchangeRate> GetFromTo(DateTime dateFrom, DateTime dateTo)
        {
            return _ratesRepository.GetFromTo(GetTimestampFrom(dateFrom), GetTimestampTo(dateTo));
        }

        //timestamp from 00:00:00 that day
        public static int GetTimestampFrom(DateTime dateFrom)
        {
            return (int)(dateFrom.Date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        //timestamp from 00:00:00 next day
        public static int GetTimestampTo(DateTime dateTo)
        {
            return (int)(dateTo.Date.AddDays(1).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

    }
}
