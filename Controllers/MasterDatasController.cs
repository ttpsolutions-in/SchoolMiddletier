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
    builder.EntitySet<MasterData>("MasterDatas");
    builder.EntitySet<StudentClass>("StudentClasses"); 
    builder.EntitySet<Student>("Students"); 
    builder.EntitySet<ClassFee>("ClassFees"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class MasterDatasController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/MasterDatas
        [EnableQuery]
        public IQueryable<MasterData> GetMasterDatas()
        {
            return db.MasterDatas;
        }

        // GET: odata/MasterDatas(5)
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] short key)
        {
            return SingleResult.Create(db.MasterDatas.Where(masterData => masterData.MasterDataId == key));
        }

        // PUT: odata/MasterDatas(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<MasterData> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MasterData masterData = await db.MasterDatas.FindAsync(key);
            if (masterData == null)
            {
                return NotFound();
            }

            patch.Put(masterData);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MasterDataExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(masterData);
        }

        // POST: odata/MasterDatas
        public async Task<IHttpActionResult> Post(MasterData masterData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MasterDatas.Add(masterData);
            await db.SaveChangesAsync();

            return Created(masterData);
        }

        // PATCH: odata/MasterDatas(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<MasterData> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MasterData masterData = await db.MasterDatas.FindAsync(key);
            if (masterData == null)
            {
                return NotFound();
            }

            patch.Patch(masterData);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MasterDataExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(masterData);
        }

        // DELETE: odata/MasterDatas(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            MasterData masterData = await db.MasterDatas.FindAsync(key);
            if (masterData == null)
            {
                return NotFound();
            }

            db.MasterDatas.Remove(masterData);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/MasterDatas(5)/StudentClasses
        [EnableQuery]
        public IQueryable<StudentClass> GetStudentClasses([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.StudentClasses);
        }

        // GET: odata/MasterDatas(5)/StudentClasses1
        [EnableQuery]
        public IQueryable<StudentClass> GetStudentClasses1([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.StudentClasses1);
        }

        // GET: odata/MasterDatas(5)/Students
        [EnableQuery]
        public IQueryable<Student> GetStudents([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.Students);
        }

        // GET: odata/MasterDatas(5)/Students1
        [EnableQuery]
        public IQueryable<Student> GetStudents1([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.Students1);
        }

        // GET: odata/MasterDatas(5)/Students2
        [EnableQuery]
        public IQueryable<Student> GetStudents2([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.Students2);
        }

        // GET: odata/MasterDatas(5)/Students3
        [EnableQuery]
        public IQueryable<Student> GetStudents3([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.Students3);
        }

        // GET: odata/MasterDatas(5)/Students4
        [EnableQuery]
        public IQueryable<Student> GetStudents4([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.Students4);
        }

        // GET: odata/MasterDatas(5)/Students5
        [EnableQuery]
        public IQueryable<Student> GetStudents5([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.Students5);
        }

        // GET: odata/MasterDatas(5)/Students6
        [EnableQuery]
        public IQueryable<Student> GetStudents6([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.Students6);
        }

        // GET: odata/MasterDatas(5)/Students7
        [EnableQuery]
        public IQueryable<Student> GetStudents7([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.Students7);
        }

        // GET: odata/MasterDatas(5)/ClassFees
        [EnableQuery]
        public IQueryable<ClassFee> GetClassFees([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.ClassFees);
        }

        // GET: odata/MasterDatas(5)/ClassFees1
        [EnableQuery]
        public IQueryable<ClassFee> GetClassFees1([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.ClassFees1);
        }

        // GET: odata/MasterDatas(5)/ClassFees2
        [EnableQuery]
        public IQueryable<ClassFee> GetClassFees2([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.ClassFees2);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MasterDataExists(short key)
        {
            return db.MasterDatas.Count(e => e.MasterDataId == key) > 0;
        }
    }
}
