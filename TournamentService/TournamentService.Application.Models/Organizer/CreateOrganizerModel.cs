using TournamentService.Application.Models.Base;
namespace TournamentService.Application.Models.Organizer;

public record class CreateOrganizerModel(string UserName) : ICreateModel;
