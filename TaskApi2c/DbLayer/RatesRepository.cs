using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskApi2c.DbLayer
{
    public class RatesRepository : IRatesRepository
    {
        private readonly TaskDbContext _dbContext;

        public RatesRepository(TaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ExchangeRate> GetFromTo(int timestampFrom, int timestampTo)
        {
            return _dbContext.ExchangeRates.Where(e => e.Timestamp >= timestampFrom
                  && e.Timestamp < timestampTo).ToList();
        }
    }
}
