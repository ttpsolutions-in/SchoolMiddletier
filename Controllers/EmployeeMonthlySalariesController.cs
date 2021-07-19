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
    builder.EntitySet<EmployeeMonthlySalary>("EmployeeMonthlySalaries");
    builder.EntitySet<EmpEmployee>("EmpEmployees"); 
    builder.EntitySet<EmpEmployeeSalaryComponent>("EmpEmployeeSalaryComponents"); 
    builder.EntitySet<Organization>("Organizations"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class EmployeeMonthlySalariesController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/EmployeeMonthlySalaries
        [EnableQuery]
        public IQueryable<EmployeeMonthlySalary> GetEmployeeMonthlySalaries()
        {
            return db.EmployeeMonthlySalaries;
        }

        // GET: odata/EmployeeMonthlySalaries(5)
        [EnableQuery]
        public SingleResult<EmployeeMonthlySalary> GetEmployeeMonthlySalary([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmployeeMonthlySalaries.Where(employeeMonthlySalary => employeeMonthlySalary.EmployeeMonthlySalaryId == key));
        }

        // PUT: odata/EmployeeMonthlySalaries(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<EmployeeMonthlySalary> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmployeeMonthlySalary employeeMonthlySalary = await db.EmployeeMonthlySalaries.FindAsync(key);
            if (employeeMonthlySalary == null)
            {
                return NotFound();
            }

            patch.Put(employeeMonthlySalary);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeMonthlySalaryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(employeeMonthlySalary);
        }

        // POST: odata/EmployeeMonthlySalaries
        public async Task<IHttpActionResult> Post(EmployeeMonthlySalary employeeMonthlySalary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmployeeMonthlySalaries.Add(employeeMonthlySalary);
            await db.SaveChangesAsync();

            return Created(employeeMonthlySalary);
        }

        // PATCH: odata/EmployeeMonthlySalaries(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<EmployeeMonthlySalary> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmployeeMonthlySalary employeeMonthlySalary = await db.EmployeeMonthlySalaries.FindAsync(key);
            if (employeeMonthlySalary == null)
            {
                return NotFound();
            }

            patch.Patch(employeeMonthlySalary);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeMonthlySalaryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(employeeMonthlySalary);
        }

        // DELETE: odata/EmployeeMonthlySalaries(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            EmployeeMonthlySalary employeeMonthlySalary = await db.EmployeeMonthlySalaries.FindAsync(key);
            if (employeeMonthlySalary == null)
            {
                return NotFound();
            }

            db.EmployeeMonthlySalaries.Remove(employeeMonthlySalary);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/EmployeeMonthlySalaries(5)/EmpEmployee
        [EnableQuery]
        public SingleResult<EmpEmployee> GetEmpEmployee([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmployeeMonthlySalaries.Where(m => m.EmployeeMonthlySalaryId == key).Select(m => m.EmpEmployee));
        }
              

        // GET: odata/EmployeeMonthlySalaries(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmployeeMonthlySalaries.Where(m => m.EmployeeMonthlySalaryId == key).Select(m => m.Organization));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeMonthlySalaryExists(short key)
        {
            return db.EmployeeMonthlySalaries.Count(e => e.EmployeeMonthlySalaryId == key) > 0;
        }
    }
}
