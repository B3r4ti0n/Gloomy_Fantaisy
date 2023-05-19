using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpContent : MonoBehaviour
{
    [SerializeField]
    public Text poiNameText;

    public void SetPoiName(string poiName)
    {
        poiNameText.text = poiName;
    }
}
