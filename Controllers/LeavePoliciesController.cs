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
    builder.EntitySet<LeavePolicy>("LeavePolicies");
    builder.EntitySet<Batch>("Batches"); 
    builder.EntitySet<MasterData>("MasterDatas"); 
    builder.EntitySet<Organization>("Organizations"); 
    builder.EntitySet<LeaveBalance>("LeaveBalances"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class LeavePoliciesController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/LeavePolicies
        [EnableQuery]
        public IQueryable<LeavePolicy> GetLeavePolicies()
        {
            return db.LeavePolicies;
        }

        // GET: odata/LeavePolicies(5)
        [EnableQuery]
        public SingleResult<LeavePolicy> GetLeavePolicy([FromODataUri] int key)
        {
            return SingleResult.Create(db.LeavePolicies.Where(leavePolicy => leavePolicy.LeavePolicyId == key));
        }

        // PUT: odata/LeavePolicies(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<LeavePolicy> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LeavePolicy leavePolicy = await db.LeavePolicies.FindAsync(key);
            if (leavePolicy == null)
            {
                return NotFound();
            }

            patch.Put(leavePolicy);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeavePolicyExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(leavePolicy);
        }

        // POST: odata/LeavePolicies
        public async Task<IHttpActionResult> Post(LeavePolicy leavePolicy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LeavePolicies.Add(leavePolicy);
            await db.SaveChangesAsync();

            return Created(leavePolicy);
        }

        // PATCH: odata/LeavePolicies(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<LeavePolicy> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LeavePolicy leavePolicy = await db.LeavePolicies.FindAsync(key);
            if (leavePolicy == null)
            {
                return NotFound();
            }

            patch.Patch(leavePolicy);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeavePolicyExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(leavePolicy);
        }

        // DELETE: odata/LeavePolicies(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            LeavePolicy leavePolicy = await db.LeavePolicies.FindAsync(key);
            if (leavePolicy == null)
            {
                return NotFound();
            }

            db.LeavePolicies.Remove(leavePolicy);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/LeavePolicies(5)/Batch
        [EnableQuery]
        public SingleResult<Batch> GetBatch([FromODataUri] int key)
        {
            return SingleResult.Create(db.LeavePolicies.Where(m => m.LeavePolicyId == key).Select(m => m.Batch));
        }

        // GET: odata/LeavePolicies(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] int key)
        {
            return SingleResult.Create(db.LeavePolicies.Where(m => m.LeavePolicyId == key).Select(m => m.MasterData));
        }

        // GET: odata/LeavePolicies(5)/MasterData1
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData1([FromODataUri] int key)
        {
            return SingleResult.Create(db.LeavePolicies.Where(m => m.LeavePolicyId == key).Select(m => m.MasterData1));
        }

        // GET: odata/LeavePolicies(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] int key)
        {
            return SingleResult.Create(db.LeavePolicies.Where(m => m.LeavePolicyId == key).Select(m => m.Organization));
        }

        // GET: odata/LeavePolicies(5)/LeaveBalances
        [EnableQuery]
        public IQueryable<LeaveBalance> GetLeaveBalances([FromODataUri] int key)
        {
            return db.LeavePolicies.Where(m => m.LeavePolicyId == key).SelectMany(m => m.LeaveBalances);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LeavePolicyExists(int key)
        {
            return db.LeavePolicies.Count(e => e.LeavePolicyId == key) > 0;
        }
    }
}
