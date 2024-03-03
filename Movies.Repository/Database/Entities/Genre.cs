using System.ComponentModel.DataAnnotations;

namespace Movies.Repository.Database.Entities
{
    public class Genre
    {
        [Key]
        public int ID { get; set; }
        public required string Name { get; set; }
    }
}