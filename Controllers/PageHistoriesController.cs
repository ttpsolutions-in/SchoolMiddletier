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

namespace StPauls.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using StPauls.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<PageHistory>("PageHistories");
    builder.EntitySet<Page>("Pages"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class PageHistoriesController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/PageHistories
        [EnableQuery]
        public IQueryable<PageHistory> GetPageHistories()
        {
            return db.PageHistories;
        }

        // GET: odata/PageHistories(5)
        [EnableQuery]
        public SingleResult<PageHistory> GetPageHistory([FromODataUri] short key)
        {
            return SingleResult.Create(db.PageHistories.Where(pageHistory => pageHistory.PageHistoryId == key));
        }

        // PUT: odata/PageHistories(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<PageHistory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PageHistory pageHistory = await db.PageHistories.FindAsync(key);
            if (pageHistory == null)
            {
                return NotFound();
            }

            patch.Put(pageHistory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PageHistoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(pageHistory);
        }

        // POST: odata/PageHistories
        public async Task<IHttpActionResult> Post(PageHistory pageHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PageHistories.Add(pageHistory);
            await db.SaveChangesAsync();

            return Created(pageHistory);
        }

        // PATCH: odata/PageHistories(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<PageHistory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PageHistory pageHistory = await db.PageHistories.FindAsync(key);
            if (pageHistory == null)
            {
                return NotFound();
            }

            patch.Patch(pageHistory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PageHistoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(pageHistory);
        }

        // DELETE: odata/PageHistories(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            PageHistory pageHistory = await db.PageHistories.FindAsync(key);
            if (pageHistory == null)
            {
                return NotFound();
            }

            db.PageHistories.Remove(pageHistory);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/PageHistories(5)/Page
        [EnableQuery]
        public SingleResult<Page> GetPage([FromODataUri] short key)
        {
            return SingleResult.Create(db.PageHistories.Where(m => m.PageHistoryId == key).Select(m => m.Page));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PageHistoryExists(short key)
        {
            return db.PageHistories.Count(e => e.PageHistoryId == key) > 0;
        }
    }
}
