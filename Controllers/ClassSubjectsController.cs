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
    builder.EntitySet<ClassSubject>("ClassSubjects");
    builder.EntitySet<Batch>("Batches"); 
    builder.EntitySet<MasterData>("MasterDatas"); 
    builder.EntitySet<Organization>("Organizations"); 
    builder.EntitySet<SubjectType>("SubjectTypes"); 
    builder.EntitySet<ClassSubjectMarkComponent>("ClassSubjectMarkComponents"); 
    builder.EntitySet<ClassSubjectTeacher>("ClassSubjectTeachers"); 
    builder.EntitySet<SchoolTimeTable>("SchoolTimeTables"); 
    builder.EntitySet<SlotAndClassSubject>("SlotAndClassSubjects"); 
    builder.EntitySet<StudentClassSubject>("StudentClassSubjects"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ClassSubjectsController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/ClassSubjects
        [EnableQuery]
        public IQueryable<ClassSubject> GetClassSubjects()
        {
            return db.ClassSubjects;
        }

        // GET: odata/ClassSubjects(5)
        [EnableQuery]
        public SingleResult<ClassSubject> GetClassSubject([FromODataUri] int key)
        {
            return SingleResult.Create(db.ClassSubjects.Where(classSubject => classSubject.ClassSubjectId == key));
        }

        // PUT: odata/ClassSubjects(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<ClassSubject> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ClassSubject classSubject = await db.ClassSubjects.FindAsync(key);
            if (classSubject == null)
            {
                return NotFound();
            }

            patch.Put(classSubject);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassSubjectExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(classSubject);
        }

        // POST: odata/ClassSubjects
        public async Task<IHttpActionResult> Post(ClassSubject classSubject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ClassSubjects.Add(classSubject);
            await db.SaveChangesAsync();

            return Created(classSubject);
        }

        // PATCH: odata/ClassSubjects(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<ClassSubject> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ClassSubject classSubject = await db.ClassSubjects.FindAsync(key);
            if (classSubject == null)
            {
                return NotFound();
            }

            patch.Patch(classSubject);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassSubjectExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(classSubject);
        }

        // DELETE: odata/ClassSubjects(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            ClassSubject classSubject = await db.ClassSubjects.FindAsync(key);
            if (classSubject == null)
            {
                return NotFound();
            }

            db.ClassSubjects.Remove(classSubject);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ClassSubjects(5)/Batch
        [EnableQuery]
        public SingleResult<Batch> GetBatch([FromODataUri] int key)
        {
            return SingleResult.Create(db.ClassSubjects.Where(m => m.ClassSubjectId == key).Select(m => m.Batch));
        }

        // GET: odata/ClassSubjects(5)/ClassSubject1
        [EnableQuery]
        public SingleResult<ClassSubject> GetClassSubject1([FromODataUri] int key)
        {
            return SingleResult.Create(db.ClassSubjects.Where(m => m.ClassSubjectId == key).Select(m => m.ClassSubject1));
        }

        // GET: odata/ClassSubjects(5)/ClassSubject2
        [EnableQuery]
        public SingleResult<ClassSubject> GetClassSubject2([FromODataUri] int key)
        {
            return SingleResult.Create(db.ClassSubjects.Where(m => m.ClassSubjectId == key).Select(m => m.ClassSubject2));
        }

        // GET: odata/ClassSubjects(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] int key)
        {
            return SingleResult.Create(db.ClassSubjects.Where(m => m.ClassSubjectId == key).Select(m => m.MasterData));
        }

        // GET: odata/ClassSubjects(5)/MasterData1
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData1([FromODataUri] int key)
        {
            return SingleResult.Create(db.ClassSubjects.Where(m => m.ClassSubjectId == key).Select(m => m.MasterData1));
        }

        // GET: odata/ClassSubjects(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] int key)
        {
            return SingleResult.Create(db.ClassSubjects.Where(m => m.ClassSubjectId == key).Select(m => m.Organization));
        }

        // GET: odata/ClassSubjects(5)/SubjectType
        [EnableQuery]
        public SingleResult<SubjectType> GetSubjectType([FromODataUri] int key)
        {
            return SingleResult.Create(db.ClassSubjects.Where(m => m.ClassSubjectId == key).Select(m => m.SubjectType));
        }

        // GET: odata/ClassSubjects(5)/ClassSubjectMarkComponents
        [EnableQuery]
        public IQueryable<ClassSubjectMarkComponent> GetClassSubjectMarkComponents([FromODataUri] int key)
        {
            return db.ClassSubjects.Where(m => m.ClassSubjectId == key).SelectMany(m => m.ClassSubjectMarkComponents);
        }

        // GET: odata/ClassSubjects(5)/SchoolTimeTables
        [EnableQuery]
        public IQueryable<SchoolTimeTable> GetSchoolTimeTables([FromODataUri] int key)
        {
            return db.ClassSubjects.Where(m => m.ClassSubjectId == key).SelectMany(m => m.SchoolTimeTables);
        }

        // GET: odata/ClassSubjects(5)/SlotAndClassSubjects
        [EnableQuery]
        public IQueryable<SlotAndClassSubject> GetSlotAndClassSubjects([FromODataUri] int key)
        {
            return db.ClassSubjects.Where(m => m.ClassSubjectId == key).SelectMany(m => m.SlotAndClassSubjects);
        }

        // GET: odata/ClassSubjects(5)/StudentClassSubjects
        [EnableQuery]
        public IQueryable<StudentClassSubject> GetStudentClassSubjects([FromODataUri] int key)
        {
            return db.ClassSubjects.Where(m => m.ClassSubjectId == key).SelectMany(m => m.StudentClassSubjects);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClassSubjectExists(int key)
        {
            return db.ClassSubjects.Count(e => e.ClassSubjectId == key) > 0;
        }
    }
}
