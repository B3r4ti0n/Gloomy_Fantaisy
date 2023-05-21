using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpContent : MonoBehaviour
{
    [SerializeField]
    public Text poiNameText; // Reference to the text component for displaying the POI name

    public void SetPoiName(string poiName)
    {
        poiNameText.text = poiName; // Set the POI name in the text component
    }
}
