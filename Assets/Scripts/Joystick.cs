using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEditor;

public class Joystick : MonoBehaviour , IDragHandler,IPointerUpHandler,IPointerDownHandler
{
    private Image bgImg;
    private Image joystickImg;
    
    public Vector3 InputDirection { set; get;}

    private void Start()
    {
        bgImg = GetComponent<Image>();
        joystickImg = transform.GetChild(0).GetComponent<Image>();
        InputDirection = Vector3.zero;
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 position = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform,ped.position,ped.pressEventCamera,out position)){
            {
                //Faire bouger le joystick
                position.x = (position.x / bgImg.rectTransform.sizeDelta.x);
                position.y = (position.y / bgImg.rectTransform.sizeDelta.y);

                float x = (bgImg.rectTransform.pivot.x == 1) ? position.x * 2 + 1 : position.x * 2 - 1;
                float y = (bgImg.rectTransform.pivot.y == 1) ? position.y * 2 + 1 : position.y * 2 - 1;

                InputDirection = new Vector3(x, 0, y);

                //Bloquer le Joystick dans la zone du Joystick
                InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;

                joystickImg.rectTransform.anchoredPosition = new Vector3(
                    InputDirection.x * (bgImg.rectTransform.sizeDelta.x / 3),
                    InputDirection.z * (bgImg.rectTransform.sizeDelta.y / 3));
            }
        }
    }
    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        //Remettre le Joystick au centre une fois qu'il est relaché
        InputDirection = Vector3.zero;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;
    }
}
