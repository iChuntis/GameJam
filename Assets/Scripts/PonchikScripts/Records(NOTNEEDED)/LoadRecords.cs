using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadRecords : MonoBehaviour
{
    [SerializeField] private GameObject pref;

    private RecordList records;

    private Vector3 state = new Vector3(0,-20 , 0);

    private Transform Image;

    private void Start()
    {
        Image = transform.GetChild(0).transform;
        records = Resources.Load<RecordList>("Records/RecordsList");


        for(int i =0; i < records.GetRecord.Count; i++)
        {
            var s = Instantiate(pref);
            s.transform.parent = Image;
            s.transform.position = state;

            state += new Vector3(0, -20 , 0);

            s.transform.SendMessage("SetRecord", records.GetRecord[i].ToString());
            s.transform.SendMessage("SetNum", (i+1).ToString());

           
        }
    }

}
