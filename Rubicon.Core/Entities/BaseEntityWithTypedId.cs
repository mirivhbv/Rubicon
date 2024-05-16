namespace Rubicon.Core.Entities;


public abstract class BaseEntityWithTypedId<TId> : IEntityWithTypedId<TId>
{
    public virtual TId Id { get; protected set; } = default!;
}