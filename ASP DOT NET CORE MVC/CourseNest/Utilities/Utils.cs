using System.Security.Cryptography;
using System.Text;

namespace CourseNest.Utilities
{
    public class Utilss
    {
        public static bool VerifyPaymentSignature(Dictionary<string, string> attributes, string secret)
        {
            string payload = attributes["razorpay_order_id"] + "|" + attributes["razorpay_payment_id"];
            string actualSignature = attributes["razorpay_signature"];

            using (HMACSHA256 hmac = new HMACSHA256(Encoding.ASCII.GetBytes(secret)))
            {
                byte[] hash = hmac.ComputeHash(Encoding.ASCII.GetBytes(payload));
                string expectedSignature = BitConverter.ToString(hash).Replace("-", "").ToLower();

                return expectedSignature.Equals(actualSignature);
            }
        }

        public static bool VerifyPaymentSignature(Dictionary<string, string> attributes)
        {
            string paymentId = attributes["razorpay_payment_id"];
            string orderId = attributes["razorpay_order_id"];
            string signature = attributes["razorpay_signature"];

            // Your Razorpay secret key
            string secret = "your_secret_key";

            // Generate the expected signature
            string generatedSignature = GenerateSignature(paymentId, orderId, secret);

            return signature == generatedSignature;
        }

        private static string GenerateSignature(string paymentId, string orderId, string secret)
        {
            // Generate a signature using your secret and the payment details
            // This is just a placeholder, replace with actual implementation
            string data = $"{paymentId}|{orderId}";
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret)))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return Convert.ToBase64String(hash);
            }
        }
    }
}
