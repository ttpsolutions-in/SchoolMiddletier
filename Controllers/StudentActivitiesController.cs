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
    builder.EntitySet<StudentActivity>("StudentActivities");
    builder.EntitySet<Organization>("Organizations"); 
    builder.EntitySet<StudentClass>("StudentClasses"); 
    builder.EntitySet<Student>("Students"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class StudentActivitiesController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/StudentActivities
        [EnableQuery]
        public IQueryable<StudentActivity> GetStudentActivities()
        {
            return db.StudentActivities;
        }

        // GET: odata/StudentActivities(5)
        [EnableQuery]
        public SingleResult<StudentActivity> GetStudentActivity([FromODataUri] short key)
        {
            return SingleResult.Create(db.StudentActivities.Where(studentActivity => studentActivity.StudentActivityId == key));
        }

        // PUT: odata/StudentActivities(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<StudentActivity> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StudentActivity studentActivity = await db.StudentActivities.FindAsync(key);
            if (studentActivity == null)
            {
                return NotFound();
            }

            patch.Put(studentActivity);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentActivityExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(studentActivity);
        }

        // POST: odata/StudentActivities
        public async Task<IHttpActionResult> Post(StudentActivity studentActivity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StudentActivities.Add(studentActivity);
            await db.SaveChangesAsync();

            return Created(studentActivity);
        }

        // PATCH: odata/StudentActivities(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<StudentActivity> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StudentActivity studentActivity = await db.StudentActivities.FindAsync(key);
            if (studentActivity == null)
            {
                return NotFound();
            }

            patch.Patch(studentActivity);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentActivityExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(studentActivity);
        }

        // DELETE: odata/StudentActivities(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            StudentActivity studentActivity = await db.StudentActivities.FindAsync(key);
            if (studentActivity == null)
            {
                return NotFound();
            }

            db.StudentActivities.Remove(studentActivity);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/StudentActivities(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.StudentActivities.Where(m => m.StudentActivityId == key).Select(m => m.Organization));
        }

        // GET: odata/StudentActivities(5)/StudentClass
        [EnableQuery]
        public SingleResult<StudentClass> GetStudentClass([FromODataUri] short key)
        {
            return SingleResult.Create(db.StudentActivities.Where(m => m.StudentActivityId == key).Select(m => m.StudentClass));
        }

        // GET: odata/StudentActivities(5)/Student
        [EnableQuery]
        public SingleResult<Student> GetStudent([FromODataUri] short key)
        {
            return SingleResult.Create(db.StudentActivities.Where(m => m.StudentActivityId == key).Select(m => m.Student));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentActivityExists(short key)
        {
            return db.StudentActivities.Count(e => e.StudentActivityId == key) > 0;
        }
    }
}
