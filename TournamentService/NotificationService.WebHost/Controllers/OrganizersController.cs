using Microsoft.AspNetCore.Mvc;
using TournamentService.Application.Models.Organizer;
using TournamentService.Application.Services.Abstractions;

namespace TournamentService.WebHost.Controllers;

[ApiController]
[Route("api/organizers")]
public sealed class OrganizersController(IOrganizerApplicationService service) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OrganizerModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<OrganizerModel>>> GetAll(
        CancellationToken cancellationToken)
        => Ok(await service.GetModelsAsync(cancellationToken));

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(OrganizerModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrganizerModel>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var organizer = await service.GetModelByIdAsync(id, cancellationToken);
        return organizer is null ? NotFound() : Ok(organizer);
    }

    [HttpGet("by-username/{userName}")]
    [ProducesResponseType(typeof(OrganizerModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrganizerModel>> GetByUserName(
        string userName,
        CancellationToken cancellationToken)
    {
        var organizer = await service.GetOrganizerByUsernameAsync(userName, cancellationToken);
        return organizer is null ? NotFound() : Ok(organizer);
    }

    [HttpPost]
    [ProducesResponseType(typeof(OrganizerModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<OrganizerModel>> Create(
        CreateOrganizerModel model,
        CancellationToken cancellationToken)
    {
        var organizer = await service.CreateModelAsync(model, cancellationToken);
        if (organizer is null)
            return Conflict("An organizer with this username already exists.");

        return CreatedAtAction(nameof(GetById), new { id = organizer.Id }, organizer);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update(
        Guid id,
        CreateOrganizerModel model,
        CancellationToken cancellationToken)
    {
        var updated = await service.UpdateModelAsync(
            new OrganizerModel(id, model.UserName),
            cancellationToken);

        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        => await service.DeleteModelAsync(id, cancellationToken)
            ? NoContent()
            : NotFound();
}
