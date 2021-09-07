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
    builder.EntitySet<InventoryItem>("InventoryItems");
    builder.EntitySet<MasterData>("MasterDatas"); 
    builder.EntitySet<Organization>("Organizations"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class InventoryItemsController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/InventoryItems
        [EnableQuery]
        public IQueryable<InventoryItem> GetInventoryItems()
        {
            return db.InventoryItems;
        }

        // GET: odata/InventoryItems(5)
        [EnableQuery]
        public SingleResult<InventoryItem> GetInventoryItem([FromODataUri] int key)
        {
            return SingleResult.Create(db.InventoryItems.Where(inventoryItem => inventoryItem.InventoryItemId == key));
        }

        // PUT: odata/InventoryItems(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<InventoryItem> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            InventoryItem inventoryItem = await db.InventoryItems.FindAsync(key);
            if (inventoryItem == null)
            {
                return NotFound();
            }

            patch.Put(inventoryItem);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryItemExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(inventoryItem);
        }

        // POST: odata/InventoryItems
        public async Task<IHttpActionResult> Post(InventoryItem inventoryItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.InventoryItems.Add(inventoryItem);
            await db.SaveChangesAsync();

            return Created(inventoryItem);
        }

        // PATCH: odata/InventoryItems(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<InventoryItem> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            InventoryItem inventoryItem = await db.InventoryItems.FindAsync(key);
            if (inventoryItem == null)
            {
                return NotFound();
            }

            patch.Patch(inventoryItem);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryItemExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(inventoryItem);
        }

        // DELETE: odata/InventoryItems(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            InventoryItem inventoryItem = await db.InventoryItems.FindAsync(key);
            if (inventoryItem == null)
            {
                return NotFound();
            }

            db.InventoryItems.Remove(inventoryItem);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/InventoryItems(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] int key)
        {
            return SingleResult.Create(db.InventoryItems.Where(m => m.InventoryItemId == key).Select(m => m.MasterData));
        }

        // GET: odata/InventoryItems(5)/MasterData1
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData1([FromODataUri] int key)
        {
            return SingleResult.Create(db.InventoryItems.Where(m => m.InventoryItemId == key).Select(m => m.MasterData1));
        }

        // GET: odata/InventoryItems(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] int key)
        {
            return SingleResult.Create(db.InventoryItems.Where(m => m.InventoryItemId == key).Select(m => m.Organization));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InventoryItemExists(int key)
        {
            return db.InventoryItems.Count(e => e.InventoryItemId == key) > 0;
        }
    }
}
