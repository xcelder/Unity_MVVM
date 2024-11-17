using R3;
using UnityEngine;

public class SubscriptionClaimButton : MonoBehaviour
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

    // Update is called once per frame
    void UpdateState(SubscriptionUiModel uiModel)
    {
        gameObject.SetActive(uiModel.CallToActionType is SubscriptionCallToActionClaim);
    }
}
