using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private RectTransform uiMenuToChoose;

    [SerializeField] private Text vCountToSend;

    private int volunteersCountToSend;

    private GameA gameManager;

    private Vector2 positionToGo;

    private void Start()
    {
        Messenger<UI_Manager>.Broadcast("InitUI", this);

        gameManager = GetComponent<GameA>();
    }

    private void Update()
    {
        //uiMenuToChoose.position = (Vector2)Input.mousePosition  + UI_Offset;
    }
    public void ShowUI(Vector2 position)
    {
        vCCHange("0");

        positionToGo = position;

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
        if(volunteersCountToSend != 0)
        {
            gameManager.SendVolunteers(volunteersCountToSend, positionToGo);
        }

        uiMenuToChoose.gameObject.SetActive(false);

        Messenger.Broadcast("SetOffCover");
    }

    //adding volunteers to send
    public void AddV_UI()
    {
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
