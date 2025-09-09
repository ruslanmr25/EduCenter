using Application.DTOs.CenterDTOs;
using Application.DTOs.SciencesDTOs;
using Application.DTOs.UserDTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<NewCenterAdminDto, User>();

        CreateMap<NewCenterDTO, Center>();
        CreateMap<UpdatedCenterDTO, Center>();

        CreateMap<UpdatedCenterAdminDto, User>();
        CreateMap<NewScienceDto, Science>();
        CreateMap<UpdatedScienceDto, Science>();

        //    CreateMap<UpdateCenterAdminDto, CenterAdmin>();
    }
}
