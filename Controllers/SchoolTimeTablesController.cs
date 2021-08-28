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
    builder.EntitySet<SchoolTimeTable>("SchoolTimeTables");
    builder.EntitySet<Batch>("Batches"); 
    builder.EntitySet<ClassSubject>("ClassSubjects"); 
    builder.EntitySet<Organization>("Organizations"); 
    builder.EntitySet<SchoolClassPeriod>("SchoolClassPeriods"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class SchoolTimeTablesController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/SchoolTimeTables
        [EnableQuery]
        public IQueryable<SchoolTimeTable> GetSchoolTimeTables()
        {
            return db.SchoolTimeTables;
        }

        // GET: odata/SchoolTimeTables(5)
        [EnableQuery]
        public SingleResult<SchoolTimeTable> GetSchoolTimeTable([FromODataUri] int key)
        {
            return SingleResult.Create(db.SchoolTimeTables.Where(schoolTimeTable => schoolTimeTable.TimeTableId == key));
        }

        // PUT: odata/SchoolTimeTables(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<SchoolTimeTable> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SchoolTimeTable schoolTimeTable = await db.SchoolTimeTables.FindAsync(key);
            if (schoolTimeTable == null)
            {
                return NotFound();
            }

            patch.Put(schoolTimeTable);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolTimeTableExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(schoolTimeTable);
        }

        // POST: odata/SchoolTimeTables
        public async Task<IHttpActionResult> Post(SchoolTimeTable schoolTimeTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SchoolTimeTables.Add(schoolTimeTable);
            await db.SaveChangesAsync();

            return Created(schoolTimeTable);
        }

        // PATCH: odata/SchoolTimeTables(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<SchoolTimeTable> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SchoolTimeTable schoolTimeTable = await db.SchoolTimeTables.FindAsync(key);
            if (schoolTimeTable == null)
            {
                return NotFound();
            }

            patch.Patch(schoolTimeTable);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolTimeTableExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(schoolTimeTable);
        }

        // DELETE: odata/SchoolTimeTables(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            SchoolTimeTable schoolTimeTable = await db.SchoolTimeTables.FindAsync(key);
            if (schoolTimeTable == null)
            {
                return NotFound();
            }

            db.SchoolTimeTables.Remove(schoolTimeTable);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/SchoolTimeTables(5)/Batch
        [EnableQuery]
        public SingleResult<Batch> GetBatch([FromODataUri] int key)
        {
            return SingleResult.Create(db.SchoolTimeTables.Where(m => m.TimeTableId == key).Select(m => m.Batch));
        }

        // GET: odata/SchoolTimeTables(5)/ClassSubject
        [EnableQuery]
        public SingleResult<ClassSubject> GetClassSubject([FromODataUri] int key)
        {
            return SingleResult.Create(db.SchoolTimeTables.Where(m => m.TimeTableId == key).Select(m => m.ClassSubject));
        }

        // GET: odata/SchoolTimeTables(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] int key)
        {
            return SingleResult.Create(db.SchoolTimeTables.Where(m => m.TimeTableId == key).Select(m => m.Organization));
        }

        // GET: odata/SchoolTimeTables(5)/SchoolClassPeriod
        [EnableQuery]
        public SingleResult<SchoolClassPeriod> GetSchoolClassPeriod([FromODataUri] int key)
        {
            return SingleResult.Create(db.SchoolTimeTables.Where(m => m.TimeTableId == key).Select(m => m.SchoolClassPeriod));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SchoolTimeTableExists(int key)
        {
            return db.SchoolTimeTables.Count(e => e.TimeTableId == key) > 0;
        }
    }
}
