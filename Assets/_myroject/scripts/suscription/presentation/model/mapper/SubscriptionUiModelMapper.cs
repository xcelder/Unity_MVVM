
using System;

public class SubscriptionUiModelMapper
{
    public SubscriptionUiModel Map(PurchasedPlayerSubscription playerSubscription)
    {
        SubscriptionCallToActionType callToActionType;

        if (playerSubscription.IsActive(DateTime.Now) && playerSubscription.CanClaimToday(DateTime.Now))
        {
            callToActionType = new SubscriptionCallToActionClaim();
        }
        else if (playerSubscription.IsActive(DateTime.Now))
        {
            var tomorrow = DateTime.Now.AddDays(1);
            var nextClaimableTime = new DateTime(
                year: tomorrow.Year,
                month: tomorrow.Month,
                day: tomorrow.Day,
                hour: 0,
                minute: 0,
                second: 0
            );
            callToActionType = new SubscriptionCallToActionTimer(nextClaimableTime);
        }
        else
        {
            callToActionType = new SubscriptionCallToActionBuy();
        }

        return new(callToActionType);
    }
}