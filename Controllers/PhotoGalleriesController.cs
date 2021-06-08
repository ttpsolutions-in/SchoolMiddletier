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
    builder.EntitySet<PhotoGallery>("PhotoGalleries");
    builder.EntitySet<Album>("Albums"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class PhotoGalleriesController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/PhotoGalleries
        [EnableQuery]
        public IQueryable<PhotoGallery> GetPhotoGalleries()
        {
            return db.PhotoGalleries;
        }

        // GET: odata/PhotoGalleries(5)
        [EnableQuery]
        public SingleResult<PhotoGallery> GetPhotoGallery([FromODataUri] short key)
        {
            return SingleResult.Create(db.PhotoGalleries.Where(photoGallery => photoGallery.PhotoId == key));
        }

        // PUT: odata/PhotoGalleries(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<PhotoGallery> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PhotoGallery photoGallery = await db.PhotoGalleries.FindAsync(key);
            if (photoGallery == null)
            {
                return NotFound();
            }

            patch.Put(photoGallery);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoGalleryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(photoGallery);
        }

        // POST: odata/PhotoGalleries
        public async Task<IHttpActionResult> Post(PhotoGallery photoGallery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PhotoGalleries.Add(photoGallery);
            await db.SaveChangesAsync();

            return Created(photoGallery);
        }

        // PATCH: odata/PhotoGalleries(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<PhotoGallery> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PhotoGallery photoGallery = await db.PhotoGalleries.FindAsync(key);
            if (photoGallery == null)
            {
                return NotFound();
            }

            patch.Patch(photoGallery);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoGalleryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(photoGallery);
        }

        // DELETE: odata/PhotoGalleries(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            PhotoGallery photoGallery = await db.PhotoGalleries.FindAsync(key);
            if (photoGallery == null)
            {
                return NotFound();
            }

            db.PhotoGalleries.Remove(photoGallery);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/PhotoGalleries(5)/Album
        [EnableQuery]
        public SingleResult<Album> GetAlbum([FromODataUri] short key)
        {
            return SingleResult.Create(db.PhotoGalleries.Where(m => m.PhotoId == key).Select(m => m.Album));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PhotoGalleryExists(short key)
        {
            return db.PhotoGalleries.Count(e => e.PhotoId == key) > 0;
        }
    }
}
