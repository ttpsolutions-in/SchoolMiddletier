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
    builder.EntitySet<LeaveEmployeeLeaf>("LeaveEmployeeLeaves");
    builder.EntitySet<EmpEmployee>("EmpEmployees"); 
    builder.EntitySet<MasterData>("MasterDatas"); 
    builder.EntitySet<Organization>("Organizations"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class LeaveEmployeeLeavesController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/LeaveEmployeeLeaves
        [EnableQuery]
        public IQueryable<LeaveEmployeeLeaf> GetLeaveEmployeeLeaves()
        {
            return db.LeaveEmployeeLeaves;
        }

        // GET: odata/LeaveEmployeeLeaves(5)
        [EnableQuery]
        public SingleResult<LeaveEmployeeLeaf> GetLeaveEmployeeLeaf([FromODataUri] int key)
        {
            return SingleResult.Create(db.LeaveEmployeeLeaves.Where(leaveEmployeeLeaf => leaveEmployeeLeaf.EmployeeLeaveId == key));
        }

        // PUT: odata/LeaveEmployeeLeaves(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<LeaveEmployeeLeaf> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LeaveEmployeeLeaf leaveEmployeeLeaf = await db.LeaveEmployeeLeaves.FindAsync(key);
            if (leaveEmployeeLeaf == null)
            {
                return NotFound();
            }

            patch.Put(leaveEmployeeLeaf);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveEmployeeLeafExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(leaveEmployeeLeaf);
        }

        // POST: odata/LeaveEmployeeLeaves
        public async Task<IHttpActionResult> Post(LeaveEmployeeLeaf leaveEmployeeLeaf)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LeaveEmployeeLeaves.Add(leaveEmployeeLeaf);
            await db.SaveChangesAsync();

            return Created(leaveEmployeeLeaf);
        }

        // PATCH: odata/LeaveEmployeeLeaves(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<LeaveEmployeeLeaf> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LeaveEmployeeLeaf leaveEmployeeLeaf = await db.LeaveEmployeeLeaves.FindAsync(key);
            if (leaveEmployeeLeaf == null)
            {
                return NotFound();
            }

            patch.Patch(leaveEmployeeLeaf);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveEmployeeLeafExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(leaveEmployeeLeaf);
        }

        // DELETE: odata/LeaveEmployeeLeaves(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            LeaveEmployeeLeaf leaveEmployeeLeaf = await db.LeaveEmployeeLeaves.FindAsync(key);
            if (leaveEmployeeLeaf == null)
            {
                return NotFound();
            }

            db.LeaveEmployeeLeaves.Remove(leaveEmployeeLeaf);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/LeaveEmployeeLeaves(5)/EmpEmployee
        [EnableQuery]
        public SingleResult<EmpEmployee> GetEmpEmployee([FromODataUri] int key)
        {
            return SingleResult.Create(db.LeaveEmployeeLeaves.Where(m => m.EmployeeLeaveId == key).Select(m => m.EmpEmployee));
        }

        // GET: odata/LeaveEmployeeLeaves(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] int key)
        {
            return SingleResult.Create(db.LeaveEmployeeLeaves.Where(m => m.EmployeeLeaveId == key).Select(m => m.MasterData));
        }

        // GET: odata/LeaveEmployeeLeaves(5)/MasterData1
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData1([FromODataUri] int key)
        {
            return SingleResult.Create(db.LeaveEmployeeLeaves.Where(m => m.EmployeeLeaveId == key).Select(m => m.MasterData1));
        }

        // GET: odata/LeaveEmployeeLeaves(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] int key)
        {
            return SingleResult.Create(db.LeaveEmployeeLeaves.Where(m => m.EmployeeLeaveId == key).Select(m => m.Organization));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LeaveEmployeeLeafExists(int key)
        {
            return db.LeaveEmployeeLeaves.Count(e => e.EmployeeLeaveId == key) > 0;
        }
    }
}
