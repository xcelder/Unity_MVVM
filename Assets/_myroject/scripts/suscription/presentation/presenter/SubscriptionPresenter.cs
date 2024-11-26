using Cysharp.Threading.Tasks;
using UnityEngine;
using R3;
using Zenject;

public class SubscriptionPresenter : MonoBehaviour
{
    [SerializeField] SubscriptionViewModel subscriptionViewModel;

    [Inject]
    readonly GetPlayerSubscription _getPlayerSubscription;
    [Inject]
    readonly SubscriptionUiModelMapper _mapper;

    [Inject]
    public void InjectDependencies()
    {
        ListenToSubscriptionUpdates();
    }

    void ListenToSubscriptionUpdates()
    {
        Debug.Log("init subscription to repo");
        var disposable = Disposable.CreateBuilder();
        _getPlayerSubscription.Invoke()
            .Subscribe(UpdateState)
            .AddTo(ref disposable);
        disposable.RegisterTo(destroyCancellationToken);
    }

    void UpdateState(PurchasedPlayerSubscription playerSubscription)
    {
        Debug.Log("New value received");
        subscriptionViewModel.State.OnNext(_mapper.Map(playerSubscription));
    }
}