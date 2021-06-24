//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace schools.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClassSubjectMarkComponent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClassSubjectMarkComponent()
        {
            this.ExamStudentSubjectResults = new HashSet<ExamStudentSubjectResult>();
        }
    
        public short ClassSubjectMarkComponentId { get; set; }
        public short ClassSubjectId { get; set; }
        public short SubjectComponentId { get; set; }
        public short FullMark { get; set; }
        public short PassMark { get; set; }
        public byte Active { get; set; }
        public short OrgId { get; set; }
        public short BatchId { get; set; }
        public Nullable<short> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<short> UpdatedBy { get; set; }
    
        public virtual Batch Batch { get; set; }
        public virtual ClassSubject ClassSubject { get; set; }
        public virtual MasterData MasterData { get; set; }
        public virtual Organization Organization { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamStudentSubjectResult> ExamStudentSubjectResults { get; set; }
    }
}