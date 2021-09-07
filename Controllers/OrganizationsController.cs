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
    builder.EntitySet<Organization>("Organizations");
    builder.EntitySet<Attendance>("Attendances"); 
    builder.EntitySet<ClassSubject>("ClassSubjects"); 
    builder.EntitySet<SlotAndClassSubject>("SlotAndClassSubjects"); 
    builder.EntitySet<StudentActivity>("StudentActivities"); 
    builder.EntitySet<StudentClass>("StudentClasses"); 
    builder.EntitySet<StudentClassSubject>("StudentClassSubjects"); 
    builder.EntitySet<StudentDocument>("StudentDocuments"); 
    builder.EntitySet<StudentFeePayment>("StudentFeePayments"); 
    builder.EntitySet<StudentFeeReceipt>("StudentFeeReceipts"); 
    builder.EntitySet<Student>("Students"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class OrganizationsController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/Organizations
        [EnableQuery]
        public IQueryable<Organization> GetOrganizations()
        {
            return db.Organizations;
        }

        // GET: odata/Organizations(5)
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.Organizations.Where(organization => organization.OrganizationId == key));
        }

        // PUT: odata/Organizations(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<Organization> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Organization organization = await db.Organizations.FindAsync(key);
            if (organization == null)
            {
                return NotFound();
            }

            patch.Put(organization);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(organization);
        }

        // POST: odata/Organizations
        public async Task<IHttpActionResult> Post(Organization organization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Organizations.Add(organization);
            await db.SaveChangesAsync();

            return Created(organization);
        }

        // PATCH: odata/Organizations(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<Organization> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Organization organization = await db.Organizations.FindAsync(key);
            if (organization == null)
            {
                return NotFound();
            }

            patch.Patch(organization);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(organization);
        }

        // DELETE: odata/Organizations(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            Organization organization = await db.Organizations.FindAsync(key);
            if (organization == null)
            {
                return NotFound();
            }

            db.Organizations.Remove(organization);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Organizations(5)/Attendances
        [EnableQuery]
        public IQueryable<Attendance> GetAttendances([FromODataUri] short key)
        {
            return db.Organizations.Where(m => m.OrganizationId == key).SelectMany(m => m.Attendances);
        }

        // GET: odata/Organizations(5)/ClassSubjects
        [EnableQuery]
        public IQueryable<ClassSubject> GetClassSubjects([FromODataUri] short key)
        {
            return db.Organizations.Where(m => m.OrganizationId == key).SelectMany(m => m.ClassSubjects);
        }

        // GET: odata/Organizations(5)/SlotAndClassSubjects
        [EnableQuery]
        public IQueryable<SlotAndClassSubject> GetSlotAndClassSubjects([FromODataUri] short key)
        {
            return db.Organizations.Where(m => m.OrganizationId == key).SelectMany(m => m.SlotAndClassSubjects);
        }

        // GET: odata/Organizations(5)/StudentActivities
        [EnableQuery]
        public IQueryable<StudentActivity> GetStudentActivities([FromODataUri] short key)
        {
            return db.Organizations.Where(m => m.OrganizationId == key).SelectMany(m => m.StudentActivities);
        }

        // GET: odata/Organizations(5)/StudentClasses
        [EnableQuery]
        public IQueryable<StudentClass> GetStudentClasses([FromODataUri] short key)
        {
            return db.Organizations.Where(m => m.OrganizationId == key).SelectMany(m => m.StudentClasses);
        }

        // GET: odata/Organizations(5)/StudentClassSubjects
        [EnableQuery]
        public IQueryable<StudentClassSubject> GetStudentClassSubjects([FromODataUri] short key)
        {
            return db.Organizations.Where(m => m.OrganizationId == key).SelectMany(m => m.StudentClassSubjects);
        }

        // GET: odata/Organizations(5)/StudentDocuments
        [EnableQuery]
        public IQueryable<StudentDocument> GetStudentDocuments([FromODataUri] short key)
        {
            return db.Organizations.Where(m => m.OrganizationId == key).SelectMany(m => m.StudentDocuments);
        }

        //// GET: odata/Organizations(5)/StudentFeePayments
        //[EnableQuery]
        //public IQueryable<StudentFeePayment> GetStudentFeePayments([FromODataUri] short key)
        //{
        //    return db.Organizations.Where(m => m.OrganizationId == key).SelectMany(m => m.StudentFeePayments);
        //}

        //// GET: odata/Organizations(5)/StudentFeeReceipt
        //[EnableQuery]
        //public SingleResult<StudentFeeReceipt> GetStudentFeeReceipt([FromODataUri] short key)
        //{
        //    return SingleResult.Create(db.Organizations.Where(m => m.OrganizationId == key).Select(m => m.StudentFeeReceipt));
        //}

        // GET: odata/Organizations(5)/Students
        [EnableQuery]
        public IQueryable<Student> GetStudents([FromODataUri] short key)
        {
            return db.Organizations.Where(m => m.OrganizationId == key).SelectMany(m => m.Students);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrganizationExists(short key)
        {
            return db.Organizations.Count(e => e.OrganizationId == key) > 0;
        }
    }
}
