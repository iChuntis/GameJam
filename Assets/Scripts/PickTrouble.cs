using UnityEngine;
using UnityEngine.EventSystems;


public class PickTrouble : MonoBehaviour , IPointerClickHandler 
{

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log("HERE CLICK ");
        Debug.Log(pointerEventData.position);
    }

}
