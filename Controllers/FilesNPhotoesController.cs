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
    builder.EntitySet<FilesNPhoto>("FilesNPhotoes");
    builder.EntitySet<MasterData>("MasterDatas"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class FilesNPhotoesController : ODataController
    {
        private StpaulsEntities db = new StpaulsEntities();

        // GET: odata/FilesNPhotoes
        [EnableQuery]
        public IQueryable<FilesNPhoto> GetFilesNPhotoes()
        {
            return db.FilesNPhotos;
        }

        // GET: odata/FilesNPhotoes(5)
        [EnableQuery]
        public SingleResult<FilesNPhoto> GetFilesNPhoto([FromODataUri] int key)
        {
            return SingleResult.Create(db.FilesNPhotos.Where(filesNPhoto => filesNPhoto.FileId == key));
        }

        // PUT: odata/FilesNPhotoes(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<FilesNPhoto> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            FilesNPhoto filesNPhoto = await db.FilesNPhotos.FindAsync(key);
            if (filesNPhoto == null)
            {
                return NotFound();
            }

            patch.Put(filesNPhoto);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilesNPhotoExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(filesNPhoto);
        }

        // POST: odata/FilesNPhotoes
        public async Task<IHttpActionResult> Post(FilesNPhoto filesNPhoto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FilesNPhotos.Add(filesNPhoto);
            await db.SaveChangesAsync();

            return Created(filesNPhoto);
        }

        // PATCH: odata/FilesNPhotoes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<FilesNPhoto> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            FilesNPhoto filesNPhoto = await db.FilesNPhotos.FindAsync(key);
            if (filesNPhoto == null)
            {
                return NotFound();
            }

            patch.Patch(filesNPhoto);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilesNPhotoExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(filesNPhoto);
        }

        // DELETE: odata/FilesNPhotoes(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            FilesNPhoto filesNPhoto = await db.FilesNPhotos.FindAsync(key);
            if (filesNPhoto == null)
            {
                return NotFound();
            }

            db.FilesNPhotos.Remove(filesNPhoto);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/FilesNPhotoes(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] int key)
        {
            return SingleResult.Create(db.FilesNPhotos.Where(m => m.FileId == key).Select(m => m.MasterData));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FilesNPhotoExists(int key)
        {
            return db.FilesNPhotos.Count(e => e.FileId == key) > 0;
        }
    }
}
