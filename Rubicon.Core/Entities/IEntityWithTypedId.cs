namespace Rubicon.Core.Entities;

public interface IEntityWithTypedId<out TId>
{
    TId Id { get; }
}