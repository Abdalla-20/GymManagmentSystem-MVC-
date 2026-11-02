using GymManagmentSystem.DAL.Data.Contexts;
using GymManagmentSystem.DAL.Models;
using GymManagmentSystem.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagmentSystem.DAL.Repositories.Classes
{
    public class MemberRepository : Repository<Member>, IMemberRepository
    {
        public MemberRepository(GymDbContext dbContext) : base(dbContext)
        {
            
        }
        public IQueryable<Member> GetMembersWithRelatedData(int id)
        {
            return _dbContext.Members.Where(x => x.Id == id).Include(x => x.MemberShips).ThenInclude(x => x.Plan);
        }
    }
}
