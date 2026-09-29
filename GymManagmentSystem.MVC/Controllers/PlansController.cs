using GymManagmentSystem.MVC.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymManagmentSystem.MVC.Controllers;
public class PlansController : Controller
{
    private readonly GymDbContext dbContext;

    public PlansController()
    {
        dbContext = new GymDbContext();
    }

    public async Task<IActionResult> Index()
    {
        var plans = await dbContext.Plans.ToListAsync();
        return View(plans);
    }

    public async Task<IActionResult> Details(int id)
    {
        var plan = await dbContext.Plans.FirstOrDefaultAsync(p => p.Id == id); 
        return View(plan);
    }
}
