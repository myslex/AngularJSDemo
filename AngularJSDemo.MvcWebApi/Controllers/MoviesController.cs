using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AngularJSDemo.MvcWebApi.Models;
using AngularJSDemo.Persistence;
using AngularJSDemo.MvcWebApi.Models.ViewModels;

namespace AngularJSDemo.MvcWebApi.Controllers
{
    public class MoviesController : ApiController
    {
        private MovieContext db = new MovieContext();

        // GET: api/Movies
        public IQueryable<Movie> GetMovies()
        {
            return db.Movies
                .Include(m => m.Director);
        }

        // GET: api/Movies/5
        [ResponseType(typeof(Movie))]
        public async Task<IHttpActionResult> GetMovie(int id)
        {
            var movie = await db.Movies
                .Include(m => m.Director)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(movie);
        }

        // PUT: api/Movies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMovie(int id, Movie movie)
        {
            var ct = Request.Headers.GetValues("CommandType").SingleOrDefault();
            if (ct != "UpdateMovie")
                return BadRequest("CommandType");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != movie.Id)
                return BadRequest();

            db.Entry(movie).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                    return NotFound();

                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Movies
        [ResponseType(typeof(Movie))]
        public async Task<IHttpActionResult> PostMovie(Movie movie)
        {
            var ct = Request.Headers.GetValues("CommandType").SingleOrDefault();
            if (ct != "AddMovie")
                return BadRequest("CommandType");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            db.Movies.Add(movie);
            await db.SaveChangesAsync();

            // ..
            db.Entry(movie).Reference(d => d.Director).Load();

            return CreatedAtRoute("DefaultApi", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movies/5
        [ResponseType(typeof(Movie))]
        public async Task<IHttpActionResult> DeleteMovie(int id)
        {
            var ct = Request.Headers.GetValues("CommandType").SingleOrDefault();
            if (ct != "DeleteMovie")
                return BadRequest("CommandType");

            var movie = await db.Movies.SingleOrDefaultAsync(m => m.Id == id);
            if (movie == null)
                return NotFound();

            db.Movies.Remove(movie);
            await db.SaveChangesAsync();

            return Ok(movie);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }

        private bool MovieExists(int id)
        {
            return db.Movies.Count(e => e.Id == id) > 0;
        }
    }
}