namespace Volunteer.Models
{
    public class SoldierUser:ApplicationUser
    {
        public DateTime DateOfBirth { get; set; }
        public string City { get; set; }
        public string About { get; set; }
    }
}
