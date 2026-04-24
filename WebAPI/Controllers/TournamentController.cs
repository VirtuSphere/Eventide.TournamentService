using Eventide.TournamentService.Application.Commands.CreateTournament;
using Eventide.TournamentService.Application.Commands.PublishTournament;
using Eventide.TournamentService.Application.Commands.RegisterParticipant;
using Eventide.TournamentService.Application.Queries.GetUpcomingTournaments;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Eventide.TournamentService.Application.Commands.CloseRegistration;
namespace Eventide.TournamentService.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TournamentController : ControllerBase
{
    private readonly IMediator _mediator;

    public TournamentController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTournamentCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ErrorMessage);
    }

    [HttpPost("{id}/register")]
    public async Task<IActionResult> Register(Guid id, [FromBody] Guid userId)
    {
        var result = await _mediator.Send(new RegisterParticipantCommand { TournamentId = id, UserId = userId });
        return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
    }

    [HttpGet("upcoming")]
    public async Task<IActionResult> GetUpcoming([FromQuery] int skip = 0, [FromQuery] int take = 20)
    {
        var result = await _mediator.Send(new GetUpcomingTournamentsQuery { Skip = skip, Take = take });
        return Ok(result.Value);
    }
    [HttpPost("{id}/close")]
    public async Task<IActionResult> CloseRegistration(Guid id)
    {
        var result = await _mediator.Send(new CloseRegistrationCommand { TournamentId = id });
        return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
    }
    [HttpPost("{id}/publish")]
    public async Task<IActionResult> Publish(Guid id)
    {
        var tournament = await _mediator.Send(new PublishTournamentCommand { TournamentId = id });
        return Ok();
    }
}