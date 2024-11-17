using System;
using Cysharp.Threading.Tasks;


public class CountDown
{
    readonly TimeSpan _refreshInterval = TimeSpan.FromSeconds(1);

    readonly DateTime _startTime;
    readonly DateTime _endTime;
    readonly Action<TimeSpan> _onTick;

    bool _isRunning;
    TimeSpan _currenTime;

    public CountDown(
        DateTime startTime,
        DateTime endTime,
        Action<TimeSpan> onTick = null
    )
    {
        _startTime = startTime;
        _endTime = endTime;
        _onTick = onTick;
        ResetTimer();
    }

    public void Start()
    {
        if (!_isRunning)
        {
            _isRunning = true;
            Run().Forget();
        }
    }

    public void Stop()
    {
        _isRunning = false;
        ResetTimer();
    }

    private void ResetTimer()
    {
        _currenTime = (_endTime - _startTime).Duration();
    }

    private async UniTask Run()
    {
        while (_isRunning || _currenTime.Seconds > 0)
        {
            await UniTask.Delay(_refreshInterval);
            _currenTime -= _refreshInterval;
            _currenTime = TimeSpan.FromSeconds(Math.Max(_currenTime.Seconds, 0));
            _onTick?.Invoke(_currenTime);
        }
    }
}