using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Region> Regions { get; }
        IGenericRepository<Country> Countries { get; }

        Task<int> CompleteAsync();

    }
}
