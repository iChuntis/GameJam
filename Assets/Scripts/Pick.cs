using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pick : MonoBehaviour
{

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

}
