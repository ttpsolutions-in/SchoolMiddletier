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
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using schools.Models;

namespace schools.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using schools.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<CustomerInvoiceItem>("CustomerInvoiceItems");
    builder.EntitySet<ApplicationPrice>("ApplicationPrices"); 
    builder.EntitySet<CustomerInvoice>("CustomerInvoices"); 
    builder.EntitySet<InventoryItem>("InventoryItems"); 
    builder.EntitySet<Organization>("Organizations"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class CustomerInvoiceItemsController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/CustomerInvoiceItems
        [EnableQuery]
        public IQueryable<CustomerInvoiceItem> GetCustomerInvoiceItems()
        {
            return db.CustomerInvoiceItems;
        }

        // GET: odata/CustomerInvoiceItems(5)
        [EnableQuery]
        public SingleResult<CustomerInvoiceItem> GetCustomerInvoiceItem([FromODataUri] int key)
        {
            return SingleResult.Create(db.CustomerInvoiceItems.Where(customerInvoiceItem => customerInvoiceItem.CustomerInvoiceItemId == key));
        }

        // PUT: odata/CustomerInvoiceItems(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<CustomerInvoiceItem> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CustomerInvoiceItem customerInvoiceItem = await db.CustomerInvoiceItems.FindAsync(key);
            if (customerInvoiceItem == null)
            {
                return NotFound();
            }

            patch.Put(customerInvoiceItem);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerInvoiceItemExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(customerInvoiceItem);
        }

        // POST: odata/CustomerInvoiceItems
        public async Task<IHttpActionResult> Post(CustomerInvoiceItem customerInvoiceItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CustomerInvoiceItems.Add(customerInvoiceItem);
            await db.SaveChangesAsync();

            return Created(customerInvoiceItem);
        }

        // PATCH: odata/CustomerInvoiceItems(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<CustomerInvoiceItem> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CustomerInvoiceItem customerInvoiceItem = await db.CustomerInvoiceItems.FindAsync(key);
            if (customerInvoiceItem == null)
            {
                return NotFound();
            }

            patch.Patch(customerInvoiceItem);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerInvoiceItemExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(customerInvoiceItem);
        }

        // DELETE: odata/CustomerInvoiceItems(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            CustomerInvoiceItem customerInvoiceItem = await db.CustomerInvoiceItems.FindAsync(key);
            if (customerInvoiceItem == null)
            {
                return NotFound();
            }

            db.CustomerInvoiceItems.Remove(customerInvoiceItem);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        //// GET: odata/CustomerInvoiceItems(5)/ApplicationPrice
        //[EnableQuery]
        //public SingleResult<ApplicationPrice> GetApplicationPrice([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.CustomerInvoiceItems.Where(m => m.CustomerInvoiceItemId == key).Select(m => m.ApplicationPrice));
        //}

        // GET: odata/CustomerInvoiceItems(5)/CustomerInvoice
        [EnableQuery]
        public SingleResult<CustomerInvoice> GetCustomerInvoice([FromODataUri] int key)
        {
            return SingleResult.Create(db.CustomerInvoiceItems.Where(m => m.CustomerInvoiceItemId == key).Select(m => m.CustomerInvoice));
        }

        // GET: odata/CustomerInvoiceItems(5)/InventoryItem
        [EnableQuery]
        public SingleResult<InventoryItem> GetInventoryItem([FromODataUri] int key)
        {
            return SingleResult.Create(db.CustomerInvoiceItems.Where(m => m.CustomerInvoiceItemId == key).Select(m => m.InventoryItem));
        }

        // GET: odata/CustomerInvoiceItems(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] int key)
        {
            return SingleResult.Create(db.CustomerInvoiceItems.Where(m => m.CustomerInvoiceItemId == key).Select(m => m.Organization));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerInvoiceItemExists(int key)
        {
            return db.CustomerInvoiceItems.Count(e => e.CustomerInvoiceItemId == key) > 0;
        }
    }
}
