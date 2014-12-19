using AngularJSDemo.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AngularJSDemo.MvcWebApi.Models
{
    public class MovieInitializer : DropCreateDatabaseIfModelChanges<MovieContext>
    {
        protected override void Seed(MovieContext context)
        {
            var directors = new List<Director>
            {
                new Director() { Id = 1, Name = "Tim Burton" },
                new Director() { Id = 2, Name = "Christopher Nolan" },
                new Director() { Id = 3, Name = "Michael Bay" }
            };

            directors.ForEach(d => context.Directors.Add(d));
            context.SaveChanges();

            var movies = new List<Movie>
            {
                new Movie()
                {
                    Id = 1,
                    Title = "Corpse Bride",
                    Year = 2005,
                    DirectorId = 1,
                    Price = 18M,
                    Genre = "Animation"
                },
                new Movie()
                {
                    Id = 2,
                    Title = "Batman Begins",
                    Year = 1817,
                    DirectorId = 2,
                    Price = 100M,
                    Genre = "Action"
                },
                new Movie()
                {
                    Id = 3,
                    Title = "Gravity",
                    Year = 2014,
                    DirectorId = 2,
                    Price = 200M,
                    Genre = "Space"
                },
                new Movie()
                {
                    Id = 4,
                    Title = "Transformers 3",
                    Year = 2014,
                    DirectorId = 3,
                    Price = 200M,
                    Genre = "Robots"
                },
                new Movie()
                {
                    Id = 5,
                    Title = "47 Ronin",
                    Year = 2014,
                    DirectorId = 3,
                    Price = 200M,
                    Genre = "Asian"
                },
                new Movie()
                {
                    Id = 6,
                    Title = "Ice Age",
                    Year = 1999,
                    DirectorId = 1,
                    Price = 18M,
                    Genre = "Animation"
                },
                new Movie()
                {
                    Id = 7,
                    Title = "Gone in sixty seconds",
                    Year = 1808,
                    DirectorId = 1,
                    Price = 60M,
                    Genre = "Action"
                }
            };

            movies.ForEach(m => context.Movies.Add(m));
            context.SaveChanges();
        }
    }
}