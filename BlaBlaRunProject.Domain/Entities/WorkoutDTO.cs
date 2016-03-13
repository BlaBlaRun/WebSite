using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaBlaRunProject.Domain.Entities
{
    public class WorkoutDTO
    {
        public long Id { get; set; }
        public long UsersId { get; set; }
        [Display(Name = "Fecha")]
        public System.DateTime StartDateTime { get; set; }

        [Display(Name = "Comienzo")]
        public System.Data.Entity.Spatial.DbGeography StartLocation { get; set; }

        [Display(Name = "Ritmo Medio (MM:SS)")]
        public Nullable<System.TimeSpan> AVGPace { get; set; }

        [Display(Name = "Circular?")]
        public bool Circular { get; set; }

        [Display(Name = "Finalización")]
        public System.Data.Entity.Spatial.DbGeography EndLocation { get; set; }

        [Display(Name = "Distancia (M)")]
        public Nullable<double> Distance { get; set; }

        [Display(Name = "Personas")]
        public Nullable<short> MaxNumberPeople { get; set; }

        [Display(Name = "Barrio")]
        public string Zone { get; set; }
        [Display(Name = "Ciudad")]
        public string City { get; set; }
        [Display(Name = "Provincia")]
        public string Region { get; set; }
        [Display(Name = "Pais")]
        public string Country { get; set; }

        [Display(Name = "Desnivel (M)")]
        public Nullable<double> ElevationGain { get; set; }
    }
}
