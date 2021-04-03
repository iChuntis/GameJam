using UnityEngine;
using UnityEngine.EventSystems;


public class People : Pick , Walking
{
    private int pCount = 1;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Volounteer")
        {
            var script = GameA.singleton.volunteers[other.gameObject];
            if (script == vol)
            {
                moving = true;
                script.PeopleCheckPoint();
            }
        }
    }

    private void FixedUpdate()
    {
        speed = vol.Speed;
        if(moving)
        {
            rb.MovePosition(Vector2.MoveTowards(rb.position, Vector2.zero , maxDelta * speed * Time.deltaTime * pCount / 10));
        }

        if(rb.position == Vector2.zero)
        {
            moving = false;
        }
    }

}
