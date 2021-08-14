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
    builder.EntitySet<StudentFeeReceipt>("StudentFeeReceipts");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class StudentFeeReceiptsController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/StudentFeeReceipts
        [EnableQuery]
        public IQueryable<StudentFeeReceipt> GetStudentFeeReceipts()
        {
            return db.StudentFeeReceipts;
        }

        // GET: odata/StudentFeeReceipts(5)
        [EnableQuery]
        public SingleResult<StudentFeeReceipt> GetStudentFeeReceipt([FromODataUri] int key)
        {
            return SingleResult.Create(db.StudentFeeReceipts.Where(studentFeeReceipt => studentFeeReceipt.StudentFeeReceiptId == key));
        }

        // PUT: odata/StudentFeeReceipts(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<StudentFeeReceipt> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StudentFeeReceipt studentFeeReceipt = await db.StudentFeeReceipts.FindAsync(key);
            if (studentFeeReceipt == null)
            {
                return NotFound();
            }

            patch.Put(studentFeeReceipt);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentFeeReceiptExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(studentFeeReceipt);
        }

        // POST: odata/StudentFeeReceipts
        public async Task<IHttpActionResult> Post(StudentFeeReceipt studentFeeReceipt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StudentFeeReceipts.Add(studentFeeReceipt);
            await db.SaveChangesAsync();

            return Created(studentFeeReceipt);
        }

        // PATCH: odata/StudentFeeReceipts(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<StudentFeeReceipt> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StudentFeeReceipt studentFeeReceipt = await db.StudentFeeReceipts.FindAsync(key);
            if (studentFeeReceipt == null)
            {
                return NotFound();
            }

            patch.Patch(studentFeeReceipt);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentFeeReceiptExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(studentFeeReceipt);
        }

        // DELETE: odata/StudentFeeReceipts(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            StudentFeeReceipt studentFeeReceipt = await db.StudentFeeReceipts.FindAsync(key);
            if (studentFeeReceipt == null)
            {
                return NotFound();
            }

            db.StudentFeeReceipts.Remove(studentFeeReceipt);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentFeeReceiptExists(int key)
        {
            return db.StudentFeeReceipts.Count(e => e.StudentFeeReceiptId == key) > 0;
        }
    }
}
