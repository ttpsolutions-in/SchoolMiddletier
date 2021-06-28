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
    builder.EntitySet<ApplicationFeature>("ApplicationFeatures");
    builder.EntitySet<MasterData>("MasterDatas"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ApplicationFeaturesController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/ApplicationFeatures
        [EnableQuery]
        public IQueryable<ApplicationFeature> GetApplicationFeatures()
        {
            return db.ApplicationFeatures;
        }

        // GET: odata/ApplicationFeatures(5)
        [EnableQuery]
        public SingleResult<ApplicationFeature> GetApplicationFeature([FromODataUri] short key)
        {
            return SingleResult.Create(db.ApplicationFeatures.Where(applicationFeature => applicationFeature.ApplicationFeatureId == key));
        }

        // PUT: odata/ApplicationFeatures(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<ApplicationFeature> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationFeature applicationFeature = await db.ApplicationFeatures.FindAsync(key);
            if (applicationFeature == null)
            {
                return NotFound();
            }

            patch.Put(applicationFeature);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationFeatureExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(applicationFeature);
        }

        // POST: odata/ApplicationFeatures
        public async Task<IHttpActionResult> Post(ApplicationFeature applicationFeature)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ApplicationFeatures.Add(applicationFeature);
            await db.SaveChangesAsync();

            return Created(applicationFeature);
        }

        // PATCH: odata/ApplicationFeatures(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<ApplicationFeature> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationFeature applicationFeature = await db.ApplicationFeatures.FindAsync(key);
            if (applicationFeature == null)
            {
                return NotFound();
            }

            patch.Patch(applicationFeature);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationFeatureExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(applicationFeature);
        }

        // DELETE: odata/ApplicationFeatures(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            ApplicationFeature applicationFeature = await db.ApplicationFeatures.FindAsync(key);
            if (applicationFeature == null)
            {
                return NotFound();
            }

            db.ApplicationFeatures.Remove(applicationFeature);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ApplicationFeatures(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] short key)
        {
            return SingleResult.Create(db.ApplicationFeatures.Where(m => m.ApplicationFeatureId == key).Select(m => m.MasterData));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ApplicationFeatureExists(short key)
        {
            return db.ApplicationFeatures.Count(e => e.ApplicationFeatureId == key) > 0;
        }
    }
}
