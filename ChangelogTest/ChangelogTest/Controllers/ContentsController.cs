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
    /// The controller for the Content of the Changelogs.
    /// </summary>
    public class ContentsController : ApiController
    {
        private FinalModel db = new FinalModel();

        // PUT: api/Contents/5
        /// <summary>
        /// Updates the Content with the specified id.
        /// </summary>
        /// <param name="id">The ID of the Content</param>
        /// <param name="content">A Content object.</param>
        /// <returns>An HTTP result.</returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutContent(int id, Content content)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != content.ContentID)
            {
                return BadRequest();
            }

            db.Entry(content).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContentExists(id))
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

        // POST: api/Contents
        /// <summary>
        /// Inserts a new instance of Content.
        /// </summary>
        /// <param name="content">A Content object.</param>
        /// <returns>An HTTP response of type Content.</returns>
        [ResponseType(typeof(Content))]
        public async Task<IHttpActionResult> PostContent(Content content)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Contents.Add(content);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = content.ContentID }, content);
        }

        // DELETE: api/Contents/5
        /// <summary>
        /// Deletes the instance of Content with the specified ID.
        /// </summary>
        /// <param name="id">The Contents ID.</param>
        /// <returns>An HTTP response of type Content.</returns>
        [ResponseType(typeof(Content))]
        public async Task<IHttpActionResult> DeleteContent(int id)
        {
            Content content = await db.Contents.FindAsync(id);
            if (content == null)
            {
                return NotFound();
            }

            db.Contents.Remove(content);
            await db.SaveChangesAsync();

            return Ok(content);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContentExists(int id)
        {
            return db.Contents.Count(e => e.ContentID == id) > 0;
        }
    }
}