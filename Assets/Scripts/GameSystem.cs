using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{


    [SerializeField] private GameObject volunteers;

    public void SendVolunteers(in int count,in Vector2 position)
    {
        var vol = Instantiate(volunteers);
        vol.GetComponent<GroupOfVolunteers>().Init(count, position);
    }
}
