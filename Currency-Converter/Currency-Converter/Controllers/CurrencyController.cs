using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.Controllers
{
    public class CurrencyController : Controller
    {
        public IActionResult Converter()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Convert(decimal amount, string fromCurrency, string toCurrency)
        {
            // Here you would implement the logic to convert the currency
            // For demonstration purposes, let's just return a dummy result
            decimal convertedAmount = 50 * amount; // Dummy conversion
            ViewBag.ConvertedAmount = convertedAmount;
            ViewBag.FromCurrency = fromCurrency;
            ViewBag.ToCurrency = toCurrency;

            return View("Converter");
        }
    }
}
