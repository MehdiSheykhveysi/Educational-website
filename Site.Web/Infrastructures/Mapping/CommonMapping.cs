using AutoMapper;
using Site.Core.Domain.Entities;
using Site.Web.Areas.User.Models.WalletModels;
using Site.Web.Infrastructures.BusinessObjects;
using Site.Web.Models.PagesModels;
using Site.Web.Models.PagesModels.CourseEpisodManageModel;
using Site.Web.Models.PagesModels.CourseManageModel;
using Site.Web.Models.PagesModels.RoleManageModel;
using System.Collections.Generic;
using System.Linq;

namespace Site.Web.Infrastructures.Mapping
{
    public class CommonMapping : Profile
    {
        public CommonMapping()
        {
            CreateMap<VerifyResponse, VerifyViewModel>();

            //UserManagement Mapping
            CreateMap<AdminCreateModel, CustomUser>().ForMember(c => c.Avatar, a => a.MapFrom(m => m.FormFile.FileName)).ForMember(c => c.EmailConfirmed, a => a.MapFrom(m => m.IsActive)).ForMember(c => c.UserName, a => a.MapFrom(m => m.Email));
            CreateMap<AdminEditModel, CustomUser>().ForMember(u => u.EmailConfirmed, a => a.MapFrom(m => m.IsActive)).ReverseMap().ForMember(a => a.IsActive, u => u.MapFrom(m => m.EmailConfirmed));
            CreateMap<AdminDeleteModel, CustomUser>().ForMember(u => u.EmailConfirmed, a => a.MapFrom(m => m.IsActive)).ReverseMap().ForMember(a => a.IsActive, u => u.MapFrom(m => m.EmailConfirmed));
            CreateMap<AdminDetailModel, CustomUser>().ForMember(u => u.EmailConfirmed, a => a.MapFrom(m => m.IsActive)).ReverseMap().ForMember(a => a.IsActive, u => u.MapFrom(m => m.EmailConfirmed));
            CreateMap<List<TransactModel>, List<Transact>>();


            //RoleManagement Mapping 
            CreateMap<List<RoleManageModel>, List<Role>>();
            CreateMap<Role, RoleDetailModel>();
            CreateMap<Role, RoleEditModel>();
            CreateMap<Role, RoleDeleteModel>().ReverseMap();


            //CourseManagement Mapping
            CreateMap<CourseCreateVm, Course>();
            CreateMap<List<CourseGroupVm>, List<CourseGroup>>();
            CreateMap<List<CourseLevelVm>, List<CourseLevel>>();
            CreateMap<List<CourseLevelVm>, IList<CustomUser>>();
            CreateMap<List<CourseVm>, List<Course>>();
            CreateMap<Course, CourseEditVm>().ReverseMap();//.ForMember(c => c.Keywords, a => a.MapFrom(m => m.Keywordkeys.Select(k => k.Title)));
            CreateMap<Course, CourseDeleteVm>();

            //CourseEpisod  Mapping
            CreateMap<List<EpisodFullBaseVm>, List<CourseEpisod>>();
        }
    }
}