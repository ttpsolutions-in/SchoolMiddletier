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
    builder.EntitySet<EmpHolidayList>("EmpHolidayLists");
    builder.EntitySet<Batch>("Batches"); 
    builder.EntitySet<MasterData>("MasterDatas"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class EmpHolidayListsController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/EmpHolidayLists
        [EnableQuery]
        public IQueryable<EmpHolidayList> GetEmpHolidayLists()
        {
            return db.EmpHolidayLists;
        }

        // GET: odata/EmpHolidayLists(5)
        [EnableQuery]
        public SingleResult<EmpHolidayList> GetEmpHolidayList([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmpHolidayLists.Where(empHolidayList => empHolidayList.HolidayCalendarId == key));
        }

        // PUT: odata/EmpHolidayLists(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<EmpHolidayList> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmpHolidayList empHolidayList = await db.EmpHolidayLists.FindAsync(key);
            if (empHolidayList == null)
            {
                return NotFound();
            }

            patch.Put(empHolidayList);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpHolidayListExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(empHolidayList);
        }

        // POST: odata/EmpHolidayLists
        public async Task<IHttpActionResult> Post(EmpHolidayList empHolidayList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmpHolidayLists.Add(empHolidayList);
            await db.SaveChangesAsync();

            return Created(empHolidayList);
        }

        // PATCH: odata/EmpHolidayLists(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<EmpHolidayList> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmpHolidayList empHolidayList = await db.EmpHolidayLists.FindAsync(key);
            if (empHolidayList == null)
            {
                return NotFound();
            }

            patch.Patch(empHolidayList);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpHolidayListExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(empHolidayList);
        }

        // DELETE: odata/EmpHolidayLists(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            EmpHolidayList empHolidayList = await db.EmpHolidayLists.FindAsync(key);
            if (empHolidayList == null)
            {
                return NotFound();
            }

            db.EmpHolidayLists.Remove(empHolidayList);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/EmpHolidayLists(5)/Batch
        [EnableQuery]
        public SingleResult<Batch> GetBatch([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmpHolidayLists.Where(m => m.HolidayCalendarId == key).Select(m => m.Batch));
        }

        // GET: odata/EmpHolidayLists(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmpHolidayLists.Where(m => m.HolidayCalendarId == key).Select(m => m.MasterData));
        }

        // GET: odata/EmpHolidayLists(5)/MasterData1
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData1([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmpHolidayLists.Where(m => m.HolidayCalendarId == key).Select(m => m.MasterData1));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmpHolidayListExists(short key)
        {
            return db.EmpHolidayLists.Count(e => e.HolidayCalendarId == key) > 0;
        }
    }
}
