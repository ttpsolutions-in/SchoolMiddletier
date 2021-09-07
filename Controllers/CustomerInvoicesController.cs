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
    builder.EntitySet<CustomerInvoice>("CustomerInvoices");
    builder.EntitySet<MasterData>("MasterDatas"); 
    builder.EntitySet<Organization>("Organizations"); 
    builder.EntitySet<CustomerInvoiceItem>("CustomerInvoiceItems"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class CustomerInvoicesController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/CustomerInvoices
        [EnableQuery]
        public IQueryable<CustomerInvoice> GetCustomerInvoices()
        {
            return db.CustomerInvoices;
        }

        // GET: odata/CustomerInvoices(5)
        [EnableQuery]
        public SingleResult<CustomerInvoice> GetCustomerInvoice([FromODataUri] int key)
        {
            return SingleResult.Create(db.CustomerInvoices.Where(customerInvoice => customerInvoice.CustomerInvoiceId == key));
        }

        // PUT: odata/CustomerInvoices(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<CustomerInvoice> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CustomerInvoice customerInvoice = await db.CustomerInvoices.FindAsync(key);
            if (customerInvoice == null)
            {
                return NotFound();
            }

            patch.Put(customerInvoice);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerInvoiceExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(customerInvoice);
        }

        // POST: odata/CustomerInvoices
        public async Task<IHttpActionResult> Post(CustomerInvoice customerInvoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CustomerInvoices.Add(customerInvoice);
            await db.SaveChangesAsync();

            return Created(customerInvoice);
        }

        // PATCH: odata/CustomerInvoices(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<CustomerInvoice> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CustomerInvoice customerInvoice = await db.CustomerInvoices.FindAsync(key);
            if (customerInvoice == null)
            {
                return NotFound();
            }

            patch.Patch(customerInvoice);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerInvoiceExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(customerInvoice);
        }

        // DELETE: odata/CustomerInvoices(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            CustomerInvoice customerInvoice = await db.CustomerInvoices.FindAsync(key);
            if (customerInvoice == null)
            {
                return NotFound();
            }

            db.CustomerInvoices.Remove(customerInvoice);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/CustomerInvoices(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] int key)
        {
            return SingleResult.Create(db.CustomerInvoices.Where(m => m.CustomerInvoiceId == key).Select(m => m.MasterData));
        }

        // GET: odata/CustomerInvoices(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] int key)
        {
            return SingleResult.Create(db.CustomerInvoices.Where(m => m.CustomerInvoiceId == key).Select(m => m.Organization));
        }

        // GET: odata/CustomerInvoices(5)/Organization1
        [EnableQuery]
        public SingleResult<Organization> GetOrganization1([FromODataUri] int key)
        {
            return SingleResult.Create(db.CustomerInvoices.Where(m => m.CustomerInvoiceId == key).Select(m => m.Organization1));
        }

        // GET: odata/CustomerInvoices(5)/CustomerInvoiceItems
        [EnableQuery]
        public IQueryable<CustomerInvoiceItem> GetCustomerInvoiceItems([FromODataUri] int key)
        {
            return db.CustomerInvoices.Where(m => m.CustomerInvoiceId == key).SelectMany(m => m.CustomerInvoiceItems);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerInvoiceExists(int key)
        {
            return db.CustomerInvoices.Count(e => e.CustomerInvoiceId == key) > 0;
        }
    }
}
