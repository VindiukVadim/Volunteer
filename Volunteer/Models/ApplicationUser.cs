using Microsoft.AspNetCore.Identity;

namespace Volunteer.Models
{
    public class ApplicationUser:IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }   
    }
}
