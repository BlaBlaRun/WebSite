//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlaBlaRunProject.Domain.Concrete
{
    using System;
    using System.Collections.Generic;
    
    public partial class Workouts
    {
        public long Id { get; set; }
        public long UsersId { get; set; }
    
        public virtual Users Users { get; set; }
    }
}