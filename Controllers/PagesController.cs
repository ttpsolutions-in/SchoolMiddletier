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
    builder.EntitySet<Page>("Pages");
    builder.EntitySet<PageGroup>("PageGroups"); 
    builder.EntitySet<PageHistory>("PageHistories"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class PagesController : ODataController
    {
        private StpaulsEntities db = new StpaulsEntities();

        // GET: odata/Pages
        [EnableQuery]
        public IQueryable<Page> GetPages()
        {
            return db.Pages;
        }

        // GET: odata/Pages(5)
        [EnableQuery]
        public SingleResult<Page> GetPage([FromODataUri] short key)
        {
            return SingleResult.Create(db.Pages.Where(page => page.PageId == key));
        }

        // PUT: odata/Pages(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<Page> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Page page = await db.Pages.FindAsync(key);
            if (page == null)
            {
                return NotFound();
            }

            patch.Put(page);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PageExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(page);
        }

        // POST: odata/Pages
        public async Task<IHttpActionResult> Post(Page page)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pages.Add(page);
            await db.SaveChangesAsync();

            return Created(page);
        }

        // PATCH: odata/Pages(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<Page> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Page page = await db.Pages.FindAsync(key);
            if (page == null)
            {
                return NotFound();
            }

            patch.Patch(page);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PageExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(page);
        }

        // DELETE: odata/Pages(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            Page page = await db.Pages.FindAsync(key);
            if (page == null)
            {
                return NotFound();
            }

            db.Pages.Remove(page);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Pages(5)/PageHistories
        [EnableQuery]
        public IQueryable<PageHistory> GetPageHistories([FromODataUri] short key)
        {
            return db.Pages.Where(m => m.PageId == key).SelectMany(m => m.PageHistories);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PageExists(short key)
        {
            return db.Pages.Count(e => e.PageId == key) > 0;
        }
    }
}
