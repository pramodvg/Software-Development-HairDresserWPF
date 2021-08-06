using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairDresserApp2
{
    public interface IAppointment : IComparable<Appointment>
    {
         string TimeStamp { get ; set; }

         ICustomer Customer { get; set; }

         Category Category { get; set ; }

    }
}
