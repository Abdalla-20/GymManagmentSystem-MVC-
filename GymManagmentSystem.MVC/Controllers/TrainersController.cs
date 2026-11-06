using GymManagmentSystem.BLL.Services.Members.classes;
using GymManagmentSystem.BLL.Services.Members.Interfaces;
using GymManagmentSystem.BLL.Services.Members.ViewModels;
using GymManagmentSystem.BLL.Services.Trainers.Interfaces;
using GymManagmentSystem.BLL.Services.Trainers.ViewModels;
using GymManagmentSystem.MVC.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmentSystem.PL.Controllers;

public class TrainersController : Controller
{
    private readonly ITrainerServices _trainerService;

    public TrainersController(ITrainerServices trainerService)
    {
        _trainerService = trainerService;
    }
    public async Task<IActionResult> Index()
    {
        var result = await _trainerService.GetTrainersAsync();
        if (result.Success)
        {
            return View(result.Value);
        }
        else
        {
            return RedirectToAction(nameof(Index), nameof(HomeController));
        }
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTrainerViewModel model)
    {
        if (!ModelState.IsValid)
            return View(nameof(Create), model);

        var result = await _trainerService.CreateTrainerAsync(model);

        if (result.Success)
        {
            TempData["SuccessMessage"] = "Trainer created successfully.";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            TempData["ErrorMessage"] = result.error;
            return View(nameof(Create), model);
        }
    }

    public async Task<IActionResult> Delete(int? Id)
    {
        return View(Id);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int? Id)
    {
        var result = await _trainerService.DeleteTrainerAsync(Id);
        if (result.Success)
        {
            TempData["SuccessMessage"] = "Trainer deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            TempData["ErrorMessage"] = result.error;
            return View(Id);
        }
    }

    public async Task<IActionResult> Details(int? Id)
    {
        var result = await _trainerService.GetTrainerAsync(Id);
        if (!result.Success)
        {
            TempData["ErrorMessage"] = result.error;
            return RedirectToAction(nameof(Index));
        }
        return View(result.Value);
    }

    public async Task<IActionResult> Edit(int? Id)
    {
        var result = await _trainerService.GetTrainerToUpdateAsync(Id);

        if (!result.Success)
        {
            TempData["ErrorMessage"] = result.error;
            return RedirectToAction(nameof(Index));
        }
        return View(result.Value);
    }

    [HttpPost]

    public async Task<IActionResult> Edit(TrainerToUpdateViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await _trainerService.UpdateTrainerAsync(model);
        if (result.Success)
        {
            TempData["SuccessMessage"] = "Trainer updated successfully.";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            TempData["ErrorMessage"] = result.error;
            return View(model);
        }
    }
}
