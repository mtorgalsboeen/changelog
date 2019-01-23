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
    /// Controller for products
    /// </summary>
    public class ProductsController : ApiController
    {
        private FinalModel db = new FinalModel();

        // GET: api/Products
        /// <summary>
        /// Gets all products
        /// </summary>
        /// <returns>A list of products</returns>
        public IQueryable<Product> GetProducts()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.Products;
        }

        // GET: api/Products/5
        /// <summary>
        /// Gets a specific product
        /// </summary>
        /// <param name="id">ID of the product</param>
        /// <returns>A HTTP result of type Product</returns>
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> GetProduct(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;

            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        /// <summary>
        /// Updates a specific product
        /// </summary>
        /// <param name="id">ID of the product</param>
        /// <param name="product">The product</param>
        /// <returns>A HTTP result of type Product</returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ProductID)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        /// <summary>
        /// Creates a new product
        /// </summary>
        /// <param name="product">The product</param>
        /// <returns>A HTTP result of type Product</returns>
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(product);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = product.ProductID }, product);
        }

        // DELETE: api/Products/5
        /// <summary>
        /// Deletes a specific product
        /// </summary>
        /// <param name="id">ID of the product</param>
        /// <returns>A HTTP result of type Product</returns>
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> DeleteProduct(int id)
        {
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Checks of Product with specific ID exists
        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.ProductID == id) > 0;
        }
    }
}