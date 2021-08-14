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
    builder.EntitySet<StudentFeePayment>("StudentFeePayments");
    builder.EntitySet<Batch>("Batches"); 
    builder.EntitySet<ClassFee>("ClassFees"); 
    builder.EntitySet<Organization>("Organizations"); 
    builder.EntitySet<StudentClass>("StudentClasses"); 
    builder.EntitySet<Student>("Students"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class StudentFeePaymentsController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/StudentFeePayments
        [EnableQuery]
        public IQueryable<StudentFeePayment> GetStudentFeePayments()
        {
            return db.StudentFeePayments;
        }

        // GET: odata/StudentFeePayments(5)
        [EnableQuery]
        public SingleResult<StudentFeePayment> GetStudentFeePayment([FromODataUri] int key)
        {
            return SingleResult.Create(db.StudentFeePayments.Where(studentFeePayment => studentFeePayment.StudentFeeId == key));
        }

        // PUT: odata/StudentFeePayments(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<StudentFeePayment> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StudentFeePayment studentFeePayment = await db.StudentFeePayments.FindAsync(key);
            if (studentFeePayment == null)
            {
                return NotFound();
            }

            patch.Put(studentFeePayment);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentFeePaymentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(studentFeePayment);
        }

        // POST: odata/StudentFeePayments
        public async Task<IHttpActionResult> Post(StudentFeePayment studentFeePayment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StudentFeePayments.Add(studentFeePayment);
            await db.SaveChangesAsync();

            return Created(studentFeePayment);
        }

        // PATCH: odata/StudentFeePayments(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<StudentFeePayment> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StudentFeePayment studentFeePayment = await db.StudentFeePayments.FindAsync(key);
            if (studentFeePayment == null)
            {
                return NotFound();
            }

            patch.Patch(studentFeePayment);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentFeePaymentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(studentFeePayment);
        }

        // DELETE: odata/StudentFeePayments(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            StudentFeePayment studentFeePayment = await db.StudentFeePayments.FindAsync(key);
            if (studentFeePayment == null)
            {
                return NotFound();
            }

            db.StudentFeePayments.Remove(studentFeePayment);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/StudentFeePayments(5)/Batch
        [EnableQuery]
        public SingleResult<Batch> GetBatch([FromODataUri] int key)
        {
            return SingleResult.Create(db.StudentFeePayments.Where(m => m.StudentFeeId == key).Select(m => m.Batch));
        }

        // GET: odata/StudentFeePayments(5)/ClassFee
        [EnableQuery]
        public SingleResult<ClassFee> GetClassFee([FromODataUri] int key)
        {
            return SingleResult.Create(db.StudentFeePayments.Where(m => m.StudentFeeId == key).Select(m => m.ClassFee));
        }

        // GET: odata/StudentFeePayments(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] int key)
        {
            return SingleResult.Create(db.StudentFeePayments.Where(m => m.StudentFeeId == key).Select(m => m.Organization));
        }

        // GET: odata/StudentFeePayments(5)/StudentClass
        [EnableQuery]
        public SingleResult<StudentClass> GetStudentClass([FromODataUri] int key)
        {
            return SingleResult.Create(db.StudentFeePayments.Where(m => m.StudentFeeId == key).Select(m => m.StudentClass));
        }

        // GET: odata/StudentFeePayments(5)/Student
        [EnableQuery]
        public SingleResult<Student> GetStudent([FromODataUri] int key)
        {
            return SingleResult.Create(db.StudentFeePayments.Where(m => m.StudentFeeId == key).Select(m => m.Student));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentFeePaymentExists(int key)
        {
            return db.StudentFeePayments.Count(e => e.StudentFeeId == key) > 0;
        }
    }
}
