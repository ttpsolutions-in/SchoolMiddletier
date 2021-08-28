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
    builder.EntitySet<TaskAssignment>("TaskAssignments");
    builder.EntitySet<EmpEmployee>("EmpEmployees"); 
    builder.EntitySet<MasterData>("MasterDatas"); 
    builder.EntitySet<Organization>("Organizations"); 
    builder.EntitySet<StudentClass>("StudentClasses"); 
    builder.EntitySet<TaskAssignmentComment>("TaskAssignmentComments"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class TaskAssignmentsController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/TaskAssignments
        [EnableQuery]
        public IQueryable<TaskAssignment> GetTaskAssignments()
        {
            return db.TaskAssignments;
        }

        // GET: odata/TaskAssignments(5)
        [EnableQuery]
        public SingleResult<TaskAssignment> GetTaskAssignment([FromODataUri] int key)
        {
            return SingleResult.Create(db.TaskAssignments.Where(taskAssignment => taskAssignment.AssignmentId == key));
        }

        // PUT: odata/TaskAssignments(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<TaskAssignment> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TaskAssignment taskAssignment = await db.TaskAssignments.FindAsync(key);
            if (taskAssignment == null)
            {
                return NotFound();
            }

            patch.Put(taskAssignment);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskAssignmentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(taskAssignment);
        }

        // POST: odata/TaskAssignments
        public async Task<IHttpActionResult> Post(TaskAssignment taskAssignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TaskAssignments.Add(taskAssignment);
            await db.SaveChangesAsync();

            return Created(taskAssignment);
        }

        // PATCH: odata/TaskAssignments(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<TaskAssignment> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TaskAssignment taskAssignment = await db.TaskAssignments.FindAsync(key);
            if (taskAssignment == null)
            {
                return NotFound();
            }

            patch.Patch(taskAssignment);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskAssignmentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(taskAssignment);
        }

        // DELETE: odata/TaskAssignments(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            TaskAssignment taskAssignment = await db.TaskAssignments.FindAsync(key);
            if (taskAssignment == null)
            {
                return NotFound();
            }

            db.TaskAssignments.Remove(taskAssignment);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/TaskAssignments(5)/EmpEmployee
        [EnableQuery]
        public SingleResult<EmpEmployee> GetEmpEmployee([FromODataUri] int key)
        {
            return SingleResult.Create(db.TaskAssignments.Where(m => m.AssignmentId == key).Select(m => m.EmpEmployee));
        }

        // GET: odata/TaskAssignments(5)/EmpEmployee1
        [EnableQuery]
        public SingleResult<EmpEmployee> GetEmpEmployee1([FromODataUri] int key)
        {
            return SingleResult.Create(db.TaskAssignments.Where(m => m.AssignmentId == key).Select(m => m.EmpEmployee1));
        }

        // GET: odata/TaskAssignments(5)/MasterData
        [EnableQuery]
        public SingleResult<MasterData> GetMasterData([FromODataUri] int key)
        {
            return SingleResult.Create(db.TaskAssignments.Where(m => m.AssignmentId == key).Select(m => m.MasterData));
        }

        // GET: odata/TaskAssignments(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] int key)
        {
            return SingleResult.Create(db.TaskAssignments.Where(m => m.AssignmentId == key).Select(m => m.Organization));
        }

        // GET: odata/TaskAssignments(5)/StudentClass
        [EnableQuery]
        public SingleResult<StudentClass> GetStudentClass([FromODataUri] int key)
        {
            return SingleResult.Create(db.TaskAssignments.Where(m => m.AssignmentId == key).Select(m => m.StudentClass));
        }

        // GET: odata/TaskAssignments(5)/TaskAssignmentComments
        [EnableQuery]
        public IQueryable<TaskAssignmentComment> GetTaskAssignmentComments([FromODataUri] int key)
        {
            return db.TaskAssignments.Where(m => m.AssignmentId == key).SelectMany(m => m.TaskAssignmentComments);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TaskAssignmentExists(int key)
        {
            return db.TaskAssignments.Count(e => e.AssignmentId == key) > 0;
        }
    }
}
