namespace Site.Web.Infrastructures.BusinessObjects
{
    public class VerifyResponse
    {
        public string Status { get; set; }
        public string TransId { get; set; }
        public string Amount { get; set; }
        public string Message { get; set; }
        public string Mobile { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
        public string CardNumber { get; set; }
        public string Description { get; set; }
        public string FactorNumber { get; set; }
    }
}
