using System.Security.Claims;
using Api.Responses;
using Application.DTOs.StudentsDto;
using Application.Results;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.CenterAdminsControllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "CenterAdmin")]
public class StudentController : ControllerBase
{
    protected readonly StudentRepository studentRepository;

    protected readonly GroupRepository groupRepository;

    protected int CenterId;

    protected readonly IMapper mapper;

    public StudentController(
        StudentRepository studentRepository,
        IMapper mapper,
        GroupRepository groupRepository
    )
    {
        this.studentRepository = studentRepository;
        this.mapper = mapper;
        this.groupRepository = groupRepository;
        CenterId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        PagedResult<Student> students = await studentRepository.GetAllAsync(1, 50);
        return Ok(students);
    }

    [HttpPost]
    public async Task<IActionResult> CreateASync(NewStudentDto dto)
    {
        Student student = mapper.Map<Student>(dto);

        var groups = await groupRepository.GetAllAsyncByIds(dto.Groups);

        student.Groups = groups;
        if (groups.Count is 0)
        {
            return UnprocessableEntity();
        }
        await studentRepository.CreateAsync(student);
        return Created();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        Student? student = await studentRepository.GetAsync(id);

        if (student is null)
        {
            return NotFound();
        }
        return Ok(student);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, UpdatedStudentDto dto)
    {
        Student? dbStudent = await studentRepository.GetAsync(id);

        if (dbStudent is null)
            return NotFound();

        mapper.Map(dto, dbStudent);

        await studentRepository.UpdateAsync(dbStudent);
        return Ok(new ApiResponse<string[]>());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        Student? student = await studentRepository.GetAsync(id);

        if (student is null)
        {
            return NotFound();
        }

        await studentRepository.DeleteAsync(student);
        return Ok(new ApiResponse<string[]>());
    }
}
