using System.Security.Claims;
using Api.Responses;
using Application.DTOs.StudentsDto;
using Application.Results;
using AutoMapper;
using Common.Queries;
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

    protected readonly PaymentRepository paymentRepository;

    protected readonly GroupRepository groupRepository;

    protected readonly IMapper mapper;

    public StudentController(
        StudentRepository studentRepository,
        IMapper mapper,
        GroupRepository groupRepository,
        PaymentRepository paymentRepository
    )
    {
        this.studentRepository = studentRepository;
        this.mapper = mapper;
        this.groupRepository = groupRepository;
        this.paymentRepository = paymentRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] StudentQuery query)
    {
        var centerId = int.Parse(User.FindFirstValue("centerId")!);
        PagedResult<Student> students = await studentRepository.GetAllAsync(centerId, query);
        return Ok(new ApiResponse<PagedResult<Student>>(students));
    }

    [HttpPost]
    public async Task<IActionResult> CreateASync(NewStudentDto dto)
    {
        var centerId = int.Parse(User.FindFirstValue("centerId")!);

        Student student = mapper.Map<Student>(dto);

        var groups = await groupRepository.GetAllAsyncByIds(dto.Groups);

        if (groups.Count is 0)
        {
            return UnprocessableEntity();
        }

        student.CenterId = centerId;

        student = await studentRepository.CreateAsync(student);

        await paymentRepository.CreateGroupStudentPaymentAsync(groups, student);

        return Ok(new ApiResponse<Student>(student));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var centerId = int.Parse(User.FindFirstValue("centerId")!);

        Student? student = await studentRepository.GetAsync(id, centerId);

        if (student is null)
        {
            return NotFound();
        }
        return Ok(new ApiResponse<Student>(student));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, UpdatedStudentDto dto)
    {
        var centerId = int.Parse(User.FindFirstValue("centerId")!);

        Student? dbStudent = await studentRepository.GetAsync(id, centerId);

        if (dbStudent is null)
            return NotFound();

        mapper.Map(dto, dbStudent);

        await studentRepository.UpdateAsync(dbStudent);
        return Ok(new ApiResponse<string[]>());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var centerId = int.Parse(User.FindFirstValue("centerId")!);

        Student? student = await studentRepository.GetAsync(id, centerId);

        if (student is null)
        {
            return NotFound();
        }

        await studentRepository.DeleteAsync(student);
        return Ok(new ApiResponse<string[]>());
    }
}
