using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairDresserApp2
{
    public class Children : Customer
    {
        private bool isChild;

        public bool IsChild { get => isChild; set => isChild = value; }
        public Children()
        {

        }
        public Children(string customerName, uint customerAge, decimal customerHeight, string customerCreditCardNumber, string customerGender) : base(customerName, customerAge, customerHeight, customerCreditCardNumber, customerGender)
        {
        }
        public override void PerformOperations()
        {
            //Categorization - Children(Male/Female)
            if (CustomerGender.Equals("F"))
            {
                DoServices += HairStyling;
            }
            else
            {
                DoServices += TrimBeard;
            }
            DoServices += SensitiveTrimmer;
            DoServices += AdjustableSeat;
        }

        public override void SensitiveTrimmer()
        {
            Console.WriteLine("Trimmer Type : Sensitive");
        }

        public override void AdjustableSeat()
        {
            Console.WriteLine("Seat : Ajustable");
        }

        public override void TrimBeard()
        {
            Console.WriteLine("Done: Additional Trimming for Male Child");
        }

        public override void HairStyling()
        {
            Console.WriteLine("Done: Occasional Hair Styling for Female Child");
        }
        public override void TrimMoustache()
        {
            throw new NotImplementedException();
        }
    }
}
