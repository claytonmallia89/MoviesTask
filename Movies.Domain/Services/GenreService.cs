using Movies.Common.Models.DTOs;
using Movies.Domain.Interfaces;
using Movies.Repository.Interfaces;

namespace Movies.Domain.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }
        public async Task<IReadOnlyCollection<GenreDTO>> GetAllGenre()
        {
            var genreList = await _genreRepository.GetAllGenre();

            //Would use automapper here to do the mapping automatically
            var genreDtoList = genreList.Select(x => new GenreDTO()
            {
                ID = x.ID,
                Name = x.Name,
            }).ToList();

            return await Task.FromResult(genreDtoList);
        }
    }
}