//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChangelogTest.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user()
        {
            this.Changelogs = new HashSet<Changelog>();
            this.UserAccessProducts = new HashSet<UserAccessProduct>();
        }
    
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int UsertypeID { get; set; }
        public int CustomerID { get; set; }
        public String Fullname => $"{Firstname} {Lastname}";


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Changelog> Changelogs { get; set; }
        public virtual Customer Customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserAccessProduct> UserAccessProducts { get; set; }
        public virtual Usertype Usertype { get; set; }
    }
}
