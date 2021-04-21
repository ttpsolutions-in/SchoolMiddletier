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
    builder.EntitySet<Message>("Messages");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class MessagesController : ODataController
    {
        private StpaulsEntities db = new StpaulsEntities();

        // GET: odata/Messages
        [EnableQuery]
        public IQueryable<Message> GetMessages()
        {
            return db.Messages;
        }

        // GET: odata/Messages(5)
        [EnableQuery]
        public SingleResult<Message> GetMessage([FromODataUri] short key)
        {
            return SingleResult.Create(db.Messages.Where(message => message.MessageId == key));
        }

        // PUT: odata/Messages(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<Message> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Message message = await db.Messages.FindAsync(key);
            if (message == null)
            {
                return NotFound();
            }

            patch.Put(message);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(message);
        }

        // POST: odata/Messages
        public async Task<IHttpActionResult> Post(Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Messages.Add(message);
            await db.SaveChangesAsync();

            return Created(message);
        }

        // PATCH: odata/Messages(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<Message> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Message message = await db.Messages.FindAsync(key);
            if (message == null)
            {
                return NotFound();
            }

            patch.Patch(message);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(message);
        }

        // DELETE: odata/Messages(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            Message message = await db.Messages.FindAsync(key);
            if (message == null)
            {
                return NotFound();
            }

            db.Messages.Remove(message);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MessageExists(short key)
        {
            return db.Messages.Count(e => e.MessageId == key) > 0;
        }
    }
}
