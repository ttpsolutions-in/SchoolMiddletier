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
    builder.EntitySet<AppUser>("AppUsers");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AppUsersController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/AppUsers
        [EnableQuery]
        public IQueryable<AppUser> GetAppUsers()
        {
            return db.AppUsers;
        }

        // GET: odata/AppUsers(5)
        [EnableQuery]
        public SingleResult<AppUser> GetAppUser([FromODataUri] short key)
        {
            return SingleResult.Create(db.AppUsers.Where(appUser => appUser.ApplicationUserId == key));
        }

        // PUT: odata/AppUsers(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<AppUser> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AppUser appUser = await db.AppUsers.FindAsync(key);
            if (appUser == null)
            {
                return NotFound();
            }

            patch.Put(appUser);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppUserExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(appUser);
        }

        // POST: odata/AppUsers
        public async Task<IHttpActionResult> Post(AppUser appUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AppUsers.Add(appUser);
            await db.SaveChangesAsync();

            return Created(appUser);
        }

        // PATCH: odata/AppUsers(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<AppUser> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AppUser appUser = await db.AppUsers.FindAsync(key);
            if (appUser == null)
            {
                return NotFound();
            }

            patch.Patch(appUser);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppUserExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(appUser);
        }

        // DELETE: odata/AppUsers(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            AppUser appUser = await db.AppUsers.FindAsync(key);
            if (appUser == null)
            {
                return NotFound();
            }

            db.AppUsers.Remove(appUser);
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

        private bool AppUserExists(short key)
        {
            return db.AppUsers.Count(e => e.ApplicationUserId == key) > 0;
        }
    }
}
