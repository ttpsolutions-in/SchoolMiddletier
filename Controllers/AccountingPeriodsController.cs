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
    builder.EntitySet<AccountingPeriod>("AccountingPeriods");
    builder.EntitySet<Organization>("Organizations"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AccountingPeriodsController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/AccountingPeriods
        [EnableQuery]
        public IQueryable<AccountingPeriod> GetAccountingPeriods()
        {
            return db.AccountingPeriods;
        }

        // GET: odata/AccountingPeriods(5)
        [EnableQuery]
        public SingleResult<AccountingPeriod> GetAccountingPeriod([FromODataUri] short key)
        {
            return SingleResult.Create(db.AccountingPeriods.Where(accountingPeriod => accountingPeriod.AccountingPeriodId == key));
        }

        // PUT: odata/AccountingPeriods(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<AccountingPeriod> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AccountingPeriod accountingPeriod = await db.AccountingPeriods.FindAsync(key);
            if (accountingPeriod == null)
            {
                return NotFound();
            }

            patch.Put(accountingPeriod);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountingPeriodExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(accountingPeriod);
        }

        // POST: odata/AccountingPeriods
        public async Task<IHttpActionResult> Post(AccountingPeriod accountingPeriod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AccountingPeriods.Add(accountingPeriod);
            await db.SaveChangesAsync();

            return Created(accountingPeriod);
        }

        // PATCH: odata/AccountingPeriods(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<AccountingPeriod> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AccountingPeriod accountingPeriod = await db.AccountingPeriods.FindAsync(key);
            if (accountingPeriod == null)
            {
                return NotFound();
            }

            patch.Patch(accountingPeriod);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountingPeriodExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(accountingPeriod);
        }

        // DELETE: odata/AccountingPeriods(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            AccountingPeriod accountingPeriod = await db.AccountingPeriods.FindAsync(key);
            if (accountingPeriod == null)
            {
                return NotFound();
            }

            db.AccountingPeriods.Remove(accountingPeriod);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/AccountingPeriods(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.AccountingPeriods.Where(m => m.AccountingPeriodId == key).Select(m => m.Organization));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccountingPeriodExists(short key)
        {
            return db.AccountingPeriods.Count(e => e.AccountingPeriodId == key) > 0;
        }
    }
}
