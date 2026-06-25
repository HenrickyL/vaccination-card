using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VaccinationCard.Application.UseCases.Vaccines.Create;

namespace VaccinationCard.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class VaccineController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VaccineController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new vaccine (Admin only)
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Employee")]
        [ProducesResponseType(typeof(CreateVaccineResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Create([FromBody] CreateVaccineCommand command)
        {
            var result = await _mediator.Send(command);
            return Created(string.Empty, result);
        }

    }
}
