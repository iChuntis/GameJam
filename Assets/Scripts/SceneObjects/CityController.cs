using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;

namespace SceneObjects
{
    public class CityController : MonoBehaviour
    {
        [SerializeField]
        UI_Manager uI_Manager;

        // Temporary prefab
        [SerializeField]
        GameObject woundPeoplePrefab; // Here we must instantiate PeopleInCity prefab

        [SerializeField]
        Vector2 xCoords;

        [SerializeField]
        Vector2 yCoords;

        [SerializeField]
        int numberArcs;

        [SerializeField]
        Collider2D myCollider;
        void Start()
        {

        }

        public void SetPlaceToSavePeople(int ppl)
        {
            float x, y;
            x = y = 0.0f;

            int radiiSeg = 0;
            int arcNumber = 0;

            bool canPlaceWoundedPeople = false;
            while (!canPlaceWoundedPeople)
            {
                canPlaceWoundedPeople = true;

                radiiSeg = Random.Range(4, 7);
                arcNumber = Random.Range(0, numberArcs);

                float segLength = ((xCoords.y-xCoords.x)/2.0f)/7.0f;
                float distRespawn = segLength * ((float)radiiSeg - 0.5f);
                float angle = (2.0f * Mathf.PI / numberArcs) * arcNumber;

                //Debug.Log("distRespawn: " + distRespawn + "  angle: " + angle);

                x = distRespawn * Mathf.Cos(angle);
                y = distRespawn * Mathf.Sin(angle);
                //x = Random.Range(xCoords.x, xCoords.y);
                //y = Random.Range(yCoords.x, yCoords.y);

                // Check if it is in the collider
                if (!myCollider.bounds.Contains(new Vector2(x, y)))
                {
                    canPlaceWoundedPeople = false;
                    continue;
                }

                foreach (KeyValuePair<GameObject, HomeObject> kvp in GameSystem.GameManager.instance.allHomes)
                {
                    if (kvp.Value.myCollider.bounds.Contains(new Vector2(x, y)))
                    {
                        canPlaceWoundedPeople = false;
                        break;
                    }
                }
            }

            // Here we find place of our wounded people
            GameObject go = Instantiate(woundPeoplePrefab, new Vector3(x, y, 0.0f), Quaternion.identity);
        
            var script = go.GetComponent<People>();
            script.Count = ppl;
            script.InitUI(uI_Manager);

            GameSystem.GameManager.instance.ChangeNotSavedPeople(ppl);
        }
    }
}