using AutoMapper;
using NetCore.Template.DTOs;
using NetCore.Template.Entities;

namespace NetCore.Template.Repositories
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<MyEntity, MyEntityDto>(MemberList.None);
            CreateMap<MyEntityDto, MyEntity>(MemberList.None);
        }
    }
}