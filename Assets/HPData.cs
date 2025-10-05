using UnityEngine;

public class HPData : MonoBehaviour
{
    //HPはもちこす
    public static int HPLeft = 8000;
    public static int HPRight = 8000;
    // 累積ダメージ（シーンごとにリセット）
    public static int EvP_Player = 0;
    public static int EvP_Enemy = 0;
    // シーン開始時に呼ぶリセット用メソッド
    public static void ResetEvP()
    {
        EvP_Player = 0;
        EvP_Enemy = 0;
    }
}
