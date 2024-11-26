using System;
using System.Collections.Generic;
using LanguageExt;

public class ClaimRewards
{
    readonly Subscriptions _subscriptions;

    public ClaimRewards(Subscriptions dataSource)
    {
        _subscriptions = dataSource;
    }

    public Either<SubscriptionError, bool> Invoke()
    {
        return _subscriptions.CurrentPlayerSubscription().Bind(subscription =>
        {
            var result = PerformClaimRewards(subscription);
            result.IfRight(updatedSubscription => _subscriptions.UpdateSubscription(updatedSubscription));
            return result.Map(_ => true);
        });
    }

    private Either<SubscriptionError, PurchasedPlayerSubscription> PerformClaimRewards(
        PurchasedPlayerSubscription playerSubscription)
    {
        if (playerSubscription is null)
            return new SubscriptionNotFoundError();
        if (!playerSubscription.IsActive(DateTime.Now))
            return new SubscriptionNotActiveError();
        if (!playerSubscription.CanClaimToday(DateTime.Now))
            return new SubscriptionAlreadyClaimedError();

        var claimDate = DateTime.Now;
        var dayToClaim = (claimDate - playerSubscription.PurchaseDate).Days;
        if (dayToClaim > playerSubscription.DurationDays)
        {
            return new SubscriptionCompletelyClaimedError();
        }

        var daysClaimed = playerSubscription.DaysClaimed ?? new List<int>();
        daysClaimed.Add(dayToClaim);

        return new PurchasedPlayerSubscription(
            id: playerSubscription.Id,
            purchaseDate: playerSubscription.PurchaseDate,
            lastClaimedDate: claimDate,
            daysClaimed: daysClaimed,
            durationDays: playerSubscription.DurationDays
        );
    }
}