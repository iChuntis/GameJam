using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pick : MonoBehaviour
{
    protected bool ifSegmentBrocken = false;

    protected UI_Manager UI_Manager;

    protected GroupOfVolunteers vol;

    public GroupOfVolunteers Vol
    {
        set => vol = value;
    }

    protected bool canGet = true;
    public bool CanGet
    {
        get => canGet;
        set => canGet = value;
    }

    private void Awake()
    {
        Messenger<UI_Manager>.AddListener("InitUI", InitUI);
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

        if (canGet)
        {
            if (gameObject.tag == "Troubles" && ifSegmentBrocken)
            {
                UI_Manager.ShowUI(this);
            }
            else if(gameObject.tag != "Troubles")
            {
                UI_Manager.ShowUI(this);
            }
        }
    }
}
