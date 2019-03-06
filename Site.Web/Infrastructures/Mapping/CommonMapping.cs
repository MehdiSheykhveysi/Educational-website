using AutoMapper;
using Site.Core.Domain.Entities;
using Site.Web.Areas.User.Models.WalletModels;
using Site.Web.Infrastructures.BusinessObjects;
using Site.Web.Models.PagesModels;

namespace Site.Web.Infrastructures.Mapping
{
    public class CommonMapping : Profile
    {
        public CommonMapping()
        {
            CreateMap<VerifyResponse, VerifyViewModel>();
            CreateMap<AdminCreateModel, CustomUser>().ForMember(c => c.Avatar, a => a.MapFrom(m => m.FormFile.FileName));
            // CreateMap<WalletTransactViewModel, PayInput>().ForMember(p => p.Deposits, w => w.MapFrom(c => c.Deposits));
            //CreateMap<CustomUser, PayInput>();
        }
    }
}
