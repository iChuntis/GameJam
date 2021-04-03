using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GroupOfVolunteers : MonoBehaviour 
{
    private Rigidbody2D rb;

    [SerializeField] private Text count;

    [SerializeField]private int int_count;

    private int peopleCount = 0;
    public int People
    {
        set => peopleCount = value;
    }
    public int Count
    {
        get => int_count;
        set => int_count = value;
    }

    [SerializeField] private float maxDelta;

    [SerializeField] private float speed;


    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    private Pick point_B;

    private Vector2 pos;

    private bool checkPoint = false;

    private bool moving = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(update());
    }

    [SerializeField] private float probability = 0;
    [SerializeField] private float timeStep = 8;
    private float lastTime = 0;

    [SerializeField] private float checkEverySec;

    private IEnumerator dieOrNot()
    {
        bool die = false;
        while(!die)
        {
            yield return new WaitForSeconds(checkEverySec);
            var rand = Random.Range(0, 100);
            if(probability > rand)
            {
                //die
                int_count -= 1;
                die = true;
            }
        }
        if(int_count == 0)
        {
            Destroy(gameObject);
        }
    }

    public void Init(in int count , Pick point_B )
    {
        int_count = count;

        this.count.text = count.ToString();

        this.point_B = point_B;

        pos = point_B.transform.position;

        moving = true;
    }

    private void FixedUpdate()
    {
        if (moving)
            rb.MovePosition(Vector2.MoveTowards(rb.position , pos , maxDelta * speed * Time.deltaTime * (int_count + peopleCount)));

        if (checkPoint && rb.position == Vector2.zero)
        {
            moving = false;
            GameA.singleton.SetVolounteers(int_count);
            Destroy(this.gameObject);
        }
    }

    private IEnumerator update()
    {
        bool firstTime = true;
        lastTime = Time.time;
        while (true)
        {
            if (Time.time >= lastTime + timeStep)
            {
                if (firstTime)
                {
                    firstTime = false;
                    timeStep = 6f;
                    probability = 1f;
                    Debug.Log("FIRST TIME ! ");
                }
                else
                {
                    Debug.Log(" TIME ! ");
                    probability += 5f;
                    probability = Mathf.Min(probability, 65);
                }
                lastTime = Time.time;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void PeopleCheckPoint()
    {
        pos = Vector2.zero;
        checkPoint = true;
    }

    public void FixCheckPoint()
    {
        moving = false;
        pos = Vector2.zero;
        checkPoint = true;
        Debug.Log("Next move to: " + pos);
    }

    public void FixingFinish()
    {
        moving = true;  
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("InnerDome"))
        {
            Debug.Log("EXITED INNER DOME");

            StartCoroutine(update());

            for(int i = 0; i < int_count; i++)
            {
                StartCoroutine(dieOrNot());
            }
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("InnerDome"))
        {
            Debug.Log("Entered INNER DOME");
            StopAllCoroutines();
        }
    }
}
