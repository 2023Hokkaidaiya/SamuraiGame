using UnityEngine;

public class WinController : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource; // Inspectorでセットしてください
    public AudioClip SE1Slashed;
    public AudioClip SE2SoundOfSword;
    public AudioClip SE3Footsteps;

    [Header("Settings")]
    public float defaultVolume = 1f;
    public bool useOneShot = true;

    private void Reset()
    {
        // 新規追加時に同一GameObjectにAudioSourceがあれば自動で参照する
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
    }

    private void OnValidate()
    {
        // インスペクタでセットされたら playOnAwake をオフにするのが安全
        if (audioSource != null) audioSource.playOnAwake = false;
    }

    public void Slashed()
    {
        if (audioSource == null || SE1Slashed == null) return;
        Play(SE1Slashed, defaultVolume);
    }

    public void SoundOfSword()
    {
        if (audioSource == null || SE2SoundOfSword == null) return;
        Play(SE2SoundOfSword, defaultVolume);
    }

    public void Footsteps()
    {
        if (audioSource == null || SE3Footsteps == null) return;
        Play(SE3Footsteps, defaultVolume);
    }

    public void PlaySEWithVolume(float volume)
    {
        if (audioSource == null || SE1Slashed == null) return;
        Play(SE1Slashed, Mathf.Clamp01(volume));
    }

    private void Play(AudioClip clip, float volume)
    {
        if (useOneShot) audioSource.PlayOneShot(clip, volume);
        else
        {
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.Play();
        }
    }
}