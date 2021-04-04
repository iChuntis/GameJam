using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneObjects;

namespace GameSystem
{
    public class GameEventsController : MonoBehaviour
    {
        // Start is called before the first frame update
        /*
        [SerializeField]
        Vector2 timeEventInterval;
        [SerializeField]
        float savePeopleProb;
        [SerializeField]
        float damageDomeProb;
        */

        float timeOfNextEvent;
        float timeCount;
        float timeOfDomeDamage;
        float timeOfPeopleSpawn;
        float timeOfSpeedPlacesSpawn;

        [SerializeField]
        float[] gameMileStones;
        [SerializeField]
        float[] domeCrashFreq;

        [SerializeField]
        float[] peopleSpawnFreq;
        [SerializeField]
        int[] peopleSpawnNumber;

        [SerializeField]
        float[] speedPlacesFreq;
        int currMileStone;


        DomeController domeController;
        CityController cityController;
        SpeedPlacesSpawner speedPlacesSpawner;
        void Start()
        {
            domeController = FindObjectOfType<DomeController>();
            cityController = FindObjectOfType<CityController>();
            speedPlacesSpawner = FindObjectOfType<SpeedPlacesSpawner>();
            //timeOfNextEvent = Random.Range(timeEventInterval.x, timeEventInterval.y);

            currMileStone = 0;
            timeCount = 0.0f;
            timeOfNextEvent = 0.0f;
            timeOfDomeDamage = 0.0f;
            timeOfPeopleSpawn = 0.0f;
            timeOfSpeedPlacesSpawn = 0.0f;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            timeCount += Time.deltaTime;

            if (timeCount >= gameMileStones[currMileStone])
            {
                currMileStone++;
                //timeOfDomeDamage = domeCrashFreq[currMileStone];
                //timeOfPeopleSpawn = peopleSpawnFreq[currMileStone];
            }

            timeOfDomeDamage += Time.deltaTime;
            if (timeOfDomeDamage >= domeCrashFreq[currMileStone])
            {
                timeOfDomeDamage = 0.0f;
                // Make random dome damage
                StartCoroutine(MakeDomeDamageEvent());
            }
            timeOfPeopleSpawn += Time.deltaTime;
            if (timeOfPeopleSpawn >= peopleSpawnFreq[currMileStone])
            {
                timeOfPeopleSpawn = 0.0f;
                // Make random people spawn
                StartCoroutine(MakeSavePeopleEvent(peopleSpawnNumber[currMileStone]));
            }
            timeOfSpeedPlacesSpawn += Time.deltaTime;
            if (timeOfSpeedPlacesSpawn >= speedPlacesFreq[currMileStone])
            {
                timeOfSpeedPlacesSpawn = 0.0f;
                StartCoroutine(MakeNewSpeedPlaces());
            }

            /*
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
            */
        }

        IEnumerator MakeSavePeopleEvent(int peopleSpawn)
        {
            // Check where we can place the people for to save
            Debug.Log("GameSystem.GameManager.instance.NonSavedPeople " + GameSystem.GameManager.instance.NonSavedPeople);
            int cnt = GameObject.FindObjectsOfType<People>().Length;
            if (cnt <= 2)
            {
                cityController.SetPlaceToSavePeople(peopleSpawn);
            }
            /*
            if (GameSystem.GameManager.instance.NonSavedPeople < 2)
            {
                cityController.SetPlaceToSavePeople(peopleSpawn);
            }
            */

            yield return null;
        }

        IEnumerator MakeDomeDamageEvent()
        {
            // Check, which part of dome is broken
            int nonSavedPeople = GameSystem.GameManager.instance.NonSavedPeople;
            int maxDamagedDomes = (int)(((float)nonSavedPeople * 1.1f)/5.0f);
            Debug.Log("maxDamagedDomes " + maxDamagedDomes + " brokenPieces " + GameSystem.GameManager.instance.DomeBrokenPieces());
            if (GameSystem.GameManager.instance.DomeBrokenPieces() <= maxDamagedDomes)
            {
                domeController.ChooseDomePartToBroke();
            }

            yield return null;
        }

        IEnumerator MakeNewSpeedPlaces()
        {
            speedPlacesSpawner.SetPlacesToSpawn();
            yield return null;
        }
    }
}