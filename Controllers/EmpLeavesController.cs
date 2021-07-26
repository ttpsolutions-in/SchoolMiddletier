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
    builder.EntitySet<EmpLeaf>("EmpLeaves");
    builder.EntitySet<Organization>("Organizations"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class EmpLeavesController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/EmpLeaves
        [EnableQuery]
        public IQueryable<EmpLeaf> GetEmpLeaves()
        {
            return db.EmpLeaves;
        }

        // GET: odata/EmpLeaves(5)
        [EnableQuery]
        public SingleResult<EmpLeaf> GetEmpLeaf([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmpLeaves.Where(empLeaf => empLeaf.EmpLeaveId == key));
        }

        // PUT: odata/EmpLeaves(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<EmpLeaf> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmpLeaf empLeaf = await db.EmpLeaves.FindAsync(key);
            if (empLeaf == null)
            {
                return NotFound();
            }

            patch.Put(empLeaf);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpLeafExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(empLeaf);
        }

        // POST: odata/EmpLeaves
        public async Task<IHttpActionResult> Post(EmpLeaf empLeaf)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmpLeaves.Add(empLeaf);
            await db.SaveChangesAsync();

            return Created(empLeaf);
        }

        // PATCH: odata/EmpLeaves(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<EmpLeaf> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmpLeaf empLeaf = await db.EmpLeaves.FindAsync(key);
            if (empLeaf == null)
            {
                return NotFound();
            }

            patch.Patch(empLeaf);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpLeafExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(empLeaf);
        }

        // DELETE: odata/EmpLeaves(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            EmpLeaf empLeaf = await db.EmpLeaves.FindAsync(key);
            if (empLeaf == null)
            {
                return NotFound();
            }

            db.EmpLeaves.Remove(empLeaf);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/EmpLeaves(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmpLeaves.Where(m => m.EmpLeaveId == key).Select(m => m.Organization));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmpLeafExists(short key)
        {
            return db.EmpLeaves.Count(e => e.EmpLeaveId == key) > 0;
        }
    }
}
