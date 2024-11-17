using System;
using Cysharp.Threading.Tasks;
using LanguageExt;
using UnityEngine;

public class BuySubscription
{
    readonly Subscriptions _subscriptions;

    public BuySubscription(Subscriptions dataSource)
    {
        _subscriptions = dataSource;
    }

    public async UniTask<Either<SubscriptionError, Boolean>> Invoke()
    {
        Debug.Log("Buy subscription use case");
        return await _subscriptions.BuySubscription();
    }
}
