using System.ComponentModel.DataAnnotations;

namespace Movies.Repository.Database.Entities
{
    public class Actor
    {
        [Key]
        public Guid ID { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
    }
}