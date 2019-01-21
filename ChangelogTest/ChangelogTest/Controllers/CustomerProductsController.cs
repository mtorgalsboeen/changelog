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
    public class CustomerProductsController : ApiController
    {
        private FinalModel db = new FinalModel();

        // GET: api/CustomerProducts
        public IQueryable<CustomerProduct> GetCustomerProducts()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.CustomerProducts;
        }

        // GET: api/CustomerProducts/5
        [ResponseType(typeof(CustomerProduct))]
        public async Task<IHttpActionResult> GetCustomerProduct(int id)
        {
            CustomerProduct customerProduct = await db.CustomerProducts.FindAsync(id);
            if (customerProduct == null)
            {
                return NotFound();
            }

            return Ok(customerProduct);
        }

        // PUT: api/CustomerProducts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCustomerProduct(int id, CustomerProduct customerProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customerProduct.CustomerProductID)
            {
                return BadRequest();
            }

            db.Entry(customerProduct).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerProductExists(id))
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

        // POST: api/CustomerProducts
        [ResponseType(typeof(CustomerProduct))]
        public async Task<IHttpActionResult> PostCustomerProduct(CustomerProduct customerProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CustomerProducts.Add(customerProduct);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = customerProduct.CustomerProductID }, customerProduct);
        }

        // DELETE: api/CustomerProducts/5
        [ResponseType(typeof(CustomerProduct))]
        public async Task<IHttpActionResult> DeleteCustomerProduct(int id)
        {
            CustomerProduct customerProduct = await db.CustomerProducts.FindAsync(id);
            if (customerProduct == null)
            {
                return NotFound();
            }

            db.CustomerProducts.Remove(customerProduct);
            await db.SaveChangesAsync();

            return Ok(customerProduct);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerProductExists(int id)
        {
            return db.CustomerProducts.Count(e => e.CustomerProductID == id) > 0;
        }
    }
}