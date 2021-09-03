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
    builder.EntitySet<ReportOrgReportColumn>("ReportOrgReportColumns");
    builder.EntitySet<Organization>("Organizations"); 
    builder.EntitySet<ReportOrgReportName>("ReportOrgReportNames"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ReportOrgReportColumnsController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/ReportOrgReportColumns
        [EnableQuery]
        public IQueryable<ReportOrgReportColumn> GetReportOrgReportColumns()
        {
            return db.ReportOrgReportColumns;
        }

        // GET: odata/ReportOrgReportColumns(5)
        [EnableQuery]
        public SingleResult<ReportOrgReportColumn> GetReportOrgReportColumn([FromODataUri] short key)
        {
            return SingleResult.Create(db.ReportOrgReportColumns.Where(reportOrgReportColumn => reportOrgReportColumn.ReportOrgReportColumnId == key));
        }

        // PUT: odata/ReportOrgReportColumns(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<ReportOrgReportColumn> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ReportOrgReportColumn reportOrgReportColumn = await db.ReportOrgReportColumns.FindAsync(key);
            if (reportOrgReportColumn == null)
            {
                return NotFound();
            }

            patch.Put(reportOrgReportColumn);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportOrgReportColumnExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(reportOrgReportColumn);
        }

        // POST: odata/ReportOrgReportColumns
        public async Task<IHttpActionResult> Post(ReportOrgReportColumn reportOrgReportColumn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ReportOrgReportColumns.Add(reportOrgReportColumn);
            await db.SaveChangesAsync();

            return Created(reportOrgReportColumn);
        }

        // PATCH: odata/ReportOrgReportColumns(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<ReportOrgReportColumn> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ReportOrgReportColumn reportOrgReportColumn = await db.ReportOrgReportColumns.FindAsync(key);
            if (reportOrgReportColumn == null)
            {
                return NotFound();
            }

            patch.Patch(reportOrgReportColumn);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportOrgReportColumnExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(reportOrgReportColumn);
        }

        // DELETE: odata/ReportOrgReportColumns(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            ReportOrgReportColumn reportOrgReportColumn = await db.ReportOrgReportColumns.FindAsync(key);
            if (reportOrgReportColumn == null)
            {
                return NotFound();
            }

            db.ReportOrgReportColumns.Remove(reportOrgReportColumn);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ReportOrgReportColumns(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.ReportOrgReportColumns.Where(m => m.ReportOrgReportColumnId == key).Select(m => m.Organization));
        }

        // GET: odata/ReportOrgReportColumns(5)/ReportOrgReportName
        [EnableQuery]
        public SingleResult<ReportOrgReportName> GetReportOrgReportName([FromODataUri] short key)
        {
            return SingleResult.Create(db.ReportOrgReportColumns.Where(m => m.ReportOrgReportColumnId == key).Select(m => m.ReportOrgReportName));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReportOrgReportColumnExists(short key)
        {
            return db.ReportOrgReportColumns.Count(e => e.ReportOrgReportColumnId == key) > 0;
        }
    }
}
