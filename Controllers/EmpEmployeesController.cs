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
    builder.EntitySet<EmpEmployee>("EmpEmployees");
    builder.EntitySet<AccountingLedgerTrialBalance>("AccountingLedgerTrialBalances"); 
    builder.EntitySet<ClassSubjectTeacher>("ClassSubjectTeachers"); 
    builder.EntitySet<EmpEmployeeGradeSalHistory>("EmpEmployeeGradeSalHistories"); 
    builder.EntitySet<EmpEmployeeSkill>("EmpEmployeeSkills"); 
    builder.EntitySet<EmployeeEducationHistory>("EmployeeEducationHistories"); 
    builder.EntitySet<EmployeeFamily>("EmployeeFamilies"); 
    builder.EntitySet<EmployeeLeaf>("EmployeeLeaves"); 
    builder.EntitySet<EmployeeMonthlySalary>("EmployeeMonthlySalaries"); 
    builder.EntitySet<EmpEmployeeSalaryComponent>("EmpEmployeeSalaryComponents"); 
    builder.EntitySet<StudTeacherClassMapping>("StudTeacherClassMappings"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class EmpEmployeesController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/EmpEmployees
        [EnableQuery]
        public IQueryable<EmpEmployee> GetEmpEmployees()
        {
            return db.EmpEmployees;
        }

        // GET: odata/EmpEmployees(5)
        [EnableQuery]
        public SingleResult<EmpEmployee> GetEmpEmployee([FromODataUri] int key)
        {
            return SingleResult.Create(db.EmpEmployees.Where(empEmployee => empEmployee.EmpEmployeeId == key));
        }

        // PUT: odata/EmpEmployees(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<EmpEmployee> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmpEmployee empEmployee = await db.EmpEmployees.FindAsync(key);
            if (empEmployee == null)
            {
                return NotFound();
            }

            patch.Put(empEmployee);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpEmployeeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(empEmployee);
        }

        // POST: odata/EmpEmployees
        public async Task<IHttpActionResult> Post(EmpEmployee empEmployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmpEmployees.Add(empEmployee);
            await db.SaveChangesAsync();

            return Created(empEmployee);
        }

        // PATCH: odata/EmpEmployees(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<EmpEmployee> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmpEmployee empEmployee = await db.EmpEmployees.FindAsync(key);
            if (empEmployee == null)
            {
                return NotFound();
            }

            patch.Patch(empEmployee);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpEmployeeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(empEmployee);
        }

        // DELETE: odata/EmpEmployees(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            EmpEmployee empEmployee = await db.EmpEmployees.FindAsync(key);
            if (empEmployee == null)
            {
                return NotFound();
            }

            db.EmpEmployees.Remove(empEmployee);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/EmpEmployees(5)/AccountingLedgerTrialBalances
        [EnableQuery]
        public IQueryable<AccountingLedgerTrialBalance> GetAccountingLedgerTrialBalances([FromODataUri] int key)
        {
            return db.EmpEmployees.Where(m => m.EmpEmployeeId == key).SelectMany(m => m.AccountingLedgerTrialBalances);
        }

        // GET: odata/EmpEmployees(5)/ClassSubjectTeachers
        [EnableQuery]
        public IQueryable<ClassSubjectTeacher> GetClassSubjectTeachers([FromODataUri] int key)
        {
            return db.EmpEmployees.Where(m => m.EmpEmployeeId == key).SelectMany(m => m.ClassSubjectTeachers);
        }

        // GET: odata/EmpEmployees(5)/EmpEmployeeGradeSalHistories
        [EnableQuery]
        public IQueryable<EmpEmployeeGradeSalHistory> GetEmpEmployeeGradeSalHistories([FromODataUri] int key)
        {
            return db.EmpEmployees.Where(m => m.EmpEmployeeId == key).SelectMany(m => m.EmpEmployeeGradeSalHistories);
        }

        // GET: odata/EmpEmployees(5)/EmpEmployeeSkills
        [EnableQuery]
        public IQueryable<EmpEmployeeSkill> GetEmpEmployeeSkills([FromODataUri] int key)
        {
            return db.EmpEmployees.Where(m => m.EmpEmployeeId == key).SelectMany(m => m.EmpEmployeeSkills);
        }

        // GET: odata/EmpEmployees(5)/EmployeeEducationHistories
        [EnableQuery]
        public IQueryable<EmployeeEducationHistory> GetEmployeeEducationHistories([FromODataUri] int key)
        {
            return db.EmpEmployees.Where(m => m.EmpEmployeeId == key).SelectMany(m => m.EmployeeEducationHistories);
        }

        // GET: odata/EmpEmployees(5)/EmployeeFamilies
        [EnableQuery]
        public IQueryable<EmployeeFamily> GetEmployeeFamilies([FromODataUri] int key)
        {
            return db.EmpEmployees.Where(m => m.EmpEmployeeId == key).SelectMany(m => m.EmployeeFamilies);
        }

        // GET: odata/EmpEmployees(5)/EmployeeLeaves
        [EnableQuery]
        public IQueryable<LeaveEmployeeLeaf> GetEmployeeLeaves([FromODataUri] int key)
        {
            return db.EmpEmployees.Where(m => m.EmpEmployeeId == key).SelectMany(m => m.LeaveEmployeeLeaves);
        }

        // GET: odata/EmpEmployees(5)/EmployeeMonthlySalaries
        [EnableQuery]
        public IQueryable<EmployeeMonthlySalary> GetEmployeeMonthlySalaries([FromODataUri] int key)
        {
            return db.EmpEmployees.Where(m => m.EmpEmployeeId == key).SelectMany(m => m.EmployeeMonthlySalaries);
        }

        // GET: odata/EmpEmployees(5)/EmpEmployeeSalaryComponents
        [EnableQuery]
        public IQueryable<EmpEmployeeSalaryComponent> GetEmpEmployeeSalaryComponents([FromODataUri] int key)
        {
            return db.EmpEmployees.Where(m => m.EmpEmployeeId == key).SelectMany(m => m.EmpEmployeeSalaryComponents);
        }

        // GET: odata/EmpEmployees(5)/StudTeacherClassMappings
        [EnableQuery]
        public IQueryable<StudTeacherClassMapping> GetStudTeacherClassMappings([FromODataUri] int key)
        {
            return db.EmpEmployees.Where(m => m.EmpEmployeeId == key).SelectMany(m => m.StudTeacherClassMappings);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmpEmployeeExists(int key)
        {
            return db.EmpEmployees.Count(e => e.EmpEmployeeId == key) > 0;
        }
    }
}
