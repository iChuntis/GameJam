using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;

namespace SceneObjects
{
    public class SlowingObject : MonoBehaviour
    {
        // Start is called before the first frame update
        public Collider2D myCollider;
        void Start()
        {
            //GameSystem.GameManager.instance.allHomes.Add(gameObject, this);
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Volounteer"))
            {
                //GameSystem.instance.allVolunteers[col.gameObject].SetSpeed(crossSpeed);
                GameA.singleton.volunteers[col.gameObject].Speed /= 2;
            }
        }

        void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Volounteer"))
            {
                //GameSystem.instance.allVolunteers[col.gameObject].DefaultSpeed();
                GameA.singleton.volunteers[col.gameObject].Speed *= 2;
            }
        }
    }
}