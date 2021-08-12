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
    builder.EntitySet<AccountingLedgerTrialBalance>("AccountingLedgerTrialBalances");
    builder.EntitySet<Batch>("Batches"); 
    builder.EntitySet<EmpEmployee>("EmpEmployees"); 
    builder.EntitySet<MasterData>("MasterDatas"); 
    builder.EntitySet<Organization>("Organizations"); 
    builder.EntitySet<StudentClass>("StudentClasses"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AccountingLedgerTrialBalancesController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/AccountingLedgerTrialBalances
        [EnableQuery]
        public IQueryable<AccountingLedgerTrialBalance> GetAccountingLedgerTrialBalances()
        {
            return db.AccountingLedgerTrialBalances;
        }

        // GET: odata/AccountingLedgerTrialBalances(5)
        [EnableQuery]
        public SingleResult<AccountingLedgerTrialBalance> GetAccountingLedgerTrialBalance([FromODataUri] short key)
        {
            return SingleResult.Create(db.AccountingLedgerTrialBalances.Where(accountingLedgerTrialBalance => accountingLedgerTrialBalance.StudentEmployeeLedegerId == key));
        }

        // PUT: odata/AccountingLedgerTrialBalances(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<AccountingLedgerTrialBalance> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AccountingLedgerTrialBalance accountingLedgerTrialBalance = await db.AccountingLedgerTrialBalances.FindAsync(key);
            if (accountingLedgerTrialBalance == null)
            {
                return NotFound();
            }

            patch.Put(accountingLedgerTrialBalance);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountingLedgerTrialBalanceExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(accountingLedgerTrialBalance);
        }

        // POST: odata/AccountingLedgerTrialBalances
        public async Task<IHttpActionResult> Post(AccountingLedgerTrialBalance accountingLedgerTrialBalance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AccountingLedgerTrialBalances.Add(accountingLedgerTrialBalance);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccountingLedgerTrialBalanceExists(accountingLedgerTrialBalance.StudentEmployeeLedegerId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(accountingLedgerTrialBalance);
        }

        // PATCH: odata/AccountingLedgerTrialBalances(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<AccountingLedgerTrialBalance> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AccountingLedgerTrialBalance accountingLedgerTrialBalance = await db.AccountingLedgerTrialBalances.FindAsync(key);
            if (accountingLedgerTrialBalance == null)
            {
                return NotFound();
            }

            patch.Patch(accountingLedgerTrialBalance);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountingLedgerTrialBalanceExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(accountingLedgerTrialBalance);
        }

        // DELETE: odata/AccountingLedgerTrialBalances(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            AccountingLedgerTrialBalance accountingLedgerTrialBalance = await db.AccountingLedgerTrialBalances.FindAsync(key);
            if (accountingLedgerTrialBalance == null)
            {
                return NotFound();
            }

            db.AccountingLedgerTrialBalances.Remove(accountingLedgerTrialBalance);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/AccountingLedgerTrialBalances(5)/Batch
        [EnableQuery]
        public SingleResult<Batch> GetBatch([FromODataUri] short key)
        {
            return SingleResult.Create(db.AccountingLedgerTrialBalances.Where(m => m.StudentEmployeeLedegerId == key).Select(m => m.Batch));
        }

        // GET: odata/AccountingLedgerTrialBalances(5)/EmpEmployee
        [EnableQuery]
        public SingleResult<EmpEmployee> GetEmpEmployee([FromODataUri] short key)
        {
            return SingleResult.Create(db.AccountingLedgerTrialBalances.Where(m => m.StudentEmployeeLedegerId == key).Select(m => m.EmpEmployee));
        }

        // GET: odata/AccountingLedgerTrialBalances(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] short key)
        {
            return SingleResult.Create(db.AccountingLedgerTrialBalances.Where(m => m.StudentEmployeeLedegerId == key).Select(m => m.MasterData));
        }

        // GET: odata/AccountingLedgerTrialBalances(5)/MasterData1
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData1([FromODataUri] short key)
        {
            return SingleResult.Create(db.AccountingLedgerTrialBalances.Where(m => m.StudentEmployeeLedegerId == key).Select(m => m.MasterData1));
        }

        // GET: odata/AccountingLedgerTrialBalances(5)/MasterData2
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData2([FromODataUri] short key)
        {
            return SingleResult.Create(db.AccountingLedgerTrialBalances.Where(m => m.StudentEmployeeLedegerId == key).Select(m => m.MasterData2));
        }

        // GET: odata/AccountingLedgerTrialBalances(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.AccountingLedgerTrialBalances.Where(m => m.StudentEmployeeLedegerId == key).Select(m => m.Organization));
        }

        // GET: odata/AccountingLedgerTrialBalances(5)/StudentClass
        [EnableQuery]
        public SingleResult<StudentClass> GetStudentClass([FromODataUri] short key)
        {
            return SingleResult.Create(db.AccountingLedgerTrialBalances.Where(m => m.StudentEmployeeLedegerId == key).Select(m => m.StudentClass));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccountingLedgerTrialBalanceExists(short key)
        {
            return db.AccountingLedgerTrialBalances.Count(e => e.StudentEmployeeLedegerId == key) > 0;
        }
    }
}
