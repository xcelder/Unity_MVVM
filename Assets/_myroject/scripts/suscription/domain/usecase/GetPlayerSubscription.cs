using Cysharp.Threading.Tasks;
using LanguageExt;

public class GetPlayerSubscription
{
    readonly Subscriptions _subscriptions;

    public GetPlayerSubscription(Subscriptions dataSource)
    {
        _subscriptions = dataSource;
    }

    public async UniTask<Either<SubscriptionError, PurchasedPlayerSubscription>> Invoke() =>
        await _subscriptions.GetPlayerSubscription();
}