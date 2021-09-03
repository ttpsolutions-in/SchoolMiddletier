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
    builder.EntitySet<ReportOrgReportName>("ReportOrgReportNames");
    builder.EntitySet<Organization>("Organizations"); 
    builder.EntitySet<ReportConfigData>("ReportConfigDatas"); 
    builder.EntitySet<ReportOrgReportColumn>("ReportOrgReportColumns"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ReportOrgReportNamesController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/ReportOrgReportNames
        [EnableQuery]
        public IQueryable<ReportOrgReportName> GetReportOrgReportNames()
        {
            return db.ReportOrgReportNames;
        }

        // GET: odata/ReportOrgReportNames(5)
        [EnableQuery]
        public SingleResult<ReportOrgReportName> GetReportOrgReportName([FromODataUri] short key)
        {
            return SingleResult.Create(db.ReportOrgReportNames.Where(reportOrgReportName => reportOrgReportName.ReportOrgReportNameId == key));
        }

        // PUT: odata/ReportOrgReportNames(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<ReportOrgReportName> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ReportOrgReportName reportOrgReportName = await db.ReportOrgReportNames.FindAsync(key);
            if (reportOrgReportName == null)
            {
                return NotFound();
            }

            patch.Put(reportOrgReportName);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportOrgReportNameExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(reportOrgReportName);
        }

        // POST: odata/ReportOrgReportNames
        public async Task<IHttpActionResult> Post(ReportOrgReportName reportOrgReportName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ReportOrgReportNames.Add(reportOrgReportName);
            await db.SaveChangesAsync();

            return Created(reportOrgReportName);
        }

        // PATCH: odata/ReportOrgReportNames(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<ReportOrgReportName> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ReportOrgReportName reportOrgReportName = await db.ReportOrgReportNames.FindAsync(key);
            if (reportOrgReportName == null)
            {
                return NotFound();
            }

            patch.Patch(reportOrgReportName);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportOrgReportNameExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(reportOrgReportName);
        }

        // DELETE: odata/ReportOrgReportNames(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            ReportOrgReportName reportOrgReportName = await db.ReportOrgReportNames.FindAsync(key);
            if (reportOrgReportName == null)
            {
                return NotFound();
            }

            db.ReportOrgReportNames.Remove(reportOrgReportName);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ReportOrgReportNames(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.ReportOrgReportNames.Where(m => m.ReportOrgReportNameId == key).Select(m => m.Organization));
        }

        // GET: odata/ReportOrgReportNames(5)/ReportConfigData
        [EnableQuery]
        public SingleResult<ReportConfigData> GetReportConfigData([FromODataUri] short key)
        {
            return SingleResult.Create(db.ReportOrgReportNames.Where(m => m.ReportOrgReportNameId == key).Select(m => m.ReportConfigData));
        }

        // GET: odata/ReportOrgReportNames(5)/ReportOrgReportColumns
        [EnableQuery]
        public IQueryable<ReportOrgReportColumn> GetReportOrgReportColumns([FromODataUri] short key)
        {
            return db.ReportOrgReportNames.Where(m => m.ReportOrgReportNameId == key).SelectMany(m => m.ReportOrgReportColumns);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReportOrgReportNameExists(short key)
        {
            return db.ReportOrgReportNames.Count(e => e.ReportOrgReportNameId == key) > 0;
        }
    }
}
