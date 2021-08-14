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
    builder.EntitySet<Batch>("Batches");
    builder.EntitySet<Attendance>("Attendances"); 
    builder.EntitySet<Organization>("Organizations"); 
    builder.EntitySet<ClassFee>("ClassFees"); 
    builder.EntitySet<ClassSubject>("ClassSubjects"); 
    builder.EntitySet<ClassSubjectMarkComponent>("ClassSubjectMarkComponents"); 
    builder.EntitySet<Exam>("Exams"); 
    builder.EntitySet<ExamSlot>("ExamSlots"); 
    builder.EntitySet<ExamStudentResult>("ExamStudentResults"); 
    builder.EntitySet<ExamStudentSubjectResult>("ExamStudentSubjectResults"); 
    builder.EntitySet<PaymentDetail>("PaymentDetails"); 
    builder.EntitySet<RoleUser>("RoleUsers"); 
    builder.EntitySet<SlotAndClassSubject>("SlotAndClassSubjects"); 
    builder.EntitySet<StudentActivity>("StudentActivities"); 
    builder.EntitySet<StudentCertificate>("StudentCertificates"); 
    builder.EntitySet<StudentClass>("StudentClasses"); 
    builder.EntitySet<StudentClassSubject>("StudentClassSubjects"); 
    builder.EntitySet<StudentDocument>("StudentDocuments"); 
    builder.EntitySet<StudentFeePayment>("StudentFeePayments"); 
    builder.EntitySet<StudentFeeReceipt>("StudentFeeReceipts"); 
    builder.EntitySet<SubjectType>("SubjectTypes"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class BatchesController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/Batches
        [EnableQuery]
        public IQueryable<Batch> GetBatches()
        {
            return db.Batches;
        }

        // GET: odata/Batches(5)
        [EnableQuery]
        public SingleResult<Batch> GetBatch([FromODataUri] short key)
        {
            return SingleResult.Create(db.Batches.Where(batch => batch.BatchId == key));
        }

        // PUT: odata/Batches(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<Batch> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Batch batch = await db.Batches.FindAsync(key);
            if (batch == null)
            {
                return NotFound();
            }

            patch.Put(batch);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BatchExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(batch);
        }

        // POST: odata/Batches
        public async Task<IHttpActionResult> Post(Batch batch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Batches.Add(batch);
            await db.SaveChangesAsync();

            return Created(batch);
        }

        // PATCH: odata/Batches(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<Batch> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Batch batch = await db.Batches.FindAsync(key);
            if (batch == null)
            {
                return NotFound();
            }

            patch.Patch(batch);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BatchExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(batch);
        }

        // DELETE: odata/Batches(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            Batch batch = await db.Batches.FindAsync(key);
            if (batch == null)
            {
                return NotFound();
            }

            db.Batches.Remove(batch);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Batches(5)/Attendances
        [EnableQuery]
        public IQueryable<Attendance> GetAttendances([FromODataUri] short key)
        {
            return db.Batches.Where(m => m.BatchId == key).SelectMany(m => m.Attendances);
        }

        // GET: odata/Batches(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.Batches.Where(m => m.BatchId == key).Select(m => m.Organization));
        }

        // GET: odata/Batches(5)/ClassFees
        [EnableQuery]
        public IQueryable<ClassFee> GetClassFees([FromODataUri] short key)
        {
            return db.Batches.Where(m => m.BatchId == key).SelectMany(m => m.ClassFees);
        }

        // GET: odata/Batches(5)/ClassSubjects
        [EnableQuery]
        public IQueryable<ClassSubject> GetClassSubjects([FromODataUri] short key)
        {
            return db.Batches.Where(m => m.BatchId == key).SelectMany(m => m.ClassSubjects);
        }

        // GET: odata/Batches(5)/ClassSubjectMarkComponents
        [EnableQuery]
        public IQueryable<ClassSubjectMarkComponent> GetClassSubjectMarkComponents([FromODataUri] short key)
        {
            return db.Batches.Where(m => m.BatchId == key).SelectMany(m => m.ClassSubjectMarkComponents);
        }

        // GET: odata/Batches(5)/Exams
        [EnableQuery]
        public IQueryable<Exam> GetExams([FromODataUri] short key)
        {
            return db.Batches.Where(m => m.BatchId == key).SelectMany(m => m.Exams);
        }

        // GET: odata/Batches(5)/ExamSlots
        [EnableQuery]
        public IQueryable<ExamSlot> GetExamSlots([FromODataUri] short key)
        {
            return db.Batches.Where(m => m.BatchId == key).SelectMany(m => m.ExamSlots);
        }

        // GET: odata/Batches(5)/ExamStudentResults
        [EnableQuery]
        public IQueryable<ExamStudentResult> GetExamStudentResults([FromODataUri] short key)
        {
            return db.Batches.Where(m => m.BatchId == key).SelectMany(m => m.ExamStudentResults);
        }

        // GET: odata/Batches(5)/ExamStudentSubjectResults
        [EnableQuery]
        public IQueryable<ExamStudentSubjectResult> GetExamStudentSubjectResults([FromODataUri] short key)
        {
            return db.Batches.Where(m => m.BatchId == key).SelectMany(m => m.ExamStudentSubjectResults);
        }
               

        // GET: odata/Batches(5)/RoleUsers
        [EnableQuery]
        public IQueryable<RoleUser> GetRoleUsers([FromODataUri] short key)
        {
            return db.Batches.Where(m => m.BatchId == key).SelectMany(m => m.RoleUsers);
        }

        // GET: odata/Batches(5)/SlotAndClassSubjects
        [EnableQuery]
        public IQueryable<SlotAndClassSubject> GetSlotAndClassSubjects([FromODataUri] short key)
        {
            return db.Batches.Where(m => m.BatchId == key).SelectMany(m => m.SlotAndClassSubjects);
        }

        // GET: odata/Batches(5)/StudentActivities
        [EnableQuery]
        public IQueryable<StudentActivity> GetStudentActivities([FromODataUri] short key)
        {
            return db.Batches.Where(m => m.BatchId == key).SelectMany(m => m.StudentActivities);
        }

        // GET: odata/Batches(5)/StudentCertificates
        [EnableQuery]
        public IQueryable<StudentCertificate> GetStudentCertificates([FromODataUri] short key)
        {
            return db.Batches.Where(m => m.BatchId == key).SelectMany(m => m.StudentCertificates);
        }

        // GET: odata/Batches(5)/StudentClasses
        [EnableQuery]
        public IQueryable<StudentClass> GetStudentClasses([FromODataUri] short key)
        {
            return db.Batches.Where(m => m.BatchId == key).SelectMany(m => m.StudentClasses);
        }

        // GET: odata/Batches(5)/StudentClassSubjects
        [EnableQuery]
        public IQueryable<StudentClassSubject> GetStudentClassSubjects([FromODataUri] short key)
        {
            return db.Batches.Where(m => m.BatchId == key).SelectMany(m => m.StudentClassSubjects);
        }

        // GET: odata/Batches(5)/StudentDocuments
        [EnableQuery]
        public IQueryable<StudentDocument> GetStudentDocuments([FromODataUri] short key)
        {
            return db.Batches.Where(m => m.BatchId == key).SelectMany(m => m.StudentDocuments);
        }

        // GET: odata/Batches(5)/StudentFeePayments
        [EnableQuery]
        public IQueryable<StudentFeePayment> GetStudentFeePayments([FromODataUri] short key)
        {
            return db.Batches.Where(m => m.BatchId == key).SelectMany(m => m.StudentFeePayments);
        }

        //// GET: odata/Batches(5)/StudentFeeReceipts
        //[EnableQuery]
        //public IQueryable<StudentFeeReceipts> GetStudentFeeReceipts([FromODataUri] short key)
        //{
        //    return db.Batches.Where(m => m.BatchId == key).SelectMany(m => m.StudentFeeReceipts);
        //}

        // GET: odata/Batches(5)/SubjectTypes
        [EnableQuery]
        public IQueryable<SubjectType> GetSubjectTypes([FromODataUri] short key)
        {
            return db.Batches.Where(m => m.BatchId == key).SelectMany(m => m.SubjectTypes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BatchExists(short key)
        {
            return db.Batches.Count(e => e.BatchId == key) > 0;
        }
    }
}
