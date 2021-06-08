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
    builder.EntitySet<SlotAndClassSubject>("SlotAndClassSubjects");
    builder.EntitySet<ClassSubject>("ClassSubjects"); 
    builder.EntitySet<ExamSlot>("ExamSlots"); 
    builder.EntitySet<Organization>("Organizations"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class SlotAndClassSubjectsController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/SlotAndClassSubjects
        [EnableQuery]
        public IQueryable<SlotAndClassSubject> GetSlotAndClassSubjects()
        {
            return db.SlotAndClassSubjects;
        }

        // GET: odata/SlotAndClassSubjects(5)
        [EnableQuery]
        public SingleResult<SlotAndClassSubject> GetSlotAndClassSubject([FromODataUri] short key)
        {
            return SingleResult.Create(db.SlotAndClassSubjects.Where(slotAndClassSubject => slotAndClassSubject.SlotClassSubjectId == key));
        }

        // PUT: odata/SlotAndClassSubjects(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<SlotAndClassSubject> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SlotAndClassSubject slotAndClassSubject = await db.SlotAndClassSubjects.FindAsync(key);
            if (slotAndClassSubject == null)
            {
                return NotFound();
            }

            patch.Put(slotAndClassSubject);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SlotAndClassSubjectExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(slotAndClassSubject);
        }

        // POST: odata/SlotAndClassSubjects
        public async Task<IHttpActionResult> Post(SlotAndClassSubject slotAndClassSubject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SlotAndClassSubjects.Add(slotAndClassSubject);
            await db.SaveChangesAsync();

            return Created(slotAndClassSubject);
        }

        // PATCH: odata/SlotAndClassSubjects(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<SlotAndClassSubject> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SlotAndClassSubject slotAndClassSubject = await db.SlotAndClassSubjects.FindAsync(key);
            if (slotAndClassSubject == null)
            {
                return NotFound();
            }

            patch.Patch(slotAndClassSubject);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SlotAndClassSubjectExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(slotAndClassSubject);
        }

        // DELETE: odata/SlotAndClassSubjects(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            SlotAndClassSubject slotAndClassSubject = await db.SlotAndClassSubjects.FindAsync(key);
            if (slotAndClassSubject == null)
            {
                return NotFound();
            }

            db.SlotAndClassSubjects.Remove(slotAndClassSubject);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/SlotAndClassSubjects(5)/ClassSubject
        [EnableQuery]
        public SingleResult<ClassSubject> GetClassSubject([FromODataUri] short key)
        {
            return SingleResult.Create(db.SlotAndClassSubjects.Where(m => m.SlotClassSubjectId == key).Select(m => m.ClassSubject));
        }

        // GET: odata/SlotAndClassSubjects(5)/ExamSlot
        [EnableQuery]
        public SingleResult<ExamSlot> GetExamSlot([FromODataUri] short key)
        {
            return SingleResult.Create(db.SlotAndClassSubjects.Where(m => m.SlotClassSubjectId == key).Select(m => m.ExamSlot));
        }

        // GET: odata/SlotAndClassSubjects(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.SlotAndClassSubjects.Where(m => m.SlotClassSubjectId == key).Select(m => m.Organization));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SlotAndClassSubjectExists(short key)
        {
            return db.SlotAndClassSubjects.Count(e => e.SlotClassSubjectId == key) > 0;
        }
    }
}
