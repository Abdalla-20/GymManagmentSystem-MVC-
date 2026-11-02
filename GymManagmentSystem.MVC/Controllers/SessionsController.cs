using GymManagmentSystem.BLL.Services.Sessions.Interfaces;
using GymManagmentSystem.BLL.Services.Sessions.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagmentSystem.PL.Controllers;

public class SessionsController : Controller
{
    private readonly ISessionService _sessionService;

    public SessionsController(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _sessionService.GetAllSessionsAsync();
        return View(result.Value);
    }

    public async Task<IActionResult> Create()
    {
        await LoadTrainersAndCategoriesForDropDownList();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSessionViewModel model)
    {
        var result = await _sessionService.CreateSessionAsync(model);


        if (result.Success)
        {
           TempData["SuccessMessage"] = "Session created successfully.";
           return RedirectToAction(nameof(Index));
        }
        await LoadTrainersAndCategoriesForDropDownList();
        TempData["ErrorMessage"] = result.error;

        return View(model);
    }

    #region Helper Methods 

    private async Task LoadTrainersAndCategoriesForDropDownList()
    {
        var trainers = await _sessionService.GetTrainersForDropdownAsync();
        var categories = await _sessionService.GetCategoriesForDropdownAsync();
        ViewBag.Trainers = new SelectList(trainers.Value, "Id", "Name");
        ViewBag.Categories = new SelectList(categories.Value, "Id", "CategoryName");
    }

    #endregion
}
