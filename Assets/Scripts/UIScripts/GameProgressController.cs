using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class GameProgressController : MonoBehaviour
    {
        // Start is called before the first frame update
        public Image peopleBarImg;
        public Image domeBarImg;

        public Text volunteersVol;
        public Text populationVol;
        public Text nonSavedVol;

        float currentPeopleBar;
        float currentDomeBar;
        float deltaChangeBar;
        void Start()
        {
            UIController.instance.gameProgress = this;
            currentDomeBar = 1.0f;
            deltaChangeBar = 0.01f;

            peopleBarImg.fillAmount = currentPeopleBar;
            domeBarImg.fillAmount = currentDomeBar;
           
        }

        public void SetValues(float peopleBar, float domeBar)
        {
            //StopAllCoroutines();
            //StartCoroutine(ChangeBarAnim(peopleBar, domeBar));
        }

        public void SetDomeValue(float domeBar)
        {
            currentDomeBar = Mathf.Min(1.0f, domeBar);
            domeBarImg.fillAmount = currentDomeBar;
        }

        public void SetPeopleValue(float peopleBar)
        {
            currentPeopleBar = Mathf.Min(1.0f, peopleBar);
            peopleBarImg.fillAmount = currentPeopleBar;
        }
        
        public void SetVolunteersNum(int num)
        {
           // volunteersVol.text = "Volunteers: " + num.ToString();
        }

        public void SetPopulationNum(int num)
        {
            //populationVol.text = "Population: " + num.ToString();
        }
        public void SetNonSavedNum(int num)
        {
            //nonSavedVol.text = "NonSaved: " + num.ToString();
        }

        /*
        IEnumerator ChangeBarAnim(float peopleBar, float domeBar)
        {
            float changeP = peopleBar - currentPeopleBar;
            while (Mathf.Abs(peopleBar-currentPeopleBar) >= deltaChangeBar)
            {
                if (changeP > 0)
                {
                    currentPeopleBar += deltaChangeBar;
                    currentDomeBar -= deltaChangeBar;
                }
                if (changeP < 0)
                {
                    currentPeopleBar += deltaChangeBar;
                    currentDomeBar -= deltaChangeBar;
                }

                peopleBarImg.fillAmount = currentPeopleBar;
                domeBarImg.fillAmount = currentDomeBar;

                yield return new WaitForEndOfFrame();
            }

            currentPeopleBar = peopleBar;
            currentDomeBar = domeBar;

            peopleBarImg.fillAmount = currentPeopleBar;
            domeBarImg.fillAmount = currentDomeBar;
        }
        */
    }
}