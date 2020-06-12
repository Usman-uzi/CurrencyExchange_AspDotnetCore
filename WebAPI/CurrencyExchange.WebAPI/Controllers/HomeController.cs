using CurrencyExchange.Business.Interfaces;
using CurrencyExchange.Business.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CurrencyExchange.WebAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICurrencyService _currencyService;
        private readonly IAuditService _auditService;

        public HomeController(ICurrencyService currencyService, IAuditService auditService)
        {
            _currencyService = currencyService;
            _auditService = auditService;
        }

        public IActionResult Index()
        {
            var model = new CurrencyInputModel
            {
                Currencies = _currencyService.GetCurrencies()
            };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CurrencyInputModel model)
        {
            if (model.GbpValue == null)
                return Content("You should provide valid input!");
            if (model.FromCurrency == model.ToCurrency)
                return Content("You should select different currencies!");
            var result = await _currencyService.Convert(model.UserName, model.GbpValue.Value, model.FromCurrency, model.ToCurrency);
            model.Result = result;
            return Content($"Your converted value is : {model.Result}");
        }

        public async Task<IActionResult> Logs(DateTime? startdate, DateTime? enddate)
        {
            var models = await _auditService.GetFiltered(startdate, enddate);
            return View(models);
        }
    }
}
