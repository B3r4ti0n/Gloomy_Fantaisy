using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    public GameObject popupPrefab;
    private Dictionary<string, GameObject> activePopups = new Dictionary<string, GameObject>(); // Références aux pop-ups actives par POI

    public void ActivatePopup(Dictionary<string, string> poiData)
    {
        string poiName = poiData.ContainsKey("name") ? poiData["name"] : "Unknown";

        if (!activePopups.ContainsKey(poiName))
        {
            GameObject popupInstance = Instantiate(popupPrefab, transform);
            activePopups.Add(poiName, popupInstance);

            PopUpContent popupContent = popupInstance.GetComponent<PopUpContent>();
            if (popupContent != null)
            {
                popupContent.SetPoiName(poiName);
            }
            else
            {
                Debug.LogWarning("Le composant PopUpContent n'a pas été trouvé sur le prefab de la pop-up.");
            }
        }
        GameObject activePopup = activePopups[poiName];
        activePopup.SetActive(true);
    }
    public bool IsPopupActive()
    {
        foreach (var kvp in activePopups)
        {
            if (kvp.Value.activeSelf)
            {
                return true;
            }
        }
        return false;
    }
}
