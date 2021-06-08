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
    builder.EntitySet<Album>("Albums");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AlbumsController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/Albums
        [EnableQuery]
        public IQueryable<Album> GetAlbums()
        {
            return db.Albums;
        }

        // GET: odata/Albums(5)
        [EnableQuery]
        public SingleResult<Album> GetAlbum([FromODataUri] short key)
        {
            return SingleResult.Create(db.Albums.Where(album => album.AlbumId == key));
        }

        // PUT: odata/Albums(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<Album> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Album album = await db.Albums.FindAsync(key);
            if (album == null)
            {
                return NotFound();
            }

            patch.Put(album);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbumExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(album);
        }

        // POST: odata/Albums
        public async Task<IHttpActionResult> Post(Album album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Albums.Add(album);
            await db.SaveChangesAsync();

            return Created(album);
        }

        // PATCH: odata/Albums(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<Album> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Album album = await db.Albums.FindAsync(key);
            if (album == null)
            {
                return NotFound();
            }

            patch.Patch(album);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbumExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(album);
        }

        // DELETE: odata/Albums(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            Album album = await db.Albums.FindAsync(key);
            if (album == null)
            {
                return NotFound();
            }

            db.Albums.Remove(album);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlbumExists(short key)
        {
            return db.Albums.Count(e => e.AlbumId == key) > 0;
        }
    }
}
