using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;

namespace SceneObjects
{
    public class DomePiece : MonoBehaviour
    {
        public bool isBroken;
        public bool canRepair;
        public bool isRepairing;
        public float timeToRepair;

        public Vector3 myPosition;

        Collider2D myCollider;
        SpriteRenderer mySpriteRenderer;

        [SerializeField]
        GameObject allertPoint;
        private IEnumerator coroutine;

        public float deltaTimer;
        // Start is called before the first frame update
        void Start()
        {
            GameSystem.GameManager.instance.allDomePieces.Add(gameObject, this);
            isBroken = false;
            canRepair = false;
            isRepairing = false;
            deltaTimer = 0.0f;

            myCollider = GetComponent<Collider2D>();
            mySpriteRenderer = GetComponent<SpriteRenderer>();
            myPosition = gameObject.transform.position;
        }

        public void InitDamage()
        {
            if (!isBroken)
            {
                deltaTimer = 0.0f;
                canRepair = true;
                allertPoint.SetActive(true);
            }
        }

        void FixedUpdate() 
        {
            if (canRepair)
            {
                deltaTimer += Time.deltaTime;
                if (deltaTimer >= timeToRepair)
                {
                    deltaTimer = 0.0f;
                    canRepair = false;
                    isBroken = true;
                    allertPoint.SetActive(false);
                    mySpriteRenderer.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
                    GameSystem.GameManager.instance.DomePartChanged(true);
                    if (coroutine != null)
                    {
                        StopCoroutine(coroutine);
                    }
                }
            }    
        }

        public void SetRepaired()
        {
            canRepair = false;
            isRepairing = false;
            deltaTimer = 0.0f;
            coroutine = null;
            allertPoint.SetActive(false);
        }

        // Возможно, начало ремонта будет производиться по другому
        void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                /*
                float timeR = GameManager.instance.player[col.gameObject].GetRepairTime();
                isRepairing = true;
                coroutine = StartRepair(timeR);
                StartCoroutine(coroutine);
                */
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
    }
}