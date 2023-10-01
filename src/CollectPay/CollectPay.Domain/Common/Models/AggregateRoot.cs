namespace CollectPay.Domain.Common.Models;

public class AggregateRoot : Entity
{
    public AggregateRoot(Guid id)
        : base(id)
    {
    }

    protected readonly List<IDomainEvent> _domainEvents = new();

    public List<IDomainEvent> PopDomainEvents()
    {
        var copy = _domainEvents.ToList();
        _domainEvents.Clear();

        return copy;
    }

    protected AggregateRoot() { }
}