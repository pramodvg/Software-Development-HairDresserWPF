using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairDresserApp2
{
    public interface ICustomer
    {
        string CustomerName { get ; set  ; }
        uint CustomerAge { get; set; }
        decimal CustomerHeight { get; set; }
        string CustomerCreditCardNumber { get; }
        string CustomerGender { get; set; }

        void Hairtrim();

        void HairDye();

        void HairWash();
        
    }
}
