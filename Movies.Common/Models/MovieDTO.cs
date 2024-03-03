using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Common.Models
{
    public class MovieDTO
    {
        public required string Title { get; init; }
        public required ushort Year { get; init; }
        public IEnumerable<string> Genre { get; init; }
        public required string Director { get; init; }
        public IEnumerable<string> Actors { get; init; }
        public float Rating { get; init; }
    }
}