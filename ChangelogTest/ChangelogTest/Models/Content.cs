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
    
    public partial class Content
    {
        public int ChangelogID { get; set; }
        public int ContentTypeID { get; set; }
        public string Content1 { get; set; }
    
        public virtual Changelog Changelog { get; set; }
        public virtual ContentType ContentType { get; set; }
    }
}
