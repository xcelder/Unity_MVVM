
using R3;
using UnityEngine;

[CreateAssetMenu(fileName = "SubscriptionViewModel", menuName = "ViewModel/Subscription")]
public class SubscriptionViewModel : ScriptableObject
{
    public readonly ReactiveProperty<SubscriptionUiModel> State = new(SubscriptionUiModel.Default());

    public void SetBuyStatus()
    {
        State.OnNext(State.CurrentValue.Clone().AsActionBuy());
    }
}