using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SamuraiGame : MonoBehaviour
{
    public Text messageText; // TextMeshPro‚Å‚Í‚È‚­UI.Text
    public float minWaitTime = 1.5f;
    public float maxWaitTime = 4.0f;
    public KeyCode attackKey = KeyCode.Space;

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
        messageText.text = "W’†...";
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));

        // ‡}
        messageText.text = "I";
        signalTime = Time.time;
        enemyAttackTime = signalTime + Random.Range(0.25f, 0.5f);
        state = GameState.Ready;
    }

    void Update()
    {
        if (state == GameState.Ended)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(StartDuel());
            }
            return;
        }

        // ‘Œ‚‚¿”»’è
        if (state == GameState.Waiting && Input.GetKeyDown(attackKey))
        {
            messageText.text = "‘Œ‚‚¿I•‰‚¯...";
            state = GameState.Ended;
        }

        // ƒvƒŒƒCƒ„[UŒ‚
        if (state == GameState.Ready && Input.GetKeyDown(attackKey))
        {
            float playerTime = Time.time;
            if (playerTime < enemyAttackTime)
                messageText.text = "Ÿ‚¿I";
            else
                messageText.text = "’x‚¢I•‰‚¯...";
            state = GameState.Ended;
        }

        // “GUŒ‚
        if (state == GameState.Ready && Time.time >= enemyAttackTime)
        {
            messageText.text = "a‚ç‚ê‚½I•‰‚¯...";
            state = GameState.Ended;
        }
    }
}

public enum GameState
{
    Waiting,
    Ready,
    Ended
}
