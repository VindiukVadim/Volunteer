namespace Volunteer.Models
{
    public class MilitaryUnit
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid MainSoldierId { get; set; }
        public List<SoldierUser>? SoldierUsers { get; set; }
    }
}
