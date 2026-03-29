using TournamentService.ValueObjects.Base;
using TournamentService.ValueObjects.Validators;
namespace TournamentService.ValueObjects;

/// <summary>
/// Represents type of the entity's username.
/// </summary>
/// <param name="name">The username of the entity.</param>
public class UserName(string name) : ValueObject<string>(new UserNameValidator(), name);