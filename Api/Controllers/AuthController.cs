using Application.Abstracts;
using Application.DTOs.AuthDto;
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
        // Bu yerda foydalanuvchini DB’dan tekshirish kerak

        User? user = await authRepository.GetUser(request.Username);

        if (user is not null && passwordHasher.Verify(request.Password, user.Password))
        {
            var token = _tokenService.GenerateToken("1", user.Username, user.Role.ToString());
            return Ok(new { token });
        }

        return Unauthorized("Invalid credentials");
    }
}
