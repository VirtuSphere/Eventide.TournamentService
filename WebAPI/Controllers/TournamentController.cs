using Eventide.TournamentService.Application.Commands.CreateTournament;
using Eventide.TournamentService.Application.Commands.RegisterParticipant;
using Eventide.TournamentService.Application.Queries.GetUpcomingTournaments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
}