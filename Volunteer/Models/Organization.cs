namespace Volunteer.Models
{
    public class Organization
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid MainVolunteerId { get; set; }   
        public List<VolunteerUser> VolunteerUsers { get; set; }
    }
}
