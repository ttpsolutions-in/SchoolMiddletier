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
    builder.EntitySet<EmployeeLeaf>("EmployeeLeaves");
    builder.EntitySet<EmpEmployee>("EmpEmployees"); 
    builder.EntitySet<MasterData>("MasterDatas"); 
    builder.EntitySet<Organization>("Organizations"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class EmployeeLeavesController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/EmployeeLeaves
        [EnableQuery]
        public IQueryable<EmployeeLeaf> GetEmployeeLeaves()
        {
            return db.EmployeeLeaves;
        }

        // GET: odata/EmployeeLeaves(5)
        [EnableQuery]
        public SingleResult<EmployeeLeaf> GetEmployeeLeaf([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmployeeLeaves.Where(employeeLeaf => employeeLeaf.EmployeeLeaveId == key));
        }

        // PUT: odata/EmployeeLeaves(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<EmployeeLeaf> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmployeeLeaf employeeLeaf = await db.EmployeeLeaves.FindAsync(key);
            if (employeeLeaf == null)
            {
                return NotFound();
            }

            patch.Put(employeeLeaf);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeLeafExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(employeeLeaf);
        }

        // POST: odata/EmployeeLeaves
        public async Task<IHttpActionResult> Post(EmployeeLeaf employeeLeaf)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmployeeLeaves.Add(employeeLeaf);
            await db.SaveChangesAsync();

            return Created(employeeLeaf);
        }

        // PATCH: odata/EmployeeLeaves(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<EmployeeLeaf> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmployeeLeaf employeeLeaf = await db.EmployeeLeaves.FindAsync(key);
            if (employeeLeaf == null)
            {
                return NotFound();
            }

            patch.Patch(employeeLeaf);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeLeafExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(employeeLeaf);
        }

        // DELETE: odata/EmployeeLeaves(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            EmployeeLeaf employeeLeaf = await db.EmployeeLeaves.FindAsync(key);
            if (employeeLeaf == null)
            {
                return NotFound();
            }

            db.EmployeeLeaves.Remove(employeeLeaf);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/EmployeeLeaves(5)/EmpEmployee
        [EnableQuery]
        public SingleResult<EmpEmployee> GetEmpEmployee([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmployeeLeaves.Where(m => m.EmployeeLeaveId == key).Select(m => m.EmpEmployee));
        }

        // GET: odata/EmployeeLeaves(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmployeeLeaves.Where(m => m.EmployeeLeaveId == key).Select(m => m.MasterData));
        }

        // GET: odata/EmployeeLeaves(5)/MasterData1
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData1([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmployeeLeaves.Where(m => m.EmployeeLeaveId == key).Select(m => m.MasterData1));
        }

        // GET: odata/EmployeeLeaves(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmployeeLeaves.Where(m => m.EmployeeLeaveId == key).Select(m => m.Organization));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeLeafExists(short key)
        {
            return db.EmployeeLeaves.Count(e => e.EmployeeLeaveId == key) > 0;
        }
    }
}
