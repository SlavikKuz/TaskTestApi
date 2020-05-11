using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskApi2d.Services
{
    public interface IDbService
    {
        List<ExchangeRate> GetFromTo(DateTime dateFrom, DateTime dateTo);
    }
}
