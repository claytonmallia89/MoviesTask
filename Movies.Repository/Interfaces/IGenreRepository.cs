using Movies.Repository.Database.Entities;

namespace Movies.Repository.Interfaces
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetAllGenre();
    }
}