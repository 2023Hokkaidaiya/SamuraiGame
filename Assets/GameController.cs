using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text messageText;
    public float minWaitTime = 1.5f;
    public float maxWaitTime = 4.0f;
    public KeyCode attackKey = KeyCode.Space;

    public GameObject Player1Prefab;
    public GameObject Enemy1Prefab;

    private GameObject Player;
    private GameObject Enemy;

    public GameObject Win1Prefab;
    private GameObject Win;

    public GameObject Lose1Prefab;
    private GameObject Lose;

    private float signalTime;
    private float enemyAttackTime;

    // state を string に変更
    [SerializeField] private string state = GameStates.Waiting;

    private Coroutine duelCoroutine;

    void Start()
    {
        SpawnPlayersAtDefault();
        duelCoroutine = StartCoroutine(StartDuel());
    }

    IEnumerator StartDuel()
    {
        SetState(GameStates.Waiting);
        messageText.text = "集中...";
        yield return new WaitForSeconds(UnityEngine.Random.Range(minWaitTime, maxWaitTime));

        messageText.text = "!";
        signalTime = Time.time;
        enemyAttackTime = signalTime + UnityEngine.Random.Range(0.25f, 0.5f);
        SetState(GameStates.Ready);
    }

    void Update()
    {
        // デバッグ用: 1 / 2 はそのまま
        if (Input.GetKeyDown(KeyCode.Alpha1)) CheckoutWin();
        if (Input.GetKeyDown(KeyCode.Alpha2)) CheckoutLose();

        if (GetState() == GameStates.Ended)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartFull();
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                RestartRound();
            }
            return;
        }

        if (GetState() == GameStates.Waiting && Input.GetKeyDown(attackKey))
        {
            messageText.text = "負け";
            SetState(GameStates.Ended);
        }

        if (GetState() == GameStates.Ready && Input.GetKeyDown(attackKey))
        {
            float playerTime = Time.time;
            if (playerTime < enemyAttackTime)
            {
                messageText.text = "勝ち！";
                CheckinWin();
            }
            SetState(GameStates.Ended);
        }

        if (GetState() == GameStates.Ready && Time.time >= enemyAttackTime)
        {
            messageText.text = "負け！";
            CheckinLose();
            SetState(GameStates.Ended);
        }
    }

    public void CheckinWin()
    {
        Vector3 positionLeft = Player.transform.position;
        Vector3 positionRight = Enemy.transform.position;
        Vector3 positionMiddle = (positionLeft + positionRight) / 2f;

        Destroy(Player.gameObject);
        Destroy(Enemy.gameObject);

        Win = Instantiate(Win1Prefab, positionMiddle, Quaternion.identity);
    }

    public void CheckoutWin()
    {
        if (Win != null) Destroy(Win.gameObject);
        SpawnPlayersAtDefault();
    }

    public void CheckinLose()
    {
        Vector3 positionLeft = Player.transform.position;
        Vector3 positionRight = Enemy.transform.position;
        Vector3 positionMiddle = (positionLeft + positionRight) / 2f;

        Destroy(Player.gameObject);
        Destroy(Enemy.gameObject);

        Lose = Instantiate(Lose1Prefab, positionMiddle, Quaternion.identity);
    }

    public void CheckoutLose()
    {
        if (Lose != null) Destroy(Lose.gameObject);
        SpawnPlayersAtDefault();
    }

    private void SpawnPlayersAtDefault()
    {
        if (Player != null) Destroy(Player);
        if (Enemy != null) Destroy(Enemy);

        Player = Instantiate(Player1Prefab, new Vector3(-7.5f, -3.8f, 0), Quaternion.identity);
        Enemy = Instantiate(Enemy1Prefab, new Vector3(6.2f, -3.8f, 0), Quaternion.identity);
    }

    private void RestartRound()
    {
        if (Win != null)
        {
            Destroy(Win.gameObject);
            Win = null;
        }
        if (Lose != null)
        {
            Destroy(Lose.gameObject);
            Lose = null;
        }

        if (Player == null || Enemy == null)
        {
            SpawnPlayersAtDefault();
        }
        else
        {
            Player.transform.position = new Vector3(-7.5f, -3.8f, 0);
            Enemy.transform.position = new Vector3(6.2f, -3.8f, 0);
        }

        if (duelCoroutine != null) StopCoroutine(duelCoroutine);
        duelCoroutine = StartCoroutine(StartDuel());
    }

    private void RestartFull()
    {
        if (Win != null) { Destroy(Win.gameObject); Win = null; }
        if (Lose != null) { Destroy(Lose.gameObject); Lose = null; }

        SpawnPlayersAtDefault();

        if (duelCoroutine != null) StopCoroutine(duelCoroutine);
        duelCoroutine = StartCoroutine(StartDuel());
    }

    private void SetState(string newState) => state = newState;
    private string GetState() => state;
}

// 状態文字列を一箇所で管理
public static class GameStates
{
    public const string Waiting = "Waiting";
    public const string Ready   = "Ready";
    public const string Ended   = "Ended";
}
