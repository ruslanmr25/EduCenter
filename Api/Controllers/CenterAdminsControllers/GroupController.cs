using Api.Responses;
using Application.DTOs.GroupDTOs;
using Application.Filters;
using Application.Results;
using AutoMapper;
using Domain.Entities;
using Domain.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers.CenterAdminsControllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "CenterAdmin")]
public class GroupController : ControllerBase
{
    protected readonly GroupRepository groupRepository;

    protected readonly PaymentRepository paymentRepository;
    protected readonly IMapper mapper;

    public GroupController(
        GroupRepository groupRepository,
        IMapper mapper,
        PaymentRepository paymentRepository
    )
    {
        this.groupRepository = groupRepository;
        this.mapper = mapper;
        this.paymentRepository = paymentRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] GroupFilter filter)
    {
        var centerId = int.Parse(User.FindFirstValue("centerId")!);

        var groups = await groupRepository.GetAllAsync(
            centerId: centerId,
            filter.Page,
            filter.PageSize
        );

        return Ok(new ApiResponse<PagedResult<Group>>(groups));
    }

    [HttpPost]
    public async Task<IActionResult> Create(NewGroupDto dto)
    {
        var centerId = int.Parse(User.FindFirstValue("centerId")!);

        Group group = mapper.Map<Group>(dto);
        group.CenterId = centerId;

        group = await groupRepository.CreateAsync(group);

        return Ok(new ApiResponse<Group>(group));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var centerId = int.Parse(User.FindFirstValue("centerId")!);

        Group? group = await groupRepository.GetAsync(id, centerId);

        if (group is null)
        {
            return NotFound();
        }

        return Ok(new ApiResponse<Group>(group));
    }

    [HttpGet("{id}/sycle")]
    public async Task<IActionResult> GetGroupSycleModel(int id)
    {
        var centerId = int.Parse(User.FindFirstValue("centerId")!);

        List<StudentPaymentRowModel>? model = await groupRepository.GroupPaymentModel(id, centerId);

        if (model is null)
        {
            return NotFound();
        }

        return Ok(new ApiResponse<List<StudentPaymentRowModel>>(model));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdatedGroupDto dto)
    {
        var centerId = int.Parse(User.FindFirstValue("centerId")!);

        Group? dbGroup = await groupRepository.GetAsync(id, centerId);

        if (dbGroup is null)
        {
            return NotFound();
        }

        mapper.Map(dto, dbGroup);

        await groupRepository.UpdateAsync(dbGroup);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var centerId = int.Parse(User.FindFirstValue("centerId")!);

        var entity = await groupRepository.GetAsync(id, centerId);
        if (entity is null)
        {
            return NotFound(
                new ApiResponse<string[]>([], success: false, message: "Hech narsa topilmadi")
            );
        }

        await groupRepository.DeleteAsync(entity);
        return Ok(new ApiResponse<string[]>());
    }

    [HttpPut("{id}/change-status")]
    public async Task<IActionResult> ChangeActiveStatus(int id, UpdateGroupActiveStateDto dto)
    {
        var centerId = int.Parse(User.FindFirstValue("centerId")!);

        Group? group = await groupRepository.GetAsync(id, centerId);

        if (group is null)
        {
            return NotFound();
        }

        group.IsActive = dto.IsActive;

        await groupRepository.UpdateAsync(group);
        return Ok(new ApiResponse<string[]>());
    }
}
