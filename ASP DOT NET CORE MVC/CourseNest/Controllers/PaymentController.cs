using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;
using CourseNest.Utilities;
using Microsoft.AspNetCore.Authorization;
using CourseNest.Models.DTOs;

namespace CourseNest.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IConfiguration _configuration;

        public PaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index(int amount=100)
        {
            // Convert amount to paise
            var amountInPaise = amount * 100;

            // Create the payment order
            var paymentModel = new PaymentViewModel
            {
                Key = _configuration["Razorpay:Key"],
                OrderId = CreateOrder(amountInPaise),
                Amount = amountInPaise,
                Name = "User Name",
                Email = "user@example.com",
                Contact = "9999999999"
            };

            return View(paymentModel);
        }


        private string CreateOrder(int amount)
        {
            // Logic to create an order using Razorpay API and return the OrderId
            var client = new RazorpayClient(_configuration["Razorpay:Key"], _configuration["Razorpay:Secret"]);
            var options = new Dictionary<string, object>
        {
            { "amount", amount * 100 }, // Razorpay expects amount in paise
            { "currency", "INR" },
            { "receipt", "order_rcptid_11" },
            { "payment_capture", 1 }
        };

            var order = client.Order.Create(options);
            return order["id"].ToString();
        }

        //href="/Payment/Index?amount=@totalAmount

        //[HttpGet]
        public IActionResult Complete()
        {
            ViewBag.Message = "Your payment was successful. Thank you for your purchase!";
            return View();
        }

        [HttpPost]
        public IActionResult Success(string razorpay_payment_id, string razorpay_order_id, string razorpay_signature)
        {
            var attributes = new Dictionary<string, string>
        {
            { "razorpay_payment_id", razorpay_payment_id },
            { "razorpay_order_id", razorpay_order_id },
            { "razorpay_signature", razorpay_signature }
        };

            bool isSignatureValid = Utilss.VerifyPaymentSignature(attributes);

            if (isSignatureValid)
            {
                return RedirectToAction("PaymentComplete", "Payment");
            }
            else
            {
                return RedirectToAction("Failed", "Payment");
            }
        }
        [HttpGet]
        public IActionResult Failed()
        {
            ViewBag.Message = "Payment failed. Please try again.";
            return View();
        }

    }
}
