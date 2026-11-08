using System.ComponentModel.DataAnnotations;

namespace GymManagmentSystem.BLL.Services.Plans.ViewModels
{
    public class PlansToUpdateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "Duration is required")]
        [Range(1, 365, ErrorMessage = "Duration must be between 1 and 365")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01,99999999.99, ErrorMessage = "Price must be between 0.01 and 99999999.99")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Description must be between 1 and 200 characters")]
        public string Description { get; set; } = default!;

    }
}
