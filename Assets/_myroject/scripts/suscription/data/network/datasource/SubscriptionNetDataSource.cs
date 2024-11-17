using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt;
using static LanguageExt.Prelude;

public class SubscriptionNetDataSource
{
    PlayerSubscriptionNet _playerSubscription;

    public async Task<Either<SubscriptionError, PlayerSubscriptionNet>> GetPlayerSuscription()
    {
        await Task.Delay(TimeSpan.FromSeconds(2));

        return _playerSubscription is not null
            ? Right(_playerSubscription)
            : Left(new SubscriptionNotFoundError() as SubscriptionError);
    }

    public async Task<Either<SubscriptionError, SubscriptionNet>> GetSuscriptionById(string id)
    {
        await Task.Delay(TimeSpan.FromSeconds(1));
        var rewardReference = new RewardReference { Id = "01" };
        List<RewardReference> rewardReferences = new() { rewardReference };
        var subscription = new SubscriptionNet
        {
            Id = "123",
            DurationDays = 15,
            StoreItemId = "01",
            DailyRewards = new Dictionary<int, List<RewardReference>> { { 1, rewardReferences } }
        };

        return subscription;
    }

    public async Task<Either<SubscriptionError, bool>> BuySubscription()
    {
        await Task.Delay(TimeSpan.FromSeconds(2));

        _playerSubscription = new PlayerSubscriptionNet
        {
            Id = "123",
            PurchaseDate = DateTime.UtcNow.AddDays(-3),
            LastClaimedDate = DateTime.UtcNow.AddDays(-1),
            DaysClaimed = new List<int> { 2 }
        };

        return _playerSubscription is not null
            ? true
            : new PurchaseNotCompletedError();
    }
}