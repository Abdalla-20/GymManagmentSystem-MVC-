using GymManagmentSystem.DAL.Models;
using GymManagmentSystem.DAL.Repositories.Classes;
using GymManagmentSystem.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmentSystem.MVC.Controllers;
public class PlansController : Controller
{
  //private IPlanRepository _planRepository;
  private IRepository<Plan> _planRepository;

    public PlansController(IRepository<Plan> PlanRepository)
    {
        _planRepository = PlanRepository;
    }

    public async Task<IActionResult> Index()
    {
        var plans = await _planRepository.GetAllAsync();
        return View(plans);
    }

    public async Task<IActionResult> Details(int id)
    {
        var plan = await _planRepository.GetByIdAsync( id);

        if (plan is null)
            return RedirectToAction(nameof(Index));
        else
        return View(plan);
    }
}
