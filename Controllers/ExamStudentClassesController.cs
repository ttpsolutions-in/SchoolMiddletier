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
    builder.EntitySet<ExamStudentClass>("ExamStudentClasses");
    builder.EntitySet<Exam>("Exams"); 
    builder.EntitySet<StudentClass>("StudentClasses"); 
    builder.EntitySet<MasterData>("MasterDatas"); 
    builder.EntitySet<ExamStudentSubject>("ExamStudentSubjects"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ExamStudentClassesController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/ExamStudentClasses
        [EnableQuery]
        public IQueryable<ExamStudentClass> GetExamStudentClasses()
        {
            return db.ExamStudentClasses;
        }

        // GET: odata/ExamStudentClasses(5)
        [EnableQuery]
        public SingleResult<ExamStudentClass> GetExamStudentClass([FromODataUri] short key)
        {
            return SingleResult.Create(db.ExamStudentClasses.Where(examStudentClass => examStudentClass.ExamStudentClassId == key));
        }

        // PUT: odata/ExamStudentClasses(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<ExamStudentClass> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ExamStudentClass examStudentClass = await db.ExamStudentClasses.FindAsync(key);
            if (examStudentClass == null)
            {
                return NotFound();
            }

            patch.Put(examStudentClass);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamStudentClassExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(examStudentClass);
        }

        // POST: odata/ExamStudentClasses
        public async Task<IHttpActionResult> Post(ExamStudentClass examStudentClass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ExamStudentClasses.Add(examStudentClass);
            await db.SaveChangesAsync();

            return Created(examStudentClass);
        }

        // PATCH: odata/ExamStudentClasses(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<ExamStudentClass> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ExamStudentClass examStudentClass = await db.ExamStudentClasses.FindAsync(key);
            if (examStudentClass == null)
            {
                return NotFound();
            }

            patch.Patch(examStudentClass);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamStudentClassExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(examStudentClass);
        }

        // DELETE: odata/ExamStudentClasses(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            ExamStudentClass examStudentClass = await db.ExamStudentClasses.FindAsync(key);
            if (examStudentClass == null)
            {
                return NotFound();
            }

            db.ExamStudentClasses.Remove(examStudentClass);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ExamStudentClasses(5)/Exam
        [EnableQuery]
        public SingleResult<Exam> GetExam([FromODataUri] short key)
        {
            return SingleResult.Create(db.ExamStudentClasses.Where(m => m.ExamStudentClassId == key).Select(m => m.Exam));
        }

        // GET: odata/ExamStudentClasses(5)/StudentClass
        [EnableQuery]
        public SingleResult<StudentClass> GetStudentClass([FromODataUri] short key)
        {
            return SingleResult.Create(db.ExamStudentClasses.Where(m => m.ExamStudentClassId == key).Select(m => m.StudentClass));
        }

        // GET: odata/ExamStudentClasses(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] short key)
        {
            return SingleResult.Create(db.ExamStudentClasses.Where(m => m.ExamStudentClassId == key).Select(m => m.MasterData));
        }

        // GET: odata/ExamStudentClasses(5)/MasterData1
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData1([FromODataUri] short key)
        {
            return SingleResult.Create(db.ExamStudentClasses.Where(m => m.ExamStudentClassId == key).Select(m => m.MasterData1));
        }

        // GET: odata/ExamStudentClasses(5)/ExamStudentSubjects
        [EnableQuery]
        public IQueryable<ExamStudentSubject> GetExamStudentSubjects([FromODataUri] short key)
        {
            return db.ExamStudentClasses.Where(m => m.ExamStudentClassId == key).SelectMany(m => m.ExamStudentSubjects);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExamStudentClassExists(short key)
        {
            return db.ExamStudentClasses.Count(e => e.ExamStudentClassId == key) > 0;
        }
    }
}
