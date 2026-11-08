using GymManagmentSystem.BLL.Services.Members.classes;
using GymManagmentSystem.BLL.Services.Members.Interfaces;
using GymManagmentSystem.BLL.Services.Plans.InterFaces;
using GymManagmentSystem.BLL.Services.Plans.ViewModels;
using GymManagmentSystem.DAL.Models;
using GymManagmentSystem.DAL.Repositories.Classes;
using GymManagmentSystem.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmentSystem.MVC.Controllers;
public class PlansController : Controller
{
    private readonly IPlansServices _planService;
    public PlansController(IPlansServices planService)
    {
        _planService = planService;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _planService.GetPlansAsync();
        if (result.Success)
        {
            return View(result.Value);
        }
        else
        {
            return RedirectToAction(nameof(Index), nameof(HomeController));
        }
    }

    public async Task<IActionResult> Edit(int? Id)
    {
        var result = await _planService.GetPlanToUpdateAsync(Id);

        if (!result.Success)
        {
            TempData["ErrorMessage"] = result.error;
            return RedirectToAction(nameof(Index));
        }
        return View(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(PlansToUpdateViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);
        var result = await _planService.UpdatePlanAsync(model);
        if (result.Success)
        {
            TempData["SuccessMessage"] = "Plan updated successfully.";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            TempData["ErrorMessage"] = result.error;
            return View(model);
        }
    }

    public async Task<IActionResult> Details(int? Id)
    {
        var result = await _planService.GetPlanAsync(Id);
        if (!result.Success)
        {
            TempData["ErrorMessage"] = result.error;
            return RedirectToAction(nameof(Index));
        }
        return View(result.Value);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeStatus(int? Id)
    {
        var result = await _planService.StatusOfPlanAsync(Id);
        if (!result.Success)
        {
            TempData["ErrorMessage"] = result.error;
            return RedirectToAction(nameof(Index));
        }
        TempData["SuccessMessage"] = "Plan status changed successfully.";
        return RedirectToAction(nameof(Index));
    }
}
