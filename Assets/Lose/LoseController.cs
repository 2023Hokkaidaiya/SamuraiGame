using UnityEngine;

public enum TargetSide { Left, Right }

public class LoseController : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource; // Inspectorでセットしてください
    public AudioClip SE1Slashed;
    public AudioClip SE2SoundOfSword;
    public AudioClip SE3Footsteps;

    [Header("Settings")]
    public float defaultVolume = 1f;
    public bool useOneShot = true;

    [Header("Damage")]
    public int damage = 1000; // このPrefabで与えるダメージ（Inspectorで設定）
    public TargetSide target = TargetSide.Right; // Winなら通常 Right（敵）

    // アニメーションイベントから呼ぶ際に使うHPコントローラ参照（GameControllerから初期化する）
    private HPController hp;
    private bool appliedOnce;

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
        if (audioSource == null || SE1Slashed == null)
        {
            Debug.LogWarning("Slashed() が呼ばれたが、AudioSource または SE1Slashed が未設定です。");
            return;
        }
        Debug.Log("Slashed() が呼ばれました。SE1Slashed を再生します。");
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
        if (useOneShot)
        {
            audioSource.PlayOneShot(clip, volume);
        }
        else
        {
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.Play();
        }
    }

    // ----------------------------------------------------
    // Animation-event 用メソッド（以下を Animation Window のイベントで指定）
    // ----------------------------------------------------

    // GameController などから呼んで HPController を渡す
    public void Init(HPController hpController)
    {
        hp = hpController;
    }

    // 単発ヒット用（イベントにこの名前を指定）
    public void AnimationDamageOnce()
    {
        if (hp == null)
        {
            hp = Object.FindFirstObjectByType<HPController>();
            if (hp == null) return;
        }

        if (appliedOnce) return;
        appliedOnce = true;

        ApplyDamage();
    }

    // 多段ヒット用（イベントごとに呼ばれる）
    public void AnimationDamageMulti()
    {
        if (hp == null) hp = Object.FindFirstObjectByType<HPController>();
        if (hp == null) return;
        
        ApplyDamage();
    }

    // アニメーション終了通知用（必要なら GameController に通知するフックを追加）
    public void AnimationFinished()
    {
        // 例: FindObjectOfType<GameController>()?.OnRoundAnimationFinished();
    }

    private void ApplyDamage()
    {
        if (target == TargetSide.Left)
        {
            hp.ApplyDamageLeft(damage);
        }
        else
        {
            hp.ApplyDamageRight(damage);
        }
    }

    // リセットして再利用する場合に呼ぶ
    public void ResetAppliedFlag()
    {
        appliedOnce = false;
    }
}



/*using UnityEngine;


public class LoseController : MonoBehaviour
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
        if (audioSource == null || SE1Slashed == null)
        {
            Debug.LogWarning("Slashed() が呼ばれたが、AudioSource または SE1Slashed が未設定です。");
            return;
        }
        Debug.Log("Slashed() が呼ばれました。SE1Slashed を再生します。");
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
        audioSource.PlayOneShot(clip, volume);
        if (useOneShot)
        {
         audioSource.PlayOneShot(clip, volume);   
        }
        else
        {
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.Play();
        }
    }
}*/