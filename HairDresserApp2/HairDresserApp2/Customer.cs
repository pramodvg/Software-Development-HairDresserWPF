using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HairDresserApp2
{
    // Delegate to Perform Tasks
    public delegate void DoServices();

    [Serializable]
    [XmlInclude(typeof(Gentlemen)), XmlInclude(typeof(Ladies)), XmlInclude(typeof(Children))]
    public abstract class Customer : ICustomer
    {
        private string customerName;
        private uint customerAge;
        private decimal customerHeight;
        private string customerCreditCardNumber;
        private string customerGender;
        private DoServices doServices;

        [DataMember]
        [XmlAttribute("customerName")]
        public string CustomerName { get => customerName; set => customerName = value; }
        public uint CustomerAge { get => customerAge; set => customerAge = value; }
        public decimal CustomerHeight { get => customerHeight; set => customerHeight = value; }
        public string CustomerCreditCardNumber { get => ConcealedCreditCard(); }
        public string CustomerGender { get => customerGender; set => customerGender = value; }
        [XmlIgnore]
        public DoServices DoServices { get => doServices; set => doServices = value; }
        public Customer()
        {

        }
        public Customer(string customerName, uint customerAge, decimal customerHeight, string customerCreditCardNumber, string customerGender)
        {
            this.customerName = customerName;
            this.CustomerAge = customerAge;
            this.customerHeight = customerHeight;
            this.customerCreditCardNumber = customerCreditCardNumber;
            this.customerGender = customerGender;

            // Adding Common services to delegete
            this.doServices += HairWash;
            this.doServices += HairDye;
            this.doServices += Hairtrim;
        }

        public void Hairtrim()
        {
            Console.WriteLine("Done: Hair Trim");
        }
        public void HairDye()
        {
            Console.WriteLine("Done: Hair Dye");
        }
        public void HairWash()
        {
            Console.WriteLine("Done: Hair Wash");
        }

        public abstract void TrimBeard();

        public abstract void TrimMoustache();

        public abstract void HairStyling();

        public abstract void SensitiveTrimmer();

        public abstract void AdjustableSeat();

        public abstract void PerformOperations();


        //Method to Conceal CreditCard Info.
        private string ConcealedCreditCard()
        {
            char[] creditArray = customerCreditCardNumber.ToString().ToCharArray();

            for (int i = 4; i < 12; i++)
            {
                creditArray[i] = 'X';
            }
            //Formatting the Masked Array
            char[] maskedArray = { creditArray[0], creditArray[1], creditArray[2], creditArray[3], ' ', creditArray[4], creditArray[5], creditArray[6], creditArray[7], ' ', creditArray[8], creditArray[9], creditArray[10], creditArray[11], ' ', creditArray[12], creditArray[13], creditArray[14], creditArray[15] };

            return new string(maskedArray);
        }

    }
}
