using System.Collections.Generic;
using RavenDB.AspNet.Identity;

namespace WriteItUp2.Areas
{
    public class WriteItUpUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}