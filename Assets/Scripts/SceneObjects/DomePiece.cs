using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;

namespace SceneObjects
{
    public class DomePiece : Pick
    {
        public bool isBroken;
        public bool canRepair;
        public bool isRepairing;

        public bool canDamage;
        //public float timeToRepair;

        int accumulatedDamage;
        int currentDamageRate;

        //public Vector3 myPosition;

        Collider2D myCollider;
        SpriteRenderer mySpriteRenderer;
        [SerializeField]
        int initDamageRate;
        [SerializeField]
        GameObject allertPoint;
        private IEnumerator coroutine;

        float deltaTimer;
        float secondsTimer;
        float increaseDamageTimer;
        // Start is called before the first frame update
        void Start()
        {
            GameSystem.GameManager.instance.allDomePieces.Add(gameObject, this);
            isBroken = false;
            canRepair = false;
            canDamage = false;
            isRepairing = false;

            deltaTimer = 0.0f;
            secondsTimer = 0.0f;
            increaseDamageTimer = 0.0f;

            accumulatedDamage = 0;
            currentDamageRate = initDamageRate;

            myCollider = GetComponent<Collider2D>();
            mySpriteRenderer = GetComponent<SpriteRenderer>();
            //myPosition = gameObject.transform.position;
        }

        public void InitDamage()
        {
            if (!isBroken)
            {
                deltaTimer = 0.0f;
                secondsTimer = 0.0f;
                increaseDamageTimer = 0.0f;
                canRepair = true;
                isBroken = true;
                canDamage = false;
                accumulatedDamage = 0;
                currentDamageRate = initDamageRate;
                allertPoint.SetActive(true);
                GameSystem.GameManager.instance.DomePartChanged(true);
            }
        }

        void FixedUpdate() 
        {
            if (canRepair)
            {
                deltaTimer += Time.deltaTime;

                if (!canDamage && deltaTimer >= 3)
                {
                    canDamage = true;
                }

                if (canDamage)
                {
                    secondsTimer += Time.deltaTime;
                    increaseDamageTimer += Time.deltaTime;
                    if (secondsTimer >= 1.0f) // Make damage for city
                    {
                        secondsTimer = 0.0f;
                        GameSystem.GameManager.instance.ChangeCityLifePoints(-1*currentDamageRate);
                        accumulatedDamage += currentDamageRate;
                    }
                    if (increaseDamageTimer >= 24.0f)
                    {
                        currentDamageRate += 1;
                        increaseDamageTimer = 0.0f;
                    }
                }

                /*
                if (deltaTimer >= timeToRepair)
                {
                    deltaTimer = 0.0f;
                    canRepair = false;
                    isBroken = true;
                    allertPoint.SetActive(false);
                    mySpriteRenderer.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
                    if (coroutine != null)
                    {
                        StopCoroutine(coroutine);
                    }
                }
                */
            }    
        }

        public void SetRepaired()
        {
            isBroken = false;
            canRepair = false;
            isRepairing = false;
            canDamage = false;
            deltaTimer = 0.0f;
            secondsTimer = 0.0f;
            increaseDamageTimer = 0.0f;
            accumulatedDamage = 0;
            currentDamageRate = initDamageRate;
            coroutine = null;
            allertPoint.SetActive(false);
            GameSystem.GameManager.instance.DomePartChanged(false);
        }

        // Возможно, начало ремонта будет производиться по другому
        void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Volounteer"))
            {
                int volunteers = GameA.singleton.volunteers[col.gameObject].Count;
                coroutine = StartRepairVolunteers(volunteers);
                StartCoroutine(coroutine);


                var script = GameA.singleton.volunteers[col.gameObject];
                if (script == vol)
                {
                    script.FixCheckPoint();
                }
            }
        }

        IEnumerator StartRepair(float repairSpeed)
        {
            yield return new WaitForSeconds(repairSpeed);

            if (!isBroken)
            {
                SetRepaired();
            }

            yield return null;
        }

        IEnumerator StartRepairVolunteers(int numOfVolunteers)
        {
            int accDam = accumulatedDamage;

            while (accDam > 0)
            {
                accDam -= 7 * numOfVolunteers;
                yield return new WaitForSeconds(1.0f);
            }
            
            int returnLife = (int)((float)accumulatedDamage * 0.99f);
            GameSystem.GameManager.instance.ChangeCityLifePoints(returnLife);
            SetRepaired();
        }
    }
}