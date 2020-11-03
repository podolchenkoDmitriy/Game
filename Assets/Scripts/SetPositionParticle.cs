using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositionParticle : MonoBehaviour
{

    private void OnEnable()
    {
        Vector2 topRight = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);

        
        transform.position = Camera.main.ScreenToWorldPoint(topRight);

    }

    
}
