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
    builder.EntitySet<Application>("Applications");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ApplicationsController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/Applications
        [EnableQuery]
        public IQueryable<Application> GetApplications()
        {
            return db.Applications;
        }

        // GET: odata/Applications(5)
        [EnableQuery]
        public SingleResult<Application> GetApplication([FromODataUri] short key)
        {
            return SingleResult.Create(db.Applications.Where(application => application.ApplicationId == key));
        }

        // PUT: odata/Applications(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<Application> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Application application = await db.Applications.FindAsync(key);
            if (application == null)
            {
                return NotFound();
            }

            patch.Put(application);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(application);
        }

        // POST: odata/Applications
        public async Task<IHttpActionResult> Post(Application application)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Applications.Add(application);
            await db.SaveChangesAsync();

            return Created(application);
        }

        // PATCH: odata/Applications(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<Application> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Application application = await db.Applications.FindAsync(key);
            if (application == null)
            {
                return NotFound();
            }

            patch.Patch(application);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(application);
        }

        // DELETE: odata/Applications(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            Application application = await db.Applications.FindAsync(key);
            if (application == null)
            {
                return NotFound();
            }

            db.Applications.Remove(application);
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

        private bool ApplicationExists(short key)
        {
            return db.Applications.Count(e => e.ApplicationId == key) > 0;
        }
    }
}
