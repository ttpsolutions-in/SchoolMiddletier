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
    
    public partial class AccountingLedgerTrialBalance
    {
        public short StudentEmployeeLedegerId { get; set; }
        public Nullable<int> StudentClassId { get; set; }
        public Nullable<short> EmployeeId { get; set; }
        public Nullable<short> GeneralLedgerId { get; set; }
        public Nullable<short> MonthYearId { get; set; }
        public short AccountGroupId { get; set; }
        public short AccountNatureId { get; set; }
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal Balance { get; set; }
        public short OrgId { get; set; }
        public short BatchId { get; set; }
        public Nullable<short> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<short> UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte Active { get; set; }
    
        public virtual Batch Batch { get; set; }
        public virtual EmpEmployee EmpEmployee { get; set; }
        public virtual MasterData MasterData { get; set; }
        public virtual MasterData MasterData1 { get; set; }
        public virtual MasterData MasterData2 { get; set; }
        public virtual MasterData MasterData3 { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual StudentClass StudentClass { get; set; }
    }
}