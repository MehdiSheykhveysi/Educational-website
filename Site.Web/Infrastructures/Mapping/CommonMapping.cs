using AutoMapper;
using Site.Core.Domain.Entities;
using Site.Web.Infrastructures.BusinessObjects;

namespace Site.Web.Infrastructures.Mapping
{
    public class CommonMapping : Profile
    {
        public CommonMapping()
        {
           // CreateMap<WalletTransactViewModel, PayInput>().ForMember(p => p.Deposits, w => w.MapFrom(c => c.Deposits));
            //CreateMap<CustomUser, PayInput>();
        }
    }
}
