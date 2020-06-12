namespace CurrencyExchange.Business.Models
{
    public class BaseModel
    {
        public bool success { get; set; }
        public object data { get; set; }
        public string message { get; set; }
        public int total { get; set; }
    }
}
