using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskApi2d.DbLayer
{
    public interface IRatesRepository
    {
        List<ExchangeRate> GetFromTo(int timestampFrom, int timestampTo);
    }
}
