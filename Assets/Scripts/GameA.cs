using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameA : MonoBehaviour
{
    public static GameA singleton;

    public Dictionary<GameObject, GroupOfVolunteers> volunteers;
    public Dictionary<GameObject, People> people;


    [SerializeField] private Text vCount;


    [SerializeField] private int peopleCount;
    public void People(int people)
    {
        peopleCount += people ;

        if(peopleCount >= 3)
        {
            volunteersTotalCount += 1;
            peopleCount %= 3;
        }
        Debug.Log("People COUNT : " + peopleCount);
        GameSystem.GameManager.instance.ChangePeople(people);
    }

    public void SetVolounteers(int volounteers)
    {
        volunteersTotalCount += volounteers;
        vCount.text = "Volounteers : " + volunteersTotalCount.ToString();
    }

    [SerializeField] private int volunteersTotalCount;

    [SerializeField] private GameObject vol_pref;

    public int VCount
    {
        get => volunteersTotalCount;
    }

    private void Start()
    {
        vCount.text = "Volounteers : " + volunteersTotalCount.ToString();
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
        yield return new WaitForSeconds(10f);
        volunteersTotalCount += 1;
        vCount.text = "Volounteers : " + volunteersTotalCount.ToString();
        yield return null;
    }


    public void SendVolunteers(in int count , Pick pick)
    {
        volunteersTotalCount -= count;

        if(volunteersTotalCount == 0)
        {
            StartCoroutine(check());
        }

        vCount.text = "Volounteers : " + volunteersTotalCount.ToString();

        var group = Instantiate(vol_pref , Vector2.zero , Quaternion.identity);

        var script = group.GetComponent<GroupOfVolunteers>();

        pick.Vol = script;

        volunteers.Add(group, script);

        script.Init(count, pick);

        pick.CanGet = false;
    }
}
