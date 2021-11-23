using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FactOfWork.Models.Identity
{
    public class AppUser : IdentityUser
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patr { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }

        public string GetFIO()
        {
            return Surname + " " + Name + " " + Patr;
        }
    }
}
