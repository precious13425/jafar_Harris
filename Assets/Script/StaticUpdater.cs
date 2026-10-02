using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lets static classes hook into Unity's Update loop, and schedule delayed
/// one-shot calls, without needing to manually place a MonoBehaviour in
/// the scene.
///
/// Usage:
///     StaticUpdater.Create(MyTickFunction);              // subscribe to Update
///     StaticUpdater.Remove(MyTickFunction);               // unsubscribe
///
///     StaticUpdater.DelayedCall(MyFunction, 2f);          // run MyFunction in 2 seconds
///     StaticUpdater.CancelDelayedCall(MyFunction);        // cancel it before it fires
/// </summary>
public static class StaticUpdater
{
    private static UpdaterRunner _runner;

    // Delayed calls pending, tracked independently of the Update event above.
    private static readonly List<DelayedEntry> _delayedCalls = new List<DelayedEntry>();
    private static readonly List<DelayedEntry> _toFire = new List<DelayedEntry>();

    private class DelayedEntry
    {
        public Action Callback;
        public float FireTime;
    }

    /// <summary>
    /// Ensures the dummy runner exists, then hooks the given action
    /// to run every Update().
    /// </summary>
    public static void Create(Action onUpdate)
    {
        if (onUpdate == null) return;

        EnsureRunner();
        _runner.OnUpdate += onUpdate;
    }

    /// <summary>
    /// Unhooks a previously registered action. Safe to call even if
    /// the runner was never created or the action was never added.
    /// </summary>
    public static void Remove(Action onUpdate)
    {
        if (onUpdate == null || _runner == null) return;

        _runner.OnUpdate -= onUpdate;
    }

    /// <summary>
    /// Runs <paramref name="callback"/> once, after <paramref name="seconds"/>
    /// have elapsed (uses Time.time, so it respects Time.timeScale).
    /// </summary>
    public static void DelayedCall(Action callback, float seconds)
    {
        if (callback == null) return;

        EnsureRunner();
        _delayedCalls.Add(new DelayedEntry
        {
            Callback = callback,
            FireTime = Time.time + seconds
        });
    }

    /// <summary>
    /// Cancels any pending delayed call(s) registered with this exact callback,
    /// as long as they haven't fired yet.
    /// </summary>
    public static void CancelDelayedCall(Action callback)
    {
        if (callback == null) return;

        _delayedCalls.RemoveAll(entry => entry.Callback == callback);
    }

    private static void EnsureRunner()
    {
        if (_runner != null) return;

        var go = new GameObject("[StaticUpdater]");
        go.hideFlags = HideFlags.HideInHierarchy | HideFlags.DontSave;
        UnityEngine.Object.DontDestroyOnLoad(go);
        _runner = go.AddComponent<UpdaterRunner>();
        _runner.OnUpdate += TickDelayedCalls;
    }

    private static void TickDelayedCalls()
    {
        if (_delayedCalls.Count == 0) return;

        float now = Time.time;
        _toFire.Clear();

        for (int i = _delayedCalls.Count - 1; i >= 0; i--)
        {
            if (_delayedCalls[i].FireTime <= now)
            {
                _toFire.Add(_delayedCalls[i]);
                _delayedCalls.RemoveAt(i);
            }
        }

        // Fire after removal so a callback that schedules a new
        // DelayedCall doesn't get mutated mid-iteration.
        for (int i = 0; i < _toFire.Count; i++)
        {
            _toFire[i].Callback?.Invoke();
        }
    }

    // Dummy MonoBehaviour — exists purely to give Unity something to call Update() on.
    private class UpdaterRunner : MonoBehaviour
    {
        public event Action OnUpdate;
        private void Update() => OnUpdate?.Invoke();
    }
}
