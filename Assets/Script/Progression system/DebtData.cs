using System;
using System.Diagnostics.Tracing;
using UnityEngine;

/// <summary>
/// Debt as a ScriptableObject, alongside PlayerData / GameWorldData.
/// - Tuning values are serialized (edit them in the Inspector).
/// - The debt level itself is runtime-only and loaded from the save file,
///   because SO changes made in a build are NOT persisted on their own.
/// - Exp is NOT stored here: you pass your existing exp field in by ref,
///   so this file doesn't depend on how PlayerData is laid out.
/// </summary>
[CreateAssetMenu(fileName = "DebtData", menuName = "Game/Debt Data")]
public class DebtData : ScriptableObject
{
    [Header("Debt")]
    public int maxDebt = 10;

    [Header("Pay-down cost (exp per step)")]
    public int payCostBase = 25;
    public int payCostPerStep = 25;

    [Header("Player effects")]
    [Range(0f, 0.2f)] public float maxHealthLossPerStep = 0.04f;
    [Range(0.1f, 1f)] public float maxHealthFloor = 0.6f;

    [Header("Enemy effects")]
    public float enemyStatGainPerStep = 0.06f; // health + damage
    public float enemyStatCap = 1.6f;
    public float eliteChancePerStep = 0.04f;
    public float eliteChanceCap = 0.4f;
    public float eliteHealthMult = 2f;

    // ---- Runtime state (not serialized) ----
    [NonSerialized] int level;
    [NonSerialized] bool loaded;

    /// <summary>Fires with the new debt level. Hook the mini-sigil / HUD here.</summary>
    public event Action<int> OnDebtChanged;

    

    void EnsureLoaded()
    {
        if (loaded) return;
       // level = Mathf.Clamp(SaveManager.Data.feedDebtLevel, 0, maxDebt);
        loaded = true;
    }

  
    // ---- State ----
    public int Level
    {
        get { EnsureLoaded(); return level; }
    }

    /// <summary>Exp cost to pay down one step at the current debt level.</summary>
    public int PayCost => payCostBase + payCostPerStep * Level;

    public bool CanPay(int exp) => Level > 0 && exp >= PayCost;

    // ---- Actions ----

    /// <summary>The power feeds: debt +1 (capped). Call once when a run ends.</summary>
    public void Feed()
    {
        EnsureLoaded();
        if (level < maxDebt)
        {
            level++;
            OnDebtChanged?.Invoke(level);
        }
      
    }

    /// <summary>
    /// Spend exp to lower debt by one step. Example: debtData.TryPay(ref playerData.exp)
    /// (if exp is a property, copy it to a local, pass that by ref, then assign it back).
    /// </summary>
    public bool TryPay( int exp)
    {
        if (!CanPay(exp)) return false;

        exp -= PayCost;
        level--;
        OnDebtChanged?.Invoke(level);
        return true;
    }

    // ---- Run modifiers (derived from the debt level, nothing extra to save) ----

    /// <summary>Multiply the player's base max health by this at run start.</summary>
    public float MaxHealthMult =>
        Mathf.Max(maxHealthFloor, 1f - maxHealthLossPerStep * Level);

    /// <summary>Multiply enemy health and damage by this at spawn.</summary>
    public float EnemyStatMult =>
        Mathf.Min(enemyStatCap, 1f + enemyStatGainPerStep * Level);

    public float EliteChance =>
        Mathf.Min(eliteChanceCap, eliteChancePerStep * Level);

    /// <summary>Roll once per spawn. Only call this in Escalate/Climax room states.</summary>
    public bool RollElite() => UnityEngine.Random.value < EliteChance;

    internal void Setup()
    {
       level=1;
       OnDebtChanged=null;
    }

}
