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
    
    public partial class StudentDocument
    {
        public short StudentDocId { get; set; }
        public string DocName { get; set; }
        public System.DateTime UploadDate { get; set; }
        public int StudentId { get; set; }
        public int StudentClassId { get; set; }
        public short DocTypeId { get; set; }
        public byte Active { get; set; }
    
        public virtual MasterData MasterData { get; set; }
        public virtual StudentClass StudentClass { get; set; }
        public virtual Student Student { get; set; }
    }
}
