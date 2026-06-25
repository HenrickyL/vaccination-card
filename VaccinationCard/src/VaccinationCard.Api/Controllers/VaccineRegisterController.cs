using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VaccinationCard.Application.UseCases.VaccinationRegisters.Create;
using VaccinationCard.Application.UseCases.VaccinationRegisters.List;

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

    /// <summary>
    /// Get vaccination card for a specific patient (Admin/Employee only)
    /// </summary>
    /// <param name="patientId">Patient ID</param>
    /// <returns>Complete patient vaccination card with summary</returns>
    [HttpGet("patient/{patientId}")]
    [Authorize(Roles = "Patient")]
    [ProducesResponseType(typeof(PatientVaccinationCardResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetByPatient(Guid patientId)
    {
        var query = new PatientVaccinationCardCommand { PatientId = patientId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
