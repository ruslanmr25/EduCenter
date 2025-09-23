using Api.Responses;
using Application.Abstracts;
using Application.DTOs.UserDTOs;
using Application.Results;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.SuperAdminsControllers;

[Route("api/super-admin/users")]
[ApiController]
[Authorize(Roles = "SuperAdmin")]
public class UserController : ControllerBase
{
    protected UserRepository userRepository;

    protected IMapper mapper;

    protected IPasswordHasher passwordHasher;

    public UserController(
        UserRepository userRepository,
        IMapper mapper,
        IPasswordHasher passwordHasher
    )
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
        this.passwordHasher = passwordHasher;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int page = 1, int pageSize = 50)
    {
        var users = await userRepository.GetAllAsync(page, pageSize);
        return Ok(new ApiResponse<PagedResult<User>>(users));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCenterAdmin(NewCenterAdminDto centerAdminDto)
    {
        var user = mapper.Map<User>(centerAdminDto);
        user.Password = passwordHasher.Hash(user.Password);
        user.Role = Role.CenterAdmin;

        var entity = await userRepository.CreateAsync(user);

        return Created();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Show(int id)
    {
        User? user = await userRepository.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }
        return Ok(new ApiResponse<User>(user));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdatedCenterAdminDto adminDto)
    {
        User? dbUser = await userRepository.GetAsync(id);
        if (dbUser is null)
        {
            return NotFound();
        }

        mapper.Map(adminDto, dbUser);

        dbUser.Role = Role.CenterAdmin;

        await userRepository.UpdateAsync(dbUser);

        return Ok(new ApiResponse<string[]>());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await userRepository.GetAsync(id);
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
