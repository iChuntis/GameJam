using UnityEngine;
using UnityEngine.EventSystems;


public class People : Pick , Walking
{
    private int pCount;

    public int Count
    {
        get => pCount;
    }

    private UI_Manager UI_Manager;

    private Rigidbody2D rb;

    private bool moving = false;

    private float maxDelta;

    private float speed;


    private void Awake()
    {
        Messenger<UI_Manager>.AddListener("InitUI", InitUI);
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var script = GameA.singleton.volunteers[other.gameObject];
        if (script == vol)
        {
            script.PeopleCheckPoint();
        }
    }


    private void InitUI(UI_Manager gm)
    {
        UI_Manager = gm;
    }

    private void OnMouseDown()
    {
        Debug.Log("MOUSE DOWN");
    }

    private void OnMouseUp()
    {
        Debug.Log("MOUSE UP");

        if(canGet)
            UI_Manager.ShowUI(this);
    }

}
