using Movies.Repository.Database.Entities;
using Movies.Repository.Interfaces;

namespace Movies.Repository.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly List<Genre> _genreList;

        public GenreRepository() 
        {
            #region Populate list
            //Since there is not database, we have to first assign the Genre first so that the movies list can be assigned with the Genre from the _genreList
            _genreList = new List<Genre>()
            {
                  new Genre()
                  {
                      ID = 1,
                      Name = "Action"
                  },
                  new Genre()
                  {
                      ID = 2,
                      Name = "Drama"
                  },
                  new Genre()
                  {
                      ID= 3,
                      Name  = "Comedy"
                  },
                  new Genre()
                  {
                      ID = 4,
                      Name ="Adventure"
                  },
                  new Genre()
                  {
                      ID = 5,
                      Name = "Sci-Fi"
                  },
                    new Genre()
                  {
                      ID = 6,
                      Name = "Crime"
                  }
            };
            #endregion
        }

        public async Task<IEnumerable<Genre>> GetAllGenre()
        {
            var filteredList = _genreList.OrderBy(x => x.Name).ToList();

            return await Task.FromResult(filteredList);
        }
    }
}