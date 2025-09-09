using Application.DTOs.CenterDTOs;
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

        //    CreateMap<UpdateCenterAdminDto, CenterAdmin>();
    }
}
