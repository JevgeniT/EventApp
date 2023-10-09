using EventApp.DTO;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace EventApp.Controllers;
    
[ApiController]
[Route("[controller]")]
public class IdentityController : ControllerBase
{  
    private readonly IIdentityService  _identityService;

    public IdentityController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    /// <summary>
    /// Authenticates user
    /// </summary>
    /// <param name="loginDto">Dto with email and password</param>
    /// <returns>JWT token</returns>
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
            var jwtToken= await _identityService.Login(loginDto);
            
            return Ok(new {jwt = jwtToken});
    }
}

