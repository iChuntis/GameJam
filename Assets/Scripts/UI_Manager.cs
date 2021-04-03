using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private RectTransform uiMenuToChoose;

    [SerializeField] private Text vCountToSend;

    private int volunteersCountToSend;

    private Pick point_B;

    private void Start()
    {
        Messenger<UI_Manager>.Broadcast("InitUI", this);
    }

    private void Update()
    {
        //uiMenuToChoose.position = (Vector2)Input.mousePosition  + UI_Offset;
    }
    public void ShowUI(Pick pickedOne)
    {
        vCCHange("0");

        point_B = pickedOne;

        volunteersCountToSend = 0;

        uiMenuToChoose.gameObject.SetActive(true);
    }

    private void vCCHange(in string count)
    {
        vCountToSend.text = count;
    }


    //send volunteers to point
    public void Send()
    {
        if(volunteersCountToSend != 0 && point_B.CanGet)
        {
            GameA.singleton.SendVolunteers(volunteersCountToSend , point_B);
        }

        point_B = null;

        uiMenuToChoose.gameObject.SetActive(false);

        Messenger.Broadcast("SetOffCover");
    }

    //adding volunteers to send
    public void AddV_UI()
    {
        if(GameA.singleton.VCount > volunteersCountToSend)
            volunteersCountToSend++;

        vCCHange(volunteersCountToSend.ToString());
    }

    //subtracting volunteers to send
    public void SubV_UI()
    {
        volunteersCountToSend--;

        vCCHange(volunteersCountToSend.ToString());
    }
}
