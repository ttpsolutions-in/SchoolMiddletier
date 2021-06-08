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
    builder.EntitySet<ClassFee>("ClassFees");
    builder.EntitySet<MasterData>("MasterDatas"); 
    builder.EntitySet<StudentFeePayment>("StudentFeePayments"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ClassFeesController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/ClassFees
        [EnableQuery]
        public IQueryable<ClassFee> GetClassFees()
        {
            return db.ClassFees;
        }

        // GET: odata/ClassFees(5)
        [EnableQuery]
        public SingleResult<ClassFee> GetClassFee([FromODataUri] short key)
        {
            return SingleResult.Create(db.ClassFees.Where(classFee => classFee.ClassFeeId == key));
        }

        // PUT: odata/ClassFees(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<ClassFee> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ClassFee classFee = await db.ClassFees.FindAsync(key);
            if (classFee == null)
            {
                return NotFound();
            }

            patch.Put(classFee);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassFeeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(classFee);
        }

        // POST: odata/ClassFees
        public async Task<IHttpActionResult> Post(ClassFee classFee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ClassFees.Add(classFee);
            await db.SaveChangesAsync();

            return Created(classFee);
        }

        // PATCH: odata/ClassFees(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<ClassFee> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ClassFee classFee = await db.ClassFees.FindAsync(key);
            if (classFee == null)
            {
                return NotFound();
            }

            patch.Patch(classFee);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassFeeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(classFee);
        }

        // DELETE: odata/ClassFees(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            ClassFee classFee = await db.ClassFees.FindAsync(key);
            if (classFee == null)
            {
                return NotFound();
            }

            db.ClassFees.Remove(classFee);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ClassFees(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] short key)
        {
            return SingleResult.Create(db.ClassFees.Where(m => m.ClassFeeId == key).Select(m => m.MasterData));
        }

        // GET: odata/ClassFees(5)/MasterData1
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData1([FromODataUri] short key)
        {
            return SingleResult.Create(db.ClassFees.Where(m => m.ClassFeeId == key).Select(m => m.MasterData1));
        }

        // GET: odata/ClassFees(5)/MasterData2
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData2([FromODataUri] short key)
        {
            return SingleResult.Create(db.ClassFees.Where(m => m.ClassFeeId == key).Select(m => m.MasterData2));
        }

        // GET: odata/ClassFees(5)/StudentFeePayments
        [EnableQuery]
        public IQueryable<StudentFeePayment> GetStudentFeePayments([FromODataUri] short key)
        {
            return db.ClassFees.Where(m => m.ClassFeeId == key).SelectMany(m => m.StudentFeePayments);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClassFeeExists(short key)
        {
            return db.ClassFees.Count(e => e.ClassFeeId == key) > 0;
        }
    }
}
