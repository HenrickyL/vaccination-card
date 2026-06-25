using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VaccinationCard.Application.UseCases.Vaccines.Create;
using VaccinationCard.Application.UseCases.Vaccines.Delete;
using VaccinationCard.Application.UseCases.Vaccines.Lis;
using VaccinationCard.Application.UseCases.Vaccines.List;

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
        /// Create a new vaccine
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


        /// <summary>
        /// list all vaccine
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ListVaccinesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListAll()
        {
            ///TODO: Add pagination
            var result = await _mediator.Send(new ListVaccinesCommand());
            return Ok(result);
        }

        /// <summary>
        /// Delete a vaccine (Employee only)
        /// </summary>
        /// <param name="id">Vaccination record ID</param>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Employee")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteVaccineCommand { Id = id });
            return NoContent();
        }

    }
}
