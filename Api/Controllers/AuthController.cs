using Api.Responses;
using Application.Abstracts;
using Application.DTOs.AuthDto;
using Application.ErrorResponse;
using Application.Services;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;

    private readonly AuthRepository authRepository;

    private readonly IPasswordHasher passwordHasher;

    public AuthController(
        TokenService tokenService,
        AuthRepository authRepository,
        IPasswordHasher passwordHasher
    )
    {
        _tokenService = tokenService;
        this.authRepository = authRepository;
        this.passwordHasher = passwordHasher;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto request)
    {
        User? user = await authRepository.GetUser(request.Username);

        if (user is not null && passwordHasher.Verify(request.Password, user.Password))
        {
            var token = _tokenService.GenerateToken(
                user.Id.ToString(),
                user.Username,
                user.Role.ToString(),
                user.Center?.Id ?? 0
            );
            return Ok(new ApiResponse<object>(new { Token = token }));
        }

        return Unauthorized(new BadRequest() { Message = "Login yoki parol xato" });
    }
}
