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
    builder.EntitySet<StudTeacherClassMapping>("StudTeacherClassMappings");
    builder.EntitySet<Batch>("Batches"); 
    builder.EntitySet<EmpEmployee>("EmpEmployees"); 
    builder.EntitySet<MasterData>("MasterDatas"); 
    builder.EntitySet<Organization>("Organizations"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class StudTeacherClassMappingsController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/StudTeacherClassMappings
        [EnableQuery]
        public IQueryable<StudTeacherClassMapping> GetStudTeacherClassMappings()
        {
            return db.StudTeacherClassMappings;
        }

        // GET: odata/StudTeacherClassMappings(5)
        [EnableQuery]
        public SingleResult<StudTeacherClassMapping> GetStudTeacherClassMapping([FromODataUri] short key)
        {
            return SingleResult.Create(db.StudTeacherClassMappings.Where(studTeacherClassMapping => studTeacherClassMapping.TeacherClassMappingId == key));
        }

        // PUT: odata/StudTeacherClassMappings(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<StudTeacherClassMapping> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StudTeacherClassMapping studTeacherClassMapping = await db.StudTeacherClassMappings.FindAsync(key);
            if (studTeacherClassMapping == null)
            {
                return NotFound();
            }

            patch.Put(studTeacherClassMapping);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudTeacherClassMappingExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(studTeacherClassMapping);
        }

        // POST: odata/StudTeacherClassMappings
        public async Task<IHttpActionResult> Post(StudTeacherClassMapping studTeacherClassMapping)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StudTeacherClassMappings.Add(studTeacherClassMapping);
            await db.SaveChangesAsync();

            return Created(studTeacherClassMapping);
        }

        // PATCH: odata/StudTeacherClassMappings(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<StudTeacherClassMapping> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StudTeacherClassMapping studTeacherClassMapping = await db.StudTeacherClassMappings.FindAsync(key);
            if (studTeacherClassMapping == null)
            {
                return NotFound();
            }

            patch.Patch(studTeacherClassMapping);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudTeacherClassMappingExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(studTeacherClassMapping);
        }

        // DELETE: odata/StudTeacherClassMappings(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            StudTeacherClassMapping studTeacherClassMapping = await db.StudTeacherClassMappings.FindAsync(key);
            if (studTeacherClassMapping == null)
            {
                return NotFound();
            }

            db.StudTeacherClassMappings.Remove(studTeacherClassMapping);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/StudTeacherClassMappings(5)/Batch
        [EnableQuery]
        public SingleResult<Batch> GetBatch([FromODataUri] short key)
        {
            return SingleResult.Create(db.StudTeacherClassMappings.Where(m => m.TeacherClassMappingId == key).Select(m => m.Batch));
        }

        // GET: odata/StudTeacherClassMappings(5)/EmpEmployee
        [EnableQuery]
        public SingleResult<EmpEmployee> GetEmpEmployee([FromODataUri] short key)
        {
            return SingleResult.Create(db.StudTeacherClassMappings.Where(m => m.TeacherClassMappingId == key).Select(m => m.EmpEmployee));
        }

        // GET: odata/StudTeacherClassMappings(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] short key)
        {
            return SingleResult.Create(db.StudTeacherClassMappings.Where(m => m.TeacherClassMappingId == key).Select(m => m.MasterData));
        }

        // GET: odata/StudTeacherClassMappings(5)/MasterData1
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData1([FromODataUri] short key)
        {
            return SingleResult.Create(db.StudTeacherClassMappings.Where(m => m.TeacherClassMappingId == key).Select(m => m.MasterData1));
        }

        // GET: odata/StudTeacherClassMappings(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.StudTeacherClassMappings.Where(m => m.TeacherClassMappingId == key).Select(m => m.Organization));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudTeacherClassMappingExists(short key)
        {
            return db.StudTeacherClassMappings.Count(e => e.TeacherClassMappingId == key) > 0;
        }
    }
}
