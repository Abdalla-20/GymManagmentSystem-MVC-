using AutoMapper;
using AutoMapper.Execution;
using GymManagmentSystem.BLL.Common;
using GymManagmentSystem.BLL.Services.Members.ViewModels;
using GymManagmentSystem.BLL.Services.Trainers.Interfaces;
using GymManagmentSystem.BLL.Services.Trainers.ViewModels;
using GymManagmentSystem.DAL.Models;
using GymManagmentSystem.DAL.Repositories.Interfaces;

namespace GymManagmentSystem.BLL.Services.Trainers.classes;

public class TrainerServices : ITrainerServices
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper mapper;

    public TrainerServices (IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<Result<IEnumerable<TrainerViewModel>>> GetTrainersAsync()
    {
        var trainers = await _unitOfWork.GetRepository<GymManagmentSystem.DAL.Models.Trainer>().GetAllAsync();

        return Result<IEnumerable<TrainerViewModel>>.Ok(mapper.Map<IEnumerable<TrainerViewModel>>(trainers));
    }
    public async Task<Result> CreateTrainerAsync(CreateTrainerViewModel model)
    {
        try
        {
            var trainer = await _unitOfWork.GetRepository<Trainer>().GetAllAsync();

            bool IsNotUniqePhone = trainer.Any(x => x.Phone == model.Phone);
            bool IsNotUniqeEmail = trainer.Any(x => x.Email == model.Email);

            if (IsNotUniqePhone)
                return Result.Fail("Phone already exists");

            if (IsNotUniqeEmail)
                return Result.Fail("Email already exists");

            var trainers = mapper.Map<Trainer>(model);

            await _unitOfWork.GetRepository<Trainer>().AddAsync(trainers);

            var result = await _unitOfWork.SaveChangesAsync() > 0;

            if (result)
            {
                return Result.Ok();
            }
            return Result.Fail("Failed to add trainer");
        }
        catch
        {
            return Result.Fail("Internal server error");
        }
    }

    public async Task<Result> DeleteTrainerAsync(int? Id)
    {
        try
        {
            if (Id == null || Id.Value == 0)
                return Result.Fail("Id not found");

            var trainer = await _unitOfWork.GetRepository<Trainer>().GetByIdAsync(Id.Value);

            if (trainer == null)
                return Result.Fail("Trainer not found");

            await _unitOfWork.GetRepository<Trainer>().DeleteAsync(trainer);

            var result = await _unitOfWork.SaveChangesAsync() > 0;

            if (result)
            {
                return Result.Ok();
            }
            else
            {
                return Result.Fail("Failed to delete trainer");
            }
        }
        catch
        {
            return Result.Fail("Internal server error");
        }
    }

    public async Task<Result<TrainerDetailsViewModel?>> GetTrainerAsync(int? Id)
    {
        if (Id == null || Id.Value == 0)
            return Result<TrainerDetailsViewModel?>.Fail("Id not found");

        var trainer = await _unitOfWork.GetRepository<Trainer>().GetByIdAsync(Id.Value);

        if (trainer == null)
            return Result<TrainerDetailsViewModel?>.Fail("Trainer not found");

        var model = mapper.Map<TrainerDetailsViewModel>(trainer);
             return Result<TrainerDetailsViewModel?>.Ok(model);
    }

    public async Task<Result<TrainerToUpdateViewModel?>> GetTrainerToUpdateAsync(int? Id)
    {
        if (Id == null || Id.Value == 0)
            return Result<TrainerToUpdateViewModel?>.Fail("Id not found");

        var trainer = await _unitOfWork.GetRepository<Trainer>().GetByIdAsync(Id.Value);

        if (trainer == null)
            return Result<TrainerToUpdateViewModel?>.Fail("Trainer not found");

        var model = mapper.Map<TrainerToUpdateViewModel>(trainer);
        return Result<TrainerToUpdateViewModel?>.Ok(model);
    }

    public async Task<Result> UpdateTrainerAsync(TrainerToUpdateViewModel model)
    {

        try
        {
            if (model == null)
                return Result.Fail("Model not found");

            var trainers = await _unitOfWork.GetRepository<Trainer>().GetAllAsync();
            bool IsNotUniqePhone = trainers.Any(x => x.Phone == model.Phone && x.Id != model.Id);
            bool IsNotUniqeEmail = trainers.Any(x => x.Email == model.Email && x.Id != model.Id);

            if (IsNotUniqePhone)
                return Result.Fail("Phone number is already exist");

            if (IsNotUniqeEmail)
                return Result.Fail("Email is already exist");


            var trainer = await _unitOfWork.GetRepository<Trainer>().GetByIdAsync(model.Id);
            if (trainer == null)
                return Result.Fail("Trainer not found");

            var mappedTrainer = mapper.Map(model, trainer);

            //await _unitOfWork.GetRepository<Trainer>().UpdateAsync(mappedTrainer);

            var result = await _unitOfWork.SaveChangesAsync() > 0;
            if (result)
            {
                return Result.Ok();
            }
            else
            {
                return Result.Fail("Failed to update trainer");
            }
        }
        catch
        {
            return Result.Fail("Internal server error");

        }
    }
}
