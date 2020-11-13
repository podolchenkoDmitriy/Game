using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerDown : MonoBehaviour, IPointerDownHandler
{

    public static  bool buttonPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
        Invoke("ButtonPressedFalse", 0.5f);
    }

    void ButtonPressedFalse()
    {
        buttonPressed = false;
      
    }
}
