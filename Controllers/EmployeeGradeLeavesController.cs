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
    builder.EntitySet<EmployeeGradeLeaf>("EmployeeGradeLeaves");
    builder.EntitySet<EmpGrade>("EmpGrades"); 
    builder.EntitySet<MasterData>("MasterDatas"); 
    builder.EntitySet<Organization>("Organizations"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class EmployeeGradeLeavesController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/EmployeeGradeLeaves
        [EnableQuery]
        public IQueryable<EmployeeGradeLeaf> GetEmployeeGradeLeaves()
        {
            return db.EmployeeGradeLeaves;
        }

        // GET: odata/EmployeeGradeLeaves(5)
        [EnableQuery]
        public SingleResult<EmployeeGradeLeaf> GetEmployeeGradeLeaf([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmployeeGradeLeaves.Where(employeeGradeLeaf => employeeGradeLeaf.EmployeeGradeLeaveId == key));
        }

        // PUT: odata/EmployeeGradeLeaves(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<EmployeeGradeLeaf> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmployeeGradeLeaf employeeGradeLeaf = await db.EmployeeGradeLeaves.FindAsync(key);
            if (employeeGradeLeaf == null)
            {
                return NotFound();
            }

            patch.Put(employeeGradeLeaf);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeGradeLeafExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(employeeGradeLeaf);
        }

        // POST: odata/EmployeeGradeLeaves
        public async Task<IHttpActionResult> Post(EmployeeGradeLeaf employeeGradeLeaf)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmployeeGradeLeaves.Add(employeeGradeLeaf);
            await db.SaveChangesAsync();

            return Created(employeeGradeLeaf);
        }

        // PATCH: odata/EmployeeGradeLeaves(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<EmployeeGradeLeaf> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmployeeGradeLeaf employeeGradeLeaf = await db.EmployeeGradeLeaves.FindAsync(key);
            if (employeeGradeLeaf == null)
            {
                return NotFound();
            }

            patch.Patch(employeeGradeLeaf);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeGradeLeafExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(employeeGradeLeaf);
        }

        // DELETE: odata/EmployeeGradeLeaves(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            EmployeeGradeLeaf employeeGradeLeaf = await db.EmployeeGradeLeaves.FindAsync(key);
            if (employeeGradeLeaf == null)
            {
                return NotFound();
            }

            db.EmployeeGradeLeaves.Remove(employeeGradeLeaf);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

      
        // GET: odata/EmployeeGradeLeaves(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmployeeGradeLeaves.Where(m => m.EmployeeGradeLeaveId == key).Select(m => m.MasterData));
        }

        // GET: odata/EmployeeGradeLeaves(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmployeeGradeLeaves.Where(m => m.EmployeeGradeLeaveId == key).Select(m => m.Organization));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeGradeLeafExists(short key)
        {
            return db.EmployeeGradeLeaves.Count(e => e.EmployeeGradeLeaveId == key) > 0;
        }
    }
}
