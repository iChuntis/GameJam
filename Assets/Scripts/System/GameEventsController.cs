using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        void Start()
        {
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
            }
        }
    }
}