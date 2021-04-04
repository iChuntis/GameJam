using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneRuleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnStartGameClick()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameMainScene", LoadSceneMode.Single);
    }

    public void OnShowRulesClick()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("RulesScene", LoadSceneMode.Single);
    }

    public void OnShowRecordsClick()
    {
        // ......
        //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Records",LoadSceneMode.Single);
    }

    public void OnExitClick()
    {
        Messenger.Broadcast("ExitGameFully");
        Application.Quit();
    }
}
