using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;
using Movies.Common;
using Movies.Common.Models.DTOs;
using Movies.Domain.Interfaces;
using Movies.Domain.Services;
using Movies.Repository.Interfaces;
using Movies.Repository.Repositories;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Movies.Lambda;

public class MoviesFunction
{
    #region Dependency Injection
    private static void ConfigureServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IGenreRepository, GenreRepository>();
        serviceCollection.AddScoped<IMovieRepository, MovieRepository>();

        serviceCollection.AddScoped<IGenreService, GenreService>();
        serviceCollection.AddScoped<IMovieService, MovieService>();

    }
    #endregion

    #region Function Handlers
    /// <summary>
    /// Returns a list of Genre
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task<IEnumerable<GenreDTO>> GenreFunctionHandler(ILambdaContext context)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        // create service provider
        var serviceProvider = serviceCollection.BuildServiceProvider();

        return await serviceProvider.GetService<IGenreService>().GetAllGenre();
    }

    /// <summary>
    /// Returns a list of movies
    /// </summary>
    /// <param name="parameters"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task<IEnumerable<MovieDTO>> MovieFunctionHandler(QueryParameter parameters, ILambdaContext context)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        // create service provider
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var movieService = serviceProvider.GetService<IMovieService>();

        //default get top 10 movies
        if (string.IsNullOrWhiteSpace(parameters.SearchQuery) && parameters.GenreId == default(int?))
        {
            return await movieService.GetTop10Movies(parameters.PageSize, parameters.PageIndex);
        }

        return await movieService.GetMovies(parameters.SearchQuery.Trim(), parameters.GenreId, parameters.PageSize, parameters.PageIndex);
    }
    #endregion
}