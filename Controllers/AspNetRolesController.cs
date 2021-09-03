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
    builder.EntitySet<AspNetRole>("AspNetRoles");
    builder.EntitySet<AspNetUser>("AspNetUsers"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AspNetRolesController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/AspNetRoles
        [EnableQuery]
        public IQueryable<AspNetRole> GetAspNetRoles()
        {
            return db.AspNetRoles;
        }

        // GET: odata/AspNetRoles(5)
        [EnableQuery]
        public SingleResult<AspNetRole> GetAspNetRole([FromODataUri] string key)
        {
            return SingleResult.Create(db.AspNetRoles.Where(aspNetRole => aspNetRole.Id == key));
        }

        // PUT: odata/AspNetRoles(5)
        public async Task<IHttpActionResult> Put([FromODataUri] string key, Delta<AspNetRole> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AspNetRole aspNetRole = await db.AspNetRoles.FindAsync(key);
            if (aspNetRole == null)
            {
                return NotFound();
            }

            patch.Put(aspNetRole);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AspNetRoleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(aspNetRole);
        }

        // POST: odata/AspNetRoles
        public async Task<IHttpActionResult> Post(AspNetRole aspNetRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AspNetRoles.Add(aspNetRole);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AspNetRoleExists(aspNetRole.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(aspNetRole);
        }

        // PATCH: odata/AspNetRoles(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] string key, Delta<AspNetRole> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AspNetRole aspNetRole = await db.AspNetRoles.FindAsync(key);
            if (aspNetRole == null)
            {
                return NotFound();
            }

            patch.Patch(aspNetRole);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AspNetRoleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(aspNetRole);
        }

        // DELETE: odata/AspNetRoles(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] string key)
        {
            AspNetRole aspNetRole = await db.AspNetRoles.FindAsync(key);
            if (aspNetRole == null)
            {
                return NotFound();
            }

            db.AspNetRoles.Remove(aspNetRole);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/AspNetRoles(5)/AspNetUsers
        [EnableQuery]
        public IQueryable<AspNetUser> GetAspNetUsers([FromODataUri] string key)
        {
            return db.AspNetRoles.Where(m => m.Id == key).SelectMany(m => m.AspNetUsers);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AspNetRoleExists(string key)
        {
            return db.AspNetRoles.Count(e => e.Id == key) > 0;
        }
    }
}
