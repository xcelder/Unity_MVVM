using Cysharp.Threading.Tasks;
using LanguageExt;
using R3;

public class GetPlayerSubscription
{
    readonly Subscriptions _subscriptions;

    public GetPlayerSubscription(Subscriptions dataSource)
    {
        _subscriptions = dataSource;
    }

    public ReadOnlyReactiveProperty<PurchasedPlayerSubscription> Invoke() =>
        _subscriptions.FetchPlayerSubscription();
}