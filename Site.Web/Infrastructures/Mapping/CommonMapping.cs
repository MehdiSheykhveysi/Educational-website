using AutoMapper;
using Site.Core.Infrastructures;
using Site.Web.Areas.User.Models.WalletModels;

namespace Site.Web.Infrastructures.Mapping
{
    public class CommonMapping : Profile
    {
        public CommonMapping()
        {
            CreateMap<WalletTransactViewModel, PayInput>().ForMember(p => p.Deposits, w => w.MapFrom(c => c.Deposits));
        }
    }
}
