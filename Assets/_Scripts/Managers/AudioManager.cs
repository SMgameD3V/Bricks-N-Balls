using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip paddleBounceSfx;
    [SerializeField] private AudioClip brickHitSfx;    // NEW — plays on non-breaking hits
    [SerializeField] private AudioClip brickBreakSfx;
    [SerializeField] private AudioClip powerupCollectSfx;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayPaddleBounce() => audioSource.PlayOneShot(paddleBounceSfx);
    public void PlayBrickHit() => audioSource.PlayOneShot(brickHitSfx != null ? brickHitSfx : brickBreakSfx);
    public void PlayBrickBreak() => audioSource.PlayOneShot(brickBreakSfx);
    public void PlayPowerupCollect() => audioSource.PlayOneShot(powerupCollectSfx);
}