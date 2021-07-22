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
    builder.EntitySet<EmpEmployeeGradeSalHistory>("EmpEmployeeGradeSalHistories");
    builder.EntitySet<MasterData>("MasterDatas"); 
    builder.EntitySet<EmpEmployee>("EmpEmployees"); 
    builder.EntitySet<EmpEmployeeSalaryComponent>("EmpEmployeeSalaryComponents"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class EmpEmployeeGradeSalHistoriesController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/EmpEmployeeGradeSalHistories
        [EnableQuery]
        public IQueryable<EmpEmployeeGradeSalHistory> GetEmpEmployeeGradeSalHistories()
        {
            return db.EmpEmployeeGradeSalHistories;
        }

        // GET: odata/EmpEmployeeGradeSalHistories(5)
        [EnableQuery]
        public SingleResult<EmpEmployeeGradeSalHistory> GetEmpEmployeeGradeSalHistory([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmpEmployeeGradeSalHistories.Where(empEmployeeGradeSalHistory => empEmployeeGradeSalHistory.EmployeeGradeHistoryId == key));
        }

        // PUT: odata/EmpEmployeeGradeSalHistories(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<EmpEmployeeGradeSalHistory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmpEmployeeGradeSalHistory empEmployeeGradeSalHistory = await db.EmpEmployeeGradeSalHistories.FindAsync(key);
            if (empEmployeeGradeSalHistory == null)
            {
                return NotFound();
            }

            patch.Put(empEmployeeGradeSalHistory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpEmployeeGradeSalHistoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(empEmployeeGradeSalHistory);
        }

        // POST: odata/EmpEmployeeGradeSalHistories
        public async Task<IHttpActionResult> Post(EmpEmployeeGradeSalHistory empEmployeeGradeSalHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmpEmployeeGradeSalHistories.Add(empEmployeeGradeSalHistory);
            await db.SaveChangesAsync();

            return Created(empEmployeeGradeSalHistory);
        }

        // PATCH: odata/EmpEmployeeGradeSalHistories(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<EmpEmployeeGradeSalHistory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmpEmployeeGradeSalHistory empEmployeeGradeSalHistory = await db.EmpEmployeeGradeSalHistories.FindAsync(key);
            if (empEmployeeGradeSalHistory == null)
            {
                return NotFound();
            }

            patch.Patch(empEmployeeGradeSalHistory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpEmployeeGradeSalHistoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(empEmployeeGradeSalHistory);
        }

        // DELETE: odata/EmpEmployeeGradeSalHistories(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            EmpEmployeeGradeSalHistory empEmployeeGradeSalHistory = await db.EmpEmployeeGradeSalHistories.FindAsync(key);
            if (empEmployeeGradeSalHistory == null)
            {
                return NotFound();
            }

            db.EmpEmployeeGradeSalHistories.Remove(empEmployeeGradeSalHistory);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/EmpEmployeeGradeSalHistories(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmpEmployeeGradeSalHistories.Where(m => m.EmployeeGradeHistoryId == key).Select(m => m.MasterData));
        }

        // GET: odata/EmpEmployeeGradeSalHistories(5)/MasterData1
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData1([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmpEmployeeGradeSalHistories.Where(m => m.EmployeeGradeHistoryId == key).Select(m => m.MasterData1));
        }

        // GET: odata/EmpEmployeeGradeSalHistories(5)/MasterData2
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData2([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmpEmployeeGradeSalHistories.Where(m => m.EmployeeGradeHistoryId == key).Select(m => m.MasterData2));
        }

        // GET: odata/EmpEmployeeGradeSalHistories(5)/EmpEmployee
        [EnableQuery]
        public SingleResult<EmpEmployee> GetEmpEmployee([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmpEmployeeGradeSalHistories.Where(m => m.EmployeeGradeHistoryId == key).Select(m => m.EmpEmployee));
        }

        // GET: odata/EmpEmployeeGradeSalHistories(5)/EmpEmployeeGradeSalHistory1
        [EnableQuery]
        public SingleResult<EmpEmployeeGradeSalHistory> GetEmpEmployeeGradeSalHistory1([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmpEmployeeGradeSalHistories.Where(m => m.EmployeeGradeHistoryId == key).Select(m => m.EmpEmployeeGradeSalHistory1));
        }

        // GET: odata/EmpEmployeeGradeSalHistories(5)/EmpEmployeeGradeSalHistory2
        [EnableQuery]
        public SingleResult<EmpEmployeeGradeSalHistory> GetEmpEmployeeGradeSalHistory2([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmpEmployeeGradeSalHistories.Where(m => m.EmployeeGradeHistoryId == key).Select(m => m.EmpEmployeeGradeSalHistory2));
        }

        // GET: odata/EmpEmployeeGradeSalHistories(5)/EmpEmployeeSalaryComponents
        [EnableQuery]
        public IQueryable<EmpEmployeeSalaryComponent> GetEmpEmployeeSalaryComponents([FromODataUri] short key)
        {
            return db.EmpEmployeeGradeSalHistories.Where(m => m.EmployeeGradeHistoryId == key).SelectMany(m => m.EmpEmployeeSalaryComponents);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmpEmployeeGradeSalHistoryExists(short key)
        {
            return db.EmpEmployeeGradeSalHistories.Count(e => e.EmployeeGradeHistoryId == key) > 0;
        }
    }
}
