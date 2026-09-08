using UnityEngine;
using System.Collections;

public class ButtonPunchEffect : MonoBehaviour
{
    [Header("Toggles")]
    public bool useScale = true;
    public bool useMove = true;

    [Header("Scale Settings")]
    public float scaleMultiplier = 1.5f;

    [Header("Move Settings")]
    public float moveLeftAmount = 20f;

    [Header("Timing")]
    public float duration = 0.2f;

    private RectTransform rectTransform;
    private Vector3 originalScale;
    private Vector2 originalPosition;
    private Coroutine currentRoutine;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
        originalPosition = rectTransform.anchoredPosition;
    }

    // Call on PointerEnter
    public void PlayEffect()
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        Vector3 targetScale = useScale ? originalScale * scaleMultiplier : rectTransform.localScale;
        Vector2 targetPosition = useMove ? originalPosition + new Vector2(-moveLeftAmount, 0f) : rectTransform.anchoredPosition;

        currentRoutine = StartCoroutine(AnimateTo(targetScale, targetPosition));
    }

      public void PlayLoopEffect()
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        Vector3 targetScale = useScale ? originalScale * scaleMultiplier : rectTransform.localScale;
        Vector2 targetPosition = useMove ? originalPosition + new Vector2(-moveLeftAmount, 0f) : rectTransform.anchoredPosition;

        currentRoutine = StartCoroutine(LoopAnimate_To(targetScale, targetPosition));
    }

    // Call on PointerExit
    public void ResetEffect()
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        Vector3 targetScale = useScale ? originalScale : rectTransform.localScale;
        Vector2 targetPosition = useMove ? originalPosition : rectTransform.anchoredPosition;

        currentRoutine = StartCoroutine(AnimateTo(targetScale, targetPosition));
    }

    IEnumerator AnimateTo(Vector3 targetScale, Vector2 targetPosition)
    {
        Vector3 startScale = rectTransform.localScale;
        Vector2 startPosition = rectTransform.anchoredPosition;

        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float eased = Mathf.SmoothStep(0f, 1f, t / duration);

            if (useScale)
                rectTransform.localScale = Vector3.Lerp(startScale, targetScale, eased);

            if (useMove)
                rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, eased);

            yield return null;
        }

        if (useScale)
            rectTransform.localScale = targetScale;

        if (useMove)
            rectTransform.anchoredPosition = targetPosition;
    }


     IEnumerator LoopAnimate_To(Vector3 targetScale, Vector2 targetPosition)
    {
        Vector3 startScale = rectTransform.localScale;
        Vector2 startPosition = rectTransform.anchoredPosition;

        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float eased = Mathf.SmoothStep(0f, 1f, t / duration);

            if (useScale)
                rectTransform.localScale = Vector3.Lerp(startScale, targetScale, eased);

            if (useMove)
                rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, eased);

            yield return null;
        }
        if (useScale)
            rectTransform.localScale = targetScale;

        if (useMove)
            rectTransform.anchoredPosition = targetPosition;

        ResetEffect();
    }

}
