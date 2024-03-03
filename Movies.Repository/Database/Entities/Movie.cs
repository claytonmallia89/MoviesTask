using System.ComponentModel.DataAnnotations;

namespace Movies.Repository.Database.Entities
{
    public class Movie
    {
        [Key]
        public required Guid ID { get; set; }
        public required string Title { get; set; }
        public ushort Year { get; set; }
        public  IEnumerable<Genre> Genre { get; set; }
        public required string Director { get; set; }
        public IEnumerable<Actor> Actors { get; set; }
        public required float Rating { get; set; }
    }
}