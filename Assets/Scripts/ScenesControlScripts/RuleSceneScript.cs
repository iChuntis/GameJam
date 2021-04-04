using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RuleSceneScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    Text textArea;
    [SerializeField]
    Text textArea_2;

    int curr_text;
    void Start()
    {
        curr_text = 0;
    }

    public void OnStartGameClick()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameMainScene", LoadSceneMode.Single);
    }

    public void OnShowMenuClick()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenuScene", LoadSceneMode.Single);
    }

    public void CLickNextText()
    {
        curr_text++;
        if (curr_text == 2)
        {
            curr_text = 0;
        }
        textArea.gameObject.SetActive(curr_text == 0);
        textArea_2.gameObject.SetActive(curr_text == 1);
    }
}
