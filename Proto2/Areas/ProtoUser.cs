using RavenDB.AspNet.Identity;

namespace Proto2.Areas
{
    public class ProtoUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}