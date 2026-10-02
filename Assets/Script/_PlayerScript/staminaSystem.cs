using System.Collections;
using UnityEngine;

public class staminaSystem : MonoBehaviour
{
    
    Pooltype StaminaPool;
    [SerializeField] float staimina_add=1,staminawaittime=1,staminaaddrate;

    public System.Action OnvalChange;
    internal float getStamina=>StaminaPool.getvalue;
    public HealthBarUI animatedbar;

    private void Start() {
        if (animatedbar)
        {
            OnvalChange +=() =>
            {
               animatedbar.SetAnimate_Value(StaminaPool.GetnormalizedValue); 
               animatedbar.SetAlpha(StaminaPool.GetnormalizedValue);
               animatedbar.SetText($"{(int)StaminaPool.getvalue}/{StaminaPool.get_MaxValue}");
            };
        }
    }
    public void Setup(float amt)
    {
        StaminaPool=new Pooltype(amt);
        OnvalChange?.Invoke();
    }

    public bool Tryadd_toFull(float amt)
    {
        StaminaPool.AddValue(amt);
        OnvalChange?.Invoke();
        return StaminaPool.isfull;
    }

    public bool TryUSeStamina(float amt)
    {

        if (StaminaPool.Try_RemoveValue(amt))
        {
            OnvalChange?.Invoke();
            StopAllCoroutines();
            StartCoroutine(Refill());
            return true;
        }
        return false;
    }

    private IEnumerator Refill()
    {
        yield return new WaitForSeconds(staminawaittime);

        while (!StaminaPool.isfull)
        {
            Tryadd_toFull(staimina_add*staminaaddrate*Time.deltaTime);
            yield return null;
        }
    }

}