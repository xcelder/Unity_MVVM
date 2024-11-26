using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class BuySubscriptionCmd : MonoBehaviour
{
    [SerializeField] 
    readonly SubscriptionViewModel SubscriptionViewModel;

    [Inject]
    readonly BuySubscription _buySubscription;

    public void OnBuySubscriptionClicked()
    {
        PerformBuySubscription().Forget();
    }

    async UniTask PerformBuySubscription()
    {
        Debug.Log("UniTask buy action running");
        var result = await _buySubscription.Invoke();
        result.IfLeft(_ => SubscriptionViewModel.SetBuyStatus());
    }
}