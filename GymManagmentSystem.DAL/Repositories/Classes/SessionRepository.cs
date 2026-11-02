using GymManagmentSystem.DAL.Data.Contexts;
using GymManagmentSystem.DAL.Models;
using GymManagmentSystem.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagmentSystem.DAL.Repositories.Classes
{
    public class SessionRepository : Repository<Session>, ISessionRepository
    {
        public SessionRepository(GymDbContext dbContext) : base(dbContext)
        {
            
        }

        public async Task<IEnumerable<Session>> GetAllSessionsWithTrainerAndCategoryAsync()
        {
            return await _dbContext.Sessions.AsNoTracking().Include(x=>x.Trainer).Include(x=>x.Category).ToListAsync();
        }

        public async Task<int> GetCountOfBookedSlotsAsync(int sessionId)
        {
           return await _dbContext.MemberSessions.AsNoTracking().Where(x=>x.SessionId == sessionId).CountAsync();
        }
    }
}
