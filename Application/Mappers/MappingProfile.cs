using Application.DTOs.CenterDTOs;
using Application.DTOs.GroupDTOs;
using Application.DTOs.SciencesDTOs;
using Application.DTOs.StudentsDto;
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

        CreateMap<TeacherDto, User>();

        //    CreateMap<UpdateCenterAdminDto, CenterAdmin>();

        CreateMap<NewGroupDto, Group>();
        CreateMap<UpdatedGroupDto, Group>();

        CreateMap<NewStudentDto, Student>()
            .ForMember(dest => dest.Groups, option => option.Ignore());

        CreateMap<UpdatedStudentDto, Student>()
            .ForMember(dest => dest.Groups, option => option.Ignore());
    }
}
