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
    builder.EntitySet<AccountingTrialBalance>("AccountingTrialBalances");
    builder.EntitySet<MasterData>("MasterDatas"); 
    builder.EntitySet<Organization>("Organizations"); 
    builder.EntitySet<AccountingVoucher>("AccountingVouchers"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AccountingTrialBalancesController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/AccountingTrialBalances
        [EnableQuery]
        public IQueryable<AccountingTrialBalance> GetAccountingTrialBalances()
        {
            return db.AccountingTrialBalances;
        }

        // GET: odata/AccountingTrialBalances(5)
        [EnableQuery]
        public SingleResult<AccountingTrialBalance> GetAccountingTrialBalance([FromODataUri] short key)
        {
            return SingleResult.Create(db.AccountingTrialBalances.Where(accountingTrialBalance => accountingTrialBalance.AccountingTrialBalanceId == key));
        }

        // PUT: odata/AccountingTrialBalances(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<AccountingTrialBalance> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AccountingTrialBalance accountingTrialBalance = await db.AccountingTrialBalances.FindAsync(key);
            if (accountingTrialBalance == null)
            {
                return NotFound();
            }

            patch.Put(accountingTrialBalance);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountingTrialBalanceExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(accountingTrialBalance);
        }

        // POST: odata/AccountingTrialBalances
        public async Task<IHttpActionResult> Post(AccountingTrialBalance accountingTrialBalance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AccountingTrialBalances.Add(accountingTrialBalance);
            await db.SaveChangesAsync();

            return Created(accountingTrialBalance);
        }

        // PATCH: odata/AccountingTrialBalances(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<AccountingTrialBalance> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AccountingTrialBalance accountingTrialBalance = await db.AccountingTrialBalances.FindAsync(key);
            if (accountingTrialBalance == null)
            {
                return NotFound();
            }

            patch.Patch(accountingTrialBalance);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountingTrialBalanceExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(accountingTrialBalance);
        }

        // DELETE: odata/AccountingTrialBalances(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            AccountingTrialBalance accountingTrialBalance = await db.AccountingTrialBalances.FindAsync(key);
            if (accountingTrialBalance == null)
            {
                return NotFound();
            }

            db.AccountingTrialBalances.Remove(accountingTrialBalance);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/AccountingTrialBalances(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] short key)
        {
            return SingleResult.Create(db.AccountingTrialBalances.Where(m => m.AccountingTrialBalanceId == key).Select(m => m.MasterData));
        }

        // GET: odata/AccountingTrialBalances(5)/MasterData1
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData1([FromODataUri] short key)
        {
            return SingleResult.Create(db.AccountingTrialBalances.Where(m => m.AccountingTrialBalanceId == key).Select(m => m.MasterData1));
        }

        // GET: odata/AccountingTrialBalances(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.AccountingTrialBalances.Where(m => m.AccountingTrialBalanceId == key).Select(m => m.Organization));
        }

        // GET: odata/AccountingTrialBalances(5)/AccountingVouchers
        [EnableQuery]
        public IQueryable<AccountingVoucher> GetAccountingVouchers([FromODataUri] short key)
        {
            return db.AccountingTrialBalances.Where(m => m.AccountingTrialBalanceId == key).SelectMany(m => m.AccountingVouchers);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccountingTrialBalanceExists(short key)
        {
            return db.AccountingTrialBalances.Count(e => e.AccountingTrialBalanceId == key) > 0;
        }
    }
}
