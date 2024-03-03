using Movies.Common.Constants;
using Movies.Repository.Database.Entities;

namespace Movies.Repository.Interfaces
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetMovies(string searchQuery, int? genreId, int pageSize , int pageIndex );
        Task<IEnumerable<Movie>> GetTop10Movies(int pageSize, int pageIndex);
    }
}