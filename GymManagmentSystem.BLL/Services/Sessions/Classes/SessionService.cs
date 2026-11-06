using AutoMapper;
using GymManagmentSystem.BLL.Common;
using GymManagmentSystem.BLL.Services.Sessions.Interfaces;
using GymManagmentSystem.BLL.Services.Sessions.ViewModels;
using GymManagmentSystem.DAL.Models;
using GymManagmentSystem.DAL.Models.Enums;
using GymManagmentSystem.DAL.Repositories.Interfaces;


namespace GymManagmentSystem.BLL.Services.Sessions.Classes;

public class SessionService : ISessionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<IEnumerable<SessionViewModel>?>> GetAllSessionsAsync()
    {
        var sessions = await _unitOfWork.sessionRepository.GetAllSessionsWithTrainerAndCategoryAsync();

        var MappedSesions = _mapper.Map<IEnumerable<SessionViewModel>>(sessions);

        foreach (var session in MappedSesions)
        {
            session.AvailableSlots =
                session.Capacity - await _unitOfWork.sessionRepository.GetCountOfBookedSlotsAsync(session.Id);
        }

        return Result<IEnumerable<SessionViewModel>?>.Ok(MappedSesions);
    }

    public async Task<Result> CreateSessionAsync(CreateSessionViewModel model)
    {
        try
        {
            if (model.EndDate <= model.StartDate) return Result.Validation("EndDate must be After startDate ");

            if (model.StartDate <= DateTime.Now) return Result.Validation("StartDate must be In the Future ");

            if (model.Capacity < 1 || model.Capacity > 25) return Result.Validation("Capacity must be between 1 and 25");

            var trainer = await _unitOfWork.GetRepository<GymManagmentSystem.DAL.Models.Trainer>().GetByIdAsync(model.TrainerId);
            if (trainer == null) return Result.NotFound("Trainer not found");

            var category = await _unitOfWork.GetRepository<Category>().GetByIdAsync(model.CategoryId);
            if (category == null) return Result.NotFound("Category not found");

            var isValid = Enum.TryParse<Speciality>(category.CategoryName, true, out var categorySpeciality);
            if (!isValid || trainer.Speciality != categorySpeciality)
                return Result.Validation("Can not create session: Trainer speciality does not match the category");

            var session = _mapper.Map<Session>(model);
            await _unitOfWork.sessionRepository.AddAsync(session);
            var result = await _unitOfWork.SaveChangesAsync() > 0;
            return result ? Result.Ok() : Result.Fail("Failed to create session");
        }
        catch (Exception ex)
        {
           return Result.Fail("Internal Server Error");
        }

    }

    public async Task<Result<IEnumerable<TrainerSelectedViewModel>?>> GetTrainersForDropdownAsync()
    {
        var result = await _unitOfWork.GetRepository<GymManagmentSystem.DAL.Models.Trainer>().GetAllAsync(); //
        var model = _mapper.Map<IEnumerable<TrainerSelectedViewModel>>(result);

        return Result<IEnumerable<TrainerSelectedViewModel>?>.Ok(model);
    }

    public async Task<Result<IEnumerable<CategorySelectedViewModel>?>> GetCategoriesForDropdownAsync()
    {
        var result = await _unitOfWork.GetRepository<Category>().GetAllAsync(); //
        var model = _mapper.Map<IEnumerable<CategorySelectedViewModel>>(result);

        return Result<IEnumerable<CategorySelectedViewModel>?>.Ok(model);
    }
}
