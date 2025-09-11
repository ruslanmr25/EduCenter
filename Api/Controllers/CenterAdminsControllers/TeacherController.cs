using Api.Responses;
using Application.Abstracts;
using Application.DTOs.UserDTOs;
using Application.Results;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.CenterAdminsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        protected readonly IMapper mapper;

        protected readonly IPasswordHasher passwordHasher;

        protected CenterRepository centerRepository;

        protected readonly UserRepository userRepository;
#warning centerni o'zgartir
        protected readonly int CenterId = 1;

        public TeacherController(
            IMapper mapper,
            UserRepository userRepository,
            IPasswordHasher passwordHasher,
            CenterRepository centerRepository
        )
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
            this.centerRepository = centerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 50)
        {
            var teachers = await userRepository.GetAllTeacher(centerId: CenterId, page, pageSize);
            return Ok(new ApiResponse<PagedResult<User>>(teachers));
        }

        [HttpPost]
        public async Task<IActionResult> CrateTeacher(TeacherDto teacherDto)
        {
            Center center =
                await centerRepository.GetAsync(CenterId)
                ?? throw new NullReferenceException("Center Id must be not null");

            var teacher = mapper.Map<User>(teacherDto);

            teacher.Centers = [center];
            teacher.Password = passwordHasher.Hash(teacher.Password);

            teacher.Role = Role.Teacher;

            var entity = await userRepository.CreateAsync(teacher);

            return Created();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Show(int id)
        {
            User? teacher = await userRepository.GetTeacherAsync(centerId: CenterId, id);

            if (teacher is null)
            {
                return NotFound();
            }
            return Ok(new ApiResponse<User>(teacher));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TeacherDto teacher)
        {
            User? dbUser = await userRepository.GetTeacherAsync(CenterId, id);
            if (dbUser is null)
            {
                return NotFound();
            }

            mapper.Map(teacher, dbUser);

            dbUser.Role = Role.Teacher;

            await userRepository.UpdateAsync(dbUser);

            return Ok(new ApiResponse<string[]>());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await userRepository.GetTeacherAsync(CenterId, id);
            if (entity is null)
            {
                return NotFound(
                    new ApiResponse<string[]>([], success: false, message: "Hech narsa topilmadi")
                );
            }

            await userRepository.DeleteAsync(entity);
            return Ok(new ApiResponse<string[]>());
        }
    }
}
