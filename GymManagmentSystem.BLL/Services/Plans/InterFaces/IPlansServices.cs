using GymManagmentSystem.BLL.Common;
using GymManagmentSystem.BLL.Services.Members.ViewModels;
using GymManagmentSystem.BLL.Services.Plans.ViewModels;

namespace GymManagmentSystem.BLL.Services.Plans.InterFaces
{
    public interface IPlansServices
    {
        public Task<Result<PlansToUpdateViewModel?>> GetPlanToUpdateAsync(int? Id);
        public Task<Result> UpdatePlanAsync(PlansToUpdateViewModel model);
        public Task<Result<PlansDetailesViewModel?>> GetPlanAsync(int? Id);
        public Task<Result<IEnumerable<PlansViewModel>>> GetPlansAsync();
        public Task<Result> StatusOfPlanAsync(int? Id);
    }
}

