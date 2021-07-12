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
    builder.EntitySet<ExamStudentResult>("ExamStudentResults");
    builder.EntitySet<Batch>("Batches"); 
    builder.EntitySet<Exam>("Exams"); 
    builder.EntitySet<StudentClass>("StudentClasses"); 
    builder.EntitySet<MasterData>("MasterDatas"); 
    builder.EntitySet<Organization>("Organizations"); 
    builder.EntitySet<ExamStudentSubjectResult>("ExamStudentSubjectResults"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ExamStudentResultsController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/ExamStudentResults
        [EnableQuery]
        public IQueryable<ExamStudentResult> GetExamStudentResults()
        {
            return db.ExamStudentResults;
        }

        // GET: odata/ExamStudentResults(5)
        [EnableQuery]
        public SingleResult<ExamStudentResult> GetExamStudentResult([FromODataUri] short key)
        {
            return SingleResult.Create(db.ExamStudentResults.Where(examStudentResult => examStudentResult.ExamStudentResultId == key));
        }

        // PUT: odata/ExamStudentResults(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<ExamStudentResult> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ExamStudentResult examStudentResult = await db.ExamStudentResults.FindAsync(key);
            if (examStudentResult == null)
            {
                return NotFound();
            }

            patch.Put(examStudentResult);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamStudentResultExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(examStudentResult);
        }

        // POST: odata/ExamStudentResults
        public async Task<IHttpActionResult> Post(ExamStudentResult examStudentResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ExamStudentResults.Add(examStudentResult);
            await db.SaveChangesAsync();

            return Created(examStudentResult);
        }

        // PATCH: odata/ExamStudentResults(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<ExamStudentResult> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ExamStudentResult examStudentResult = await db.ExamStudentResults.FindAsync(key);
            if (examStudentResult == null)
            {
                return NotFound();
            }

            patch.Patch(examStudentResult);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamStudentResultExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(examStudentResult);
        }

        // DELETE: odata/ExamStudentResults(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            ExamStudentResult examStudentResult = await db.ExamStudentResults.FindAsync(key);
            if (examStudentResult == null)
            {
                return NotFound();
            }

            db.ExamStudentResults.Remove(examStudentResult);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ExamStudentResults(5)/Batch
        [EnableQuery]
        public SingleResult<Batch> GetBatch([FromODataUri] short key)
        {
            return SingleResult.Create(db.ExamStudentResults.Where(m => m.ExamStudentResultId == key).Select(m => m.Batch));
        }

        // GET: odata/ExamStudentResults(5)/Exam
        [EnableQuery]
        public SingleResult<Exam> GetExam([FromODataUri] short key)
        {
            return SingleResult.Create(db.ExamStudentResults.Where(m => m.ExamStudentResultId == key).Select(m => m.Exam));
        }

        // GET: odata/ExamStudentResults(5)/StudentClass
        [EnableQuery]
        public SingleResult<StudentClass> GetStudentClass([FromODataUri] short key)
        {
            return SingleResult.Create(db.ExamStudentResults.Where(m => m.ExamStudentResultId == key).Select(m => m.StudentClass));
        }

        // GET: odata/ExamStudentResults(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] short key)
        {
            return SingleResult.Create(db.ExamStudentResults.Where(m => m.ExamStudentResultId == key).Select(m => m.MasterData));
        }

        // GET: odata/ExamStudentResults(5)/MasterData1
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData1([FromODataUri] short key)
        {
            return SingleResult.Create(db.ExamStudentResults.Where(m => m.ExamStudentResultId == key).Select(m => m.MasterData1));
        }

        // GET: odata/ExamStudentResults(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.ExamStudentResults.Where(m => m.ExamStudentResultId == key).Select(m => m.Organization));
        }

        //// GET: odata/ExamStudentResults(5)/ExamStudentSubjectResults
        //[EnableQuery]
        //public IQueryable<ExamStudentSubjectResult> GetExamStudentSubjectResults([FromODataUri] short key)
        //{
        //    return db.ExamStudentResults.Where(m => m.ExamStudentResultId == key).SelectMany(m => m.ExamStudentSubjectResults);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExamStudentResultExists(short key)
        {
            return db.ExamStudentResults.Count(e => e.ExamStudentResultId == key) > 0;
        }
    }
}
