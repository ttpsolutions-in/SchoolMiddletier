using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.IO;
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
    builder.EntitySet<ApplicationRole>("ApplicationRoles");
    builder.EntitySet<MasterData>("MasterDatas"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ApplicationRolesController : ODataController
    {
        string errorPath = ConfigurationManager.AppSettings["dev"];
        private TTPEntities db = new TTPEntities();

        // GET: odata/ApplicationRoles
        [EnableQuery]
        public IQueryable<ApplicationRole> GetApplicationRoles()
        {
            return db.ApplicationRoles;
        }

        // GET: odata/ApplicationRoles(5)
        [EnableQuery]
        public SingleResult<ApplicationRole> GetApplicationRole([FromODataUri] short key)
        {
            return SingleResult.Create(db.ApplicationRoles.Where(applicationRole => applicationRole.ApplicationRoleId == key));
        }

        // PUT: odata/ApplicationRoles(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<ApplicationRole> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationRole applicationRole = await db.ApplicationRoles.FindAsync(key);
            if (applicationRole == null)
            {
                return NotFound();
            }

            patch.Put(applicationRole);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationRoleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(applicationRole);
        }

        // POST: odata/ApplicationRoles
        public async Task<IHttpActionResult> Post(ApplicationRole applicationRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ApplicationRoles.Add(applicationRole);
            await db.SaveChangesAsync();

            return Created(applicationRole);
        }

        // PATCH: odata/ApplicationRoles(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<ApplicationRole> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationRole applicationRole = await db.ApplicationRoles.FindAsync(key);
            if (applicationRole == null)
            {
                return NotFound();
            }

            patch.Patch(applicationRole);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbEntityValidationException e)
            {
                string error = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    error += $"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:";

                    //Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                    //    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        error += $"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"";
                    }
                }
                File.AppendAllText(errorPath, error);
                throw;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationRoleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(applicationRole);
        }

        // DELETE: odata/ApplicationRoles(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            ApplicationRole applicationRole = await db.ApplicationRoles.FindAsync(key);
            if (applicationRole == null)
            {
                return NotFound();
            }

            db.ApplicationRoles.Remove(applicationRole);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ApplicationRoles(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] short key)
        {
            return SingleResult.Create(db.ApplicationRoles.Where(m => m.ApplicationRoleId == key).Select(m => m.MasterData));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ApplicationRoleExists(short key)
        {
            return db.ApplicationRoles.Count(e => e.ApplicationRoleId == key) > 0;
        }
    }
}
