using AutoMapper;
using TournamentService.Application.Models.Bracket;
using TournamentService.Application.Models.Organizer;
using TournamentService.Application.Models.Tournament;
using TournamentService.Domain;

namespace TournamentService.Application.Services.Mapping;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<Bracket, BracketModel>();

        CreateMap<Organizer, OrganizerModel>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName.Value));

        CreateMap<Tournament, TournamentModel>()
            .ForMember(dest => dest.OrganizerId, opt => opt.MapFrom(src => src.Organizer.Id))
            .ForMember(dest => dest.TournamentName, opt => opt.MapFrom(src => src.TournamentName.Value))
            .ForMember(dest => dest.Game, opt => opt.MapFrom(src => src.Game.Value))
            .ForMember(dest => dest.MaxTeams, opt => opt.MapFrom(src => src.MaxTeams.Value))
            .ForMember(dest => dest.PrizePool, opt => opt.MapFrom(src => src.PrizePool.Value))
            .ForMember(dest => dest.BracketId, opt => opt.MapFrom(src => src.Bracket.Id))
            .ForMember(dest => dest.RegisteredTeamsCount, opt => opt.MapFrom(src => src.RegisteredTeamsCount.Value));
    }
}