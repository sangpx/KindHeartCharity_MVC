using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace KindHeartCharity.Models.Domain
{
    public class User : IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

    }
}
