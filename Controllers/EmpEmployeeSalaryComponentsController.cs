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
    builder.EntitySet<EmpEmployeeSalaryComponent>("EmpEmployeeSalaryComponents");
    builder.EntitySet<EmpComponent>("EmpComponents"); 
    builder.EntitySet<EmpEmployee>("EmpEmployees"); 
    builder.EntitySet<Organization>("Organizations"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class EmpEmployeeSalaryComponentsController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/EmpEmployeeSalaryComponents
        [EnableQuery]
        public IQueryable<EmpEmployeeSalaryComponent> GetEmpEmployeeSalaryComponents()
        {
            return db.EmpEmployeeSalaryComponents;
        }

        // GET: odata/EmpEmployeeSalaryComponents(5)
        [EnableQuery]
        public SingleResult<EmpEmployeeSalaryComponent> GetEmpEmployeeSalaryComponent([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmpEmployeeSalaryComponents.Where(empEmployeeSalaryComponent => empEmployeeSalaryComponent.EmployeeSalaryComponentId == key));
        }

        // PUT: odata/EmpEmployeeSalaryComponents(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<EmpEmployeeSalaryComponent> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmpEmployeeSalaryComponent empEmployeeSalaryComponent = await db.EmpEmployeeSalaryComponents.FindAsync(key);
            if (empEmployeeSalaryComponent == null)
            {
                return NotFound();
            }

            patch.Put(empEmployeeSalaryComponent);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpEmployeeSalaryComponentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(empEmployeeSalaryComponent);
        }

        // POST: odata/EmpEmployeeSalaryComponents
        public async Task<IHttpActionResult> Post(EmpEmployeeSalaryComponent empEmployeeSalaryComponent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmpEmployeeSalaryComponents.Add(empEmployeeSalaryComponent);
            await db.SaveChangesAsync();

            return Created(empEmployeeSalaryComponent);
        }

        // PATCH: odata/EmpEmployeeSalaryComponents(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<EmpEmployeeSalaryComponent> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmpEmployeeSalaryComponent empEmployeeSalaryComponent = await db.EmpEmployeeSalaryComponents.FindAsync(key);
            if (empEmployeeSalaryComponent == null)
            {
                return NotFound();
            }

            patch.Patch(empEmployeeSalaryComponent);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpEmployeeSalaryComponentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(empEmployeeSalaryComponent);
        }

        // DELETE: odata/EmpEmployeeSalaryComponents(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            EmpEmployeeSalaryComponent empEmployeeSalaryComponent = await db.EmpEmployeeSalaryComponents.FindAsync(key);
            if (empEmployeeSalaryComponent == null)
            {
                return NotFound();
            }

            db.EmpEmployeeSalaryComponents.Remove(empEmployeeSalaryComponent);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/EmpEmployeeSalaryComponents(5)/EmpComponent
        [EnableQuery]
        public SingleResult<EmpComponent> GetEmpComponent([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmpEmployeeSalaryComponents.Where(m => m.EmployeeSalaryComponentId == key).Select(m => m.EmpComponent));
        }

        // GET: odata/EmpEmployeeSalaryComponents(5)/EmpEmployee
        [EnableQuery]
        public SingleResult<EmpEmployee> GetEmpEmployee([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmpEmployeeSalaryComponents.Where(m => m.EmployeeSalaryComponentId == key).Select(m => m.EmpEmployee));
        }

        // GET: odata/EmpEmployeeSalaryComponents(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmpEmployeeSalaryComponents.Where(m => m.EmployeeSalaryComponentId == key).Select(m => m.Organization));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmpEmployeeSalaryComponentExists(short key)
        {
            return db.EmpEmployeeSalaryComponents.Count(e => e.EmployeeSalaryComponentId == key) > 0;
        }
    }
}
