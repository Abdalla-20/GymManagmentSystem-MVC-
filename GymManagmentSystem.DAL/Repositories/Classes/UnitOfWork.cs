using GymManagmentSystem.DAL.Data.Contexts;
using GymManagmentSystem.DAL.Models;
using GymManagmentSystem.DAL.Repositories.Interfaces;

namespace GymManagmentSystem.DAL.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymDbContext _dbcontext;
        public IMemberRepository memberRepository { get;  }

        public ISessionRepository sessionRepository { get; }

        public ITrainerRepository trainerRepository { get; }

        public UnitOfWork(GymDbContext dbcontext, IMemberRepository memberRepository, ISessionRepository sessionRepository, ITrainerRepository trainerRepository)
        {
            _dbcontext = dbcontext;
            this.memberRepository = memberRepository;
            this.sessionRepository = sessionRepository;
            this.trainerRepository = trainerRepository;
        }
        private readonly Dictionary<string,object> Repos = [];

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
           var type = typeof(TEntity).Name;
            if (string.IsNullOrEmpty(type)&& Repos.TryGetValue(type, out object value))
            {
                return (IRepository<TEntity>)Repos[type];
            }
            var repo = new Repository<TEntity>(_dbcontext);

            Repos[type] = repo;
            return repo;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbcontext.SaveChangesAsync();
        }
    }
}
