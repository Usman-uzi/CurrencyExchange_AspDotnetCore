using CurrencyExchange.Data.Models.Core;
using System.ComponentModel.DataAnnotations;

namespace CurrencyExchange.Data.Models
{
    public class Audit : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [StringLength(30)]
        public string UserName { get; set; }
        public decimal InputAmmount { get; set; }
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal Rate { get; set; }
        public decimal ResultAmount { get; set; }
    }
}
