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
    builder.EntitySet<EmpComponent>("EmpComponents");
    builder.EntitySet<MasterData>("MasterDatas"); 
    builder.EntitySet<Organization>("Organizations"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class EmpComponentsController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/EmpComponents
        [EnableQuery]
        public IQueryable<EmpComponent> GetEmpComponents()
        {
            return db.EmpComponents;
        }

        // GET: odata/EmpComponents(5)
        [EnableQuery]
        public SingleResult<EmpComponent> GetEmpComponent([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmpComponents.Where(empComponent => empComponent.EmpSalaryComponentId == key));
        }

        // PUT: odata/EmpComponents(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<EmpComponent> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmpComponent empComponent = await db.EmpComponents.FindAsync(key);
            if (empComponent == null)
            {
                return NotFound();
            }

            patch.Put(empComponent);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpComponentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(empComponent);
        }

        // POST: odata/EmpComponents
        public async Task<IHttpActionResult> Post(EmpComponent empComponent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmpComponents.Add(empComponent);
            await db.SaveChangesAsync();

            return Created(empComponent);
        }

        // PATCH: odata/EmpComponents(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<EmpComponent> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmpComponent empComponent = await db.EmpComponents.FindAsync(key);
            if (empComponent == null)
            {
                return NotFound();
            }

            patch.Patch(empComponent);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpComponentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(empComponent);
        }

        // DELETE: odata/EmpComponents(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            EmpComponent empComponent = await db.EmpComponents.FindAsync(key);
            if (empComponent == null)
            {
                return NotFound();
            }

            db.EmpComponents.Remove(empComponent);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/EmpComponents(5)/MasterData
        //[EnableQuery]
        //public SingleResult<MasterData> GetMasterData([FromODataUri] short key)
        //{
        //    return SingleResult.Create(db.EmpComponents.Where(m => m.EmpSalaryComponentId == key).Select(m => m.MasterData));
        //}

        // GET: odata/EmpComponents(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmpComponents.Where(m => m.EmpSalaryComponentId == key).Select(m => m.Organization));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmpComponentExists(short key)
        {
            return db.EmpComponents.Count(e => e.EmpSalaryComponentId == key) > 0;
        }
    }
}
