using UnityEngine;
using UnityEngine.EventSystems;


public class Pick : MonoBehaviour 
{
    [SerializeField] private GameObject coverPref;

    private UI_Manager UI_Manager;

    private GameObject cover;

    private void Awake()
    {
        Messenger<UI_Manager>.AddListener("InitUI", InitGameManager);

        Messenger.AddListener("SetOffCover",SetOff);
    }

    private void SetOff()
    {
        Destroy(cover);
    }

    private void InitGameManager(UI_Manager gm)
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

        UI_Manager.ShowUI(transform.position);

        if(cover == null)
            cover = Instantiate(coverPref, transform.position , Quaternion.identity);
    }

}
