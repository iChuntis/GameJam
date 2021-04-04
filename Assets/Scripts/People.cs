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
        transform.rotation = Quaternion.LookRotation(Vector3.zero, Vector3.forward);
    }

    [SerializeField] private float probability = 0;
    [SerializeField] private float timeStep = 8;
    private float lastTime = 0;
    private bool firstTime;

    public void InitUI(UI_Manager ui)
    {
        UI_Manager = ui;
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
    private void FixedUpdate()
    {

        transform.rotation = Quaternion.LookRotation(Vector2.zero, Vector3.forward);
        if (moving)
        {
            speed = vol.SpeedBack;
            rb.MovePosition(Vector2.MoveTowards(rb.position, Vector2.zero, maxDelta * speed * Time.deltaTime ));
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
