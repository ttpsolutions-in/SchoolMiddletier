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
    builder.EntitySet<EmpManagerGroupMapping>("EmpManagerGroupMappings");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class EmpManagerGroupMappingsController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/EmpManagerGroupMappings
        [EnableQuery]
        public IQueryable<EmpManagerGroupMapping> GetEmpManagerGroupMappings()
        {
            return db.EmpManagerGroupMappings;
        }

        // GET: odata/EmpManagerGroupMappings(5)
        [EnableQuery]
        public SingleResult<EmpManagerGroupMapping> GetEmpManagerGroupMapping([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmpManagerGroupMappings.Where(empManagerGroupMapping => empManagerGroupMapping.ManagerGroupMappingId == key));
        }

        // PUT: odata/EmpManagerGroupMappings(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<EmpManagerGroupMapping> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmpManagerGroupMapping empManagerGroupMapping = await db.EmpManagerGroupMappings.FindAsync(key);
            if (empManagerGroupMapping == null)
            {
                return NotFound();
            }

            patch.Put(empManagerGroupMapping);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpManagerGroupMappingExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(empManagerGroupMapping);
        }

        // POST: odata/EmpManagerGroupMappings
        public async Task<IHttpActionResult> Post(EmpManagerGroupMapping empManagerGroupMapping)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmpManagerGroupMappings.Add(empManagerGroupMapping);
            await db.SaveChangesAsync();

            return Created(empManagerGroupMapping);
        }

        // PATCH: odata/EmpManagerGroupMappings(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<EmpManagerGroupMapping> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmpManagerGroupMapping empManagerGroupMapping = await db.EmpManagerGroupMappings.FindAsync(key);
            if (empManagerGroupMapping == null)
            {
                return NotFound();
            }

            patch.Patch(empManagerGroupMapping);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpManagerGroupMappingExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(empManagerGroupMapping);
        }

        // DELETE: odata/EmpManagerGroupMappings(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            EmpManagerGroupMapping empManagerGroupMapping = await db.EmpManagerGroupMappings.FindAsync(key);
            if (empManagerGroupMapping == null)
            {
                return NotFound();
            }

            db.EmpManagerGroupMappings.Remove(empManagerGroupMapping);
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

        private bool EmpManagerGroupMappingExists(short key)
        {
            return db.EmpManagerGroupMappings.Count(e => e.ManagerGroupMappingId == key) > 0;
        }
    }
}
