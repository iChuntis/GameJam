using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameA : MonoBehaviour
{
    public static GameA singleton;

    public Dictionary<GameObject, GroupOfVolunteers> volunteers;

    [SerializeField] private int volunteersTotalCount;

    [SerializeField] private GameObject vol_pref;

    public int VCount
    {
        get => volunteersTotalCount;
    }

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        volunteers = new Dictionary<GameObject, GroupOfVolunteers>();
    }

    public void SendVolunteers(in int count , Pick pick)
    {
        volunteersTotalCount -= count;

        var group = Instantiate(vol_pref , Vector2.zero , Quaternion.identity);

        var script = group.GetComponent<GroupOfVolunteers>();

        pick.Vol = script;

        volunteers.Add(group, script);

        script.Init(count, pick);

    }
}
