using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PaymentSchedule.Models;

namespace PaymentSchedule.Controllers
{
    public class HomeController : Controller
    {
        readonly IOptions<FeeOptions> feeOptions;

        public HomeController(IOptions<FeeOptions> feeOptions)
        {
            this.feeOptions = feeOptions;
        }

        [HttpGet]
        public ViewResult NewLoan()
        {
            return View();
        }

        [HttpPost]
        public ViewResult NewLoan(LoanCalculationInput input)
        {
            if (ModelState.IsValid)
            {
                return View("PaymentSummary", new PaymentSummary(input, feeOptions.Value));
            }

            return View();
        }
    }
}
