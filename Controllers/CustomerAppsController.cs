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
    builder.EntitySet<CustomerApp>("CustomerApps");
    builder.EntitySet<ApplicationPrice>("ApplicationPrices"); 
    builder.EntitySet<CustomerInvoiceItem>("CustomerInvoiceItems"); 
    builder.EntitySet<Organization>("Organizations"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class CustomerAppsController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/CustomerApps
        [EnableQuery]
        public IQueryable<CustomerApp> GetCustomerApps()
        {
            return db.CustomerApps;
        }

        // GET: odata/CustomerApps(5)
        [EnableQuery]
        public SingleResult<CustomerApp> GetCustomerApp([FromODataUri] short key)
        {
            return SingleResult.Create(db.CustomerApps.Where(customerApp => customerApp.CustomerAppsId == key));
        }

        // PUT: odata/CustomerApps(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<CustomerApp> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CustomerApp customerApp = await db.CustomerApps.FindAsync(key);
            if (customerApp == null)
            {
                return NotFound();
            }

            patch.Put(customerApp);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerAppExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(customerApp);
        }

        // POST: odata/CustomerApps
        public async Task<IHttpActionResult> Post(CustomerApp customerApp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CustomerApps.Add(customerApp);
            await db.SaveChangesAsync();

            return Created(customerApp);
        }

        // PATCH: odata/CustomerApps(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<CustomerApp> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CustomerApp customerApp = await db.CustomerApps.FindAsync(key);
            if (customerApp == null)
            {
                return NotFound();
            }

            patch.Patch(customerApp);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerAppExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(customerApp);
        }

        // DELETE: odata/CustomerApps(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            CustomerApp customerApp = await db.CustomerApps.FindAsync(key);
            if (customerApp == null)
            {
                return NotFound();
            }

            db.CustomerApps.Remove(customerApp);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/CustomerApps(5)/ApplicationPrice
        [EnableQuery]
        public SingleResult<ApplicationPrice> GetApplicationPrice([FromODataUri] short key)
        {
            return SingleResult.Create(db.CustomerApps.Where(m => m.CustomerAppsId == key).Select(m => m.ApplicationPrice));
        }

        // GET: odata/CustomerApps(5)/CustomerInvoiceItems
        [EnableQuery]
        public IQueryable<CustomerInvoiceItem> GetCustomerInvoiceItems([FromODataUri] short key)
        {
            return db.CustomerApps.Where(m => m.CustomerAppsId == key).SelectMany(m => m.CustomerInvoiceItems);
        }

        // GET: odata/CustomerApps(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.CustomerApps.Where(m => m.CustomerAppsId == key).Select(m => m.Organization));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerAppExists(short key)
        {
            return db.CustomerApps.Count(e => e.CustomerAppsId == key) > 0;
        }
    }
}
