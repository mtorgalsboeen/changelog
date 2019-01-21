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
    public class UserAccessProductsController : ApiController
    {
        private FinalModel db = new FinalModel();

        // GET: api/UserAccessProducts
        public IQueryable<UserAccessProduct> GetUserAccessProducts()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.UserAccessProducts;
        }

        // GET: api/UserAccessProducts/5
        [ResponseType(typeof(UserAccessProduct))]
        public async Task<IHttpActionResult> GetUserAccessProduct(int id)
        {
            UserAccessProduct userAccessProduct = await db.UserAccessProducts.FindAsync(id);
            if (userAccessProduct == null)
            {
                return NotFound();
            }

            return Ok(userAccessProduct);
        }

        // PUT: api/UserAccessProducts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserAccessProduct(int id, UserAccessProduct userAccessProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userAccessProduct.UserAccessProductID)
            {
                return BadRequest();
            }

            db.Entry(userAccessProduct).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAccessProductExists(id))
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

        // POST: api/UserAccessProducts
        [ResponseType(typeof(UserAccessProduct))]
        public async Task<IHttpActionResult> PostUserAccessProduct(UserAccessProduct userAccessProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserAccessProducts.Add(userAccessProduct);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = userAccessProduct.UserAccessProductID }, userAccessProduct);
        }

        // DELETE: api/UserAccessProducts/5
        [ResponseType(typeof(UserAccessProduct))]
        public async Task<IHttpActionResult> DeleteUserAccessProduct(int id)
        {
            UserAccessProduct userAccessProduct = await db.UserAccessProducts.FindAsync(id);
            if (userAccessProduct == null)
            {
                return NotFound();
            }

            db.UserAccessProducts.Remove(userAccessProduct);
            await db.SaveChangesAsync();

            return Ok(userAccessProduct);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserAccessProductExists(int id)
        {
            return db.UserAccessProducts.Count(e => e.UserAccessProductID == id) > 0;
        }
    }
}