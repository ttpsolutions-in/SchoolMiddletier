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
    builder.EntitySet<SchoolClassPeriod>("SchoolClassPeriods");
    builder.EntitySet<Batch>("Batches"); 
    builder.EntitySet<MasterData>("MasterDatas"); 
    builder.EntitySet<Organization>("Organizations"); 
    builder.EntitySet<SchoolTimeTable>("SchoolTimeTables"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class SchoolClassPeriodsController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/SchoolClassPeriods
        [EnableQuery]
        public IQueryable<SchoolClassPeriod> GetSchoolClassPeriods()
        {
            return db.SchoolClassPeriods;
        }

        // GET: odata/SchoolClassPeriods(5)
        [EnableQuery]
        public SingleResult<SchoolClassPeriod> GetSchoolClassPeriod([FromODataUri] int key)
        {
            return SingleResult.Create(db.SchoolClassPeriods.Where(schoolClassPeriod => schoolClassPeriod.SchoolClassPeriodId == key));
        }

        // PUT: odata/SchoolClassPeriods(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<SchoolClassPeriod> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SchoolClassPeriod schoolClassPeriod = await db.SchoolClassPeriods.FindAsync(key);
            if (schoolClassPeriod == null)
            {
                return NotFound();
            }

            patch.Put(schoolClassPeriod);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolClassPeriodExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(schoolClassPeriod);
        }

        // POST: odata/SchoolClassPeriods
        public async Task<IHttpActionResult> Post(SchoolClassPeriod schoolClassPeriod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SchoolClassPeriods.Add(schoolClassPeriod);
            await db.SaveChangesAsync();

            return Created(schoolClassPeriod);
        }

        // PATCH: odata/SchoolClassPeriods(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<SchoolClassPeriod> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SchoolClassPeriod schoolClassPeriod = await db.SchoolClassPeriods.FindAsync(key);
            if (schoolClassPeriod == null)
            {
                return NotFound();
            }

            patch.Patch(schoolClassPeriod);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolClassPeriodExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(schoolClassPeriod);
        }

        // DELETE: odata/SchoolClassPeriods(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            SchoolClassPeriod schoolClassPeriod = await db.SchoolClassPeriods.FindAsync(key);
            if (schoolClassPeriod == null)
            {
                return NotFound();
            }

            db.SchoolClassPeriods.Remove(schoolClassPeriod);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/SchoolClassPeriods(5)/Batch
        [EnableQuery]
        public SingleResult<Batch> GetBatch([FromODataUri] int key)
        {
            return SingleResult.Create(db.SchoolClassPeriods.Where(m => m.SchoolClassPeriodId == key).Select(m => m.Batch));
        }

        // GET: odata/SchoolClassPeriods(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] int key)
        {
            return SingleResult.Create(db.SchoolClassPeriods.Where(m => m.SchoolClassPeriodId == key).Select(m => m.MasterData));
        }

        // GET: odata/SchoolClassPeriods(5)/MasterData1
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData1([FromODataUri] int key)
        {
            return SingleResult.Create(db.SchoolClassPeriods.Where(m => m.SchoolClassPeriodId == key).Select(m => m.MasterData1));
        }

        // GET: odata/SchoolClassPeriods(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] int key)
        {
            return SingleResult.Create(db.SchoolClassPeriods.Where(m => m.SchoolClassPeriodId == key).Select(m => m.Organization));
        }

        // GET: odata/SchoolClassPeriods(5)/SchoolTimeTables
        [EnableQuery]
        public IQueryable<SchoolTimeTable> GetSchoolTimeTables([FromODataUri] int key)
        {
            return db.SchoolClassPeriods.Where(m => m.SchoolClassPeriodId == key).SelectMany(m => m.SchoolTimeTables);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SchoolClassPeriodExists(int key)
        {
            return db.SchoolClassPeriods.Count(e => e.SchoolClassPeriodId == key) > 0;
        }
    }
}
