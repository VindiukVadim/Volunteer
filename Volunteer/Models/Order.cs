namespace Volunteer.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set;}
        public DateTime StartDate { get; set;}
        public DateTime EndDate { get; set; }
        public DateTime ExecutionDate { get; set;} 
        public int Status { get; set;}
        public Guid? MilitaryUnitId { get; set;}
        public MilitaryUnit MilitaryUnits { get; set;}
        public Guid? OrganizationId { get; set;}
        public Organization Organizations { get; set;}
    }
}
