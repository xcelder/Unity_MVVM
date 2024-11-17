using UnityEngine;
using Zenject;

public class SuscriptionInstaller : MonoInstaller
{
    [SerializeField] SubscriptionViewModel subscriptionViewModel;

    public override void InstallBindings()
    {
        Container.Bind<SubscriptionNetDataSource>().AsSingle().NonLazy();
        Container.Bind<PurchasedPlayerSubscriptionMapper>().AsTransient();
        Container.Bind<Subscriptions>().To<SubscriptionRepository>().AsSingle();
        Container.Bind<BuySubscription>().AsSingle();
        Container.Bind<ClaimRewards>().AsSingle();
        Container.Bind<GetPlayerSubscription>().AsSingle();
        Container.Bind<SubscriptionUiModelMapper>().AsTransient();
        Container.Bind<SubscriptionViewModel>().FromInstance(subscriptionViewModel);

        Container.Inject(subscriptionViewModel);
    }
}