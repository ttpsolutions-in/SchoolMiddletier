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
    builder.EntitySet<ExamSlot>("ExamSlots");
    builder.EntitySet<Exam>("Exams"); 
    builder.EntitySet<SlotAndClassSubject>("SlotAndClassSubjects"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ExamSlotsController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/ExamSlots
        [EnableQuery]
        public IQueryable<ExamSlot> GetExamSlots()
        {
            return db.ExamSlots;
        }

        // GET: odata/ExamSlots(5)
        [EnableQuery]
        public SingleResult<ExamSlot> GetExamSlot([FromODataUri] short key)
        {
            return SingleResult.Create(db.ExamSlots.Where(examSlot => examSlot.ExamSlotId == key));
        }

        // PUT: odata/ExamSlots(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<ExamSlot> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ExamSlot examSlot = await db.ExamSlots.FindAsync(key);
            if (examSlot == null)
            {
                return NotFound();
            }

            patch.Put(examSlot);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamSlotExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(examSlot);
        }

        // POST: odata/ExamSlots
        public async Task<IHttpActionResult> Post(ExamSlot examSlot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ExamSlots.Add(examSlot);
            await db.SaveChangesAsync();

            return Created(examSlot);
        }

        // PATCH: odata/ExamSlots(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<ExamSlot> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ExamSlot examSlot = await db.ExamSlots.FindAsync(key);
            if (examSlot == null)
            {
                return NotFound();
            }

            patch.Patch(examSlot);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamSlotExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(examSlot);
        }

        // DELETE: odata/ExamSlots(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            ExamSlot examSlot = await db.ExamSlots.FindAsync(key);
            if (examSlot == null)
            {
                return NotFound();
            }

            db.ExamSlots.Remove(examSlot);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ExamSlots(5)/Exam
        [EnableQuery]
        public SingleResult<Exam> GetExam([FromODataUri] short key)
        {
            return SingleResult.Create(db.ExamSlots.Where(m => m.ExamSlotId == key).Select(m => m.Exam));
        }

        // GET: odata/ExamSlots(5)/SlotAndClassSubjects
        [EnableQuery]
        public IQueryable<SlotAndClassSubject> GetSlotAndClassSubjects([FromODataUri] short key)
        {
            return db.ExamSlots.Where(m => m.ExamSlotId == key).SelectMany(m => m.SlotAndClassSubjects);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExamSlotExists(short key)
        {
            return db.ExamSlots.Count(e => e.ExamSlotId == key) > 0;
        }
    }
}
