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
    builder.EntitySet<AttendanceReport>("AttendanceReports");
    builder.EntitySet<Batch>("Batches"); 
    builder.EntitySet<EmpEmployee>("EmpEmployees"); 
    builder.EntitySet<Organization>("Organizations"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AttendanceReportsController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/AttendanceReports
        [EnableQuery]
        public IQueryable<AttendanceReport> GetAttendanceReports()
        {
            return db.AttendanceReports;
        }

        // GET: odata/AttendanceReports(5)
        [EnableQuery]
        public SingleResult<AttendanceReport> GetAttendanceReport([FromODataUri] int key)
        {
            return SingleResult.Create(db.AttendanceReports.Where(attendanceReport => attendanceReport.AttendanceReportId == key));
        }

        // PUT: odata/AttendanceReports(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<AttendanceReport> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AttendanceReport attendanceReport = await db.AttendanceReports.FindAsync(key);
            if (attendanceReport == null)
            {
                return NotFound();
            }

            patch.Put(attendanceReport);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceReportExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(attendanceReport);
        }

        // POST: odata/AttendanceReports
        public async Task<IHttpActionResult> Post(AttendanceReport attendanceReport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AttendanceReports.Add(attendanceReport);
            await db.SaveChangesAsync();

            return Created(attendanceReport);
        }

        // PATCH: odata/AttendanceReports(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<AttendanceReport> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AttendanceReport attendanceReport = await db.AttendanceReports.FindAsync(key);
            if (attendanceReport == null)
            {
                return NotFound();
            }

            patch.Patch(attendanceReport);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceReportExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(attendanceReport);
        }

        // DELETE: odata/AttendanceReports(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            AttendanceReport attendanceReport = await db.AttendanceReports.FindAsync(key);
            if (attendanceReport == null)
            {
                return NotFound();
            }

            db.AttendanceReports.Remove(attendanceReport);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/AttendanceReports(5)/Batch
        [EnableQuery]
        public SingleResult<Batch> GetBatch([FromODataUri] int key)
        {
            return SingleResult.Create(db.AttendanceReports.Where(m => m.AttendanceReportId == key).Select(m => m.Batch));
        }

        // GET: odata/AttendanceReports(5)/EmpEmployee
        [EnableQuery]
        public SingleResult<EmpEmployee> GetEmpEmployee([FromODataUri] int key)
        {
            return SingleResult.Create(db.AttendanceReports.Where(m => m.AttendanceReportId == key).Select(m => m.EmpEmployee));
        }

        // GET: odata/AttendanceReports(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] int key)
        {
            return SingleResult.Create(db.AttendanceReports.Where(m => m.AttendanceReportId == key).Select(m => m.Organization));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AttendanceReportExists(int key)
        {
            return db.AttendanceReports.Count(e => e.AttendanceReportId == key) > 0;
        }
    }
}
