using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * SETUP
 *  1. Add BossAttackSystem to the boss and fill the Attacks list in the Inspector.
 *  2. On each attack, drag the boss brain into "On Fire" and pick its method
 *     (DoSwing, DoSlam, DoCharge). No registration code needed.
 *
 * BRAIN
 *   void Update() {
 *       if (atk.IsBusy) return;
 *       var next = atk.Pick(distToPlayer, phase);
 *       if (next != null) atk.Begin(next); else Chase();
 *   }
 *   public void DoSlam()   { ...spawn shockwave... }
 *   public void DoCharge() { lock direction here, then StartCoroutine(ChargeRoutine()); }
 *
 *   // Attacks that take time: set Recovery to roughly the dash length + stun.
 *   // Phase change / stagger:
 *   atk.Cancel();
 *   atk.SetPhaseScale(0.7f, 0.7f);
 */

[Serializable]
public class AttackDef
{
    public string name = "Swing";

    [Header("Timing (seconds)")]
    public float windup = 0.6f;
    public float recovery = 0.5f;
    public float cooldown = 4f;

    [Header("Use conditions")]
    public float minRange = 0f;
    public float maxRange = 99f;
    public int minPhase = 1;

    [Header("Telegraph (optional)")]
    public AudioClip windupSound;
    public Transform indicator;
    public float indicatorSize = 4f;

    [Header("Runs after windup")]
    public UnityEvent onFire;

    [NonSerialized] public float nextReadyTime;
}

public class BossAttackSystem : MonoBehaviour
{
    [SerializeField] List<AttackDef> attacks = new List<AttackDef>();

    public event Action<AttackDef> WindupStarted;
    public event Action<AttackDef, float> WindupProgress;   // 0..1
    public event Action<AttackDef> AttackFired;
    public event Action<AttackDef> AttackEnded;
    public event Action<AttackDef> AttackCancelled;

    public bool IsBusy { get; private set; }

    float windupScale = 1f;
    float cooldownScale = 1f;
    AttackDef current, last;
    Coroutine running;

    public void SetPhaseScale(float windup, float cooldown)
    {
        windupScale = windup;
        cooldownScale = cooldown;
    }

    /// Random ready attack valid for this distance and phase, avoiding an immediate repeat.
    public AttackDef Pick(float distance, int phase)
    {
        var options = attacks.FindAll(a =>
            Time.time >= a.nextReadyTime &&
            phase >= a.minPhase &&
            distance >= a.minRange && distance <= a.maxRange);

        if (options.Count > 1) options.Remove(last);
        return options.Count == 0 ? null : options[UnityEngine.Random.Range(0, options.Count)];
    }

    public void Begin(AttackDef def)
    {
        if (IsBusy || def == null) return;
        running = StartCoroutine(Run(def));
    }

    /// Interrupt (stagger, phase change, death).
    public void Cancel()
    {
        if (!IsBusy) return;
        StopCoroutine(running);
        var def = current;
        Finish(def);
        AttackCancelled?.Invoke(def);
    }

    IEnumerator Run(AttackDef def)
    {
        IsBusy = true;
        current = last = def;

        WindupStarted?.Invoke(def);
        float total = def.windup * windupScale;
        for (float t = 0f; t < total; t += Time.deltaTime)
        {
            WindupProgress?.Invoke(def, t / total);
            yield return null;
        }

        AttackFired?.Invoke(def);
        def.onFire.Invoke();

        yield return new WaitForSeconds(def.recovery);

        Finish(def);
        AttackEnded?.Invoke(def);
    }

    void Finish(AttackDef def)
    {
        def.nextReadyTime = Time.time + def.cooldown * cooldownScale;
        IsBusy = false;
        current = null;
    }
}
