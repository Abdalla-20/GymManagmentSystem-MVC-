using GymManagmentSystem.DAL.Models;
using System.Collections;

namespace GymManagmentSystem.DAL.Repositories.Interfaces
{
    public interface IPlanRepository : IRepository<Plan>
    {
        public Task<IEnumerable<Plan>> GetAllwithMemberShipAsync(bool tracking = false);
    
    }
}
