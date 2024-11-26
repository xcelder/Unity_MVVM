

using System;
using Cysharp.Threading.Tasks;
using LanguageExt;
using R3;
using UnityEngine;

public class SubscriptionRepository : Subscriptions
{
    readonly SubscriptionNetDataSource _dataSource;
    readonly PurchasedPlayerSubscriptionMapper _mapper;

    readonly ReactiveProperty<PurchasedPlayerSubscription> _playerSubscription = new();

    public SubscriptionRepository(
        SubscriptionNetDataSource dataSource,
        PurchasedPlayerSubscriptionMapper mapper
    )
    {
        _dataSource = dataSource;
        _mapper = mapper;
    }


    public Either<SubscriptionError, PurchasedPlayerSubscription> CurrentPlayerSubscription()
    {
        return _playerSubscription.CurrentValue is not null
            ? _playerSubscription.CurrentValue
            : new SubscriptionNotFoundError();
    }

    public async UniTask<Either<SubscriptionError, bool>> BuySubscription()
    {
        Debug.Log("Buy subdscription in repository");
        var result = await _dataSource.BuySubscription();
        FetchPlayerSubscription();
        return result;
    }

    public ReadOnlyReactiveProperty<PurchasedPlayerSubscription> FetchPlayerSubscription()
    {
        FetchPlayerSubscriptionFromNet().Forget();
        return _playerSubscription;
    }

    public void UpdateSubscription(PurchasedPlayerSubscription updatedPlayerSubscription)
    {
        _playerSubscription.OnNext(updatedPlayerSubscription);
    }

    private async UniTask FetchPlayerSubscriptionFromNet()
    {
        var playerSubscriptionResult = await _dataSource.GetPlayerSuscription();
        var result = await playerSubscriptionResult.BindAsync(async playerSubscription =>
        {
            var subscriptionResult = await _dataSource.GetSuscriptionById(playerSubscription.Id);
            return subscriptionResult.Map(subscription => _mapper.Map(playerSubscription, subscription));
        });

        result.IfRight(playerSubscription =>
        {
            Debug.Log("emit new value from fetched");
            _playerSubscription.OnNext(playerSubscription);
        });
    }
}