// AuthController.cs
using Contract;
using ENTITYAPP.Dto;
using ENTITYAPP.DTO;
using ENTITYAPP.Service;
using ENTITYAPP.Service.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[AllowAnonymous]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IJWTAuthenticationManager _IJWTAuthenticationManager;
    private readonly UserService _userService;

    public AuthController(IJWTAuthenticationManager IJWTAuthenticationManager, UserService userService)
    {
        _IJWTAuthenticationManager = IJWTAuthenticationManager;
        _userService = userService;
    }

    [HttpPost]
    [Route("api/signup")]
    public async Task AddUser(UserDto user)
    {
        await _userService.CreateUser(user);
    }

    [HttpPost("api/generateToken")]
    public IActionResult GenerateToken([FromBody] GenerateTokenRequest userDetail)
    {
        var token = _IJWTAuthenticationManager.Authenticate(userDetail.email, userDetail.password);

        if (token == null)
        {
            return Unauthorized();
        }

        return Ok(new { Token = token });
    }
}
public class GenerateTokenRequest
{
    public string email { get; set; }
    public string password { get; set; }

}
