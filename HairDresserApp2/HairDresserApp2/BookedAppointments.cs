using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairDresserApp2
{

    public class BookedAppointments
    {
        public List<Appointment> bookedAppointmentsList = null;

        public BookedAppointments()
        {
            bookedAppointmentsList = new List<Appointment>();
        }

        public void Add(Appointment appointment)
        {
            bookedAppointmentsList.Add(appointment);
        }

        // Sorting the List<Appointment>
     /*   public void Sort()
        {
            this.bookedAppointmentsList.Sort();
        }
*/
      /*  public IEnumerator<Appointment> GetEnumerator()
        {
            return ((IEnumerable<Appointment>)bookedAppointmentsList).GetEnumerator();
        }*/


      /*  public int Count
        {
            get { return bookedAppointmentsList.Count; }
        }

        public Appointment this[int i]
        {
            get { return bookedAppointmentsList[i]; }
            set { bookedAppointmentsList[i] = value; }
        }*/
    }
}
