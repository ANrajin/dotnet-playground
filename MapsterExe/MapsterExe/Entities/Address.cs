using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapsterExe.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Region { get; set; }  
        public string Country { get; set; }
    }
}
