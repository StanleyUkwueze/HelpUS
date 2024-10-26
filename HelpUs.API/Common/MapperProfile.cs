using AutoMapper;
using HelpUs.API.DataTransferObjects.Requests;
using HelpUs.API.DataTransferObjects.Responses;
using HelpUs.API.Models;
using HelpUs.Service.DataTransferObjects.Responses;

namespace HelpUs.Service.Common
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserResponse>().ReverseMap();
            CreateMap<User, CreatedUserResponse>().ReverseMap();
            CreateMap<ProjectCreateDto, Project>().ReverseMap();
            CreateMap<ProjectEditDto, Project>().ReverseMap();
            CreateMap<ProjectResponse, Project>().ReverseMap();
        }
    }
}
