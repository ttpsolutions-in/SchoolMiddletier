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
    builder.EntitySet<Exam>("Exams");
    builder.EntitySet<Batch>("Batches"); 
    builder.EntitySet<MasterData>("MasterDatas"); 
    builder.EntitySet<Organization>("Organizations"); 
    builder.EntitySet<ExamSlot>("ExamSlots"); 
    builder.EntitySet<ExamStudentResult>("ExamStudentResults"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ExamsController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/Exams
        [EnableQuery]
        public IQueryable<Exam> GetExams()
        {
            return db.Exams;
        }

        // GET: odata/Exams(5)
        [EnableQuery]
        public SingleResult<Exam> GetExam([FromODataUri] short key)
        {
            return SingleResult.Create(db.Exams.Where(exam => exam.ExamId == key));
        }

        // PUT: odata/Exams(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<Exam> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Exam exam = await db.Exams.FindAsync(key);
            if (exam == null)
            {
                return NotFound();
            }

            patch.Put(exam);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(exam);
        }

        // POST: odata/Exams
        public async Task<IHttpActionResult> Post(Exam exam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Exams.Add(exam);
            await db.SaveChangesAsync();

            return Created(exam);
        }

        // PATCH: odata/Exams(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<Exam> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Exam exam = await db.Exams.FindAsync(key);
            if (exam == null)
            {
                return NotFound();
            }

            patch.Patch(exam);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(exam);
        }

        // DELETE: odata/Exams(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            Exam exam = await db.Exams.FindAsync(key);
            if (exam == null)
            {
                return NotFound();
            }

            db.Exams.Remove(exam);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Exams(5)/Batch
        [EnableQuery]
        public SingleResult<Batch> GetBatch([FromODataUri] short key)
        {
            return SingleResult.Create(db.Exams.Where(m => m.ExamId == key).Select(m => m.Batch));
        }

        // GET: odata/Exams(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] short key)
        {
            return SingleResult.Create(db.Exams.Where(m => m.ExamId == key).Select(m => m.MasterData));
        }

        // GET: odata/Exams(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.Exams.Where(m => m.ExamId == key).Select(m => m.Organization));
        }

        // GET: odata/Exams(5)/ExamSlots
        [EnableQuery]
        public IQueryable<ExamSlot> GetExamSlots([FromODataUri] short key)
        {
            return db.Exams.Where(m => m.ExamId == key).SelectMany(m => m.ExamSlots);
        }

        // GET: odata/Exams(5)/ExamStudentResults
        [EnableQuery]
        public IQueryable<ExamStudentResult> GetExamStudentResults([FromODataUri] short key)
        {
            return db.Exams.Where(m => m.ExamId == key).SelectMany(m => m.ExamStudentResults);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExamExists(short key)
        {
            return db.Exams.Count(e => e.ExamId == key) > 0;
        }
    }
}
