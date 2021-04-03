using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GroupOfVolunteers : MonoBehaviour ,Walking
{
    private Rigidbody2D rb;

    [SerializeField] private Text count;

    private int int_count;
    public int Count
    {
        get => int_count;
    }

    [SerializeField] private float maxDelta;

    [SerializeField] private float speed;
    public float Speed
    {
        get => speed;
    }

    private Pick point_B;

    private Vector2 pos;

    private bool checkPoint = false;

    private bool moving = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
            rb.MovePosition(Vector2.MoveTowards(rb.position , pos , maxDelta * speed));
    }

    public void PeopleCheckPoint()
    {
        pos = Vector2.zero;
    }

    public void FixCheckPoint()
    {
        moving = false;
        pos = Vector2.zero;
    }

    public void FixingFinish()
    {
        moving = true;
    }
}
