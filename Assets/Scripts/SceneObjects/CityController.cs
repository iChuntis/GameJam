using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;

namespace SceneObjects
{
    public class CityController : MonoBehaviour
    {
        // Temporary prefab
        [SerializeField]
        GameObject woundPeoplePrefab;

        [SerializeField]
        Vector2 xCoords;

        [SerializeField]
        Vector2 yCoords;

        [SerializeField]
        Collider2D myCollider;
        void Start()
        {

        }

        public void SetPlaceToSavePeople()
        {
            float x, y;
            x = y = 0.0f;

            bool canPlaceWoundedPeople = false;
            while (!canPlaceWoundedPeople)
            {
                canPlaceWoundedPeople = true;
                x = Random.Range(xCoords.x, xCoords.y);
                y = Random.Range(yCoords.x, yCoords.y);

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
            Instantiate(woundPeoplePrefab, new Vector3(x, y, 0.0f), Quaternion.identity);
        }
    }
}