using CurrencyExchange.Business.Interfaces;
using CurrencyExchange.Business.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyExchange.Business.Services.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IAuditService _auditService;

        public CurrencyService(IAuditService auditService)
        {
            _auditService = auditService;
        }

        public async Task<decimal> Convert(string userName, decimal input, string fromCurrency, string toCurrency)
        {
            var currenctRate = GetRatio(fromCurrency, toCurrency);
            var result = Math.Round(input * currenctRate, 2);
            await _auditService.Add(new Models.AuditModel { UserName = userName, FromCurrency = fromCurrency, ToCurrency = toCurrency, InputAmmount = input, Rate = currenctRate, ResultAmount = result, AddedOn = DateTime.UtcNow });
            return result;
        }

        public IEnumerable<Currency> GetCurrencies()
        {
            return new List<Currency>
            {
                new Currency { Key ="GBP", Value="GBP" },
                new Currency { Key ="USD", Value="USD" },
                new Currency { Key ="AUD", Value="AUD" },
                new Currency { Key ="EUR", Value="EUR" }
            };
        }
        private decimal GetRatio(string fromCurrency, string toCurrency)
        {
            decimal result = (fromCurrency, toCurrency) switch
            {
                ("GBP", "USD") => 1.27m,
                ("GBP", "AUD") => 1.82m,
                ("GBP", "EUR") => 1.12m,
                ("USD", "GBP") => 1 / 1.27m,
                ("AUD", "GBP") => 1 / 1.82m,
                ("EUR", "GBP") => 1 / 1.12m,
                _ => 0.00m
            };
            return result;
        }

    }
}
