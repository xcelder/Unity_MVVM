

using System;

public class SubscriptionUiModel
{
    public SubscriptionCallToActionType CallToActionType {get; private set;}

    public SubscriptionUiModel(SubscriptionCallToActionType callToActionType)
    {
        CallToActionType = callToActionType;
    }

    public SubscriptionUiModel AsActionBuy()
    {
        CallToActionType = new SubscriptionCallToActionBuy();
        return this;
    }

    public SubscriptionUiModel Clone()
    {
        return MemberwiseClone() as SubscriptionUiModel;
    }

    public static SubscriptionUiModel Default() => new(new SubscriptionCallToActionBuy());
}