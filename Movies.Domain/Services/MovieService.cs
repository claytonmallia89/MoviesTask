using Movies.Common.Constants;
using Movies.Common.Helper;
using Movies.Common.Models.DTOs;
using Movies.Domain.Interfaces;
using Movies.Repository.Database.Entities;
using Movies.Repository.Interfaces;


namespace Movies.Domain.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;       

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public async Task<IReadOnlyCollection<MovieDTO>> GetMovies(string searchQuery, int? genreId, int pageSize, int pageIndex )
        {
            var movies = await _movieRepository.GetMovies( searchQuery,  genreId,  pageSize,  pageIndex);

            // would have used auto mapper to do the mapping automatically
            var filteredList = movies.Select(movie => new MovieDTO()
                                      {  
                                        Title = movie.Title,
                                        Actors = GetActorsFullNames(movie.Actors),
                                        Director = movie.Director,
                                        Year = movie.Year,
                                        Rating = movie.Rating,
                                        Genre = movie.Genre.Select(x => x.Name).ToList(),
                                        })
                                    .ToList();

            return await Task.FromResult(filteredList);
        }

        /// <summary>
        /// Get Top 10 movies. The order of the movies is by the rating descending
        /// </summary>
        /// <returns></returns>
        public async Task<IReadOnlyCollection<MovieDTO>> GetTop10Movies(int pageSize, int pageIndex)
        {
            var movies = await _movieRepository.GetTop10Movies(pageSize,pageIndex);
            // would have used automapper to do the mapping automatically
            var moviesDTO = movies
                    .Select(movie => new MovieDTO()
                    { 
                        Title = movie.Title,
                        Actors = GetActorsFullNames(movie.Actors),
                        Director = movie.Director,
                        Year = movie.Year,
                        Rating = movie.Rating,
                        Genre = movie.Genre.Select(x => x.Name).ToList(),
                    })
                    .ToList();

            return await Task.FromResult(moviesDTO);
        }

        //This method would have been replaces by the automapper configurations
        IEnumerable<string> GetActorsFullNames(IEnumerable<Actor> actors) => actors.Select(actor => StringHelper.GetFullname(actor.Name, actor.Surname));
     
    }
}