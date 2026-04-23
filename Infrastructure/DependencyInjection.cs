using Eventide.TournamentService.Domain.Interfaces;
using Eventide.TournamentService.Infrastructure.Data;
using Eventide.TournamentService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eventide.TournamentService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<TournamentDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("TournamentDb")));

        services.AddScoped<ITournamentRepository, TournamentRepository>();
        return services;
    }
}