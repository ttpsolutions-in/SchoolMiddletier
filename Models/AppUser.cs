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
    
    public partial class AppUser
    {
        public short ApplicationUserId { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public Nullable<short> ApplicationId { get; set; }
        public Nullable<short> RoleId { get; set; }
        public Nullable<byte> Active { get; set; }
    }
}
