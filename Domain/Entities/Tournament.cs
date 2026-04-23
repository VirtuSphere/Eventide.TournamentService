using Eventide.TournamentService.Domain.Enums;
using Eventide.TournamentService.Domain.Exceptions;

namespace Eventide.TournamentService.Domain.Entities;

public class Tournament
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Game { get; private set; }
    public TournamentFormat Format { get; private set; }
    public TournamentStatus Status { get; private set; }
    public Guid OrganizerId { get; private set; }
    public DateTime RegistrationStart { get; private set; }
    public DateTime RegistrationEnd { get; private set; }
    public DateTime TournamentStart { get; private set; }
    public int MaxParticipants { get; private set; }
    public int MaxTeamSize { get; private set; }
    public List<Guid> ParticipantIds { get; private set; } = new();
    public Guid? BracketId { get; private set; }
    public string? Rules { get; private set; }
    public string? Prize { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Tournament() { }

    public static Tournament Create(string name, string game, TournamentFormat format, Guid organizerId,
        DateTime registrationStart, DateTime registrationEnd, DateTime tournamentStart,
        int maxParticipants, int maxTeamSize)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new DomainException("Tournament name required");
        if (registrationEnd <= registrationStart) throw new DomainException("Registration end must be after start");
        if (tournamentStart <= registrationEnd) throw new DomainException("Tournament must start after registration ends");
        if (maxParticipants < 2) throw new DomainException("Minimum 2 participants");

        return new Tournament
        {
            Id = Guid.NewGuid(),
            Name = name,
            Game = game,
            Format = format,
            Status = TournamentStatus.Draft,
            OrganizerId = organizerId,
            RegistrationStart = registrationStart,
            RegistrationEnd = registrationEnd,
            TournamentStart = tournamentStart,
            MaxParticipants = maxParticipants,
            MaxTeamSize = maxTeamSize,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Publish() => Status = TournamentStatus.RegistrationOpened;

    public void RegisterParticipant(Guid userId)
    {
        if (Status != TournamentStatus.RegistrationOpened) throw new DomainException("Registration is closed");
        if (ParticipantIds.Count >= MaxParticipants) throw new DomainException("Tournament is full");
        if (ParticipantIds.Contains(userId)) throw new DomainException("Already registered");
        ParticipantIds.Add(userId);
    }

    public void SetBracket(Guid bracketId)
    {
        BracketId = bracketId;
        Status = TournamentStatus.BracketGenerated;
    }

    public void Start() => Status = TournamentStatus.InProgress;
    public void Complete() => Status = TournamentStatus.Completed;
    public void Cancel() => Status = TournamentStatus.Cancelled;
}