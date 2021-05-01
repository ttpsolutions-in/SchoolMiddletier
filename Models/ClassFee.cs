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
    
    public partial class ClassFee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClassFee()
        {
            this.PaymentDetails = new HashSet<PaymentDetail>();
            this.StudentFeePayments = new HashSet<StudentFeePayment>();
        }
    
        public short ClassFeeId { get; set; }
        public short FeeNameId { get; set; }
        public short ClassId { get; set; }
        public decimal Amount { get; set; }
        public short Batch { get; set; }
        public byte Active { get; set; }
        public Nullable<short> LocationId { get; set; }
        public Nullable<byte> PaymentOrder { get; set; }
    
        public virtual MasterData MasterData { get; set; }
        public virtual MasterData MasterData1 { get; set; }
        public virtual MasterData MasterData2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaymentDetail> PaymentDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentFeePayment> StudentFeePayments { get; set; }
    }
}