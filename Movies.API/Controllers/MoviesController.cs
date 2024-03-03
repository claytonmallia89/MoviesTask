using Microsoft.AspNetCore.Mvc;
using Movies.Common;
using Movies.Common.Models.DTOs;
using Movies.Domain.Interfaces;

namespace Movies.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ILogger _logger;
        public MoviesController(
            IMovieService movieService,
            ILogger<MoviesController> logger )
        {
            _movieService = movieService;
            _logger = logger;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpGet]
        [Route("")]
        public async Task<IReadOnlyCollection<MovieDTO>> GetMovies([FromQuery] QueryParameter parameters)
        {
            try
            {               
                //default get top 10 movies
                if (string.IsNullOrWhiteSpace(parameters.SearchQuery) && parameters.GenreId == default(int?))
                {
                    return await _movieService.GetTop10Movies(parameters.PageSize,parameters.PageIndex);
                }

                return await _movieService.GetMovies(parameters.SearchQuery.Trim(), parameters.GenreId, parameters.PageSize,parameters.PageIndex);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, ex.Message);
                return new List<MovieDTO>();
            }
        }
    }
}