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
    builder.EntitySet<ClassSubjectMarkComponent>("ClassSubjectMarkComponents");
    builder.EntitySet<MasterData>("MasterDatas"); 
    builder.EntitySet<Organization>("Organizations"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ClassSubjectMarkComponentsController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/ClassSubjectMarkComponents
        [EnableQuery]
        public IQueryable<ClassSubjectMarkComponent> GetClassSubjectMarkComponents()
        {
            return db.ClassSubjectMarkComponents;
        }

        // GET: odata/ClassSubjectMarkComponents(5)
        [EnableQuery]
        public SingleResult<ClassSubjectMarkComponent> GetClassSubjectMarkComponent([FromODataUri] short key)
        {
            return SingleResult.Create(db.ClassSubjectMarkComponents.Where(classSubjectMarkComponent => classSubjectMarkComponent.ClassSubjectComponentId == key));
        }

        // PUT: odata/ClassSubjectMarkComponents(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<ClassSubjectMarkComponent> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ClassSubjectMarkComponent classSubjectMarkComponent = await db.ClassSubjectMarkComponents.FindAsync(key);
            if (classSubjectMarkComponent == null)
            {
                return NotFound();
            }

            patch.Put(classSubjectMarkComponent);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassSubjectMarkComponentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(classSubjectMarkComponent);
        }

        // POST: odata/ClassSubjectMarkComponents
        public async Task<IHttpActionResult> Post(ClassSubjectMarkComponent classSubjectMarkComponent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ClassSubjectMarkComponents.Add(classSubjectMarkComponent);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClassSubjectMarkComponentExists(classSubjectMarkComponent.ClassSubjectComponentId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(classSubjectMarkComponent);
        }

        // PATCH: odata/ClassSubjectMarkComponents(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<ClassSubjectMarkComponent> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ClassSubjectMarkComponent classSubjectMarkComponent = await db.ClassSubjectMarkComponents.FindAsync(key);
            if (classSubjectMarkComponent == null)
            {
                return NotFound();
            }

            patch.Patch(classSubjectMarkComponent);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassSubjectMarkComponentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(classSubjectMarkComponent);
        }

        // DELETE: odata/ClassSubjectMarkComponents(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            ClassSubjectMarkComponent classSubjectMarkComponent = await db.ClassSubjectMarkComponents.FindAsync(key);
            if (classSubjectMarkComponent == null)
            {
                return NotFound();
            }

            db.ClassSubjectMarkComponents.Remove(classSubjectMarkComponent);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ClassSubjectMarkComponents(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] short key)
        {
            return SingleResult.Create(db.ClassSubjectMarkComponents.Where(m => m.ClassSubjectComponentId == key).Select(m => m.MasterData));
        }

        // GET: odata/ClassSubjectMarkComponents(5)/MasterData1
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData1([FromODataUri] short key)
        {
            return SingleResult.Create(db.ClassSubjectMarkComponents.Where(m => m.ClassSubjectComponentId == key).Select(m => m.MasterData1));
        }

        // GET: odata/ClassSubjectMarkComponents(5)/MasterData2
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData2([FromODataUri] short key)
        {
            return SingleResult.Create(db.ClassSubjectMarkComponents.Where(m => m.ClassSubjectComponentId == key).Select(m => m.MasterData2));
        }

        // GET: odata/ClassSubjectMarkComponents(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.ClassSubjectMarkComponents.Where(m => m.ClassSubjectComponentId == key).Select(m => m.Organization));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClassSubjectMarkComponentExists(short key)
        {
            return db.ClassSubjectMarkComponents.Count(e => e.ClassSubjectComponentId == key) > 0;
        }
    }
}
