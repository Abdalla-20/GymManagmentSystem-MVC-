using GymManagmentSystem.BLL.Common;
using GymManagmentSystem.BLL.Services.Sessions.ViewModels;

namespace GymManagmentSystem.BLL.Services.Sessions.Interfaces
{
    public interface ISessionService
    {
        Task<Result<IEnumerable<SessionViewModel>?>> GetAllSessionsAsync();

        Task<Result> CreateSessionAsync(CreateSessionViewModel model);

        Task<Result<IEnumerable<TrainerSelectedViewModel>?>> GetTrainersForDropdownAsync();
        Task<Result<IEnumerable<CategorySelectedViewModel>?>> GetCategoriesForDropdownAsync();
    }
}
