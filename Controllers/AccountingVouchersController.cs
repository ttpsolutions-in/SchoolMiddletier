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
    builder.EntitySet<AccountingVoucher>("AccountingVouchers");
    builder.EntitySet<AccountingTrialBalance>("AccountingTrialBalances"); 
    builder.EntitySet<Organization>("Organizations"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AccountingVouchersController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/AccountingVouchers
        [EnableQuery]
        public IQueryable<AccountingVoucher> GetAccountingVouchers()
        {
            return db.AccountingVouchers;
        }

        // GET: odata/AccountingVouchers(5)
        [EnableQuery]
        public SingleResult<AccountingVoucher> GetAccountingVoucher([FromODataUri] short key)
        {
            return SingleResult.Create(db.AccountingVouchers.Where(accountingVoucher => accountingVoucher.AccountingVoucherId == key));
        }

        // PUT: odata/AccountingVouchers(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<AccountingVoucher> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AccountingVoucher accountingVoucher = await db.AccountingVouchers.FindAsync(key);
            if (accountingVoucher == null)
            {
                return NotFound();
            }

            patch.Put(accountingVoucher);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountingVoucherExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(accountingVoucher);
        }

        // POST: odata/AccountingVouchers
        public async Task<IHttpActionResult> Post(AccountingVoucher accountingVoucher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AccountingVouchers.Add(accountingVoucher);
            await db.SaveChangesAsync();

            return Created(accountingVoucher);
        }

        // PATCH: odata/AccountingVouchers(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<AccountingVoucher> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AccountingVoucher accountingVoucher = await db.AccountingVouchers.FindAsync(key);
            if (accountingVoucher == null)
            {
                return NotFound();
            }

            patch.Patch(accountingVoucher);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountingVoucherExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(accountingVoucher);
        }

        // DELETE: odata/AccountingVouchers(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            AccountingVoucher accountingVoucher = await db.AccountingVouchers.FindAsync(key);
            if (accountingVoucher == null)
            {
                return NotFound();
            }

            db.AccountingVouchers.Remove(accountingVoucher);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/AccountingVouchers(5)/AccountingTrialBalance
        [EnableQuery]
        public SingleResult<AccountingTrialBalance> GetAccountingTrialBalance([FromODataUri] short key)
        {
            return SingleResult.Create(db.AccountingVouchers.Where(m => m.AccountingVoucherId == key).Select(m => m.AccountingTrialBalance));
        }

        // GET: odata/AccountingVouchers(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.AccountingVouchers.Where(m => m.AccountingVoucherId == key).Select(m => m.Organization));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccountingVoucherExists(short key)
        {
            return db.AccountingVouchers.Count(e => e.AccountingVoucherId == key) > 0;
        }
    }
}
