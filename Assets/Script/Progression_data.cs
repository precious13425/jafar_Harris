using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DebtData", menuName = "Game/LevelProgress Data")]
public class Progression_data:ScriptableObject
{
    [Header("Debt")]
    public int MaxLevel = 100;

    [Header("Pay-down cost (exp per step)")]
    public int payCostBase = 25;
    public int payCostPerStep = 25;

    [Header("Player effects")]
    [Range(0f, 0.2f)] public float maxHealthGain = 0.04f;
    

   
    // ---- Runtime state (not serialized) ----
    int level;

    /// <summary>Fires with the new debt level. Hook the mini-sigil / HUD here.</summary>
    public event Action<int> OnLevelChanged;

    


  
    // ---- State ----
    public int Level
    {
        get { 
            
            level=Mathf.Clamp(level,0,MaxLevel);
             return level; }
    }

    /// <summary>Exp cost to pay down one step at the current debt level.</summary>
    public int PayCost => payCostBase + payCostPerStep * Level;

    public bool CanPay(int exp) => Level > 0 && exp >= PayCost;

    // ---- Actions ----

    /// <summary>The power feeds: debt +1 (capped). Call once when a run ends.</summary>
    public void LevelUp()
    {
    
        if (level < MaxLevel)
        {
            level++;
            level=Mathf.Clamp(level,0,MaxLevel);
            OnLevelChanged?.Invoke(level);
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
        OnLevelChanged?.Invoke(level);
        return true;
    }

    // ---- Run modifiers (derived from the debt level, nothing extra to save) ----

  
   /// <summary>Roll once per spawn. Only call this in Escalate/Climax room states.</summary>
    public bool RollElite(float trychance=7) => UnityEngine.Random.value < trychance;

    internal void Setup(int d)
    {
       level=d;
       OnLevelChanged=null;
    }

}

