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

        int accDam;

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

        GroupOfVolunteers group;

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
            accDam = 0;
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
            Debug.Log("FULL REPAIR");
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
            group.FixingFinish();
            group = null;
        }

        public void SetPartialRepaired()
        {
            StopCoroutine(coroutine);

            if (accDam <= currentDamageRate)
            {
                SetRepaired();
                return;
            }

            int returnLife = (int)((float)(accumulatedDamage - accDam) * 0.99f);
            GameSystem.GameManager.instance.ChangeCityLifePoints(returnLife);

            accumulatedDamage = accDam;
            accDam = 0;
            coroutine = null;
            isRepairing = false;
            canRepair = true;
        }

        // Возможно, начало ремонта будет производиться по другому
        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Volounteer"))
            {
                Debug.Log("VOLUNTEERS COME");
                canRepair = false;
                var script = GameA.singleton.volunteers[col.gameObject];
                int volunteers = script.Count;
                Debug.Log("Their count: " + volunteers);
                coroutine = StartRepairVolunteers(volunteers);
                StartCoroutine(coroutine);
                group = script;
                if (script == vol)
                {
                    script.FixCheckPoint();
                }
            }
        }

        void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Volounteer"))
            {
                Debug.Log("PARTIAL VOLUNTEERS STOP");
                SetPartialRepaired();
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
            accDam = accumulatedDamage;
            isRepairing = true;

            while (accDam > 0)
            {
                accDam -= 7 * numOfVolunteers;
                Debug.Log("accDam: " + accDam + "  accumulatedDamage: " + accumulatedDamage);
                yield return new WaitForSeconds(1.0f);
            }
            
            int returnLife = (int)((float)accumulatedDamage * 0.99f);
            GameSystem.GameManager.instance.ChangeCityLifePoints(returnLife);
            SetRepaired();
        }
    }
}