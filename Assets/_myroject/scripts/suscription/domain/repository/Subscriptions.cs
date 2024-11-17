using System;
using LanguageExt;
using Cysharp.Threading.Tasks;

public interface Subscriptions
{
    public UniTask<Either<SubscriptionError, Boolean>> BuySubscription();

    public UniTask<Either<SubscriptionError, PurchasedPlayerSubscription>> GetPlayerSubscription();

    public Either<SubscriptionError, PurchasedPlayerSubscription> ClaimRewards();
}