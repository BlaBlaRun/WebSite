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
    
    public partial class WorkoutsOld
    {
        public long Id { get; set; }
        public long UsersId { get; set; }
        public System.DateTime StartDateTime { get; set; }
        public System.Data.Entity.Spatial.DbGeography StartLocation { get; set; }
        public Nullable<System.TimeSpan> AVGPace { get; set; }
        public bool Circular { get; set; }
        public System.Data.Entity.Spatial.DbGeography EndLocation { get; set; }
        public Nullable<double> Distance { get; set; }
        public Nullable<short> MaxNumberPeople { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public Nullable<double> ElevationGain { get; set; }
    }
}
