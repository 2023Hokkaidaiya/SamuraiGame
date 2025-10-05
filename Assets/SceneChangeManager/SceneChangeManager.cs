using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    [Header("参照する HPController")]
    public HPController hpController; // シーン内の HPController を Inspector で指定

    [Header("プレイヤーが敵に与えた累積ダメージで遷移")]
    public int playerThreshold = 3000;   // EvP_Player がこの値以上なら遷移
    public string playerWinSceneName;    // 遷移先シーン名（Inspectorで指定）

    [Header("敵がプレイヤーに与えた累積ダメージで遷移")]
    public int enemyThreshold = 5000;    // EvP_Enemy がこの値以上なら遷移
    public string enemyWinSceneName;     // 遷移先シーン名（Inspectorで指定）

    private bool sceneChanged = false;   // 二重遷移防止フラグ

    void Update()
    {
        if (sceneChanged || hpController == null) return;

        // Enterキーが押されたときのみ判定
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // プレイヤーが敵に与えた累積ダメージ
            if (hpController.EvP_Player >= playerThreshold)
            {
                sceneChanged = true;
                SceneManager.LoadScene(playerWinSceneName);
            }

            // 敵がプレイヤーに与えた累積ダメージ
            else if (hpController.EvP_Enemy >= enemyThreshold)
            {
                sceneChanged = true;
                SceneManager.LoadScene(enemyWinSceneName);
            }
        }
    }
}
