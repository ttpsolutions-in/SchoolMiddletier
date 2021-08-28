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
    builder.EntitySet<LeaveBalance>("LeaveBalances");
    builder.EntitySet<Batch>("Batches"); 
    builder.EntitySet<EmpEmployee>("EmpEmployees"); 
    builder.EntitySet<LeavePolicy>("LeavePolicies"); 
    builder.EntitySet<Organization>("Organizations"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class LeaveBalancesController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/LeaveBalances
        [EnableQuery]
        public IQueryable<LeaveBalance> GetLeaveBalances()
        {
            return db.LeaveBalances;
        }

        // GET: odata/LeaveBalances(5)
        [EnableQuery]
        public SingleResult<LeaveBalance> GetLeaveBalance([FromODataUri] int key)
        {
            return SingleResult.Create(db.LeaveBalances.Where(leaveBalance => leaveBalance.LeaveBalanceId == key));
        }

        // PUT: odata/LeaveBalances(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<LeaveBalance> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LeaveBalance leaveBalance = await db.LeaveBalances.FindAsync(key);
            if (leaveBalance == null)
            {
                return NotFound();
            }

            patch.Put(leaveBalance);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveBalanceExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(leaveBalance);
        }

        // POST: odata/LeaveBalances
        public async Task<IHttpActionResult> Post(LeaveBalance leaveBalance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LeaveBalances.Add(leaveBalance);
            await db.SaveChangesAsync();

            return Created(leaveBalance);
        }

        // PATCH: odata/LeaveBalances(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<LeaveBalance> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LeaveBalance leaveBalance = await db.LeaveBalances.FindAsync(key);
            if (leaveBalance == null)
            {
                return NotFound();
            }

            patch.Patch(leaveBalance);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveBalanceExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(leaveBalance);
        }

        // DELETE: odata/LeaveBalances(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            LeaveBalance leaveBalance = await db.LeaveBalances.FindAsync(key);
            if (leaveBalance == null)
            {
                return NotFound();
            }

            db.LeaveBalances.Remove(leaveBalance);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/LeaveBalances(5)/Batch
        [EnableQuery]
        public SingleResult<Batch> GetBatch([FromODataUri] int key)
        {
            return SingleResult.Create(db.LeaveBalances.Where(m => m.LeaveBalanceId == key).Select(m => m.Batch));
        }

        // GET: odata/LeaveBalances(5)/EmpEmployee
        [EnableQuery]
        public SingleResult<EmpEmployee> GetEmpEmployee([FromODataUri] int key)
        {
            return SingleResult.Create(db.LeaveBalances.Where(m => m.LeaveBalanceId == key).Select(m => m.EmpEmployee));
        }

        // GET: odata/LeaveBalances(5)/LeavePolicy
        [EnableQuery]
        public SingleResult<LeavePolicy> GetLeavePolicy([FromODataUri] int key)
        {
            return SingleResult.Create(db.LeaveBalances.Where(m => m.LeaveBalanceId == key).Select(m => m.LeavePolicy));
        }

        // GET: odata/LeaveBalances(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] int key)
        {
            return SingleResult.Create(db.LeaveBalances.Where(m => m.LeaveBalanceId == key).Select(m => m.Organization));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LeaveBalanceExists(int key)
        {
            return db.LeaveBalances.Count(e => e.LeaveBalanceId == key) > 0;
        }
    }
}
