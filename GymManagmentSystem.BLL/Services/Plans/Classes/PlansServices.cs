using AutoMapper;
using AutoMapper.Execution;
using GymManagmentSystem.BLL.Common;
using GymManagmentSystem.BLL.Services.Members.ViewModels;
using GymManagmentSystem.BLL.Services.Plans.InterFaces;
using GymManagmentSystem.BLL.Services.Plans.ViewModels;
using GymManagmentSystem.DAL.Models;
using GymManagmentSystem.DAL.Repositories.Classes;
using GymManagmentSystem.DAL.Repositories.Interfaces;

namespace GymManagmentSystem.BLL.Services.Plans.Class;

public class PlansServices : IPlansServices
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper mapper;

    public PlansServices(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
    public Task<Result<PlansDetailesViewModel?>> GetPlanAsync(int? Id)
    {
        if (Id == null || Id.Value == 0)
            return Task.FromResult(Result<PlansDetailesViewModel?>.Fail("Id not found"));

        var plan = _unitOfWork.GetRepository<Plan>().GetByIdAsync(Id.Value).Result;
        if (plan == null)
            return Task.FromResult(Result<PlansDetailesViewModel?>.Fail("Plan not found"));

        var model = mapper.Map<PlansDetailesViewModel>(plan);
        return Task.FromResult(Result<PlansDetailesViewModel?>.Ok(model));


    }

    public async Task<Result<IEnumerable<PlansViewModel>>> GetPlansAsync()
    {
        var members = await _unitOfWork.GetRepository<Plan>().GetAllAsync();

        return Result<IEnumerable<PlansViewModel>>.Ok(mapper.Map<IEnumerable<PlansViewModel>>(members));
    }

    public async Task<Result<PlansToUpdateViewModel?>> GetPlanToUpdateAsync(int? Id)
    {
        if (Id == null || Id.Value == 0)
            return Result<PlansToUpdateViewModel?>.Fail("Id not found");

        var plan = await _unitOfWork.GetRepository<Plan>().GetByIdAsync(Id.Value);

        if (plan == null)
            return Result<PlansToUpdateViewModel?>.Fail("Plan not found");

        var model = mapper.Map<PlansToUpdateViewModel>(plan);
        return Result<PlansToUpdateViewModel?>.Ok(model);
    }
    public async Task<Result> UpdatePlanAsync(PlansToUpdateViewModel model)
    {
        try
        {
            var plan = await _unitOfWork.GetRepository<Plan>().GetByIdAsync(model.Id);
            if (plan == null)
                return Result.Fail("Plan not found");

            var mappedPlan = mapper.Map(model, plan);

            

            await _unitOfWork.GetRepository<Plan>().UpdateAsync(mappedPlan);

            var result= _unitOfWork.GetRepository<Plan>().UpdateAsync(plan);

            var results = await _unitOfWork.SaveChangesAsync() > 0;
            if (results)
            {
                return Result.Ok();
            }
            else
            {
                return Result.Fail("Failed to update plan");
            }
        }
        catch (Exception ex)
        {
            return Result.Fail("Internal server error");
        }
    }

    public async Task<Result> StatusOfPlanAsync(int? Id)
    {
        try
        {
            var plan = await _unitOfWork.GetRepository<Plan>().GetByIdAsync(Id.Value);
            if (plan == null)
                return Result.Fail("Plan not found");
            plan.IsActive = !plan.IsActive;
            await _unitOfWork.GetRepository<Plan>().UpdateAsync(plan);
            var results = await _unitOfWork.SaveChangesAsync() > 0;
            if (results)
            {
                return Result.Ok();
            }
            else
            {
                return Result.Fail("Failed to update plan status");
            }
        }
        catch (Exception ex)
        {
            return Result.Fail("Internal server error");
        }
    }
}
