using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairDresserApp2
{
    public class Gentlemen : Customer
    {
        private bool isGentleman;

        public bool IsGentleman { get => isGentleman; set => isGentleman = value; }
        public Gentlemen()
        {

        }
        public Gentlemen(string customerName, uint customerAge, decimal customerHeight, string customerCreditCardNumber, string customerGender) : base(customerName, customerAge, customerHeight, customerCreditCardNumber, customerGender)
        {
        }
       
        public override void TrimBeard()
        {
            Console.WriteLine("Done: Trim Beard");
        }
        public override void TrimMoustache()
        {
            Console.WriteLine("Done: Trim Moustache");
        }
        public override void PerformOperations()
        {
            DoServices += TrimBeard;
            DoServices += TrimMoustache;
        }
        public override void HairStyling()
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
