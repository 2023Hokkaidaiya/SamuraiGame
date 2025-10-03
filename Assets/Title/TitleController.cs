using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    public string sceneName; //読み込むシーン名
  
      // Start is called before the first frame update
    void Start()
    {
                
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //スタートボタンでシーン遷移
    public void StartButton()
    {
        //シーンを読み込む
        SceneManager.LoadScene(sceneName);
    }
}
