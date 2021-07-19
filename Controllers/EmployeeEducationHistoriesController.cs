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
    builder.EntitySet<EmployeeEducationHistory>("EmployeeEducationHistories");
    builder.EntitySet<EmpEmployee>("EmpEmployees"); 
    builder.EntitySet<Organization>("Organizations"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class EmployeeEducationHistoriesController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/EmployeeEducationHistories
        [EnableQuery]
        public IQueryable<EmployeeEducationHistory> GetEmployeeEducationHistories()
        {
            return db.EmployeeEducationHistories;
        }

        // GET: odata/EmployeeEducationHistories(5)
        [EnableQuery]
        public SingleResult<EmployeeEducationHistory> GetEmployeeEducationHistory([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmployeeEducationHistories.Where(employeeEducationHistory => employeeEducationHistory.EmployeeEducationHistoryId == key));
        }

        // PUT: odata/EmployeeEducationHistories(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<EmployeeEducationHistory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmployeeEducationHistory employeeEducationHistory = await db.EmployeeEducationHistories.FindAsync(key);
            if (employeeEducationHistory == null)
            {
                return NotFound();
            }

            patch.Put(employeeEducationHistory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeEducationHistoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(employeeEducationHistory);
        }

        // POST: odata/EmployeeEducationHistories
        public async Task<IHttpActionResult> Post(EmployeeEducationHistory employeeEducationHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmployeeEducationHistories.Add(employeeEducationHistory);
            await db.SaveChangesAsync();

            return Created(employeeEducationHistory);
        }

        // PATCH: odata/EmployeeEducationHistories(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<EmployeeEducationHistory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmployeeEducationHistory employeeEducationHistory = await db.EmployeeEducationHistories.FindAsync(key);
            if (employeeEducationHistory == null)
            {
                return NotFound();
            }

            patch.Patch(employeeEducationHistory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeEducationHistoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(employeeEducationHistory);
        }

        // DELETE: odata/EmployeeEducationHistories(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            EmployeeEducationHistory employeeEducationHistory = await db.EmployeeEducationHistories.FindAsync(key);
            if (employeeEducationHistory == null)
            {
                return NotFound();
            }

            db.EmployeeEducationHistories.Remove(employeeEducationHistory);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/EmployeeEducationHistories(5)/EmpEmployee
        [EnableQuery]
        public SingleResult<EmpEmployee> GetEmpEmployee([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmployeeEducationHistories.Where(m => m.EmployeeEducationHistoryId == key).Select(m => m.EmpEmployee));
        }

        // GET: odata/EmployeeEducationHistories(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmployeeEducationHistories.Where(m => m.EmployeeEducationHistoryId == key).Select(m => m.Organization));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeEducationHistoryExists(short key)
        {
            return db.EmployeeEducationHistories.Count(e => e.EmployeeEducationHistoryId == key) > 0;
        }
    }
}
