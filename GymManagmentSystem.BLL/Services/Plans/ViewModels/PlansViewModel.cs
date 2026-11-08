namespace GymManagmentSystem.BLL.Services.Plans.ViewModels;

public class PlansViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int Duration { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; } = default!;
    public bool IsActive { get; set; }
}  
