using System;
using R3;
using TMPro;
using UnityEngine;

public class SubscriptionBoxTimer : MonoBehaviour
{
    [SerializeField] TMP_Text timerValue;
    [SerializeField] SubscriptionViewModel viewModel;

    CountDown _countDown;

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

        var timerAction = uiModel.CallToActionType as SubscriptionCallToActionTimer;
        if (timerAction is null) {
            _countDown?.Stop();
        } else
        {
            _countDown = new(
                startTime: DateTime.Now,
                endTime: timerAction.ExpirationDate,
                onTick: UpdateTimer
            );
            _countDown.Start();
        }

        gameObject.SetActive(uiModel.CallToActionType is SubscriptionCallToActionTimer);
    }

    void UpdateTimer(TimeSpan timeLeft)
    {
        timerValue.text = timeLeft.ToString("hh\\:mm\\:ss");
    }
}
