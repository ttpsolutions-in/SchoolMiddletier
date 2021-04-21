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
    builder.EntitySet<Student>("Students");
    builder.EntitySet<MasterData>("MasterDatas"); 
    builder.EntitySet<StudentClass>("StudentClasses"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class StudentsController : ODataController
    {
        private StpaulsEntities db = new StpaulsEntities();

        // GET: odata/Students
        [EnableQuery]
        public IQueryable<Student> GetStudents()
        {
            return db.Students;
        }

        // GET: odata/Students(5)
        [EnableQuery]
        public SingleResult<Student> GetStudent([FromODataUri] int key)
        {
            return SingleResult.Create(db.Students.Where(student => student.StudentId == key));
        }

        // PUT: odata/Students(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Student> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Student student = await db.Students.FindAsync(key);
            if (student == null)
            {
                return NotFound();
            }

            patch.Put(student);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(student);
        }

        // POST: odata/Students
        public async Task<IHttpActionResult> Post(Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Students.Add(student);
            await db.SaveChangesAsync();

            return Created(student);
        }

        // PATCH: odata/Students(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Student> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Student student = await db.Students.FindAsync(key);
            if (student == null)
            {
                return NotFound();
            }

            patch.Patch(student);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(student);
        }

        // DELETE: odata/Students(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Student student = await db.Students.FindAsync(key);
            if (student == null)
            {
                return NotFound();
            }

            db.Students.Remove(student);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Students(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] int key)
        {
            return SingleResult.Create(db.Students.Where(m => m.StudentId == key).Select(m => m.MasterData));
        }

        // GET: odata/Students(5)/MasterData1
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData1([FromODataUri] int key)
        {
            return SingleResult.Create(db.Students.Where(m => m.StudentId == key).Select(m => m.MasterData1));
        }

        // GET: odata/Students(5)/MasterData2
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData2([FromODataUri] int key)
        {
            return SingleResult.Create(db.Students.Where(m => m.StudentId == key).Select(m => m.MasterData2));
        }

        // GET: odata/Students(5)/MasterData3
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData3([FromODataUri] int key)
        {
            return SingleResult.Create(db.Students.Where(m => m.StudentId == key).Select(m => m.MasterData3));
        }

        // GET: odata/Students(5)/MasterData4
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData4([FromODataUri] int key)
        {
            return SingleResult.Create(db.Students.Where(m => m.StudentId == key).Select(m => m.MasterData4));
        }

        // GET: odata/Students(5)/MasterData5
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData5([FromODataUri] int key)
        {
            return SingleResult.Create(db.Students.Where(m => m.StudentId == key).Select(m => m.MasterData5));
        }

        // GET: odata/Students(5)/MasterData6
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData6([FromODataUri] int key)
        {
            return SingleResult.Create(db.Students.Where(m => m.StudentId == key).Select(m => m.MasterData6));
        }

        // GET: odata/Students(5)/MasterData7
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData7([FromODataUri] int key)
        {
            return SingleResult.Create(db.Students.Where(m => m.StudentId == key).Select(m => m.MasterData7));
        }

        // GET: odata/Students(5)/StudentClasses
        [EnableQuery]
        public IQueryable<StudentClass> GetStudentClasses([FromODataUri] int key)
        {
            return db.Students.Where(m => m.StudentId == key).SelectMany(m => m.StudentClasses);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentExists(int key)
        {
            return db.Students.Count(e => e.StudentId == key) > 0;
        }
    }
}
