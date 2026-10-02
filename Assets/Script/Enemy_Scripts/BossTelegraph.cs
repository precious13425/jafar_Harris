using UnityEngine;

/// Procedural windup tells for a boss with no animations:
/// emission ramp, squash, light pulse, floor indicator, and a windup sound.
/// Add next to BossAttackSystem and assign the fields.
public class BossTelegraph : MonoBehaviour
{
    [SerializeField] BossAttackSystem attacks;
    [SerializeField] Renderer rend;                 // skull mesh (material needs emission enabled)
    [SerializeField] Transform visual;              // what to squash (usually the skull mesh)
    [SerializeField] Light pulseLight;              // optional
    [SerializeField] AudioSource audioSource;       // optional

    [Header("Look")]
    [SerializeField] Color dim = Color.black;
    [SerializeField] Color bright = new Color(1f, 0.35f, 0.1f) * 3f;
    [SerializeField, Range(0.7f, 1f)] float squashY = 0.9f;
    [SerializeField] float lightMax = 4f;

    Material mat;
    Vector3 baseScale;

    void Awake()
    {
       if(rend){
        mat = rend.material;
        mat.EnableKeyword("_EMISSION");
        }
        if (visual == null) visual = transform;
        baseScale = visual.localScale;
        ResetVisuals();
    }

    void OnEnable()
    {
        attacks.WindupStarted += OnStart;
        attacks.WindupProgress += OnProgress;
        attacks.AttackFired += OnEnd;
        attacks.AttackCancelled += OnEnd;
    }

    void OnDisable()
    {
        attacks.WindupStarted -= OnStart;
        attacks.WindupProgress -= OnProgress;
        attacks.AttackFired -= OnEnd;
        attacks.AttackCancelled -= OnEnd;
    }

    void OnStart(AttackDef def)
    {
        if (def.indicator) def.indicator.gameObject.SetActive(true);
        if (audioSource && def.windupSound) audioSource.PlayOneShot(def.windupSound);
    }

    void OnProgress(AttackDef def, float k)
    {
        if(mat)
        mat.SetColor("_EmissionColor", Color.Lerp(dim, bright, k));

        // Squash down and widen slightly
        float xz = Mathf.Lerp(1f, 1f + (1f - squashY) * 0.5f, k);
        visual.localScale = new Vector3(baseScale.x * xz, baseScale.y * Mathf.Lerp(1f, squashY, k), baseScale.z * xz);

        if (pulseLight) pulseLight.intensity = Mathf.Lerp(0f, lightMax, k);
        if (def.indicator) def.indicator.localScale = new Vector3(def.indicatorSize * k, 1f, def.indicatorSize * k);
    }

    void OnEnd(AttackDef def)
    {
        if (def.indicator) def.indicator.gameObject.SetActive(false);
        ResetVisuals();
    }

    void ResetVisuals()
    {
        if(mat)
        mat.SetColor("_EmissionColor", dim);
        visual.localScale = baseScale;
        if (pulseLight) pulseLight.intensity = 0f;
    }
}
