namespace GymManagmentSystem.BLL.Services.MemberShip.ViewModels
{
    public class MemberShipViewModel
    {
        //public int Id { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; } = default!;
        public int PlanId { get; set; }
        public string PlanName { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //public string Status => IsActive ? "Active" : "Expired";
        //public bool IsActive => EndDate > DateTime.Now && StartDate <= DateTime.Now;
    }
}
