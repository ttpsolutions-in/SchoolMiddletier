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
    builder.EntitySet<StudentCertificate>("StudentCertificates");
    builder.EntitySet<Batch>("Batches"); 
    builder.EntitySet<Organization>("Organizations"); 
    builder.EntitySet<StudentClass>("StudentClasses"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class StudentCertificatesController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/StudentCertificates
        [EnableQuery]
        public IQueryable<StudentCertificate> GetStudentCertificates()
        {
            return db.StudentCertificates;
        }

        // GET: odata/StudentCertificates(5)
        [EnableQuery]
        public SingleResult<StudentCertificate> GetStudentCertificate([FromODataUri] short key)
        {
            return SingleResult.Create(db.StudentCertificates.Where(studentCertificate => studentCertificate.StudentCertificateId == key));
        }

        // PUT: odata/StudentCertificates(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<StudentCertificate> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StudentCertificate studentCertificate = await db.StudentCertificates.FindAsync(key);
            if (studentCertificate == null)
            {
                return NotFound();
            }

            patch.Put(studentCertificate);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentCertificateExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(studentCertificate);
        }

        // POST: odata/StudentCertificates
        public async Task<IHttpActionResult> Post(StudentCertificate studentCertificate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StudentCertificates.Add(studentCertificate);
            await db.SaveChangesAsync();

            return Created(studentCertificate);
        }

        // PATCH: odata/StudentCertificates(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<StudentCertificate> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StudentCertificate studentCertificate = await db.StudentCertificates.FindAsync(key);
            if (studentCertificate == null)
            {
                return NotFound();
            }

            patch.Patch(studentCertificate);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentCertificateExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(studentCertificate);
        }

        // DELETE: odata/StudentCertificates(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            StudentCertificate studentCertificate = await db.StudentCertificates.FindAsync(key);
            if (studentCertificate == null)
            {
                return NotFound();
            }

            db.StudentCertificates.Remove(studentCertificate);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/StudentCertificates(5)/Batch
        [EnableQuery]
        public SingleResult<Batch> GetBatch([FromODataUri] short key)
        {
            return SingleResult.Create(db.StudentCertificates.Where(m => m.StudentCertificateId == key).Select(m => m.Batch));
        }

        // GET: odata/StudentCertificates(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.StudentCertificates.Where(m => m.StudentCertificateId == key).Select(m => m.Organization));
        }

        // GET: odata/StudentCertificates(5)/StudentClass
        [EnableQuery]
        public SingleResult<StudentClass> GetStudentClass([FromODataUri] short key)
        {
            return SingleResult.Create(db.StudentCertificates.Where(m => m.StudentCertificateId == key).Select(m => m.StudentClass));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentCertificateExists(short key)
        {
            return db.StudentCertificates.Count(e => e.StudentCertificateId == key) > 0;
        }
    }
}
