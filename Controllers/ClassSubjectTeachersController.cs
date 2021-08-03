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
    builder.EntitySet<ClassSubjectTeacher>("ClassSubjectTeachers");
    builder.EntitySet<Batch>("Batches"); 
    builder.EntitySet<ClassSubject>("ClassSubjects"); 
    builder.EntitySet<EmpEmployee>("EmpEmployees"); 
    builder.EntitySet<Organization>("Organizations"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ClassSubjectTeachersController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/ClassSubjectTeachers
        [EnableQuery]
        public IQueryable<ClassSubjectTeacher> GetClassSubjectTeachers()
        {
            return db.ClassSubjectTeachers;
        }

        // GET: odata/ClassSubjectTeachers(5)
        [EnableQuery]
        public SingleResult<ClassSubjectTeacher> GetClassSubjectTeacher([FromODataUri] short key)
        {
            return SingleResult.Create(db.ClassSubjectTeachers.Where(classSubjectTeacher => classSubjectTeacher.ClassSubjectTeacherId == key));
        }

        // PUT: odata/ClassSubjectTeachers(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<ClassSubjectTeacher> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ClassSubjectTeacher classSubjectTeacher = await db.ClassSubjectTeachers.FindAsync(key);
            if (classSubjectTeacher == null)
            {
                return NotFound();
            }

            patch.Put(classSubjectTeacher);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassSubjectTeacherExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(classSubjectTeacher);
        }

        // POST: odata/ClassSubjectTeachers
        public async Task<IHttpActionResult> Post(ClassSubjectTeacher classSubjectTeacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ClassSubjectTeachers.Add(classSubjectTeacher);
            await db.SaveChangesAsync();

            return Created(classSubjectTeacher);
        }

        // PATCH: odata/ClassSubjectTeachers(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<ClassSubjectTeacher> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ClassSubjectTeacher classSubjectTeacher = await db.ClassSubjectTeachers.FindAsync(key);
            if (classSubjectTeacher == null)
            {
                return NotFound();
            }

            patch.Patch(classSubjectTeacher);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassSubjectTeacherExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(classSubjectTeacher);
        }

        // DELETE: odata/ClassSubjectTeachers(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            ClassSubjectTeacher classSubjectTeacher = await db.ClassSubjectTeachers.FindAsync(key);
            if (classSubjectTeacher == null)
            {
                return NotFound();
            }

            db.ClassSubjectTeachers.Remove(classSubjectTeacher);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ClassSubjectTeachers(5)/Batch
        [EnableQuery]
        public SingleResult<Batch> GetBatch([FromODataUri] short key)
        {
            return SingleResult.Create(db.ClassSubjectTeachers.Where(m => m.ClassSubjectTeacherId == key).Select(m => m.Batch));
        }

        // GET: odata/ClassSubjectTeachers(5)/ClassSubject
        [EnableQuery]
        public SingleResult<ClassSubject> GetClassSubject([FromODataUri] short key)
        {
            return SingleResult.Create(db.ClassSubjectTeachers.Where(m => m.ClassSubjectTeacherId == key).Select(m => m.ClassSubject));
        }

        // GET: odata/ClassSubjectTeachers(5)/EmpEmployee
        [EnableQuery]
        public SingleResult<EmpEmployee> GetEmpEmployee([FromODataUri] short key)
        {
            return SingleResult.Create(db.ClassSubjectTeachers.Where(m => m.ClassSubjectTeacherId == key).Select(m => m.EmpEmployee));
        }

        // GET: odata/ClassSubjectTeachers(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.ClassSubjectTeachers.Where(m => m.ClassSubjectTeacherId == key).Select(m => m.Organization));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClassSubjectTeacherExists(short key)
        {
            return db.ClassSubjectTeachers.Count(e => e.ClassSubjectTeacherId == key) > 0;
        }
    }
}
