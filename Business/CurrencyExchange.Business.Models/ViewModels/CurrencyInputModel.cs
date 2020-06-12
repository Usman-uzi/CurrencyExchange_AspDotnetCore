using System.Collections.Generic;
using System.ComponentModel;

namespace CurrencyExchange.Business.Models.ViewModels
{
    public class CurrencyInputModel
    {
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [DisplayName("Input")]
        public decimal? GbpValue { get; set; }

        public decimal Result { get; set; }

        public IEnumerable<Currency> Currencies { get; set; }

        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
    }

    public class Currency
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
