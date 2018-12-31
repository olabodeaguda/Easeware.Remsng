using AutoMapper;
using Easeware.Remsng.Common.Models;
using Easeware.Remsng.Entities.Entities;

namespace Easeware.Remsng.Entities
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<LcdaModel, Lcda>().ReverseMap();
            CreateMap<VerificationDetailModel, VerificationDetail>(MemberList.None).ReverseMap();
            CreateMap<UserModel, User>(MemberList.None).ReverseMap()
                .ForMember(c => c.ConfirmPassword, c => c.Ignore())
                .ForMember(c => c.Password, c => c.Ignore());
            CreateMap<UserLcdaModel, UserLcda>(MemberList.None).ReverseMap();
            CreateMap<WardModel, Ward>(MemberList.None).ReverseMap();
            CreateMap<SectorModel, Sector>(MemberList.None).ReverseMap();
            CreateMap<CompanyModel, Company>(MemberList.None).ReverseMap();
        }
    }
}
