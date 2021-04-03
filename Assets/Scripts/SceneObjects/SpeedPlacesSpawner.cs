using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GameSystem;

namespace SceneObjects
{
    public class SpeedPlacesSpawner : MonoBehaviour
    {
        [SerializeField]
        GameObject speedUpObject;
        [SerializeField]
        GameObject speedDownObject;
        [SerializeField]
        Vector2 wideRange;
        [SerializeField]
        int numberArcs;
        [SerializeField]
        Collider2D myCollider;
        int maxSpeedPlacesGen;
        int currSpeedPlacesGen;

        [System.Serializable]
        public struct RandPair
        {
            public int radii;
            public int angle;
            
            public RandPair(int r, int a)
            {
                this.radii = r;
                this.angle = a;
            }
        }

        List<RandPair> collectRands;

        // Start is called before the first frame update
        void Start()
        {
            maxSpeedPlacesGen = 15;
            currSpeedPlacesGen = 0;

            collectRands = new List<RandPair>();
        }

        public void SetPlacesToSpawn()
        {
            if (currSpeedPlacesGen >= maxSpeedPlacesGen)
            {
                return;
            }

            currSpeedPlacesGen++;

            // Set place for slowDownObject
            float x, y;
            x = y = 0.0f;

            int radiiSeg = 0;
            int arcNumber = 0;

            bool canPlacecSlow = false;
            while (!canPlacecSlow)
            {
                canPlacecSlow = true;

                radiiSeg = Random.Range(3, 8);
                arcNumber = Random.Range(0, numberArcs);

                // Check if we had such pair in past
                int ind = collectRands.FindIndex(x => x.radii == radiiSeg && x.angle == arcNumber);
                if (ind != -1)
                {
                    canPlacecSlow = false;
                    continue;
                }

                float segLength = ((wideRange.y-wideRange.x)/2.0f)/7.0f;
                float distRespawn = segLength * ((float)radiiSeg - 0.5f);
                float angle = (2.0f * Mathf.PI / numberArcs) * arcNumber;

                x = distRespawn * Mathf.Cos(angle);
                y = distRespawn * Mathf.Sin(angle);

                // Check if it is in the collider
                if (!myCollider.bounds.Contains(new Vector2(x, y)))
                {
                    canPlacecSlow = false;
                    continue;
                }

                // Check not to intersect with houses;
                foreach (KeyValuePair<GameObject, HomeObject> kvp in GameSystem.GameManager.instance.allHomes)
                {
                    if (kvp.Value.myCollider.bounds.Contains(new Vector2(x, y)))
                    {
                        canPlacecSlow = false;
                        break;
                    }
                }
            }

            collectRands.Add(new RandPair(radiiSeg, arcNumber));

            GameObject go = Instantiate(speedDownObject, new Vector3(x, y, 0.0f), Quaternion.identity);
            float scale = (float)radiiSeg / 2.0f;
            go.transform.localScale = new Vector3(scale, scale, 1.0f);

            // Once more for speed up objects
            x = y = 0.0f;

            radiiSeg = 0;
            arcNumber = 0;

            bool canPlacecAcs = false;
            while (!canPlacecAcs)
            {
                canPlacecAcs = true;

                radiiSeg = Random.Range(3, 8);
                arcNumber = Random.Range(0, numberArcs);

                // Check if we had such pair in past
                int ind = collectRands.FindIndex(x => x.radii == radiiSeg && x.angle == arcNumber);
                if (ind != -1)
                {
                    canPlacecAcs = false;
                    continue;
                }

                float segLength = ((wideRange.y-wideRange.x)/2.0f)/7.0f;
                float distRespawn = segLength * ((float)radiiSeg - 0.5f);
                float angle = (2.0f * Mathf.PI / numberArcs) * arcNumber;

                x = distRespawn * Mathf.Cos(angle);
                y = distRespawn * Mathf.Sin(angle);

                // Check if it is in the collider
                if (!myCollider.bounds.Contains(new Vector2(x, y)))
                {
                    canPlacecAcs = false;
                    continue;
                }

                // Check not to intersect with houses;
                foreach (KeyValuePair<GameObject, HomeObject> kvp in GameSystem.GameManager.instance.allHomes)
                {
                    if (kvp.Value.myCollider.bounds.Contains(new Vector2(x, y)))
                    {
                        canPlacecAcs = false;
                        break;
                    }
                }
            }

            collectRands.Add(new RandPair(radiiSeg, arcNumber));

            GameObject go2 = Instantiate(speedUpObject, new Vector3(x, y, 0.0f), Quaternion.identity);
            scale = (float)radiiSeg / 2.0f;
            go2.transform.localScale = new Vector3(scale, scale, 1.0f);
        }
    }
}