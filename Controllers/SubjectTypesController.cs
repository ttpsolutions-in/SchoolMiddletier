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
    builder.EntitySet<SubjectType>("SubjectTypes");
    builder.EntitySet<Batch>("Batches"); 
    builder.EntitySet<ClassSubject>("ClassSubjects"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class SubjectTypesController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/SubjectTypes
        [EnableQuery]
        public IQueryable<SubjectType> GetSubjectTypes()
        {
            return db.SubjectTypes;
        }

        // GET: odata/SubjectTypes(5)
        [EnableQuery]
        public SingleResult<SubjectType> GetSubjectType([FromODataUri] short key)
        {
            return SingleResult.Create(db.SubjectTypes.Where(subjectType => subjectType.SubjectTypeId == key));
        }

        // PUT: odata/SubjectTypes(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<SubjectType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SubjectType subjectType = await db.SubjectTypes.FindAsync(key);
            if (subjectType == null)
            {
                return NotFound();
            }

            patch.Put(subjectType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(subjectType);
        }

        // POST: odata/SubjectTypes
        public async Task<IHttpActionResult> Post(SubjectType subjectType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SubjectTypes.Add(subjectType);
            await db.SaveChangesAsync();

            return Created(subjectType);
        }

        // PATCH: odata/SubjectTypes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<SubjectType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SubjectType subjectType = await db.SubjectTypes.FindAsync(key);
            if (subjectType == null)
            {
                return NotFound();
            }

            patch.Patch(subjectType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(subjectType);
        }

        // DELETE: odata/SubjectTypes(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            SubjectType subjectType = await db.SubjectTypes.FindAsync(key);
            if (subjectType == null)
            {
                return NotFound();
            }

            db.SubjectTypes.Remove(subjectType);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/SubjectTypes(5)/Batch
        [EnableQuery]
        public SingleResult<Batch> GetBatch([FromODataUri] short key)
        {
            return SingleResult.Create(db.SubjectTypes.Where(m => m.SubjectTypeId == key).Select(m => m.Batch));
        }

        // GET: odata/SubjectTypes(5)/ClassSubjects
        [EnableQuery]
        public IQueryable<ClassSubject> GetClassSubjects([FromODataUri] short key)
        {
            return db.SubjectTypes.Where(m => m.SubjectTypeId == key).SelectMany(m => m.ClassSubjects);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubjectTypeExists(short key)
        {
            return db.SubjectTypes.Count(e => e.SubjectTypeId == key) > 0;
        }
    }
}
