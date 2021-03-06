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
    
    public partial class PaymentDetail
    {
        public int PaymentId { get; set; }
        public Nullable<decimal> PaymentAmt { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public string ReceivedBy { get; set; }
        public Nullable<int> ParentId { get; set; }
        public Nullable<short> ReceiptNo { get; set; }
        public Nullable<short> ClassFeeId { get; set; }
        public Nullable<byte> Active { get; set; }
    
        public virtual ClassFee ClassFee { get; set; }
        public virtual StudentFeePayment StudentFeePayment { get; set; }
        public virtual StudentFeeReceipt StudentFeeReceipt { get; set; }
    }
}
