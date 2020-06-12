using CurrencyExchange.Business.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyExchange.Business.Interfaces
{
    public interface ICurrencyService
    {
        Task<decimal> Convert(string userName, decimal input, string fromCurrency, string toCurrency);
        IEnumerable<Currency> GetCurrencies();
    }
}
