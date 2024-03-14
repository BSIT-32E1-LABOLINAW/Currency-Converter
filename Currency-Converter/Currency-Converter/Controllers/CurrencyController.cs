using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CurrencyConverter.Controllers
{
    public class CurrencyController : Controller
    {
        public IActionResult Converter()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Convert(decimal amount, string fromCurrency, string toCurrency)
        {
            // Your API key from ExchangeRate-API
            string apiKey = "62c72379a1-4a8c137e67-sacb6p\r\n\r\n";

            // Construct the URL to fetch exchange rates
            string apiUrl = $"https://v6.exchangeratesapi.io/latest?base={fromCurrency}&symbols={toCurrency}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Parse the JSON response to get exchange rates
                    dynamic exchangeData = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);
                    decimal exchangeRate = exchangeData.rates[toCurrency];

                    // Perform currency conversion
                    decimal convertedAmount = amount * exchangeRate;

                    // Pass converted amount and other data to the view
                    ViewBag.ConvertedAmount = convertedAmount;
                    ViewBag.FromCurrency = fromCurrency;
                    ViewBag.ToCurrency = toCurrency;

                    return View("Converter");
                }
                catch (HttpRequestException ex)
                {
                    // Handle any errors from API request
                    ViewBag.ErrorMessage = "Failed to fetch exchange rates. Please try again later.";
                    return View("Converter");
                }
                catch (Exception ex)
                {
                    // Handle any other unexpected errors
                    ViewBag.ErrorMessage = "An unexpected error occurred.";
                    return View("Converter");
                }
            }
        }
    }
}
