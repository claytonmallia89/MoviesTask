using Movies.Common.Constants;
using Movies.Common.Models.DTOs;

namespace Movies.Domain.Interfaces
{
    public interface IMovieService
    {
        Task<IReadOnlyCollection<MovieDTO>> GetMovies(string searchQuery, int? genreId, int pageSize,  int pageIndex );
        Task<IReadOnlyCollection<MovieDTO>> GetTop10Movies(int pageSize, int pageIndex);
    }
}