using GymManagmentSystem.DAL.Data.Contexts;
using GymManagmentSystem.DAL.Models;
using GymManagmentSystem.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagmentSystem.DAL.Repositories.Classes
{
    public class PlanRepository : IPlanRepository
    {
        private readonly GymDbContext _dbcontext;

        public PlanRepository(GymDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<int> AddAsync(Plan plan)
        {
            await _dbcontext.Plans.AddAsync(plan);
            return await _dbcontext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Plan plan)
        {
            _dbcontext.Plans.Remove(plan);
            return await _dbcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Plan>> GetAllAsync(bool tracking = false)
        {
            return tracking ? await _dbcontext.Plans.ToListAsync() :await _dbcontext.Plans.AsNoTracking().ToListAsync() ;
        }

        public async Task<Plan?> GetByIdAsync(int id)
        {
            return await _dbcontext.Plans.FindAsync(id);
        }

        public async Task<int> UpdateAsync(Plan plan)
        {
            return await _dbcontext.SaveChangesAsync();
        }
    }
}
