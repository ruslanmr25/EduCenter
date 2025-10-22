using System.Security.Claims;
using Api.Responses;
using Application.DTOs.GroupDTOs;
using Application.Filters;
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
public class GroupController : ControllerBase
{
    protected readonly GroupRepository groupRepository;
    protected readonly IMapper mapper;

    public GroupController(GroupRepository groupRepository, IMapper mapper)
    {
        this.groupRepository = groupRepository;
        this.mapper = mapper;
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

        await groupRepository.CreateAsync(group);

        return Created();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var centerId = int.Parse(User.FindFirstValue("centerId")!);

        Group? group = await groupRepository.GetAsync(id);

        if (group is null)
        {
            return NotFound();
        }

        return Ok(new ApiResponse<Group>(group));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdatedGroupDto dto)
    {
        Group? dbGroup = await groupRepository.GetAsync(id);

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

        var entity = await groupRepository.GetAsync(id);
        if (entity is null)
        {
            return NotFound(
                new ApiResponse<string[]>([], success: false, message: "Hech narsa topilmadi")
            );
        }

        await groupRepository.DeleteAsync(entity);
        return Ok(new ApiResponse<string[]>());
    }
}
