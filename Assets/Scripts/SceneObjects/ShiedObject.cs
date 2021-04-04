using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;

namespace SceneObjects
{
    public class ShiedObject : Pick
    {
        // Start is called before the first frame update
        [SerializeField]
        int addHealth;
        Collider2D myCollider;
        void Start()
        {
            myCollider = GetComponent<Collider2D>();
        }

        // Update is called once per frame
        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Volounteer"))
            {
                GameSystem.GameManager.instance.ChangeCityLifePoints(addHealth);

                
                var script = GameA.singleton.volunteers[col.gameObject];
                if (script == vol)
                {
                    script.FixCheckPoint();
                }
                

                var script2 = GameA.singleton.volunteers[col.gameObject];
                if (script2 == vol)
                {
                    script2.FixingFinish();
                }

                DieFunction();
            }
        }

        void DieFunction()
        {
            Destroy(gameObject);
        }
    }
}