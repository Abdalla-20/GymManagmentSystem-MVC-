using GymManagmentSystem.BLL.Services.Members.Interfaces;
using GymManagmentSystem.BLL.Services.Members.ViewModels;
using GymManagmentSystem.MVC.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmentSystem.PL.Controllers;

public class MembersController : Controller
{
    private readonly IMemberService _memberService;
    public MembersController(IMemberService memberService)
    {
        _memberService = memberService;
    }
    public async Task<IActionResult> Index()
    {
        var result = await _memberService.GetMembersAsync();
        if (result.Success)
        {
            return View(result.Value);
        }
        else
        {
           return RedirectToAction(nameof(Index),nameof(HomeController));
        }
    }

    [HttpGet]

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateMember(CreateMemberViewModel model)
    {
      if (!ModelState.IsValid)
            return View(nameof(Create),model);

      var result = await _memberService.CreateMemberAsync(model);

        if (result.Success) 
        {
            TempData["SuccessMessage"] = "Member created successfully.";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            TempData["ErrorMessage"] = result.error;

            return View(nameof(Create), model);
        }

    }

    [HttpGet]
    public async Task<IActionResult> Details(int? Id)
    {
       var result = await _memberService.GetMemberAsync(Id);
       if (!result.Success)
       {
            TempData["ErrorMessage"] = result.error;
            return RedirectToAction(nameof(Index));
       }
        return View(result.Value);
    }

    public async Task<IActionResult> HealthRecordDetails(int? Id)
    {
        var result = await _memberService.GetHealthRecordDetailsAsync(Id);

        if (!result.Success)
        {
            TempData["ErrorMessage"] = result.error;
            return RedirectToAction(nameof(Index));
        }
        return View(result.Value);

    }

    public async Task<IActionResult> EditMember(int? Id)
    {
        var result = await _memberService.GetMemberToUpdateAsync(Id);

        if (!result.Success)
        {
            TempData["ErrorMessage"] = result.error;
            return RedirectToAction(nameof(Index));
        }
        return View(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> EditMember(MemberToUpdateViewModel model)
    {
        if (!ModelState.IsValid)
            return View( model);

        var result = await _memberService.UpdateMemberAsync(model);
        if (result.Success) 
        {
            TempData["SuccessMessage"] = "Member updated successfully.";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            TempData["ErrorMessage"] = result.error;
            return View(model);
        }
    }

    public async Task<IActionResult> Delete(int? Id)
    {
      return View (Id);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var result = await _memberService.DeleteMemberAsync(id);

        if (result.Success) 
        {
            TempData["SuccessMessage"] = "Member deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            TempData["ErrorMessage"] =result.error;
            return View(id);
        }
    }
}    

