using System;
using UnityEngine;

/// <summary>
/// Simple lerp-based bar animator. Drives itself via StaticUpdater —
/// just read CurrentValue each frame (or after IsComplete) and apply
/// it to whatever you're displaying (Slider.value, Image.fillAmount, etc).
///
/// Usage:
///     var bar = new BarAnimator();
///     bar.CurrentValue = healthImage.fillAmount;
///     bar.AnimateTo(0.4f, 0.5f);
///
///     // elsewhere, whenever you want to reflect it:
///     healthImage.fillAmount = bar.CurrentValue;
/// </summary>
public class BarAnimator
{
    public float CurrentValue;
    public float TargetValue;
    public float Duration;
    public bool UseUnscaledTime;
    public bool IsComplete = true;

    public Action OnComplete;

    private float _startValue;
    private float _elapsed;
    public Action OnValueChanged;

    /// <summary>
    /// Starts animating from CurrentValue to <paramref name="target"/>
    /// over <paramref name="duration"/> seconds.
    /// </summary>
    public void AnimateTo(float target, float duration, bool useUnscaledTime = false, Action onComplete = null)
    {
        // Stop any animation already running before starting a new one.
        Stop();

        _startValue = CurrentValue;
        TargetValue = target;
        Duration = duration;
        UseUnscaledTime = useUnscaledTime;
        OnComplete = onComplete;
        _elapsed = 0f;
        IsComplete = false;

        if (duration <= 0f)
        {
            CurrentValue = target;
            IsComplete = true;
            OnComplete?.Invoke();
            return;
        }

        StaticUpdater.Create(Tick);
    }

    /// <summary>
    /// Stops the animation wherever it currently is.
    /// </summary>
    public void Stop()
    {
        if (IsComplete) return;

        StaticUpdater.Remove(Tick);
        IsComplete = true;
    }

    private void Tick()
    {
        _elapsed += UseUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
        float t = Mathf.Clamp01(_elapsed / Duration);
        CurrentValue = Mathf.Lerp(_startValue, TargetValue, t);
        OnValueChanged?.Invoke();
        if (t >= 1f)
        {
            OnComplete?.Invoke();
            Stop();
        }
    }
}
