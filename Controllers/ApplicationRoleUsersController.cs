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
    builder.EntitySet<ApplicationRoleUser>("ApplicationRoleUsers");
    builder.EntitySet<AppUser>("AppUsers"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ApplicationRoleUsersController : ODataController
    {
        private StpaulsEntities db = new StpaulsEntities();

        // GET: odata/ApplicationRoleUsers
        [EnableQuery]
        public IQueryable<ApplicationRoleUser> GetApplicationRoleUsers()
        {
            return db.ApplicationRoleUsers;
        }

        // GET: odata/ApplicationRoleUsers(5)
        [EnableQuery]
        public SingleResult<ApplicationRoleUser> GetApplicationRoleUser([FromODataUri] short key)
        {
            return SingleResult.Create(db.ApplicationRoleUsers.Where(applicationRoleUser => applicationRoleUser.ApplicationRoleUserId == key));
        }

        // PUT: odata/ApplicationRoleUsers(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<ApplicationRoleUser> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationRoleUser applicationRoleUser = await db.ApplicationRoleUsers.FindAsync(key);
            if (applicationRoleUser == null)
            {
                return NotFound();
            }

            patch.Put(applicationRoleUser);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationRoleUserExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(applicationRoleUser);
        }

        // POST: odata/ApplicationRoleUsers
        public async Task<IHttpActionResult> Post(ApplicationRoleUser applicationRoleUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ApplicationRoleUsers.Add(applicationRoleUser);
            await db.SaveChangesAsync();

            return Created(applicationRoleUser);
        }

        // PATCH: odata/ApplicationRoleUsers(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<ApplicationRoleUser> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationRoleUser applicationRoleUser = await db.ApplicationRoleUsers.FindAsync(key);
            if (applicationRoleUser == null)
            {
                return NotFound();
            }

            patch.Patch(applicationRoleUser);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationRoleUserExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(applicationRoleUser);
        }

        // DELETE: odata/ApplicationRoleUsers(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            ApplicationRoleUser applicationRoleUser = await db.ApplicationRoleUsers.FindAsync(key);
            if (applicationRoleUser == null)
            {
                return NotFound();
            }

            db.ApplicationRoleUsers.Remove(applicationRoleUser);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ApplicationRoleUsers(5)/AppUser
        [EnableQuery]
        public SingleResult<AppUser> GetAppUser([FromODataUri] short key)
        {
            return SingleResult.Create(db.ApplicationRoleUsers.Where(m => m.ApplicationRoleUserId == key).Select(m => m.AppUser));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ApplicationRoleUserExists(short key)
        {
            return db.ApplicationRoleUsers.Count(e => e.ApplicationRoleUserId == key) > 0;
        }
    }
}
