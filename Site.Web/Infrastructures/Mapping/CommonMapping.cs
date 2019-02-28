using AutoMapper;
using Site.Web.Areas.User.Models.WalletModels;
using Site.Web.Infrastructures.BusinessObjects;

namespace Site.Web.Infrastructures.Mapping
{
    public class CommonMapping : Profile
    {
        public CommonMapping()
        {
            CreateMap<VerifyResponse, VerifyViewModel>();
            // CreateMap<WalletTransactViewModel, PayInput>().ForMember(p => p.Deposits, w => w.MapFrom(c => c.Deposits));
            //CreateMap<CustomUser, PayInput>();
        }
    }
}
