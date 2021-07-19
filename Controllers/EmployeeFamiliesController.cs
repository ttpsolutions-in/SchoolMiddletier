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
    builder.EntitySet<EmployeeFamily>("EmployeeFamilies");
    builder.EntitySet<EmpEmployee>("EmpEmployees"); 
    builder.EntitySet<MasterData>("MasterDatas"); 
    builder.EntitySet<Organization>("Organizations"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class EmployeeFamiliesController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/EmployeeFamilies
        [EnableQuery]
        public IQueryable<EmployeeFamily> GetEmployeeFamilies()
        {
            return db.EmployeeFamilies;
        }

        // GET: odata/EmployeeFamilies(5)
        [EnableQuery]
        public SingleResult<EmployeeFamily> GetEmployeeFamily([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmployeeFamilies.Where(employeeFamily => employeeFamily.EmployeeFamilyId == key));
        }

        // PUT: odata/EmployeeFamilies(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<EmployeeFamily> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmployeeFamily employeeFamily = await db.EmployeeFamilies.FindAsync(key);
            if (employeeFamily == null)
            {
                return NotFound();
            }

            patch.Put(employeeFamily);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeFamilyExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(employeeFamily);
        }

        // POST: odata/EmployeeFamilies
        public async Task<IHttpActionResult> Post(EmployeeFamily employeeFamily)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmployeeFamilies.Add(employeeFamily);
            await db.SaveChangesAsync();

            return Created(employeeFamily);
        }

        // PATCH: odata/EmployeeFamilies(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<EmployeeFamily> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmployeeFamily employeeFamily = await db.EmployeeFamilies.FindAsync(key);
            if (employeeFamily == null)
            {
                return NotFound();
            }

            patch.Patch(employeeFamily);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeFamilyExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(employeeFamily);
        }

        // DELETE: odata/EmployeeFamilies(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            EmployeeFamily employeeFamily = await db.EmployeeFamilies.FindAsync(key);
            if (employeeFamily == null)
            {
                return NotFound();
            }

            db.EmployeeFamilies.Remove(employeeFamily);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/EmployeeFamilies(5)/EmpEmployee
        [EnableQuery]
        public SingleResult<EmpEmployee> GetEmpEmployee([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmployeeFamilies.Where(m => m.EmployeeFamilyId == key).Select(m => m.EmpEmployee));
        }

        // GET: odata/EmployeeFamilies(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmployeeFamilies.Where(m => m.EmployeeFamilyId == key).Select(m => m.MasterData));
        }

        // GET: odata/EmployeeFamilies(5)/MasterData1
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData1([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmployeeFamilies.Where(m => m.EmployeeFamilyId == key).Select(m => m.MasterData1));
        }

        // GET: odata/EmployeeFamilies(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmployeeFamilies.Where(m => m.EmployeeFamilyId == key).Select(m => m.Organization));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeFamilyExists(short key)
        {
            return db.EmployeeFamilies.Count(e => e.EmployeeFamilyId == key) > 0;
        }
    }
}
