namespace Site.Web.Infrastructures.BusinessObjects
{
    public class PaymentRequest 
    {
        public int status { get; set; }
        public string token { get; set; }
        public double amount { get; set; }
        public string errorCode { get; set; }
        public string errorMessage { get; set; }
    }
}
