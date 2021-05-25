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
    builder.EntitySet<TaskConfiguration>("TaskConfigurations");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class TaskConfigurationsController : ODataController
    {
        private StpaulsEntities db = new StpaulsEntities();

        // GET: odata/TaskConfigurations
        [EnableQuery]
        public IQueryable<TaskConfiguration> GetTaskConfigurations()
        {
            return db.TaskConfigurations;
        }

        // GET: odata/TaskConfigurations(5)
        [EnableQuery]
        public SingleResult<TaskConfiguration> GetTaskConfiguration([FromODataUri] int key)
        {
            return SingleResult.Create(db.TaskConfigurations.Where(taskConfiguration => taskConfiguration.Id == key));
        }

        // PUT: odata/TaskConfigurations(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<TaskConfiguration> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TaskConfiguration taskConfiguration = await db.TaskConfigurations.FindAsync(key);
            if (taskConfiguration == null)
            {
                return NotFound();
            }

            patch.Put(taskConfiguration);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskConfigurationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(taskConfiguration);
        }

        // POST: odata/TaskConfigurations
        public async Task<IHttpActionResult> Post(TaskConfiguration taskConfiguration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TaskConfigurations.Add(taskConfiguration);
            await db.SaveChangesAsync();

            return Created(taskConfiguration);
        }

        // PATCH: odata/TaskConfigurations(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<TaskConfiguration> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TaskConfiguration taskConfiguration = await db.TaskConfigurations.FindAsync(key);
            if (taskConfiguration == null)
            {
                return NotFound();
            }

            patch.Patch(taskConfiguration);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskConfigurationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(taskConfiguration);
        }

        // DELETE: odata/TaskConfigurations(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            TaskConfiguration taskConfiguration = await db.TaskConfigurations.FindAsync(key);
            if (taskConfiguration == null)
            {
                return NotFound();
            }

            db.TaskConfigurations.Remove(taskConfiguration);
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

        private bool TaskConfigurationExists(int key)
        {
            return db.TaskConfigurations.Count(e => e.Id == key) > 0;
        }
    }
}
