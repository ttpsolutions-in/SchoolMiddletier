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
    
    public partial class EmpEmployeeSkill
    {
        public short EmpEmployeeSkillId { get; set; }
        public short SkillId { get; set; }
        public short EmployeeId { get; set; }
        public byte Status { get; set; }
        public short ExperienceInMonths { get; set; }
        public short OrgId { get; set; }
    
        public virtual EmpEmployee EmpEmployee { get; set; }
        public virtual Organization Organization { get; set; }
    }
}