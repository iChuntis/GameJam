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
    }

    public void OnExitClick()
    {
        Application.Quit();
    }
}
