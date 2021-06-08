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
    builder.EntitySet<RoleUser>("RoleUsers");
    builder.EntitySet<AppUser>("AppUsers"); 
    builder.EntitySet<MasterData>("MasterDatas"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class RoleUsersController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/RoleUsers
        [EnableQuery]
        public IQueryable<RoleUser> GetRoleUsers()
        {
            return db.RoleUsers;
        }

        // GET: odata/RoleUsers(5)
        [EnableQuery]
        public SingleResult<RoleUser> GetRoleUser([FromODataUri] short key)
        {
            return SingleResult.Create(db.RoleUsers.Where(roleUser => roleUser.RoleUserId == key));
        }

        // PUT: odata/RoleUsers(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<RoleUser> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RoleUser roleUser = await db.RoleUsers.FindAsync(key);
            if (roleUser == null)
            {
                return NotFound();
            }

            patch.Put(roleUser);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleUserExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(roleUser);
        }

        // POST: odata/RoleUsers
        public async Task<IHttpActionResult> Post(RoleUser roleUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RoleUsers.Add(roleUser);
            await db.SaveChangesAsync();

            return Created(roleUser);
        }

        // PATCH: odata/RoleUsers(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<RoleUser> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RoleUser roleUser = await db.RoleUsers.FindAsync(key);
            if (roleUser == null)
            {
                return NotFound();
            }

            patch.Patch(roleUser);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleUserExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(roleUser);
        }

        // DELETE: odata/RoleUsers(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            RoleUser roleUser = await db.RoleUsers.FindAsync(key);
            if (roleUser == null)
            {
                return NotFound();
            }

            db.RoleUsers.Remove(roleUser);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/RoleUsers(5)/AppUser
        [EnableQuery]
        public SingleResult<AppUser> GetAppUser([FromODataUri] short key)
        {
            return SingleResult.Create(db.RoleUsers.Where(m => m.RoleUserId == key).Select(m => m.AppUser));
        }

        // GET: odata/RoleUsers(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] short key)
        {
            return SingleResult.Create(db.RoleUsers.Where(m => m.RoleUserId == key).Select(m => m.MasterData));
        }

        // GET: odata/RoleUsers(5)/MasterData1
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData1([FromODataUri] short key)
        {
            return SingleResult.Create(db.RoleUsers.Where(m => m.RoleUserId == key).Select(m => m.MasterData1));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoleUserExists(short key)
        {
            return db.RoleUsers.Count(e => e.RoleUserId == key) > 0;
        }
    }
}
