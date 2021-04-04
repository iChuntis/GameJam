using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UserInterface
{
    public class UIController : MonoBehaviour
    {
        public static UIController instance;
        public GameProgressController gameProgress;
        public Text endText;
        public GameObject menuPanel;
        public Text timerText;

        // Start is called before the first frame update
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetDomePopBalance(float pop, float dome)
        {
            gameProgress.SetValues(pop, dome);
        }

        public void SetVolunteersNum(int vNum)
        {
            gameProgress.SetVolunteersNum(vNum);
        }
        public void SetPopulationNum(int vNum)
        {
            gameProgress.SetPopulationNum(vNum);
        }
        public void SetNonSavedNum(int vNum)
        {
            gameProgress.SetNonSavedNum(vNum);
        }

        public void SetDomeBalance(float val)
        {
            gameProgress.SetDomeValue(val);
        }
        public void SetPeopleBalance(float val)
        {
            gameProgress.SetPeopleValue(val);
        }
        public void ShowEndText()
        {
            endText.gameObject.SetActive(true);
        } 

        public void OnMenuButtonClick()
        {
            menuPanel.SetActive(true);
            Time.timeScale = 0;
        }

        public void OnSubMenuProceedClick()
        {
            menuPanel.SetActive(false);
            Time.timeScale = 1;
        }
        public void OnSubMenuGoToMenuClick()
        {
            menuPanel.SetActive(false);
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenuScene", LoadSceneMode.Single);
        }

        public void SetNewTimeResult(int res)
        {
            timerText.text = "" + res;
        }
        
    }
}