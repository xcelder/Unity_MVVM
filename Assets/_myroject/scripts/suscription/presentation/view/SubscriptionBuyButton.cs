using R3;
using UnityEngine;

public class SubscriptionBuyButton : MonoBehaviour
{
    [SerializeField] SubscriptionViewModel viewModel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var disposable = Disposable.CreateBuilder();
        viewModel.State
            .Subscribe(UpdateState)
            .AddTo(ref disposable);
        disposable.RegisterTo(destroyCancellationToken);
    }

    void UpdateState(SubscriptionUiModel uiModel)
    {
        gameObject.SetActive(uiModel.CallToActionType is SubscriptionCallToActionBuy);
    }
}
