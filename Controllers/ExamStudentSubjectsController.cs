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
    builder.EntitySet<ExamStudentSubject>("ExamStudentSubjects");
    builder.EntitySet<ExamStudentClass>("ExamStudentClasses"); 
    builder.EntitySet<MasterData>("MasterDatas"); 
    builder.EntitySet<StudentClassSubject>("StudentClassSubjects"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ExamStudentSubjectsController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/ExamStudentSubjects
        [EnableQuery]
        public IQueryable<ExamStudentSubject> GetExamStudentSubjects()
        {
            return db.ExamStudentSubjects;
        }

        // GET: odata/ExamStudentSubjects(5)
        [EnableQuery]
        public SingleResult<ExamStudentSubject> GetExamStudentSubject([FromODataUri] short key)
        {
            return SingleResult.Create(db.ExamStudentSubjects.Where(examStudentSubject => examStudentSubject.ExamStudentSubjectId == key));
        }

        // PUT: odata/ExamStudentSubjects(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<ExamStudentSubject> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ExamStudentSubject examStudentSubject = await db.ExamStudentSubjects.FindAsync(key);
            if (examStudentSubject == null)
            {
                return NotFound();
            }

            patch.Put(examStudentSubject);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamStudentSubjectExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(examStudentSubject);
        }

        // POST: odata/ExamStudentSubjects
        public async Task<IHttpActionResult> Post(ExamStudentSubject examStudentSubject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ExamStudentSubjects.Add(examStudentSubject);
            await db.SaveChangesAsync();

            return Created(examStudentSubject);
        }

        // PATCH: odata/ExamStudentSubjects(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<ExamStudentSubject> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ExamStudentSubject examStudentSubject = await db.ExamStudentSubjects.FindAsync(key);
            if (examStudentSubject == null)
            {
                return NotFound();
            }

            patch.Patch(examStudentSubject);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamStudentSubjectExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(examStudentSubject);
        }

        // DELETE: odata/ExamStudentSubjects(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            ExamStudentSubject examStudentSubject = await db.ExamStudentSubjects.FindAsync(key);
            if (examStudentSubject == null)
            {
                return NotFound();
            }

            db.ExamStudentSubjects.Remove(examStudentSubject);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ExamStudentSubjects(5)/ExamStudentClass
        [EnableQuery]
        public SingleResult<ExamStudentClass> GetExamStudentClass([FromODataUri] short key)
        {
            return SingleResult.Create(db.ExamStudentSubjects.Where(m => m.ExamStudentSubjectId == key).Select(m => m.ExamStudentClass));
        }

        // GET: odata/ExamStudentSubjects(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] short key)
        {
            return SingleResult.Create(db.ExamStudentSubjects.Where(m => m.ExamStudentSubjectId == key).Select(m => m.MasterData));
        }

        // GET: odata/ExamStudentSubjects(5)/StudentClassSubject
        [EnableQuery]
        public SingleResult<StudentClassSubject> GetStudentClassSubject([FromODataUri] short key)
        {
            return SingleResult.Create(db.ExamStudentSubjects.Where(m => m.ExamStudentSubjectId == key).Select(m => m.StudentClassSubject));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExamStudentSubjectExists(short key)
        {
            return db.ExamStudentSubjects.Count(e => e.ExamStudentSubjectId == key) > 0;
        }
    }
}
