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
    
    public partial class Page
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Page()
        {
            this.PageHistories = new HashSet<PageHistory>();
        }
    
        public short PageId { get; set; }
        public string PageTitle { get; set; }
        public Nullable<short> LatestPublishedId { get; set; }
        public Nullable<short> LatestDraftId { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public byte Active { get; set; }
        public Nullable<byte> CurrentVersion { get; set; }
        public string label { get; set; }
        public string faIcon { get; set; }
        public string link { get; set; }
        public Nullable<short> ParentId { get; set; }
        public Nullable<byte> IsTemplate { get; set; }
        public Nullable<short> DisplayOrder { get; set; }
        public Nullable<byte> HasSubmenu { get; set; }
        public Nullable<byte> HomePage { get; set; }
        public Nullable<short> Module { get; set; }
        public Nullable<short> OrgId { get; set; }
        public string FullPath { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PageHistory> PageHistories { get; set; }
        public virtual MasterData MasterData { get; set; }
    }
}
