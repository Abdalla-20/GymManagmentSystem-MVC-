using Azure.Identity;

namespace GymManagmentSystem.BLL.Services.Trainers.ViewModels
{
    public class TrainerDetailsViewModel
    {
        public string? Name { get; set; }
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string? DateOfBirth { get; set; }
        public string Address { get; set; } = default!;
        public string Specialization { get; set; } = default!;
    }
}
