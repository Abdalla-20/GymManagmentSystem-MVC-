using GymManagmentSystem.BLL.Common;
using GymManagmentSystem.BLL.Services.Members.ViewModels;
using GymManagmentSystem.BLL.Services.Trainers.ViewModels;

namespace GymManagmentSystem.BLL.Services.Trainers.Interfaces;

public interface ITrainerServices
{
    public Task<Result<IEnumerable<TrainerViewModel>>> GetTrainersAsync();
    public Task<Result> CreateTrainerAsync(CreateTrainerViewModel model);
    public Task<Result<TrainerDetailsViewModel?>> GetTrainerAsync(int? Id);
    public Task<Result<TrainerToUpdateViewModel?>> GetTrainerToUpdateAsync(int? Id);
    public Task<Result> UpdateTrainerAsync(TrainerToUpdateViewModel model);
    public Task<Result> DeleteTrainerAsync(int? Id);
}
