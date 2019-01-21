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
    public class ContentTypesController : ApiController
    {
        private FinalModel db = new FinalModel();

        // GET: api/ContentTypes
        public IQueryable<ContentType> GetContentTypes()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.ContentTypes;
        }

        // GET: api/ContentTypes/5
        [ResponseType(typeof(ContentType))]
        public async Task<IHttpActionResult> GetContentType(int id)
        {
            ContentType contentType = await db.ContentTypes.FindAsync(id);
            if (contentType == null)
            {
                return NotFound();
            }

            return Ok(contentType);
        }

        // PUT: api/ContentTypes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutContentType(int id, ContentType contentType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contentType.ContentTypeID)
            {
                return BadRequest();
            }

            db.Entry(contentType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContentTypeExists(id))
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

        // POST: api/ContentTypes
        [ResponseType(typeof(ContentType))]
        public async Task<IHttpActionResult> PostContentType(ContentType contentType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ContentTypes.Add(contentType);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = contentType.ContentTypeID }, contentType);
        }

        // DELETE: api/ContentTypes/5
        [ResponseType(typeof(ContentType))]
        public async Task<IHttpActionResult> DeleteContentType(int id)
        {
            ContentType contentType = await db.ContentTypes.FindAsync(id);
            if (contentType == null)
            {
                return NotFound();
            }

            db.ContentTypes.Remove(contentType);
            await db.SaveChangesAsync();

            return Ok(contentType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContentTypeExists(int id)
        {
            return db.ContentTypes.Count(e => e.ContentTypeID == id) > 0;
        }
    }
}