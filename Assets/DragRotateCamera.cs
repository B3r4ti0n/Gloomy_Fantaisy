using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRotateCamera : MonoBehaviour
{

    public GameObject Player;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Debug.Log("vrai:"+touch);
        }
        else
        {
        }
    }
}
