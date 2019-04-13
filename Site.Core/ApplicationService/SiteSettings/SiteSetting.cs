namespace Site.Core.ApplicationService.SiteSettings
{
    public class SiteSetting
    {
        public string DefaultConnection { get; set; }
        public string Api { get; set; }
        public string GatewaySend { get; set; }
        public string GatewayResult { get; set; }
        public string RedirectUrl { get; set; }
        public string CallBackUrl { get; set; }
    }
}
