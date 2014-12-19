using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSDemo.Persistence
{
    public class Movie
    {
        public Movie() { }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int Year { get; set; }
        public decimal Price { get; set; }
        public string Genre { get; set; }

        [Required]
        public int DirectorId { get; set; }

        // Navigation property
        public virtual Director Director { get; set; }
    }
}
