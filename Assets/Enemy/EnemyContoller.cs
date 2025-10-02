using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("攻撃までの待機時間")]
    [SerializeField] public float minWaitTime = 1.5f;
    [SerializeField] public float maxWaitTime = 4.0f;

    // 将来的に敵のAIや挙動をここに追加できる
}

