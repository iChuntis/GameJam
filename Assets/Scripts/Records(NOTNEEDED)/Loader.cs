using UnityEngine;
using UnityEngine.UI;


public class Loader : MonoBehaviour
{
    private Text recordText;
    private Text numeration;

    private void Awake()
    {
        recordText = GetComponent<Text>();
        numeration = GetComponentInChildren<Text>();
    }

    public void SetRecord(string record)
    {
        recordText.text = record;
    }
    public void SetNum(string num)
    {
        numeration.text = num;
    }
}
