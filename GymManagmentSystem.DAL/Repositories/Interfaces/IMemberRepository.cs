using GymManagmentSystem.DAL.Models;

namespace GymManagmentSystem.DAL.Repositories.Interfaces
{
    public interface IMemberRepository : IRepository<Member>
    {
        public IQueryable<Member> GetMembersWithRelatedData(int id);
    }
}
