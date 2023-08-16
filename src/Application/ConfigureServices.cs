using System.Reflection;
using FluentValidation;
using MediatR;
using MvDb.Application.Common.Behaviours;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Application.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        });

        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<IAgeRatingService, AgeRatingService>();
        services.AddScoped<IActorService, ActorService>();
        services.AddScoped<IDirectorService, DirectorService>();
        services.AddScoped<IEpisodeService, EpisodeService>();
        services.AddScoped<ISeasonService, SeasonService>();
        services.AddScoped<IMediaService, MediaService>();

        services.AddScoped<ISearchService, SearchService>();

        return services;
    }
}
