using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApp.Model
{
    public interface ICustomer
    {
        string Name { get; set; }
        String Surname { get; set; }
        String TelephoneNumber { get; set; }
        String Address { get; set; }
    }
}
