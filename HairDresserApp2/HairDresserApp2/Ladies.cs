using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairDresserApp2
{
    public class Ladies : Customer
    {
        private bool isLady;

        public bool IsLady { get => isLady; set => isLady = value; }
        public Ladies()
        {

        }
        public Ladies(string customerName, uint customerAge, decimal customerHeight, string customerCreditCardNumber, string customerGender) : base(customerName, customerAge, customerHeight, customerCreditCardNumber, customerGender)
        {

        }
       
        public override void HairStyling()
        {
            Console.WriteLine("Done: Hair Styling for Occasions");
        }
        public override void PerformOperations()
        {
            DoServices += HairStyling;
        }
        public override void TrimBeard()
        {
            throw new NotImplementedException();
        }

        public override void TrimMoustache()
        {
            throw new NotImplementedException();
        }

        public override void SensitiveTrimmer()
        {
            throw new NotImplementedException();
        }

        public override void AdjustableSeat()
        {
            throw new NotImplementedException();
        }
    }
}
