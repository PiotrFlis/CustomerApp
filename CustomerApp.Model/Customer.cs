using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApp.Model
{
    
    public class Customer : ICustomer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public String Surname { get; set; }
        public String TelephoneNumber { get; set; }
        public String Address { get; set; }

        public override bool Equals(object obj)
        {
            if ( typeof (object) == typeof(Customer))
                return false;
            Customer c = (Customer)obj;
            
            return c.Name == this.Name
                && c.Surname == this.Surname
                && c.Address == this.Address
                && c.TelephoneNumber == this.TelephoneNumber;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    


}
