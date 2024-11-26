using System;
using LanguageExt;
using Cysharp.Threading.Tasks;
using R3;

public interface Subscriptions
{
    public Either<SubscriptionError, PurchasedPlayerSubscription> CurrentPlayerSubscription();

    public UniTask<Either<SubscriptionError, Boolean>> BuySubscription();

    public ReadOnlyReactiveProperty<PurchasedPlayerSubscription> FetchPlayerSubscription();

    public void UpdateSubscription(PurchasedPlayerSubscription updatedPlayerSubscription);
}