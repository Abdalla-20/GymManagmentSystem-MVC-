using GymManagmentSystem.DAL.Models;

namespace GymManagmentSystem.DAL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        public IMemberRepository memberRepository { get; }
        public ISessionRepository sessionRepository { get;  }
        public ITrainerRepository trainerRepository { get; }
        IRepository<TEntity> GetRepository<TEntity>() where TEntity :BaseEntity , new();
        
        Task<int> SaveChangesAsync();



    }
}
