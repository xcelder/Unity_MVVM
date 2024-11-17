using LanguageExt;

public class ClaimRewards
{
    readonly Subscriptions _subscriptions;

    public ClaimRewards(Subscriptions dataSource)
    {
        _subscriptions = dataSource;
    }

    public Either<SubscriptionError, PurchasedPlayerSubscription> Invoke() => _subscriptions.ClaimRewards();
}