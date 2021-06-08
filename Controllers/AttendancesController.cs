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
    builder.EntitySet<Attendance>("Attendances");
    builder.EntitySet<Organization>("Organizations"); 
    builder.EntitySet<StudentClass>("StudentClasses"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AttendancesController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/Attendances
        [EnableQuery]
        public IQueryable<Attendance> GetAttendances()
        {
            return db.Attendances;
        }

        // GET: odata/Attendances(5)
        [EnableQuery]
        public SingleResult<Attendance> GetAttendance([FromODataUri] int key)
        {
            return SingleResult.Create(db.Attendances.Where(attendance => attendance.AttendanceId == key));
        }

        // PUT: odata/Attendances(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Attendance> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Attendance attendance = await db.Attendances.FindAsync(key);
            if (attendance == null)
            {
                return NotFound();
            }

            patch.Put(attendance);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(attendance);
        }

        // POST: odata/Attendances
        public async Task<IHttpActionResult> Post(Attendance attendance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Attendances.Add(attendance);
            await db.SaveChangesAsync();

            return Created(attendance);
        }

        // PATCH: odata/Attendances(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Attendance> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Attendance attendance = await db.Attendances.FindAsync(key);
            if (attendance == null)
            {
                return NotFound();
            }

            patch.Patch(attendance);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(attendance);
        }

        // DELETE: odata/Attendances(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Attendance attendance = await db.Attendances.FindAsync(key);
            if (attendance == null)
            {
                return NotFound();
            }

            db.Attendances.Remove(attendance);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Attendances(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] int key)
        {
            return SingleResult.Create(db.Attendances.Where(m => m.AttendanceId == key).Select(m => m.Organization));
        }

        // GET: odata/Attendances(5)/StudentClass
        [EnableQuery]
        public SingleResult<StudentClass> GetStudentClass([FromODataUri] int key)
        {
            return SingleResult.Create(db.Attendances.Where(m => m.AttendanceId == key).Select(m => m.StudentClass));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AttendanceExists(int key)
        {
            return db.Attendances.Count(e => e.AttendanceId == key) > 0;
        }
    }
}
