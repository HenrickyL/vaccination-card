using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VaccinationCard.Application.UseCases.VaccinationRegisters.Create;

namespace VaccinationCard.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize]
public class VaccineRegisterController : ControllerBase
{
    private readonly IMediator _mediator;

    public VaccineRegisterController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Register a vaccination (Employee or Admin only)
    /// </summary>
    [HttpPost]
    //[Authorize(Roles = "Admin,Employee")]
    [ProducesResponseType(typeof(CreateVaccinationRegisterResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Register([FromBody] CreateVaccinationRegisterCommand command)
    {
        var result = await _mediator.Send(command);
        return Created(string.Empty, result);
    }
}
