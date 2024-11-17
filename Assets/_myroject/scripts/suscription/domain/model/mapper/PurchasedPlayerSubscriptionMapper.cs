
public class PurchasedPlayerSubscriptionMapper
{
    public PurchasedPlayerSubscription Map(
        PlayerSubscriptionNet playerSubscriptionNet,
        SubscriptionNet subscriptionNet
    ) => new(
        playerSubscriptionNet.Id,
        playerSubscriptionNet.PurchaseDate,
        playerSubscriptionNet.LastClaimedDate,
        playerSubscriptionNet.DaysClaimed,
        subscriptionNet.DurationDays
    );
}