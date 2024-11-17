
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SubscriptionViewModel", menuName = "ViewModel/Subscription")]
public class SubscriptionViewModel : ScriptableObject
{
    public readonly ReactiveProperty<SubscriptionUiModel> State = new(SubscriptionUiModel.Default());

    BuySubscription _buySubscription;
    GetPlayerSubscription _getPlayerSubscription;
    ClaimRewards _claimRewards;
    SubscriptionUiModelMapper _mapper;

    [Inject]
    public void InjectDependencies(
        BuySubscription buySubscription,
        GetPlayerSubscription getPlayerSubscription,
        ClaimRewards claimRewards,
        SubscriptionUiModelMapper mapper
    )
    {
        _buySubscription = buySubscription;
        _getPlayerSubscription = getPlayerSubscription;
        _claimRewards = claimRewards;
        _mapper = mapper;
    }

    public void OnBuyClicked()
    {
        PerformBuySubscription().Forget();
    }

    public void OnClaimClicked()
    {
        _claimRewards.Invoke().Match(
            Left: _ => { },
            Right: UpdateSubscriptionState
        );
    }

    async UniTask PerformBuySubscription()
    {
        Debug.Log("UniTask buy action running");
        var result = await _buySubscription.Invoke();
        result.Match(
            Left: _ => HandleError(),
            Right: _ => SetupSuscription().Forget()
        );
    }

    async UniTask SetupSuscription()
    {
        var subscriptionResult = await _getPlayerSubscription.Invoke();
        subscriptionResult.Match(
            Left: _ => HandleError(),
            Right: UpdateSubscriptionState
        );
    }

    void UpdateSubscriptionState(PurchasedPlayerSubscription playerSubscription)
    {
        State.OnNext(_mapper.Map(playerSubscription));
    }

    void HandleError()
    {
        State.OnNext(State.CurrentValue.Clone().AsActionBuy());
    }
}