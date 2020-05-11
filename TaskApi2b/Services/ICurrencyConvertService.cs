using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskApi2b.Services
{
    public interface ICurrencyConvertService
    {
        Task<decimal> Convert(string baseCurrency, string targetCurrency, decimal amount, DateTime? date);
    }
}
