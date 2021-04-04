using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CR RecordList" , menuName = "Record List")]
public class RecordList : ScriptableObject
{
    [SerializeField]private List<float> recordList;

    public List<float> GetRecord
    {
        get { return recordList; }
    }

    private void OnEnable()
    {
        Messenger.AddListener("ExitGameFully",ExitGame);
        Messenger<float>.AddListener("MatchFinished" , AddRecordToList);
    }

    private void ExitGame()
    {
        recordList.Clear();
    }

    private void AddRecordToList(float value)
    {
        recordList.Add(value);
        recordList.Sort();
    }
}
