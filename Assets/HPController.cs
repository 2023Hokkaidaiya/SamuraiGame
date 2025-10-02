using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    [Header("HP UI")]
    public Text hpLeftText;   // HP_Player
    public Text hpRightText;  // HP_Enemy

    [Header("累積ダメージ UI")]
    public Text evpPlayerText; // EvP_Player（右側に与えたダメージ合計）
    public Text evpEnemyText;  // EvP_Enemy（左側に与えたダメージ合計）

    [Header("初期HP")]
    public int initialHPLeft = 8000;
    public int initialHPRight = 8000;

    public int HPLeft { get; private set; }
    public int HPRight { get; private set; }

    // 累積ダメージ
    public int EvP_Player { get; private set; } // HPRight に与えた合計ダメージ
    public int EvP_Enemy  { get; private set; } // HPLeft に与えた合計ダメージ

    void Awake()
    {
        HPLeft = Mathf.Max(0, initialHPLeft);
        HPRight = Mathf.Max(0, initialHPRight);
        EvP_Player = 0;
        EvP_Enemy = 0;
        UpdateUI();
    }

    public void ApplyDamageLeft(int damage)
    {
        if (damage <= 0) return;
        int before = HPLeft;
        HPLeft = Mathf.Max(0, HPLeft - damage);

        // 実際に減った分をカウント
        int actualDamage = before - HPLeft;
        EvP_Enemy += actualDamage;

        UpdateUI();
    }

    public void ApplyDamageRight(int damage)
    {
        if (damage <= 0) return;
        int before = HPRight;
        HPRight = Mathf.Max(0, HPRight - damage);

        // 実際に減った分をカウント
        int actualDamage = before - HPRight;
        EvP_Player += actualDamage;

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (hpLeftText)  hpLeftText.text  = $"HP:{HPLeft}";
        if (hpRightText) hpRightText.text = $"HP:{HPRight}";

        if (evpPlayerText) evpPlayerText.text = $"EvPP:{EvP_Player}";
        if (evpEnemyText)  evpEnemyText.text  = $"EvPE:{EvP_Enemy}";
    }
}

/*public class HPController : MonoBehaviour
    {
        private GameObject hpleftText;
        //右のHPテキスト
        private GameObject hprightText;
        //HP player&enemy計算用変数
        public int HPLeft;  //1
        public int HPRight; //2
                            //=================================================

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            //シーン中のHPTextオブジェクトを取得
            this.hpleftText = GameObject.Find("HP_Player");
            this.hprightText = GameObject.Find("HP_Enemy");
        }

        // Update is called once per frame
        void Update()
        {
            //HPを更新する=====================================================================
            if (HPLeft <= 0)
            {
                this.hpleftText.GetComponent<Text>().text = "HP:0";
            }
            else
            {
                this.hpleftText.GetComponent<Text>().text = "HP:" + HPLeft.ToString();
            }

            if (HPRight <= 0)
            {
                this.hprightText.GetComponent<Text>().text = "HP:0";
            }
            else
            {
                this.hprightText.GetComponent<Text>().text = "HP:" + HPRight.ToString();
            }



        }
    }*/
