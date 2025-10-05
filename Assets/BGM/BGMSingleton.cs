using UnityEngine;

public class BGMSingleton : MonoBehaviour
{
    public static BGMSingleton Instance { get; private set; }

    void Awake()
    {
        // すでにインスタンスが存在する場合は自分を破棄
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // 最初のインスタンスとして登録
        Instance = this;
        DontDestroyOnLoad(gameObject); // シーンをまたいでも破棄されない
    }
}
