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
    builder.EntitySet<SchoolFeeType>("SchoolFeeTypes");
    builder.EntitySet<StudentClass>("StudentClasses"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class SchoolFeeTypesController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/SchoolFeeTypes
        [EnableQuery]
        public IQueryable<SchoolFeeType> GetSchoolFeeTypes()
        {
            return db.SchoolFeeTypes;
        }

        // GET: odata/SchoolFeeTypes(5)
        [EnableQuery]
        public SingleResult<SchoolFeeType> GetSchoolFeeType([FromODataUri] short key)
        {
            return SingleResult.Create(db.SchoolFeeTypes.Where(schoolFeeType => schoolFeeType.FeeTypeId == key));
        }

        // PUT: odata/SchoolFeeTypes(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<SchoolFeeType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SchoolFeeType schoolFeeType = await db.SchoolFeeTypes.FindAsync(key);
            if (schoolFeeType == null)
            {
                return NotFound();
            }

            patch.Put(schoolFeeType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolFeeTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(schoolFeeType);
        }

        // POST: odata/SchoolFeeTypes
        public async Task<IHttpActionResult> Post(SchoolFeeType schoolFeeType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SchoolFeeTypes.Add(schoolFeeType);
            await db.SaveChangesAsync();

            return Created(schoolFeeType);
        }

        // PATCH: odata/SchoolFeeTypes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<SchoolFeeType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SchoolFeeType schoolFeeType = await db.SchoolFeeTypes.FindAsync(key);
            if (schoolFeeType == null)
            {
                return NotFound();
            }

            patch.Patch(schoolFeeType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolFeeTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(schoolFeeType);
        }

        // DELETE: odata/SchoolFeeTypes(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            SchoolFeeType schoolFeeType = await db.SchoolFeeTypes.FindAsync(key);
            if (schoolFeeType == null)
            {
                return NotFound();
            }

            db.SchoolFeeTypes.Remove(schoolFeeType);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/SchoolFeeTypes(5)/StudentClasses
        [EnableQuery]
        public IQueryable<StudentClass> GetStudentClasses([FromODataUri] short key)
        {
            return db.SchoolFeeTypes.Where(m => m.FeeTypeId == key).SelectMany(m => m.StudentClasses);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SchoolFeeTypeExists(short key)
        {
            return db.SchoolFeeTypes.Count(e => e.FeeTypeId == key) > 0;
        }
    }
}
