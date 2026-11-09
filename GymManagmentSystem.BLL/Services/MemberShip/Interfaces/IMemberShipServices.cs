using GymManagmentSystem.BLL.Common;
using GymManagmentSystem.BLL.Services.MemberShip.ViewModels;

namespace GymManagmentSystem.BLL.Services.MemberShip.Interfaces
{
    public interface IMemberShipServices
    {
        Task<IEnumerable<MemberShipViewModel>> GetAllMembersShipAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<PlanSelectListViewModel>> GetAllPlansForSelectListAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<MemberSelectListViewModel>> GetAllMembersForSelectListAsync(CancellationToken cancellationToken = default);
        Task<Result> CreateMemberShipAsync(CreatMemberShipViewModel model, CancellationToken cancellationToken = default);
        Task<Result> DeleteActiveMemberShipAsync(int memberId, CancellationToken cancellationToken = default);
    }
}
