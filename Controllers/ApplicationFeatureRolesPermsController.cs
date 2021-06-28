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
    builder.EntitySet<ApplicationFeatureRolesPerm>("ApplicationFeatureRolesPerms");
    builder.EntitySet<ApplicationFeature>("ApplicationFeatures"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ApplicationFeatureRolesPermsController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/ApplicationFeatureRolesPerms
        [EnableQuery]
        public IQueryable<ApplicationFeatureRolesPerm> GetApplicationFeatureRolesPerms()
        {
            return db.ApplicationFeatureRolesPerms;
        }

        // GET: odata/ApplicationFeatureRolesPerms(5)
        [EnableQuery]
        public SingleResult<ApplicationFeatureRolesPerm> GetApplicationFeatureRolesPerm([FromODataUri] short key)
        {
            return SingleResult.Create(db.ApplicationFeatureRolesPerms.Where(applicationFeatureRolesPerm => applicationFeatureRolesPerm.ApplicationFeatureRoleId == key));
        }

        // PUT: odata/ApplicationFeatureRolesPerms(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<ApplicationFeatureRolesPerm> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationFeatureRolesPerm applicationFeatureRolesPerm = await db.ApplicationFeatureRolesPerms.FindAsync(key);
            if (applicationFeatureRolesPerm == null)
            {
                return NotFound();
            }

            patch.Put(applicationFeatureRolesPerm);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationFeatureRolesPermExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(applicationFeatureRolesPerm);
        }

        // POST: odata/ApplicationFeatureRolesPerms
        public async Task<IHttpActionResult> Post(ApplicationFeatureRolesPerm applicationFeatureRolesPerm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ApplicationFeatureRolesPerms.Add(applicationFeatureRolesPerm);
            await db.SaveChangesAsync();

            return Created(applicationFeatureRolesPerm);
        }

        // PATCH: odata/ApplicationFeatureRolesPerms(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<ApplicationFeatureRolesPerm> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationFeatureRolesPerm applicationFeatureRolesPerm = await db.ApplicationFeatureRolesPerms.FindAsync(key);
            if (applicationFeatureRolesPerm == null)
            {
                return NotFound();
            }

            patch.Patch(applicationFeatureRolesPerm);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationFeatureRolesPermExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(applicationFeatureRolesPerm);
        }

        // DELETE: odata/ApplicationFeatureRolesPerms(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            ApplicationFeatureRolesPerm applicationFeatureRolesPerm = await db.ApplicationFeatureRolesPerms.FindAsync(key);
            if (applicationFeatureRolesPerm == null)
            {
                return NotFound();
            }

            db.ApplicationFeatureRolesPerms.Remove(applicationFeatureRolesPerm);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ApplicationFeatureRolesPerms(5)/ApplicationFeature
        [EnableQuery]
        public SingleResult<ApplicationFeature> GetApplicationFeature([FromODataUri] short key)
        {
            return SingleResult.Create(db.ApplicationFeatureRolesPerms.Where(m => m.ApplicationFeatureRoleId == key).Select(m => m.ApplicationFeature));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ApplicationFeatureRolesPermExists(short key)
        {
            return db.ApplicationFeatureRolesPerms.Count(e => e.ApplicationFeatureRoleId == key) > 0;
        }
    }
}
