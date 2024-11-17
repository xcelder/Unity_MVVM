

using System;
using Cysharp.Threading.Tasks;
using LanguageExt;
using UnityEngine;

public class SubscriptionRepository : Subscriptions
{
    readonly SubscriptionNetDataSource _dataSource;
    readonly PurchasedPlayerSubscriptionMapper _mapper;

    PurchasedPlayerSubscription _playerSubscription;

    public SubscriptionRepository(
        SubscriptionNetDataSource dataSource,
        PurchasedPlayerSubscriptionMapper mapper
    )
    {
        _dataSource = dataSource;
        _mapper = mapper;
    }

    public async UniTask<Either<SubscriptionError, bool>> BuySubscription()
    {
        Debug.Log("Buy subdscription in repository");
        return await _dataSource.BuySubscription();
    }

    public async UniTask<Either<SubscriptionError, PurchasedPlayerSubscription>> GetPlayerSubscription()
    {
        var playerSubscriptionResult = await _dataSource.GetPlayerSuscription();
        var result = await playerSubscriptionResult.BindAsync(async playerSubscription =>
        {
            var subscriptionResult = await _dataSource.GetSuscriptionById(playerSubscription.Id);
            return subscriptionResult.Map(subscription => _mapper.Map(playerSubscription, subscription));
        });

        result.IfRight(playerSubscription => { _playerSubscription = playerSubscription; });

        return result;
    }

    public Either<SubscriptionError, PurchasedPlayerSubscription> ClaimRewards()
    {
        if (_playerSubscription is null)
            return new SubscriptionNotFoundError();
        if (!_playerSubscription.IsActive(DateTime.Now))
            return new SubscriptionNotActiveError();
        if (!_playerSubscription.CanClaimToday(DateTime.Now))
            return new SubscriptionAlreadyClaimedError();

        _playerSubscription.Claim(DateTime.Now);

        return _playerSubscription;
    }
}