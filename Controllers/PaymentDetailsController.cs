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
    builder.EntitySet<PaymentDetail>("PaymentDetails");
    builder.EntitySet<ClassFee>("ClassFees"); 
    builder.EntitySet<StudentFeePayment>("StudentFeePayments"); 
    builder.EntitySet<StudentFeeReceipt>("StudentFeeReceipts"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class PaymentDetailsController : ODataController
    {
        private StpaulsEntities db = new StpaulsEntities();

        // GET: odata/PaymentDetails
        [EnableQuery]
        public IQueryable<PaymentDetail> GetPaymentDetails()
        {
            return db.PaymentDetails;
        }

        // GET: odata/PaymentDetails(5)
        [EnableQuery]
        public SingleResult<PaymentDetail> GetPaymentDetail([FromODataUri] int key)
        {
            return SingleResult.Create(db.PaymentDetails.Where(paymentDetail => paymentDetail.PaymentId == key));
        }

        // PUT: odata/PaymentDetails(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<PaymentDetail> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PaymentDetail paymentDetail = await db.PaymentDetails.FindAsync(key);
            if (paymentDetail == null)
            {
                return NotFound();
            }

            patch.Put(paymentDetail);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentDetailExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(paymentDetail);
        }

        // POST: odata/PaymentDetails
        public async Task<IHttpActionResult> Post(PaymentDetail paymentDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PaymentDetails.Add(paymentDetail);
            await db.SaveChangesAsync();

            return Created(paymentDetail);
        }

        // PATCH: odata/PaymentDetails(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<PaymentDetail> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PaymentDetail paymentDetail = await db.PaymentDetails.FindAsync(key);
            if (paymentDetail == null)
            {
                return NotFound();
            }

            patch.Patch(paymentDetail);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentDetailExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(paymentDetail);
        }

        // DELETE: odata/PaymentDetails(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            PaymentDetail paymentDetail = await db.PaymentDetails.FindAsync(key);
            if (paymentDetail == null)
            {
                return NotFound();
            }

            db.PaymentDetails.Remove(paymentDetail);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/PaymentDetails(5)/ClassFee
        [EnableQuery]
        public SingleResult<ClassFee> GetClassFee([FromODataUri] int key)
        {
            return SingleResult.Create(db.PaymentDetails.Where(m => m.PaymentId == key).Select(m => m.ClassFee));
        }

        // GET: odata/PaymentDetails(5)/StudentFeePayment
        [EnableQuery]
        public SingleResult<StudentFeePayment> GetStudentFeePayment([FromODataUri] int key)
        {
            return SingleResult.Create(db.PaymentDetails.Where(m => m.PaymentId == key).Select(m => m.StudentFeePayment));
        }

        // GET: odata/PaymentDetails(5)/StudentFeeReceipt
        [EnableQuery]
        public SingleResult<StudentFeeReceipt> GetStudentFeeReceipt([FromODataUri] int key)
        {
            return SingleResult.Create(db.PaymentDetails.Where(m => m.PaymentId == key).Select(m => m.StudentFeeReceipt));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PaymentDetailExists(int key)
        {
            return db.PaymentDetails.Count(e => e.PaymentId == key) > 0;
        }
    }
}
