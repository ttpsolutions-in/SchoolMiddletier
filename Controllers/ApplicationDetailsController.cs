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
    builder.EntitySet<ApplicationDetail>("ApplicationDetails");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ApplicationDetailsController : ODataController
    {
        private StpaulsEntities db = new StpaulsEntities();

        // GET: odata/ApplicationDetails
        [EnableQuery]
        public IQueryable<ApplicationDetail> GetApplicationDetails()
        {
            return db.ApplicationDetails;
        }

        // GET: odata/ApplicationDetails(5)
        [EnableQuery]
        public SingleResult<ApplicationDetail> GetApplicationDetail([FromODataUri] short key)
        {
            return SingleResult.Create(db.ApplicationDetails.Where(applicationDetail => applicationDetail.ApplicationDetailId == key));
        }

        // PUT: odata/ApplicationDetails(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<ApplicationDetail> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationDetail applicationDetail = await db.ApplicationDetails.FindAsync(key);
            if (applicationDetail == null)
            {
                return NotFound();
            }

            patch.Put(applicationDetail);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationDetailExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(applicationDetail);
        }

        // POST: odata/ApplicationDetails
        public async Task<IHttpActionResult> Post(ApplicationDetail applicationDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ApplicationDetails.Add(applicationDetail);
            await db.SaveChangesAsync();

            return Created(applicationDetail);
        }

        // PATCH: odata/ApplicationDetails(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<ApplicationDetail> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationDetail applicationDetail = await db.ApplicationDetails.FindAsync(key);
            if (applicationDetail == null)
            {
                return NotFound();
            }

            patch.Patch(applicationDetail);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationDetailExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(applicationDetail);
        }

        // DELETE: odata/ApplicationDetails(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            ApplicationDetail applicationDetail = await db.ApplicationDetails.FindAsync(key);
            if (applicationDetail == null)
            {
                return NotFound();
            }

            db.ApplicationDetails.Remove(applicationDetail);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ApplicationDetailExists(short key)
        {
            return db.ApplicationDetails.Count(e => e.ApplicationDetailId == key) > 0;
        }
    }
}
