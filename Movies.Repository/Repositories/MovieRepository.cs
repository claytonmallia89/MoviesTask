using Movies.Repository.Database.Entities;
using Movies.Repository.Interfaces;
using Movies.Common.Extensions;
using Movies.Common.Constants;

namespace Movies.Repository.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly List<Movie> _movies;
        private readonly IGenreRepository _genreRepository;
        public MovieRepository( IGenreRepository genreRepository) {
            _genreRepository = genreRepository;
        /*
         * since there is no database in place i have to get the genre list first and then link them with the movie records .
         * the use of  StringComparison.CurrentCultureIgnoreCase is not need if reading from the database, since the where in SQL is not case sensitive unlike the where in the list
         */
            var _genreList = Task.Run(() => _genreRepository.GetAllGenre()).GetAwaiter().GetResult();
            #region Movies
        
            _movies = new List<Movie>()
            {
               new Movie()
               {
                    ID =  Guid.NewGuid(),
                    Title = "The Shawshank Redemption",
                    Year = 1994,
                    Genre = new List<Genre>()
                    {
                      _genreList.SingleOrDefault(x => x.Name.Equals("Crime", StringComparison.CurrentCultureIgnoreCase)),
                      _genreList.SingleOrDefault(x => x.Name.Equals("Drama", StringComparison.CurrentCultureIgnoreCase))
                    },
                    Director =  "Frank Darabont",
                    Actors= new List<Actor>() 
                    {
                        new Actor()
                        {
                            ID = Guid.NewGuid(),
                            Name = "Tim",
                            Surname = "Robbins"
                        },
                        new Actor()
                        {
                            ID= Guid.NewGuid(),
                            Name = "Morgan",
                            Surname = "Freeman"
                        }
                    },
                    Rating = 9.3f
               },
               new Movie()
               {
                   ID =  Guid.NewGuid(),
                   Title = "The Godfather",
                   Year = 1972,
                    Genre = new List<Genre>()
                    {
                      _genreList.SingleOrDefault(x => x.Name.Equals("Crime", StringComparison.CurrentCultureIgnoreCase)),
                      _genreList.SingleOrDefault(x => x.Name.Equals("Drama", StringComparison.CurrentCultureIgnoreCase))
                    },
                   Director = "Francis Ford Coppola",
                   Actors= new List<Actor>()
                   {
                       new Actor()
                        {
                             ID =  Guid.NewGuid(),
                             Name = "Marlon",
                             Surname = "Brando"
                       },
                       new Actor()
                        {
                             ID =  Guid.NewGuid(),
                              Name = "Al",
                              Surname = "Pacino"

                       },new Actor()
                        {
                             ID =  Guid.NewGuid(),
                             Name="James",
                             Surname= "Caan"
                       }
                   }, 
                   Rating = 9.2f
               },
               new Movie()
               {
                   ID =  Guid.NewGuid(),
                   Title = "The Dark Knight",
                   Year = 2008,
                   Genre = new List<Genre>()
                   {
                      _genreList.SingleOrDefault(x => x.Name.Equals("Crime", StringComparison.CurrentCultureIgnoreCase)),
                      _genreList.SingleOrDefault(x => x.Name.Equals("Drama", StringComparison.CurrentCultureIgnoreCase)),
                      _genreList.SingleOrDefault(x => x.Name.Equals("Action", StringComparison.CurrentCultureIgnoreCase))
                   },
                   Director = "Christopher Nolan",
                   Actors= new List<Actor>()
                   {
                        new Actor()
                        {
                            ID =  Guid.NewGuid(),
                            Name = "Christian",
                            Surname = "Bale"
                        },
                        new Actor()
                        {
                            ID =  Guid.NewGuid(),
                            Name = "Heath",
                            Surname = "Ledger"
                        },
                       new Actor()
                        {
                            ID =  Guid.NewGuid(),
                            Name = "Gary",
                            Surname = "Oldman"
                        }                          
                    },
                   Rating = 9.0f
               },
               new Movie()
               {
                   ID =  Guid.NewGuid(),
                   Title = "Inception",
                   Year = 2010,
                   Genre = new List<Genre>()
                   {
                      _genreList.SingleOrDefault(x => x.Name.Equals("Action", StringComparison.CurrentCultureIgnoreCase)),
                      _genreList.SingleOrDefault(x => x.Name.Equals("Adventure", StringComparison.CurrentCultureIgnoreCase)),
                      _genreList.SingleOrDefault(x => x.Name.Equals("Sci-Fi", StringComparison.CurrentCultureIgnoreCase))
                   },
                   Director = "Christopher Nolan",
                   Actors= new List<Actor>()
                   {
                       new Actor()
                       {
                            ID =  Guid.NewGuid(),
                            Name = "Leonardo",
                            Surname = "DiCaprio"
                       },
                       new Actor()
                       {
                            ID =  Guid.NewGuid(),
                            Name = "Joseph",
                            Surname = "Gordon-Levitt"
                       },
                       new Actor()
                       {
                            ID =  Guid.NewGuid(),
                            Name = "Ellen",
                            Surname = "Page"
                       }
                   },
                   Rating = 8.8f
               }
        };
            #endregion
        }

        public async Task<IEnumerable<Movie>> GetMovies(string searchQuery, int? genreId, int pageSize , int pageIndex)
        {
            //Contains has the StringComparison.InvariantCultureIgnoreCase because since we are querying from a list, the contains is case sensitive
            var filteredList = _movies
                                    .Where(x =>
                                      (string.IsNullOrWhiteSpace(searchQuery) || x.Title.Contains(searchQuery, StringComparison.InvariantCultureIgnoreCase)) &&
                                      (!genreId.HasValue || x.Genre.Any(genre => genre.ID == genreId.Value)))
                                    .OrderBy(x => x.Title).Page(pageSize,pageIndex);

            return await Task.FromResult(filteredList);
        }

        public async Task<IEnumerable<Movie>> GetTop10Movies(int pageSize , int pageIndex)
        {
            var top10Movies = _movies
                    .OrderByDescending(x => x.Rating)
                    .Take(10)
                    .Page(pageSize, pageIndex); 
                   
            return await Task.FromResult(top10Movies);
        }
    }
}