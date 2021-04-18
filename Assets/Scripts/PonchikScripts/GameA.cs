using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameA : MonoBehaviour
{
    public static GameA singleton;

    public Dictionary<GameObject, GroupOfVolunteers> volunteers;
    public Dictionary<GameObject, People> people;

    [SerializeField]private int colvo;

    [SerializeField] private Text vCount;

    //[SerializeField] private Text pCount;


    [SerializeField] private int peopleCount;

    public void People(int people)
    {
        peopleCount += people ;

        if(peopleCount >= colvo)
        {
            volunteersTotalCount += peopleCount / 5;
            peopleCount %= colvo;
        }
        Debug.Log("People COUNT : " + peopleCount);
        GameSystem.GameManager.instance.ChangePeople(people);
    }

    public void SetVolounteers(int volounteers)
    {
        volunteersTotalCount += volounteers;
        vCount.text = volunteersTotalCount.ToString();
    }

    [SerializeField] private int volunteersTotalCount;

    [SerializeField] private GameObject vol_pref;

    public int VCount
    {
        get => volunteersTotalCount;
    }

    private void Start()
    {
        vCount.text = volunteersTotalCount.ToString();
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

        people = new Dictionary<GameObject, People>();
    }

    private IEnumerator check()
    {
        yield return new WaitForSeconds(7f);
        if (volunteersTotalCount == 0)
        {
            volunteersTotalCount += 1;
        }
        vCount.text = volunteersTotalCount.ToString();
        yield return null;
    }


    public void SendVolunteers(in int count , Pick pick)
    {
        volunteersTotalCount -= count;

        if(volunteersTotalCount == 0)
        {
            StartCoroutine(check());
        }

        vCount.text = volunteersTotalCount.ToString();

        var group = Instantiate(vol_pref , new Vector3(0, 0, -3f), Quaternion.identity);

        var script = group.GetComponent<GroupOfVolunteers>();

        pick.Vol = script;

        volunteers.Add(group, script);

        script.Init(count, pick);

        pick.CanGet = false;
    }
}
