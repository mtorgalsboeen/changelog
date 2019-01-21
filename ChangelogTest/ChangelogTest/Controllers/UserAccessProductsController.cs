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
    /// Controller for accesses to the different products
    /// </summary>
    public class UserAccessProductsController : ApiController
    {
        private FinalModel db = new FinalModel();

        // GET: api/UserAccessProducts
        /// <summary>
        /// Gets all Accesses
        /// </summary>
        /// <returns>A list of all the accesses</returns>
        public IQueryable<UserAccessProduct> GetUserAccessProducts()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.UserAccessProducts;
        }

        // GET: api/UserAccessProducts/5
        /// <summary>
        /// Gets a specific access
        /// </summary>
        /// <param name="id">ID of the specific access</param>
        /// <returns>A HTTP result of type UserAccessProduct</returns>
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
        /// <summary>
        /// Updates a specific access
        /// </summary>
        /// <param name="id">ID of the specific access</param>
        /// <param name="userAccessProduct">The access object</param>
        /// <returns>A HTTP result of type UserAccessProduct</returns>
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
        /// <summary>
        /// Creates a new access
        /// </summary>
        /// <param name="userAccessProduct">The access object</param>
        /// <returns>A HTTP result of type UserAccessProduct</returns>
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
        /// <summary>
        /// Deletes a specific access
        /// </summary>
        /// <param name="id">ID of the specific access</param>
        /// <returns>A HTTP result of type UserAccessProduct</returns>
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

        //
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Checks if access of specififc ID exists
        private bool UserAccessProductExists(int id)
        {
            return db.UserAccessProducts.Count(e => e.UserAccessProductID == id) > 0;
        }
    }
}