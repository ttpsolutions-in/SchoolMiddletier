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
    builder.EntitySet<StudentClassSubject>("StudentClassSubjects");
    builder.EntitySet<ExamStudentSubject>("ExamStudentSubjects"); 
    builder.EntitySet<MasterData>("MasterDatas"); 
    builder.EntitySet<Organization>("Organizations"); 
    builder.EntitySet<StudentClass>("StudentClasses"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class StudentClassSubjectsController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/StudentClassSubjects
        [EnableQuery]
        public IQueryable<StudentClassSubject> GetStudentClassSubjects()
        {
            return db.StudentClassSubjects;
        }

        // GET: odata/StudentClassSubjects(5)
        [EnableQuery]
        public SingleResult<StudentClassSubject> GetStudentClassSubject([FromODataUri] short key)
        {
            return SingleResult.Create(db.StudentClassSubjects.Where(studentClassSubject => studentClassSubject.StudentClassSubjectId == key));
        }

        // PUT: odata/StudentClassSubjects(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<StudentClassSubject> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StudentClassSubject studentClassSubject = await db.StudentClassSubjects.FindAsync(key);
            if (studentClassSubject == null)
            {
                return NotFound();
            }

            patch.Put(studentClassSubject);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentClassSubjectExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(studentClassSubject);
        }

        // POST: odata/StudentClassSubjects
        public async Task<IHttpActionResult> Post(StudentClassSubject studentClassSubject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StudentClassSubjects.Add(studentClassSubject);
            await db.SaveChangesAsync();

            return Created(studentClassSubject);
        }

        // PATCH: odata/StudentClassSubjects(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<StudentClassSubject> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StudentClassSubject studentClassSubject = await db.StudentClassSubjects.FindAsync(key);
            if (studentClassSubject == null)
            {
                return NotFound();
            }

            patch.Patch(studentClassSubject);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentClassSubjectExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(studentClassSubject);
        }

        // DELETE: odata/StudentClassSubjects(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            StudentClassSubject studentClassSubject = await db.StudentClassSubjects.FindAsync(key);
            if (studentClassSubject == null)
            {
                return NotFound();
            }

            db.StudentClassSubjects.Remove(studentClassSubject);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/StudentClassSubjects(5)/ExamStudentSubjects
        [EnableQuery]
        public IQueryable<ExamStudentSubject> GetExamStudentSubjects([FromODataUri] short key)
        {
            return db.StudentClassSubjects.Where(m => m.StudentClassSubjectId == key).SelectMany(m => m.ExamStudentSubjects);
        }

        // GET: odata/StudentClassSubjects(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] short key)
        {
            return SingleResult.Create(db.StudentClassSubjects.Where(m => m.StudentClassSubjectId == key).Select(m => m.MasterData));
        }

        // GET: odata/StudentClassSubjects(5)/MasterData1
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData1([FromODataUri] short key)
        {
            return SingleResult.Create(db.StudentClassSubjects.Where(m => m.StudentClassSubjectId == key).Select(m => m.MasterData1));
        }

        // GET: odata/StudentClassSubjects(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.StudentClassSubjects.Where(m => m.StudentClassSubjectId == key).Select(m => m.Organization));
        }

        // GET: odata/StudentClassSubjects(5)/StudentClass
        [EnableQuery]
        public SingleResult<StudentClass> GetStudentClass([FromODataUri] short key)
        {
            return SingleResult.Create(db.StudentClassSubjects.Where(m => m.StudentClassSubjectId == key).Select(m => m.StudentClass));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentClassSubjectExists(short key)
        {
            return db.StudentClassSubjects.Count(e => e.StudentClassSubjectId == key) > 0;
        }
    }
}
