namespace GymManagmentSystem.DAL.Models
{
    public class HealthRecord : BaseEntity
    {
        public string Height { get; set; } = default!;
        public string Weight { get; set; } = default!;
        public string BloodType { get; set; }=default!;
        public string? Note { get; set; } 

    }
}
