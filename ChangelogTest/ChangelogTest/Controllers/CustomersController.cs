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
    /// The controller for Customers.
    /// </summary>
    public class CustomersController : ApiController
    {
        private FinalModel db = new FinalModel();

        // GET: api/Customers
        /// <summary>
        /// Gets all Customers.
        /// </summary>
        /// <returns>A list of Customers.</returns>
        public IQueryable<Customer> GetCustomers()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.Customers;
        }

        // GET: api/Customers/5
        /// <summary>
        /// Gets the Customer with the specified ID.
        /// </summary>
        /// <param name="id">The Customer's ID.</param>
        /// <returns>An HTTP response.</returns>
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> GetCustomer(int id)
        {
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/Customers/5
        /// <summary>
        /// Updates the Customer with the specified ID.
        /// </summary>
        /// <param name="id">The Customer's ID.</param>
        /// <param name="customer">A Customer object.</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerID)
            {
                return BadRequest();
            }

            db.Entry(customer).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customers
        /// <summary>
        /// Inserts a new Customer.
        /// </summary>
        /// <param name="customer">A Customer Object.</param>
        /// <returns>An HTTP response.</returns>
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Customers.Add(customer);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = customer.CustomerID }, customer);
        }

        // DELETE: api/Customers/5
        /// <summary>
        /// Deletes the Customer with the specified ID.
        /// </summary>
        /// <param name="id">The Customer's ID.</param>
        /// <returns>An HTTP response.</returns>
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> DeleteCustomer(int id)
        {
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            db.Customers.Remove(customer);
            await db.SaveChangesAsync();

            return Ok(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int id)
        {
            return db.Customers.Count(e => e.CustomerID == id) > 0;
        }
    }
}