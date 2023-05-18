using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    public GameObject popup; // La référence vers la pop-up dans la scène

    public void ActivatePopup()
    {
        popup.SetActive(true);
    }
}
