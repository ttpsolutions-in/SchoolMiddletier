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
    builder.EntitySet<StudentClass>("StudentClasses");
    builder.EntitySet<MasterData>("MasterDatas"); 
    builder.EntitySet<Student>("Students"); 
    builder.EntitySet<StudentFeePayment>("StudentFeePayments"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class StudentClassesController : ODataController
    {
        private StpaulsEntities db = new StpaulsEntities();

        // GET: odata/StudentClasses
        [EnableQuery]
        public IQueryable<StudentClass> GetStudentClasses()
        {
            return db.StudentClasses;
        }

        // GET: odata/StudentClasses(5)
        [EnableQuery]
        public SingleResult<StudentClass> GetStudentClass([FromODataUri] int key)
        {
            return SingleResult.Create(db.StudentClasses.Where(studentClass => studentClass.StudentClassId == key));
        }

        // PUT: odata/StudentClasses(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<StudentClass> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StudentClass studentClass = await db.StudentClasses.FindAsync(key);
            if (studentClass == null)
            {
                return NotFound();
            }

            patch.Put(studentClass);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentClassExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(studentClass);
        }

        // POST: odata/StudentClasses
        public async Task<IHttpActionResult> Post(StudentClass studentClass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StudentClasses.Add(studentClass);
            await db.SaveChangesAsync();

            return Created(studentClass);
        }

        // PATCH: odata/StudentClasses(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<StudentClass> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StudentClass studentClass = await db.StudentClasses.FindAsync(key);
            if (studentClass == null)
            {
                return NotFound();
            }

            patch.Patch(studentClass);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentClassExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(studentClass);
        }

        // DELETE: odata/StudentClasses(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            StudentClass studentClass = await db.StudentClasses.FindAsync(key);
            if (studentClass == null)
            {
                return NotFound();
            }

            db.StudentClasses.Remove(studentClass);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/StudentClasses(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] int key)
        {
            return SingleResult.Create(db.StudentClasses.Where(m => m.StudentClassId == key).Select(m => m.MasterData));
        }

        // GET: odata/StudentClasses(5)/MasterData1
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData1([FromODataUri] int key)
        {
            return SingleResult.Create(db.StudentClasses.Where(m => m.StudentClassId == key).Select(m => m.MasterData1));
        }

        // GET: odata/StudentClasses(5)/Student
        [EnableQuery]
        public SingleResult<Student> GetStudent([FromODataUri] int key)
        {
            return SingleResult.Create(db.StudentClasses.Where(m => m.StudentClassId == key).Select(m => m.Student));
        }

        // GET: odata/StudentClasses(5)/StudentFeePayments
        [EnableQuery]
        public IQueryable<StudentFeePayment> GetStudentFeePayments([FromODataUri] int key)
        {
            return db.StudentClasses.Where(m => m.StudentClassId == key).SelectMany(m => m.StudentFeePayments);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentClassExists(int key)
        {
            return db.StudentClasses.Count(e => e.StudentClassId == key) > 0;
        }
    }
}
