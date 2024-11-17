
using System;

public class SubscriptionUiModelMapper
{
    public SubscriptionUiModel Map(PurchasedPlayerSubscription playerSubscription)
    {
        SubscriptionCallToActionType callToActionType;

        if (playerSubscription.IsActive(DateTime.Now) && playerSubscription.CanClaimToday(DateTime.Now))
        {
            callToActionType = new SubscriptionCallToActionClaim();
        } else
        {
            callToActionType = new SubscriptionCallToActionTimer(playerSubscription.ExpirationDate);
        }

        return new(callToActionType);
    }
}