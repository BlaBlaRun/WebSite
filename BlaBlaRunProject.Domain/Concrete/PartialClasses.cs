using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlaBlaRunProject.Domain.Concrete
{

    [MetadataType(typeof(BlaBlaRunProject.Domain.Entities.WorkoutDTO))]
    public partial class Workout : Abstract.IIdentityKey<long>
    {
        
        
    }

    [MetadataType(typeof(BlaBlaRunProject.Domain.Entities.UsersDTO))]
    public partial class Users : Abstract.IIdentityKey<long>
    {

    }
    
}