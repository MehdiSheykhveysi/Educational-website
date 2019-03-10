using AutoMapper;
using Site.Core.Domain.Entities;
using Site.Web.Areas.User.Models.WalletModels;
using Site.Web.Infrastructures.BusinessObjects;
using Site.Web.Models.PagesModels;
using System.Collections.Generic;

namespace Site.Web.Infrastructures.Mapping
{
    public class CommonMapping : Profile
    {
        public CommonMapping()
        {
            CreateMap<VerifyResponse, VerifyViewModel>();
            CreateMap<AdminCreateModel, CustomUser>().ForMember(c => c.Avatar, a => a.MapFrom(m => m.FormFile.FileName));
            CreateMap<List<RoleModel>, List<Role>>();
            //CreateMap<List<Role>, List<RoleModel>>();
        }
    }
}