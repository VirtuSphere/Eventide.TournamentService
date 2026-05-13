
using TournamentService.ValueObjects.Base;
using TournamentService.ValueObjects.Validators;
namespace TournamentService.ValueObjects;

public class MaxTeams(byte val) : ValueObject<byte>(new MaxTeamsValidator(), val);