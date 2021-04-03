using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RuleSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnStartGameClick()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameMainScene", LoadSceneMode.Single);
    }

    public void OnShowMenuClick()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenuScene", LoadSceneMode.Single);
    }
}
