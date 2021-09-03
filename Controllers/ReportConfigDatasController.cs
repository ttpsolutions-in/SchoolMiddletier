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
    builder.EntitySet<ReportConfigData>("ReportConfigDatas");
    builder.EntitySet<MasterData>("MasterDatas"); 
    builder.EntitySet<ReportOrgReport>("ReportOrgReports"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ReportConfigDatasController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/ReportConfigDatas
        [EnableQuery]
        public IQueryable<ReportConfigData> GetReportConfigDatas()
        {
            return db.ReportConfigDatas;
        }

        // GET: odata/ReportConfigDatas(5)
        [EnableQuery]
        public SingleResult<ReportConfigData> GetReportConfigData([FromODataUri] short key)
        {
            return SingleResult.Create(db.ReportConfigDatas.Where(reportConfigData => reportConfigData.ReportConfigDataId == key));
        }

        // PUT: odata/ReportConfigDatas(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<ReportConfigData> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ReportConfigData reportConfigData = await db.ReportConfigDatas.FindAsync(key);
            if (reportConfigData == null)
            {
                return NotFound();
            }

            patch.Put(reportConfigData);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportConfigDataExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(reportConfigData);
        }

        // POST: odata/ReportConfigDatas
        public async Task<IHttpActionResult> Post(ReportConfigData reportConfigData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ReportConfigDatas.Add(reportConfigData);
            await db.SaveChangesAsync();

            return Created(reportConfigData);
        }

        // PATCH: odata/ReportConfigDatas(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<ReportConfigData> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ReportConfigData reportConfigData = await db.ReportConfigDatas.FindAsync(key);
            if (reportConfigData == null)
            {
                return NotFound();
            }

            patch.Patch(reportConfigData);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportConfigDataExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(reportConfigData);
        }

        // DELETE: odata/ReportConfigDatas(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            ReportConfigData reportConfigData = await db.ReportConfigDatas.FindAsync(key);
            if (reportConfigData == null)
            {
                return NotFound();
            }

            db.ReportConfigDatas.Remove(reportConfigData);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ReportConfigDatas(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] short key)
        {
            return SingleResult.Create(db.ReportConfigDatas.Where(m => m.ReportConfigDataId == key).Select(m => m.MasterData));
        }

        // GET: odata/ReportConfigDatas(5)/ReportOrgReportNames
        [EnableQuery]
        public IQueryable<ReportOrgReportName> GetReportOrgReports([FromODataUri] short key)
        {
            return db.ReportConfigDatas.Where(m => m.ReportConfigDataId == key).SelectMany(m => m.ReportOrgReportNames);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReportConfigDataExists(short key)
        {
            return db.ReportConfigDatas.Count(e => e.ReportConfigDataId == key) > 0;
        }
    }
}
