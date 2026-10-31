using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private AudioClip soundClip;

    [Header("Settings")]
    [SerializeField] private float minPitch = 0.9f;
    [SerializeField] private float maxPitch = 1.1f;

    public void PlaySound()
    {
        if (soundClip == null) return;

        SfxManager.Instance.PlaySound(soundClip, minPitch, maxPitch);
    }
}
