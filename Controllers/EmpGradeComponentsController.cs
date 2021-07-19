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
    builder.EntitySet<EmpGradeComponent>("EmpGradeComponents");
    builder.EntitySet<EmpEmployeeSalaryComponent>("EmpEmployeeSalaryComponents"); 
    builder.EntitySet<MasterData>("MasterDatas"); 
    builder.EntitySet<Organization>("Organizations"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class EmpGradeComponentsController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/EmpGradeComponents
        [EnableQuery]
        public IQueryable<EmpGradeComponent> GetEmpGradeComponents()
        {
            return db.EmpGradeComponents;
        }

        // GET: odata/EmpGradeComponents(5)
        [EnableQuery]
        public SingleResult<EmpGradeComponent> GetEmpGradeComponent([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmpGradeComponents.Where(empGradeComponent => empGradeComponent.EmpGradeSalaryComponentId == key));
        }

        // PUT: odata/EmpGradeComponents(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<EmpGradeComponent> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmpGradeComponent empGradeComponent = await db.EmpGradeComponents.FindAsync(key);
            if (empGradeComponent == null)
            {
                return NotFound();
            }

            patch.Put(empGradeComponent);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpGradeComponentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(empGradeComponent);
        }

        // POST: odata/EmpGradeComponents
        public async Task<IHttpActionResult> Post(EmpGradeComponent empGradeComponent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmpGradeComponents.Add(empGradeComponent);
            await db.SaveChangesAsync();

            return Created(empGradeComponent);
        }

        // PATCH: odata/EmpGradeComponents(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<EmpGradeComponent> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmpGradeComponent empGradeComponent = await db.EmpGradeComponents.FindAsync(key);
            if (empGradeComponent == null)
            {
                return NotFound();
            }

            patch.Patch(empGradeComponent);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpGradeComponentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(empGradeComponent);
        }

        // DELETE: odata/EmpGradeComponents(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            EmpGradeComponent empGradeComponent = await db.EmpGradeComponents.FindAsync(key);
            if (empGradeComponent == null)
            {
                return NotFound();
            }

            db.EmpGradeComponents.Remove(empGradeComponent);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/EmpGradeComponents(5)/EmpEmployeeSalaryComponents
        [EnableQuery]
        public IQueryable<EmpEmployeeSalaryComponent> GetEmpEmployeeSalaryComponents([FromODataUri] short key)
        {
            return db.EmpGradeComponents.Where(m => m.EmpGradeSalaryComponentId == key).SelectMany(m => m.EmpEmployeeSalaryComponents);
        }

        // GET: odata/EmpGradeComponents(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmpGradeComponents.Where(m => m.EmpGradeSalaryComponentId == key).Select(m => m.MasterData));
        }

        // GET: odata/EmpGradeComponents(5)/MasterData1
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData1([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmpGradeComponents.Where(m => m.EmpGradeSalaryComponentId == key).Select(m => m.MasterData1));
        }

        // GET: odata/EmpGradeComponents(5)/MasterData2
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData2([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmpGradeComponents.Where(m => m.EmpGradeSalaryComponentId == key).Select(m => m.MasterData2));
        }

        // GET: odata/EmpGradeComponents(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmpGradeComponents.Where(m => m.EmpGradeSalaryComponentId == key).Select(m => m.Organization));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmpGradeComponentExists(short key)
        {
            return db.EmpGradeComponents.Count(e => e.EmpGradeSalaryComponentId == key) > 0;
        }
    }
}
