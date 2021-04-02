using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserInterface
{
    public class UIController : MonoBehaviour
    {
        public static UIController instance;
        public GameProgressController gameProgress;
        // Start is called before the first frame update
        void Start()
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
    }
}