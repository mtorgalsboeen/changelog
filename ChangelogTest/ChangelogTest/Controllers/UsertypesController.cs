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
    public class UsertypesController : ApiController
    {
        private FinalModel db = new FinalModel();

        // GET: api/Usertypes
        public IQueryable<Usertype> GetUsertypes()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.Usertypes;
        }

        // GET: api/Usertypes/5
        [ResponseType(typeof(Usertype))]
        public async Task<IHttpActionResult> GetUsertype(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;

            Usertype usertype = await db.Usertypes.FindAsync(id);
            if (usertype == null)
            {
                return NotFound();
            }

            return Ok(usertype);
        }

        // PUT: api/Usertypes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUsertype(int id, Usertype usertype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usertype.UsertypeID)
            {
                return BadRequest();
            }

            db.Entry(usertype).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsertypeExists(id))
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

        // POST: api/Usertypes
        [ResponseType(typeof(Usertype))]
        public async Task<IHttpActionResult> PostUsertype(Usertype usertype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Usertypes.Add(usertype);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = usertype.UsertypeID }, usertype);
        }

        // DELETE: api/Usertypes/5
        [ResponseType(typeof(Usertype))]
        public async Task<IHttpActionResult> DeleteUsertype(int id)
        {
            Usertype usertype = await db.Usertypes.FindAsync(id);
            if (usertype == null)
            {
                return NotFound();
            }

            db.Usertypes.Remove(usertype);
            await db.SaveChangesAsync();

            return Ok(usertype);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsertypeExists(int id)
        {
            return db.Usertypes.Count(e => e.UsertypeID == id) > 0;
        }
    }
}