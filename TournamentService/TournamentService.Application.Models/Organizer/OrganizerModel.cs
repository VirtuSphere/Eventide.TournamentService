using TournamentService.Application.Models.Base;

namespace TournamentService.Application.Models.Organizer;

public record class OrganizerModel(Guid Id, string UserName) : IModel<Guid>;
