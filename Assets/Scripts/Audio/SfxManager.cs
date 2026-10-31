using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public static SfxManager Instance { get; private set; }

    [SerializeField] private AudioSource sfxAudioSource;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }
    }
    public void PlaySound(AudioClip clip, float minPitch = 0.9f, float maxPitch = 1.1f)
    {
        if (clip == null) return;

        sfxAudioSource.pitch = Random.Range(minPitch, maxPitch);
        sfxAudioSource.PlayOneShot(clip);
    }
}
