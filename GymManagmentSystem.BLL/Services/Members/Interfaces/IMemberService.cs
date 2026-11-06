using GymManagmentSystem.BLL.Common;
using GymManagmentSystem.BLL.Services.Members.ViewModels;
using GymManagmentSystem.DAL.Models;
namespace GymManagmentSystem.BLL.Services.Members.Interfaces;

public interface IMemberService 
{
    public Task<Result<IEnumerable<MemberViewModel>>> GetMembersAsync();
    public Task<Result> CreateMemberAsync(CreateMemberViewModel model);
    public Task<Result< MemberDetailsViewModel?>> GetMemberAsync(int? Id);
    public Task<Result<HealthRecordViewModel?>> GetHealthRecordDetailsAsync(int? Id);
    public Task<Result<MemberToUpdateViewModel?>> GetMemberToUpdateAsync(int? Id);
    public Task<Result> UpdateMemberAsync(MemberToUpdateViewModel model);
    public Task<Result> DeleteMemberAsync(int? Id);

   
}
