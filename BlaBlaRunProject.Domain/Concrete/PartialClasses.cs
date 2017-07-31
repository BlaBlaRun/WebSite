using BlaBlaRunProject.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlaBlaRunProject.Domain.Concrete
{

    [MetadataType(typeof(BlaBlaRunProject.Domain.Entities.WorkoutDTO))]
    public partial class Workouts : IIdentityKey<long>
    {


    }

    [MetadataType(typeof(BlaBlaRunProject.Domain.Entities.WorkoutOldDTO))]
    public partial class WorkoutsOld : IIdentityKey<long>
    {
        
    }

    [MetadataType(typeof(BlaBlaRunProject.Domain.Entities.AuditDTO))]
    public partial class Audit : IIdentityKey<long>
    {

    }

    //[MetadataType(typeof(BlaBlaRunProject.Domain.Entities.UsersDTO))]
    public partial class Users : IIdentityKey<long>
    {

    }

}