using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApplication5.Data;

namespace WebApplication5.Models.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //
            CreateMap<User, UserView>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForPath(dest => dest.Role.Id, opt => opt.MapFrom(src => src.Role.Id))
                .ForPath(dest => dest.Role.role, opt => opt.MapFrom(src => src.Role.RoleName));
                //.ForPath(dest => dest.Role, opt => opt.MapFrom(src => src.Role.RoleName));
            CreateMap<Role, RoleView>()
                .ForMember(dest => dest.role, opt => opt.MapFrom(src => src.RoleName));
            CreateMap<Permission, PermissionView>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.PermissionName));

            //
            CreateMap<UserView, User>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForPath(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
                //.ForPath(dest => dest.Role.RoleName, opt => opt.MapFrom(src => src.Role)); ;
            CreateMap<RoleView, Role>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.role));
            CreateMap<PermissionView, Permission>()
                .ForMember(dest => dest.PermissionName, opt => opt.MapFrom(src => src.Name));
        }
    }
}
