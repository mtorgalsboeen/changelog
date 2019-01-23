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
using ChangelogTest.Models;

namespace ChangelogTest.Controllers
{
    /// <summary>
    /// Controller for Changelogs.
    /// </summary>
    public class ChangelogsController : ApiController
    {
        private FinalModel db = new FinalModel();

        // GET: api/Changelogs
        /// <summary>
        /// Gets all Changelogs
        /// </summary>
        /// <returns>A list of Changelogs.</returns>
        public IQueryable<Changelog> GetChangelogs()
        {
            db.Configuration.LazyLoadingEnabled = false;

            return db.Changelogs;
        }

        // GET: api/Changelogs/5
        /// <summary>
        /// Gets the Changelog with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the Changelog</param>
        /// <returns>An HTTP result of type Changelog</returns>
        [ResponseType(typeof(Changelog))]
        public async Task<IHttpActionResult> GetChangelog(int id)
        {
            Changelog changelog = await db.Changelogs.FindAsync(id);
            if (changelog == null)
            {
                return NotFound();
            }

            return Ok(changelog);
        }

        // PUT: api/Changelogs/5
        /// <summary>
        /// Updates the Changelog with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the Changelog.</param>
        /// <param name="changelog">A Changelog object.</param>
        /// <returns>A HTTP result.</returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutChangelog(int id, Changelog changelog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != changelog.ChangelogID)
            {
                return BadRequest();
            }

            db.Entry(changelog).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChangelogExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Changelogs
        /// <summary>
        /// Inserts a new Changelog.
        /// </summary>
        /// <param name="changelog">A Changelog object.</param>
        /// <returns>An HTTP result of type Changelog</returns>
        [ResponseType(typeof(Changelog))]
        public async Task<IHttpActionResult> PostChangelog(Changelog changelog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Changelogs.Add(changelog);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = changelog.ChangelogID }, changelog);
        }

        // DELETE: api/Changelogs/5
        /// <summary>
        /// Deletes the Changelog with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the Changelog</param>
        /// <returns>An HTTP respose of type Changelog</returns>
        [ResponseType(typeof(Changelog))]
        public async Task<IHttpActionResult> DeleteChangelog(int id)
        {
            Changelog changelog = await db.Changelogs.FindAsync(id);
            if (changelog == null)
            {
                return NotFound();
            }

            db.Changelogs.Remove(changelog);
            await db.SaveChangesAsync();

            return Ok(changelog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChangelogExists(int id)
        {
            return db.Changelogs.Count(e => e.ChangelogID == id) > 0;
        }
    }
}