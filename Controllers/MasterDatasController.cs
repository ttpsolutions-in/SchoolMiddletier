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
    builder.EntitySet<ClassSubjectMarkComponent>("ClassSubjectMarkComponents"); 
    builder.EntitySet<EmpEmployeeGradeSalHistory>("EmpEmployeeGradeSalHistories"); 
    builder.EntitySet<EmpHolidayList>("EmpHolidayLists"); 
    builder.EntitySet<EmployeeFamily>("EmployeeFamilies"); 
    builder.EntitySet<EmployeeLeaf>("EmployeeLeaves"); 
    builder.EntitySet<Exam>("Exams"); 
    builder.EntitySet<ExamStudentResult>("ExamStudentResults"); 
    builder.EntitySet<ExamStudentSubjectResult>("ExamStudentSubjectResults"); 
    builder.EntitySet<FilesNPhoto>("FilesNPhotos"); 
    builder.EntitySet<Organization>("Organizations"); 
    builder.EntitySet<RoleUser>("RoleUsers"); 
    builder.EntitySet<StudentClass>("StudentClasses"); 
    builder.EntitySet<StudentDocument>("StudentDocuments"); 
    builder.EntitySet<Student>("Students"); 
    builder.EntitySet<EmpComponent>("EmpComponents"); 
    builder.EntitySet<ClassSubject>("ClassSubjects"); 
    builder.EntitySet<StudTeacherClassMapping>("StudTeacherClassMappings"); 
    builder.EntitySet<Page>("Pages"); 
    builder.EntitySet<ApplicationFeature>("ApplicationFeatures"); 
    builder.EntitySet<AccountingTrialBalance>("AccountingTrialBalances"); 
    builder.EntitySet<ClassFee>("ClassFees"); 
    builder.EntitySet<AccountingLedgerTrialBalance>("AccountingLedgerTrialBalances"); 
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

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MasterDataExists(masterData.MasterDataId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

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

        // GET: odata/MasterDatas(5)/ClassSubjectMarkComponents
        [EnableQuery]
        public IQueryable<ClassSubjectMarkComponent> GetClassSubjectMarkComponents([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.ClassSubjectMarkComponents);
        }

        // GET: odata/MasterDatas(5)/EmpEmployeeGradeSalHistories
        [EnableQuery]
        public IQueryable<EmpEmployeeGradeSalHistory> GetEmpEmployeeGradeSalHistories([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.EmpEmployeeGradeSalHistories);
        }

        // GET: odata/MasterDatas(5)/EmpEmployeeGradeSalHistories1
        [EnableQuery]
        public IQueryable<EmpEmployeeGradeSalHistory> GetEmpEmployeeGradeSalHistories1([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.EmpEmployeeGradeSalHistories1);
        }

        // GET: odata/MasterDatas(5)/EmpHolidayLists
        [EnableQuery]
        public IQueryable<EmpHolidayList> GetEmpHolidayLists([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.EmpHolidayLists);
        }

        // GET: odata/MasterDatas(5)/EmpHolidayLists1
        [EnableQuery]
        public IQueryable<EmpHolidayList> GetEmpHolidayLists1([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.EmpHolidayLists1);
        }

        // GET: odata/MasterDatas(5)/EmployeeFamilies
        [EnableQuery]
        public IQueryable<EmployeeFamily> GetEmployeeFamilies([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.EmployeeFamilies);
        }

        // GET: odata/MasterDatas(5)/EmployeeFamilies1
        [EnableQuery]
        public IQueryable<EmployeeFamily> GetEmployeeFamilies1([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.EmployeeFamilies1);
        }

        // GET: odata/MasterDatas(5)/EmployeeLeaves
        [EnableQuery]
        public IQueryable<EmployeeLeaf> GetEmployeeLeaves([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.EmployeeLeaves);
        }

        // GET: odata/MasterDatas(5)/EmployeeLeaves1
        [EnableQuery]
        public IQueryable<EmployeeLeaf> GetEmployeeLeaves1([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.EmployeeLeaves1);
        }

        // GET: odata/MasterDatas(5)/Exams
        [EnableQuery]
        public IQueryable<Exam> GetExams([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.Exams);
        }

        // GET: odata/MasterDatas(5)/ExamStudentResults
        [EnableQuery]
        public IQueryable<ExamStudentResult> GetExamStudentResults([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.ExamStudentResults);
        }

        // GET: odata/MasterDatas(5)/ExamStudentResults1
        [EnableQuery]
        public IQueryable<ExamStudentResult> GetExamStudentResults1([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.ExamStudentResults1);
        }

        // GET: odata/MasterDatas(5)/ExamStudentSubjectResults
        [EnableQuery]
        public IQueryable<ExamStudentSubjectResult> GetExamStudentSubjectResults([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.ExamStudentSubjectResults);
        }

        // GET: odata/MasterDatas(5)/FilesNPhotos
        [EnableQuery]
        public IQueryable<FilesNPhoto> GetFilesNPhotos([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.FilesNPhotos);
        }

        // GET: odata/MasterDatas(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] short key)
        {
            return SingleResult.Create(db.MasterDatas.Where(m => m.MasterDataId == key).Select(m => m.Organization));
        }

        // GET: odata/MasterDatas(5)/RoleUsers
        [EnableQuery]
        public IQueryable<RoleUser> GetRoleUsers([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.RoleUsers);
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

        // GET: odata/MasterDatas(5)/StudentDocuments
        [EnableQuery]
        public IQueryable<StudentDocument> GetStudentDocuments([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.StudentDocuments);
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

        // GET: odata/MasterDatas(5)/Students8
        [EnableQuery]
        public IQueryable<Student> GetStudents8([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.Students8);
        }

        // GET: odata/MasterDatas(5)/EmpComponents
        [EnableQuery]
        public IQueryable<EmpComponent> GetEmpComponents([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.EmpComponents);
        }

        // GET: odata/MasterDatas(5)/ClassSubjects
        [EnableQuery]
        public IQueryable<ClassSubject> GetClassSubjects([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.ClassSubjects);
        }

        // GET: odata/MasterDatas(5)/ClassSubjects1
        [EnableQuery]
        public IQueryable<ClassSubject> GetClassSubjects1([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.ClassSubjects1);
        }

        // GET: odata/MasterDatas(5)/StudTeacherClassMappings
        [EnableQuery]
        public IQueryable<StudTeacherClassMapping> GetStudTeacherClassMappings([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.StudTeacherClassMappings);
        }

        // GET: odata/MasterDatas(5)/StudTeacherClassMappings1
        [EnableQuery]
        public IQueryable<StudTeacherClassMapping> GetStudTeacherClassMappings1([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.StudTeacherClassMappings1);
        }

        // GET: odata/MasterDatas(5)/Pages
        [EnableQuery]
        public IQueryable<Page> GetPages([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.Pages);
        }

        // GET: odata/MasterDatas(5)/ApplicationFeatures
        [EnableQuery]
        public IQueryable<ApplicationFeature> GetApplicationFeatures([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.ApplicationFeatures);
        }

        // GET: odata/MasterDatas(5)/AccountingTrialBalances
        [EnableQuery]
        public IQueryable<AccountingTrialBalance> GetAccountingTrialBalances([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.AccountingTrialBalances);
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

        // GET: odata/MasterDatas(5)/AccountingLedgerTrialBalances
        [EnableQuery]
        public IQueryable<AccountingLedgerTrialBalance> GetAccountingLedgerTrialBalances([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.AccountingLedgerTrialBalances);
        }

        // GET: odata/MasterDatas(5)/AccountingLedgerTrialBalances1
        [EnableQuery]
        public IQueryable<AccountingLedgerTrialBalance> GetAccountingLedgerTrialBalances1([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.AccountingLedgerTrialBalances1);
        }

        // GET: odata/MasterDatas(5)/AccountingLedgerTrialBalances2
        [EnableQuery]
        public IQueryable<AccountingLedgerTrialBalance> GetAccountingLedgerTrialBalances2([FromODataUri] short key)
        {
            return db.MasterDatas.Where(m => m.MasterDataId == key).SelectMany(m => m.AccountingLedgerTrialBalances2);
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
