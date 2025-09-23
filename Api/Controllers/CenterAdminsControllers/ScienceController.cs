using System.Security.Claims;
using Api.Responses;
using Application.DTOs.SciencesDTOs;
using Application.Results;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.scienceAdminsControllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "CenterAdmin")]
public class ScienceController : ControllerBase
{
    protected ScienceRepository scienceRepository;

    protected IMapper mapper;
    protected int centerId;

    public ScienceController(ScienceRepository scienceRepository, IMapper mapper)
    {
        this.scienceRepository = scienceRepository;
        this.mapper = mapper;

        centerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }

    [HttpGet]
    public async Task<IActionResult> Index(int page = 1, int pageSize = 50)
    {
        var science = await scienceRepository.GetAllAsync(centerId: centerId, page, pageSize);
        return Ok(new ApiResponse<PagedResult<Science>>(science));
    }

    [HttpGet("{id}")]
    public virtual async Task<IActionResult> Show(int id)
    {
        Science? science = await scienceRepository.GetAsync(centerId, id);

        if (science is null)
        {
            return NotFound();
        }
        return Ok(new ApiResponse<Science>(science));
    }

    [HttpPost]
    public async Task<IActionResult> Create(NewScienceDto scienceDto)
    {
        Science science = mapper.Map<Science>(scienceDto);

        science.CenterId = centerId;

        await scienceRepository.CreateAsync(science);
        // science science = ;
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdatedScienceDto scienceDTO)
    {
        Science? dbScience = await scienceRepository.GetAsync(id);
        if (dbScience is null)
        {
            return NotFound();
        }

        mapper.Map(scienceDTO, dbScience);

        await scienceRepository.UpdateAsync(dbScience);

        return Ok(new ApiResponse<string[]>());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await scienceRepository.GetAsync(id);
        if (entity is null)
        {
            return NotFound(
                new ApiResponse<string[]>([], success: false, message: "Hech narsa topilmadi")
            );
        }

        await scienceRepository.DeleteAsync(entity);
        return Ok(new ApiResponse<string[]>());
    }
}
