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
    builder.EntitySet<ApplicationPrice>("ApplicationPrices");
    builder.EntitySet<MasterData>("MasterDatas"); 
    builder.EntitySet<Organization>("Organizations"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ApplicationPricesController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/ApplicationPrices
        [EnableQuery]
        public IQueryable<ApplicationPrice> GetApplicationPrices()
        {
            return db.ApplicationPrices;
        }

        // GET: odata/ApplicationPrices(5)
        [EnableQuery]
        public SingleResult<ApplicationPrice> GetApplicationPrice([FromODataUri] short key)
        {
            return SingleResult.Create(db.ApplicationPrices.Where(applicationPrice => applicationPrice.ApplicationPriceId == key));
        }

        // PUT: odata/ApplicationPrices(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<ApplicationPrice> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationPrice applicationPrice = await db.ApplicationPrices.FindAsync(key);
            if (applicationPrice == null)
            {
                return NotFound();
            }

            patch.Put(applicationPrice);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationPriceExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(applicationPrice);
        }

        // POST: odata/ApplicationPrices
        public async Task<IHttpActionResult> Post(ApplicationPrice applicationPrice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ApplicationPrices.Add(applicationPrice);
            await db.SaveChangesAsync();

            return Created(applicationPrice);
        }

        // PATCH: odata/ApplicationPrices(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<ApplicationPrice> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationPrice applicationPrice = await db.ApplicationPrices.FindAsync(key);
            if (applicationPrice == null)
            {
                return NotFound();
            }

            patch.Patch(applicationPrice);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationPriceExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(applicationPrice);
        }

        // DELETE: odata/ApplicationPrices(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            ApplicationPrice applicationPrice = await db.ApplicationPrices.FindAsync(key);
            if (applicationPrice == null)
            {
                return NotFound();
            }

            db.ApplicationPrices.Remove(applicationPrice);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ApplicationPrices(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] short key)
        {
            return SingleResult.Create(db.ApplicationPrices.Where(m => m.ApplicationPriceId == key).Select(m => m.MasterData));
        }

        // GET: odata/ApplicationPrices(5)/MasterData1
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData1([FromODataUri] short key)
        {
            return SingleResult.Create(db.ApplicationPrices.Where(m => m.ApplicationPriceId == key).Select(m => m.MasterData1));
        }

        // GET: odata/ApplicationPrices(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.ApplicationPrices.Where(m => m.ApplicationPriceId == key).Select(m => m.Organization));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ApplicationPriceExists(short key)
        {
            return db.ApplicationPrices.Count(e => e.ApplicationPriceId == key) > 0;
        }
    }
}
