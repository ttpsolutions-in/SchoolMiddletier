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
    builder.EntitySet<VariableConfiguration>("VariableConfigurations");
    builder.EntitySet<Organization>("Organizations"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class VariableConfigurationsController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/VariableConfigurations
        [EnableQuery]
        public IQueryable<VariableConfiguration> GetVariableConfigurations()
        {
            return db.VariableConfigurations;
        }

        // GET: odata/VariableConfigurations(5)
        [EnableQuery]
        public SingleResult<VariableConfiguration> GetVariableConfiguration([FromODataUri] short key)
        {
            return SingleResult.Create(db.VariableConfigurations.Where(variableConfiguration => variableConfiguration.VariableConfigurationId == key));
        }

        // PUT: odata/VariableConfigurations(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<VariableConfiguration> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VariableConfiguration variableConfiguration = await db.VariableConfigurations.FindAsync(key);
            if (variableConfiguration == null)
            {
                return NotFound();
            }

            patch.Put(variableConfiguration);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VariableConfigurationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(variableConfiguration);
        }

        // POST: odata/VariableConfigurations
        public async Task<IHttpActionResult> Post(VariableConfiguration variableConfiguration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VariableConfigurations.Add(variableConfiguration);
            await db.SaveChangesAsync();

            return Created(variableConfiguration);
        }

        // PATCH: odata/VariableConfigurations(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<VariableConfiguration> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VariableConfiguration variableConfiguration = await db.VariableConfigurations.FindAsync(key);
            if (variableConfiguration == null)
            {
                return NotFound();
            }

            patch.Patch(variableConfiguration);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VariableConfigurationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(variableConfiguration);
        }

        // DELETE: odata/VariableConfigurations(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            VariableConfiguration variableConfiguration = await db.VariableConfigurations.FindAsync(key);
            if (variableConfiguration == null)
            {
                return NotFound();
            }

            db.VariableConfigurations.Remove(variableConfiguration);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/VariableConfigurations(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.VariableConfigurations.Where(m => m.VariableConfigurationId == key).Select(m => m.Organization));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VariableConfigurationExists(short key)
        {
            return db.VariableConfigurations.Count(e => e.VariableConfigurationId == key) > 0;
        }
    }
}
