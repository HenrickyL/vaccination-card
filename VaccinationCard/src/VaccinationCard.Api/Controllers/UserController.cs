using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VaccinationCard.Application.UseCases.Users;

namespace VaccinationCard.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    /// <summary>
    /// List all users with their patient information (SuperAdmin or Admin only)
    /// </summary>
    [HttpGet("users")]
    [ProducesResponseType(typeof(IEnumerable<ListAllUsersResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> ListAllUsers()
    {
        var result = await _mediator.Send(new ListAllUsersCommand());
        return Ok(result);
    }
}
