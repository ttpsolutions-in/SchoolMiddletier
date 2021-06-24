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
    builder.EntitySet<ExamStudentSubjectResult>("ExamStudentSubjectResults");
    builder.EntitySet<ClassSubjectMarkComponent>("ClassSubjectMarkComponents"); 
    builder.EntitySet<MasterData>("MasterDatas"); 
    builder.EntitySet<Organization>("Organizations"); 
    builder.EntitySet<StudentClassSubject>("StudentClassSubjects"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ExamStudentSubjectResultsController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/ExamStudentSubjectResults
        [EnableQuery]
        public IQueryable<ExamStudentSubjectResult> GetExamStudentSubjectResults()
        {
            return db.ExamStudentSubjectResults;
        }

        // GET: odata/ExamStudentSubjectResults(5)
        [EnableQuery]
        public SingleResult<ExamStudentSubjectResult> GetExamStudentSubjectResult([FromODataUri] short key)
        {
            return SingleResult.Create(db.ExamStudentSubjectResults.Where(examStudentSubjectResult => examStudentSubjectResult.ExamStudentSubjectResultId == key));
        }

        // PUT: odata/ExamStudentSubjectResults(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<ExamStudentSubjectResult> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ExamStudentSubjectResult examStudentSubjectResult = await db.ExamStudentSubjectResults.FindAsync(key);
            if (examStudentSubjectResult == null)
            {
                return NotFound();
            }

            patch.Put(examStudentSubjectResult);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamStudentSubjectResultExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(examStudentSubjectResult);
        }

        // POST: odata/ExamStudentSubjectResults
        public async Task<IHttpActionResult> Post(ExamStudentSubjectResult examStudentSubjectResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ExamStudentSubjectResults.Add(examStudentSubjectResult);
            await db.SaveChangesAsync();

            return Created(examStudentSubjectResult);
        }

        // PATCH: odata/ExamStudentSubjectResults(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<ExamStudentSubjectResult> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ExamStudentSubjectResult examStudentSubjectResult = await db.ExamStudentSubjectResults.FindAsync(key);
            if (examStudentSubjectResult == null)
            {
                return NotFound();
            }

            patch.Patch(examStudentSubjectResult);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamStudentSubjectResultExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(examStudentSubjectResult);
        }

        // DELETE: odata/ExamStudentSubjectResults(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            ExamStudentSubjectResult examStudentSubjectResult = await db.ExamStudentSubjectResults.FindAsync(key);
            if (examStudentSubjectResult == null)
            {
                return NotFound();
            }

            db.ExamStudentSubjectResults.Remove(examStudentSubjectResult);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ExamStudentSubjectResults(5)/ClassSubjectMarkComponent
        [EnableQuery]
        public SingleResult<ClassSubjectMarkComponent> GetClassSubjectMarkComponent([FromODataUri] short key)
        {
            return SingleResult.Create(db.ExamStudentSubjectResults.Where(m => m.ExamStudentSubjectResultId== key).Select(m => m.ClassSubjectMarkComponent));
        }

        // GET: odata/ExamStudentSubjectResults(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] short key)
        {
            return SingleResult.Create(db.ExamStudentSubjectResults.Where(m => m.ExamStudentSubjectResultId == key).Select(m => m.MasterData));
        }

        // GET: odata/ExamStudentSubjectResults(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.ExamStudentSubjectResults.Where(m => m.ExamStudentSubjectResultId == key).Select(m => m.Organization));
        }

        // GET: odata/ExamStudentSubjectResults(5)/StudentClassSubject
        [EnableQuery]
        public SingleResult<StudentClassSubject> GetStudentClassSubject([FromODataUri] short key)
        {
            return SingleResult.Create(db.ExamStudentSubjectResults.Where(m => m.ExamStudentSubjectResultId == key).Select(m => m.StudentClassSubject));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExamStudentSubjectResultExists(short key)
        {
            return db.ExamStudentSubjectResults.Count(e => e.ExamStudentSubjectResultId == key) > 0;
        }
    }
}
