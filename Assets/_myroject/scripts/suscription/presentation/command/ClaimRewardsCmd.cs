
using UnityEngine;
using Zenject;

public class ClaimRewardsCmd : MonoBehaviour
{

    [SerializeField] 
    readonly SubscriptionViewModel subscriptionViewModel;

    [Inject]
    readonly ClaimRewards _claimRewards;

    public void OnClaimRewardsClicked()
    {
        _claimRewards.Invoke();
    }
}