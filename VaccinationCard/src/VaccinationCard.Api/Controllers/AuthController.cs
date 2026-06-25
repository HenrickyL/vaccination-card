using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VaccinationCard.Application.UseCases.Auth;
using VaccinationCard.Application.UseCases.Login;

namespace VaccinationCard.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
[AllowAnonymous] // Not Authenticated
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Register a new patient
    /// </summary>
    /// <param name="command">Registration data</param>
    /// <returns>User data with JWT token</returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(Register), new { id = result.UserId }, result);
    }

    /// <summary>
    /// Login to the system
    /// </summary>
    /// <param name="command">Login credentials</param>
    /// <returns>User data with JWT token</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
