using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic; //�悭�������Ă��Ȃ�
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;
using System.Numerics;
using Unity.Mathematics.Geometry;


public class GameController : MonoBehaviour
{
    public Text messageText; // TextMeshPro�ł͂Ȃ�UI.Text
    public float minWaitTime = 1.5f;
    public float maxWaitTime = 4.0f;
    public KeyCode attackKey = KeyCode.Space;

    //PlayerPrefab������
    public GameObject Player1Prefab;
    public GameObject Enemy1Prefab;

    private GameObject Player;
    private GameObject Enemy;

    //�ꖇ�GPrefab������(Win or Lose)�@���ꂼ���Prefab�����K�v����
    public GameObject Win1Prefab;
    private GameObject Win;

    public GameObject Lose1Prefab;
    private GameObject Lose;

    //�J�E���^�[�Q�[���p�̒l
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
        messageText.text = "�W��...";
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));

        // ���}
        messageText.text = "�I";
        signalTime = Time.time;
        enemyAttackTime = signalTime + Random.Range(0.25f, 0.5f);
        state = GameState.Ready;
    }

    void Update()
    {
        
        //0�ňꖇ�G�ɕς���i�f�o�b�O�p�ł���̂��ɍX�V���邱�Ɓj
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

        // ����������@�i����͍ŏI�I�d�l�ɖ����̂ō폜�\��j
        if (state == GameState.Waiting && Input.GetKeyDown(attackKey))
        {
            messageText.text = "�������I����...";
            state = GameState.Ended;
        }

        // �v���C���[�U���i�����ĊԂɍ������ꍇ�ƁA���������ǊԂɍ���Ȃ������ꍇ�j
        if (state == GameState.Ready && Input.GetKeyDown(attackKey))
        {
            float playerTime = Time.time;
            if (playerTime < enemyAttackTime)
                messageText.text = "�����I";
            //������CheckinWin();
            else
                messageText.text = "�x���I����..."; //������Ȃ��H
            //������CheckinLose();
            state = GameState.Ended;
        }

        // �G�U���i�����Ȃ������ꍇ�j
        if (state == GameState.Ready && Time.time >= enemyAttackTime)
        {
            messageText.text = "�a��ꂽ�I����...";
            //������CheckinLose();
            state = GameState.Ended;
        }
    }
    /*
     * public void CheckinWin();
    {
     //����ւ����O�̃|�W�V�������擾(L��R�����擾)
       Vector3 positionLeft = Player.transform.position;
       Vector3 positionRight = Enemy.transform.position;
       Vector3 positionMiddle = (positionLeft + positionRight) / 2f;

     //�|�W�V�������擾�ł����̔j��
        Destroy(Player.gameObject, 0.0f);
        Destroy(Enemy.gameObject, 0.0f);
    //�ꖇ�G�iWIN)�𐶐�
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
