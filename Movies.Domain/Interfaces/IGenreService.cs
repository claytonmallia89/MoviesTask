using Movies.Common.Models.DTOs;

namespace Movies.Domain.Interfaces
{
    public interface IGenreService
    {
        Task<IReadOnlyCollection<GenreDTO>> GetAllGenre();
    }
}