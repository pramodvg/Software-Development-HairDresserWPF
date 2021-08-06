using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HairDresserApp2
{
    public enum Category
    {
        Children, Ladies, Gentlemen,
    }

    public class Appointment /*: IAppointment*/
    {
        private string timeStamp;
        private Category category;

        private Customer customer1;
        private string availedServices;

        private ICustomer customer;


        public string TimeStamp { get => timeStamp; set => timeStamp = value; }
        public Customer Customer1 { get => customer1; set => customer1 = value; }
        public Category Category { get => category; set => category = value; }
        public string AvailedServices { get => availedServices; set => availedServices = value; }

        [XmlIgnore] 
        public ICustomer Customer { get => customer; set => customer = value; }

        // Comparing the age of customer
       /* public int CompareTo(Appointment other)
        {
            return this.customer.CustomerAge.CompareTo(other.customer.CustomerAge);
        }*/
    }
}
