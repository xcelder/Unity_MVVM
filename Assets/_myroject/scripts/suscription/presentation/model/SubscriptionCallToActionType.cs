
using System;

public interface SubscriptionCallToActionType
{
}

public class SubscriptionCallToActionBuy : SubscriptionCallToActionType
{
}

public class SubscriptionCallToActionClaim : SubscriptionCallToActionType
{
}

public class SubscriptionCallToActionTimer : SubscriptionCallToActionType
{
    public DateTime ExpirationDate { get; }

    public SubscriptionCallToActionTimer(DateTime expirationDate)
    {
        ExpirationDate = expirationDate;
    }
}