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

        float currentPeopleBar;
        float currentDomeBar;
        float deltaChangeBar;
        void Start()
        {
            UIController.instance.gameProgress = this;
            currentPeopleBar = 0.5f;
            currentDomeBar = 0.5f;
            deltaChangeBar = 0.01f;

            peopleBarImg.fillAmount = currentPeopleBar;
            domeBarImg.fillAmount = currentDomeBar;
            volunteersVol.text = "Volunteers: 0";
        }

        public void SetValues(float peopleBar, float domeBar)
        {
            StopAllCoroutines();
            StartCoroutine(ChangeBarAnim(peopleBar, domeBar));
        }

        public void SetVolunteersNum(int num)
        {
            volunteersVol.text = "Volunteers: " + num.ToString();
        }

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
    }
}