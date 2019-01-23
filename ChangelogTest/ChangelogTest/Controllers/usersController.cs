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
    /// Controller for users
    /// </summary>
    [RoutePrefix("api")]
    public class usersController : ApiController
    {

        private FinalModel db = new FinalModel();

        // GET: api/users
        /// <summary>
        /// Gets all non-admin users.
        /// </summary>
        /// <returns>A list of users.</returns>
        public List<user> Getusers()
        {
            db.Configuration.LazyLoadingEnabled = false;

            return db.users.Where(u => u.UsertypeID == 2).ToList();
        }

        /// <summary>
        /// Gets all users with a specific CustomerID
        /// </summary>
        /// <param name="customerID">The specific customerID</param>
        /// <returns>A list of users</returns>
        //[HttpGet]
        //[Route("users/GetUsersByCustomer/{customerID}")]
        //public List<user> GetUserByCustomer(int customerID)
        //{
        //    db.Configuration.LazyLoadingEnabled = false;

        //    return db.users.Where(u => u.CustomerID == customerID).ToList();
        //}

        [HttpGet]
        [Route("users/GetUsersByCustomer/{customerID}")]
        [ResponseType(typeof(List<user>))]
        public async Task<IHttpActionResult> GetUserByCustomer(int customerID)
        {
            db.Configuration.LazyLoadingEnabled = false;

            List<user> userList = await db.users.Where(u => u.CustomerID == customerID).ToListAsync();

            return Ok(userList);
        }

        // GET: api/users/5
        /// <summary>
        /// Gets a specific user
        /// </summary>
        /// <param name="id">ID of the specific user</param>
        /// <returns>A HTTP result of type User</returns>
        [ResponseType(typeof(user))]
        public async Task<IHttpActionResult> Getuser(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;

            user user = await db.users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/users/5
        /// <summary>
        /// Updates a specific user
        /// </summary>
        /// <param name="id">ID of the specific user</param>
        /// <param name="user">The user object</param>
        /// <returns>A HTTP result of type User</returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putuser(int id, user user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserID)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userExists(id))
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

        // POST: api/users
        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="user">The user object</param>
        /// <returns>A HTTP result of type User</returns>
        [ResponseType(typeof(user))]
        public async Task<IHttpActionResult> Postuser(user user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.users.Add(user);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = user.UserID }, user);
        }

        // DELETE: api/users/5
        /// <summary>
        /// Deletes a specific user
        /// </summary>
        /// <param name="id">ID of the specific user</param>
        /// <returns>A HTTP result of type User</returns>
        [ResponseType(typeof(user))]
        public async Task<IHttpActionResult> Deleteuser(int id)
        {
            user user = await db.users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Checks if a user with a specific ID exists
        private bool userExists(int id)
        {
            return db.users.Count(e => e.UserID == id) > 0;
        }
    }
}