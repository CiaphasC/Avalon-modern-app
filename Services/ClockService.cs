using System;
using System.Timers;

namespace AvaloniaModernApp.Services;

public interface IClockService
{
    event EventHandler<DateTime> Tick;
    void Start();
    void Stop();
}

public class ClockService : IClockService
{
    private readonly Timer _timer;

    public event EventHandler<DateTime>? Tick;

    public ClockService()
    {
        _timer = new Timer(1000);
        _timer.Elapsed += (s, e) => Tick?.Invoke(this, DateTime.Now);
    }

    public void Start()
    {
        _timer.Start();
        // Invoke immediately
        Tick?.Invoke(this, DateTime.Now);
    }

    public void Stop() => _timer.Stop();
}
