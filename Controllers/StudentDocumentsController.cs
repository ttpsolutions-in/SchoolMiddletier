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
    builder.EntitySet<StudentDocument>("StudentDocuments");
    builder.EntitySet<Student>("Students"); 
    builder.EntitySet<MasterData>("MasterDatas"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class StudentDocumentsController : ODataController
    {
        private StpaulsEntities db = new StpaulsEntities();

        // GET: odata/StudentDocuments
        [EnableQuery]
        public IQueryable<StudentDocument> GetStudentDocuments()
        {
            return db.StudentDocuments;
        }

        // GET: odata/StudentDocuments(5)
        [EnableQuery]
        public SingleResult<StudentDocument> GetStudentDocument([FromODataUri] short key)
        {
            return SingleResult.Create(db.StudentDocuments.Where(studentDocument => studentDocument.StudentDocId == key));
        }

        // PUT: odata/StudentDocuments(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<StudentDocument> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StudentDocument studentDocument = await db.StudentDocuments.FindAsync(key);
            if (studentDocument == null)
            {
                return NotFound();
            }

            patch.Put(studentDocument);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentDocumentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(studentDocument);
        }

        // POST: odata/StudentDocuments
        public async Task<IHttpActionResult> Post(StudentDocument studentDocument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StudentDocuments.Add(studentDocument);
            await db.SaveChangesAsync();

            return Created(studentDocument);
        }

        // PATCH: odata/StudentDocuments(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<StudentDocument> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StudentDocument studentDocument = await db.StudentDocuments.FindAsync(key);
            if (studentDocument == null)
            {
                return NotFound();
            }

            patch.Patch(studentDocument);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentDocumentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(studentDocument);
        }

        // DELETE: odata/StudentDocuments(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            StudentDocument studentDocument = await db.StudentDocuments.FindAsync(key);
            if (studentDocument == null)
            {
                return NotFound();
            }

            db.StudentDocuments.Remove(studentDocument);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/StudentDocuments(5)/Student
        [EnableQuery]
        public SingleResult<Student> GetStudent([FromODataUri] short key)
        {
            return SingleResult.Create(db.StudentDocuments.Where(m => m.StudentDocId == key).Select(m => m.Student));
        }

        // GET: odata/StudentDocuments(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] short key)
        {
            return SingleResult.Create(db.StudentDocuments.Where(m => m.StudentDocId == key).Select(m => m.MasterData));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentDocumentExists(short key)
        {
            return db.StudentDocuments.Count(e => e.StudentDocId == key) > 0;
        }
    }
}
