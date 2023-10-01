using CollectPay.Domain.Common.Models;

namespace CollectPay.Domain.BuddyAggregate;

public class Buddy : AggregateRoot
{
    public string NickName { get; set; }
}