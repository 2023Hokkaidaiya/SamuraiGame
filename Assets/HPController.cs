using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    //================================================
    //左のHPテキスト
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
}
