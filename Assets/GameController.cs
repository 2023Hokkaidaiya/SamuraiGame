using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic; //よく分かっていない
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;
using System.Numerics;
using Unity.Mathematics.Geometry;


public class GameController : MonoBehaviour
{
    public Text messageText; // TextMeshProではなくUI.Text
    public float minWaitTime = 1.5f;
    public float maxWaitTime = 4.0f;
    public KeyCode attackKey = KeyCode.Space;

    //PlayerPrefabを入れる
    public GameObject Player1Prefab;
    public GameObject Enemy1Prefab;

    private GameObject Player;
    private GameObject Enemy;

    //一枚絵Prefabを入れる(Win or Lose)　それぞれのPrefabを作る必要あり
    public GameObject Win1Prefab;
    private GameObject Win;

    public GameObject Lose1Prefab;
    private GameObject Lose;

    //カウンターゲーム用の値
    private float signalTime;
    private float enemyAttackTime;
    private GameState state = GameState.Waiting;

    void Start()
    {
        StartCoroutine(StartDuel());
    }

    IEnumerator StartDuel()
    {
        state = GameState.Waiting;
        messageText.text = "集中...";
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));

        // 合図
        messageText.text = "！";
        signalTime = Time.time;
        enemyAttackTime = signalTime + Random.Range(0.25f, 0.5f);
        state = GameState.Ready;
    }

    void Update()
    {
        
        //0で一枚絵に変える（デバッグ用でありのちに更新すること）
        /*
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            CheckinWin();
        }
        */
        if (state == GameState.Ended)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(StartDuel());
            }
            return;
        }

        // 早撃ち判定　（これは最終的仕様に無いので削除予定）
        if (state == GameState.Waiting && Input.GetKeyDown(attackKey))
        {
            messageText.text = "早撃ち！負け...";
            state = GameState.Ended;
        }

        // プレイヤー攻撃（押して間に合った場合と、押したけど間に合わなかった場合）
        if (state == GameState.Ready && Input.GetKeyDown(attackKey))
        {
            float playerTime = Time.time;
            if (playerTime < enemyAttackTime)
                messageText.text = "勝ち！";
            //ここにCheckinWin();
            else
                messageText.text = "遅い！負け..."; //←いらない？
            //ここにCheckinLose();
            state = GameState.Ended;
        }

        // 敵攻撃（押さなかった場合）
        if (state == GameState.Ready && Time.time >= enemyAttackTime)
        {
            messageText.text = "斬られた！負け...";
            //ここにCheckinLose();
            state = GameState.Ended;
        }
    }
    /*
     * public void CheckinWin();
    {
     //入れ替え直前のポジションを取得(LとR両方取得)
       Vector3 positionLeft = Player.transform.position;
       Vector3 positionRight = Enemy.transform.position;
       Vector3 positionMiddle = (positionLeft + positionRight) / 2f;

     //ポジションが取得できたの破棄
        Destroy(Player.gameObject, 0.0f);
        Destroy(Enemy.gameObject, 0.0f);
    //一枚絵（WIN)を生成
        Win = Instantiate(Win1Prefab, positionMiddle, Quaternion.identity);
     }
    */

}





public enum GameState
{
    Waiting,
    Ready,
    Ended
}
