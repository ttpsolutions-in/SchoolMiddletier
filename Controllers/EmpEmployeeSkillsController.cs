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
    builder.EntitySet<EmpEmployeeSkill>("EmpEmployeeSkills");
    builder.EntitySet<EmpEmployee>("EmpEmployees"); 
    builder.EntitySet<Organization>("Organizations"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class EmpEmployeeSkillsController : ODataController
    {
        private TTPEntities db = new TTPEntities();

        // GET: odata/EmpEmployeeSkills
        [EnableQuery]
        public IQueryable<EmpEmployeeSkill> GetEmpEmployeeSkills()
        {
            return db.EmpEmployeeSkills;
        }

        // GET: odata/EmpEmployeeSkills(5)
        [EnableQuery]
        public SingleResult<EmpEmployeeSkill> GetEmpEmployeeSkill([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmpEmployeeSkills.Where(empEmployeeSkill => empEmployeeSkill.EmpEmployeeSkillId == key));
        }

        // PUT: odata/EmpEmployeeSkills(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Delta<EmpEmployeeSkill> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmpEmployeeSkill empEmployeeSkill = await db.EmpEmployeeSkills.FindAsync(key);
            if (empEmployeeSkill == null)
            {
                return NotFound();
            }

            patch.Put(empEmployeeSkill);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpEmployeeSkillExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(empEmployeeSkill);
        }

        // POST: odata/EmpEmployeeSkills
        public async Task<IHttpActionResult> Post(EmpEmployeeSkill empEmployeeSkill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmpEmployeeSkills.Add(empEmployeeSkill);
            await db.SaveChangesAsync();

            return Created(empEmployeeSkill);
        }

        // PATCH: odata/EmpEmployeeSkills(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<EmpEmployeeSkill> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmpEmployeeSkill empEmployeeSkill = await db.EmpEmployeeSkills.FindAsync(key);
            if (empEmployeeSkill == null)
            {
                return NotFound();
            }

            patch.Patch(empEmployeeSkill);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpEmployeeSkillExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(empEmployeeSkill);
        }

        // DELETE: odata/EmpEmployeeSkills(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            EmpEmployeeSkill empEmployeeSkill = await db.EmpEmployeeSkills.FindAsync(key);
            if (empEmployeeSkill == null)
            {
                return NotFound();
            }

            db.EmpEmployeeSkills.Remove(empEmployeeSkill);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/EmpEmployeeSkills(5)/EmpEmployee
        [EnableQuery]
        public SingleResult<EmpEmployee> GetEmpEmployee([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmpEmployeeSkills.Where(m => m.EmpEmployeeSkillId == key).Select(m => m.EmpEmployee));
        }

        // GET: odata/EmpEmployeeSkills(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.EmpEmployeeSkills.Where(m => m.EmpEmployeeSkillId == key).Select(m => m.Organization));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmpEmployeeSkillExists(short key)
        {
            return db.EmpEmployeeSkills.Count(e => e.EmpEmployeeSkillId == key) > 0;
        }
    }
}
