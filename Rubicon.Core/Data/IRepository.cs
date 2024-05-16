namespace Rubicon.Core.Data;

public interface IRepository<T> : IRepositoryWithTypedId<T, Guid> where T : IEntityWithTypedId<Guid>
{
}