using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Movies.Common.Models.DTOs;
using Movies.Domain.Interfaces;

namespace Movies.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : Controller
    {
        private readonly IGenreService _genreService;
        private readonly ILogger _logger;
        public GenresController(
            IGenreService genreService,
            ILogger<GenresController> logger)
        {
            _genreService = genreService;
            _logger = logger;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        [Route("")]
        [OutputCache(PolicyName = "DropdownListPolicy")]
        public async Task<IEnumerable<GenreDTO>> GetAllGenres()
        {
            try
            {
                return await _genreService.GetAllGenre();
            } catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, ex.Message);
                return new List<GenreDTO>();
            }
        }
    }
}
