using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Example usage of BarAnimator — drives a UI Slider's value toward a
/// target over time whenever SetHealth() is called.
///
/// Requires: StaticUpdater.cs, BarAnimator.cs (same project).
///
/// Setup:
///   1. Attach this to a GameObject with (or referencing) a UI Slider.
///   2. Assign the Slider in the Inspector.
///   3. Call healthBarUI.SetHealth(normalizedValue) whenever health changes,
///      e.g. healthBarUI.SetHealth(currentHp / maxHp);
/// </summary>
public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField]CanvasGroup rootcanvas;
   [SerializeField] TMP_Text nametext;

    private readonly BarAnimator _bar = new BarAnimator();
    [SerializeField]float myduration=3.3f;
    [SerializeField]float endval=1;

    private void Start()
    {
        // Initialize the bar's internal value to match the slider's starting value
        _bar.CurrentValue = healthSlider.value;
        _bar.OnValueChanged = () =>
        {
          healthSlider.value=_bar.CurrentValue;  
        };

        SetAnimate_Value(endval,myduration);
    }

   // private void Update()
  //  {
        // Reflect whatever the bar's animation has computed this frame
    //    healthSlider.value = _bar.CurrentValue;
   // }

    /// <summary>
    /// Animates the health bar to a new normalized value (0-1).
    /// </summary>
    public void SetAnimate_Value(float normalizedHealth, float duration = -0.4f, bool useUnscaledTime = false)
    {
        _bar.AnimateTo(
            normalizedHealth,
            duration<=0?myduration:duration,
            useUnscaledTime,
             () => Debug.Log("Health bar finished animating")
        );
    }

    public void SetText(string dns)
    {
       if(nametext) nametext.text=$"{dns}";
    }

   public void SetAlpha(float normalizedVal)
    {
        if (normalizedVal >= 1)
        {
            rootcanvas.alpha=0;
            return;
        }
        rootcanvas.alpha=1;
    }
}
