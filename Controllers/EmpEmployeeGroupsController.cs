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
    builder.EntitySet<EmpEmployeeGroup>("EmpEmployeeGroups");
    builder.EntitySet<Organization>("Organizations"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class EmpEmployeeGroupsController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/EmpEmployeeGroups
        [EnableQuery]
        public IQueryable<EmpEmployeeGroup> GetEmpEmployeeGroups()
        {
            return db.EmpEmployeeGroups;
        }

        // GET: odata/EmpEmployeeGroups(5)
        [EnableQuery]
        public SingleResult<EmpEmployeeGroup> GetEmpEmployeeGroup([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmpEmployeeGroups.Where(empEmployeeGroup => empEmployeeGroup.EmployeeGroupId == key));
        }

        // PUT: odata/EmpEmployeeGroups(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<EmpEmployeeGroup> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmpEmployeeGroup empEmployeeGroup = await db.EmpEmployeeGroups.FindAsync(key);
            if (empEmployeeGroup == null)
            {
                return NotFound();
            }

            patch.Put(empEmployeeGroup);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpEmployeeGroupExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(empEmployeeGroup);
        }

        // POST: odata/EmpEmployeeGroups
        public async Task<IHttpActionResult> Post(EmpEmployeeGroup empEmployeeGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmpEmployeeGroups.Add(empEmployeeGroup);
            await db.SaveChangesAsync();

            return Created(empEmployeeGroup);
        }

        // PATCH: odata/EmpEmployeeGroups(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<EmpEmployeeGroup> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmpEmployeeGroup empEmployeeGroup = await db.EmpEmployeeGroups.FindAsync(key);
            if (empEmployeeGroup == null)
            {
                return NotFound();
            }

            patch.Patch(empEmployeeGroup);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpEmployeeGroupExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(empEmployeeGroup);
        }

        // DELETE: odata/EmpEmployeeGroups(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            EmpEmployeeGroup empEmployeeGroup = await db.EmpEmployeeGroups.FindAsync(key);
            if (empEmployeeGroup == null)
            {
                return NotFound();
            }

            db.EmpEmployeeGroups.Remove(empEmployeeGroup);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/EmpEmployeeGroups(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmpEmployeeGroups.Where(m => m.EmployeeGroupId == key).Select(m => m.Organization));
        }

        // GET: odata/EmpEmployeeGroups(5)/Organization1
        [EnableQuery]
        public SingleResult<Organization> GetOrganization1([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmpEmployeeGroups.Where(m => m.EmployeeGroupId == key).Select(m => m.Organization1));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmpEmployeeGroupExists(short key)
        {
            return db.EmpEmployeeGroups.Count(e => e.EmployeeGroupId == key) > 0;
        }
    }
}
