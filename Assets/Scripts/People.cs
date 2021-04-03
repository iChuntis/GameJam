using UnityEngine;
using System.Collections;


public class People : Pick 
{
    [SerializeField]private int pCount = 1;

    private bool volounteersCame = false;
    public int Count
    {
        get => pCount;
        set => pCount = value;
    }

    private Rigidbody2D rb;

    private bool moving = false;

    private float maxDelta = 0.01f;

    private float speed;
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    [SerializeField] private float probability = 0;
    [SerializeField] private float timeStep = 8;
    private float lastTime = 0;
    private bool firstTime;

    public void InitUI(UI_Manager ui)
    {
        UI_Manager = ui;
    }
    private IEnumerator dieOrNot()
    {
        while (gameObject != null)
        {
            yield return new WaitForSeconds(1);
            var rand = Random.Range(0, 100);
            if (probability > rand)
            {
                //die
                pCount -= 1;
                GameSystem.GameManager.instance.ChangePeople(-1); 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Volounteer")
        {
            var script = GameA.singleton.volunteers[other.gameObject];
            if (script == vol)
            {
                Debug.Log("Volounteer has come");
                moving = true;
                script.PeopleCheckPoint();
                script.People = pCount;
                volounteersCame = true;
            }
        }
        if (other.CompareTag("InnerDome"))
        {
            StopAllCoroutines();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("InnerDome"))
        {
            Debug.Log("EXITED INNER DOME");
            StartCoroutine(dieOrNot());
        }
    }
    private void FixedUpdate()
    {
        if(moving)
        {
            speed = vol.Speed;
            rb.MovePosition(Vector2.MoveTowards(rb.position, Vector2.zero, maxDelta * speed * Time.deltaTime * (pCount + vol.Count)));
        }

        if(rb.position == Vector2.zero)
        {
            moving = false;
            GameA.singleton.People(pCount);
            Destroy(gameObject);
        }

        if (volounteersCame)
            if(vol.Count == 0)
                Destroy(gameObject);
    }

}
