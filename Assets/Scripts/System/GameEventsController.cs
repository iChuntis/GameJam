using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneObjects;

namespace GameSystem
{
    public class GameEventsController : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField]
        Vector2 timeEventInterval;
        [SerializeField]
        float savePeopleProb;
        [SerializeField]
        float damageDomeProb;

        float timeOfNextEvent;
        float timeCount;

        DomeController domeController;
        CityController cityController;
        void Start()
        {
            domeController = FindObjectOfType<DomeController>();
            cityController = FindObjectOfType<CityController>();
            timeOfNextEvent = Random.Range(timeEventInterval.x, timeEventInterval.y);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            timeCount += Time.deltaTime;
            if (timeCount >= timeOfNextEvent)
            {
                timeCount = 0;
                timeOfNextEvent = Random.Range(timeEventInterval.x, timeEventInterval.y);

                // Decide which event instantiate
                float peopleEvent = Random.Range(0, 1);
                float domeEvent = Random.Range(0, 1);

                if (peopleEvent < savePeopleProb)
                {
                    StartCoroutine(MakeSavePeopleEvent());
                }

                if (domeEvent < damageDomeProb)
                {
                    StartCoroutine(MakeDomeDamageEvent());
                }
            }
        }

        IEnumerator MakeSavePeopleEvent()
        {
            // Check where we can place the people for to save
            cityController.SetPlaceToSavePeople();

            yield return null;
        }

        IEnumerator MakeDomeDamageEvent()
        {
            // Check, which part of dome is broken
            domeController.ChooseDomePartToBroke();

            yield return null;
        }
    }
}