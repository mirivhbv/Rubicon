namespace Rubicon.Data;

public class Repository<T> : RepositoryWithTypedId<T, Guid>, IRepository<T>
    where T : class, IEntityWithTypedId<Guid>
{
    public Repository(AppDbContext context) : base(context)
    {
    }
}