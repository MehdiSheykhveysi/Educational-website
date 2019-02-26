namespace Site.Web.Infrastructures.BusinessObjects
{
    public class PaymentRequest 
    {
        public int Status { get; set; }
        public string Token { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}