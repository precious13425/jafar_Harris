using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Plays randomized ambient sounds at unpredictable intervals and positions
/// to build dread/horror atmosphere. Optionally reacts to RoomState pacing
/// (quieter/rarer in Calm, more frequent in Escalate/Climax).
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class AmbientDreadSystem : MonoBehaviour
{
    [Header("Sound Pool")]
    [Tooltip("Pool of ambient sound clips to randomly pick from.")]
    public List<AudioClip> soundPool = new List<AudioClip>();

    [Header("Timing")]
    [Tooltip("Minimum seconds between ambient sounds.")]
    public float minInterval = 8f;
    [Tooltip("Maximum seconds between ambient sounds.")]
    public float maxInterval = 35f;

    [Header("Spatial Settings")]
    [Tooltip("Reference to the player transform, used to position sounds around them.")]
    public bool useplayerpos;
    public Transform player;
    [Tooltip("Minimum distance from player to place the sound source.")]
    public float minRadius = 3f;
    [Tooltip("Maximum distance from player to place the sound source.")]
    public float maxRadius = 8f;
    [Tooltip("Degrees around the player to avoid directly in front (keeps sounds from feeling centered/expected).")]
    public float forwardExclusionAngle = 60f;

    [Header("Volume")]
    public float minVolume = 0.4f;
    public float maxVolume = 0.8f;

    [Header("Room State Pacing (optional)")]
    [Tooltip("If true, interval ranges are scaled per RoomState via SetPacingMultiplier.")]
    public bool useRoomStatePacing = false;
    [Range(0.2f, 3f)]
    public float pacingMultiplier = 1f; // lower = more frequent sounds

    private float nextTriggerTime;
    private AudioClip lastPlayedClip;
    public AudioSource audioSource;

    void Awake()
    {
        // Use a dedicated AudioSource so we can freely reposition it without
        // disturbing other audio on this GameObject.
        //audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f; // full 3D
    }

    void Start()
    {
        ScheduleNextSound();
    }

    void Update()
    {
        if (Time.time >= nextTriggerTime)
        {
            PlayRandomAmbientSound();
            ScheduleNextSound();
        }
    }

    void ScheduleNextSound()
    {
        float interval = Random.Range(minInterval, maxInterval);
        if (useRoomStatePacing)
        {
            interval *= pacingMultiplier;
        }
        nextTriggerTime = Time.time + interval;
    }

    void PlayRandomAmbientSound()
    {
        if (soundPool.Count == 0 || useplayerpos && player == null)
        {
            return;
        }

        AudioClip clip = PickRandomExcluding(lastPlayedClip);
        if (clip == null)
        {
            return;
        }
        lastPlayedClip = clip;


        float volume = Random.Range(minVolume, maxVolume);
        audioSource.PlayOneShot(clip, volume);
    }

    AudioClip PickRandomExcluding(AudioClip exclude)
    {
        if (soundPool.Count == 1)
        {
            return soundPool[0];
        }

        List<AudioClip> candidates = new List<AudioClip>(soundPool);
        if (exclude != null)
        {
            candidates.Remove(exclude);
        }

        if (candidates.Count == 0)
        {
            return soundPool[Random.Range(0, soundPool.Count)];
        }

        return candidates[Random.Range(0, candidates.Count)];
    }

    Vector3 GetOffAxisPositionAroundPlayer()
    {
        // Pick a random angle around the player, biased away from directly
        // in front of them, so sounds feel like they come from the periphery
        // rather than where the player is already looking.
        float excludedHalfAngle = forwardExclusionAngle / 2f;
        float angle;

        do
        {
            angle = Random.Range(0f, 360f);
        }
        while (Mathf.Abs(Mathf.DeltaAngle(angle, GetPlayerForwardAngle())) < excludedHalfAngle);

        float radius = Random.Range(minRadius, maxRadius);
        Vector3 offset = Quaternion.Euler(0f, angle, 0f) * Vector3.forward * radius;

        return player.position + offset;
    }

    float GetPlayerForwardAngle()
    {
        Vector3 forward = player.forward;
        return Mathf.Atan2(forward.x, forward.z) * Mathf.Rad2Deg;
    }

    /// <summary>
    /// Call this from your RoomDirector when RoomState changes to adjust
    /// how frequently ambient sounds trigger. Lower multiplier = more frequent.
    /// e.g. Calm = 1.5f, Escalate = 1f, Climax = 0.5f
    /// </summary>
    public void SetPacingMultiplier(float multiplier)
    {
        pacingMultiplier = multiplier;
    }


    void Reset()
    {
        audioSource=GetComponent<AudioSource>();    
    }
}
