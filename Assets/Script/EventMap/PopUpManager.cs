using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    public GameObject popupPrefab; // Reference to the prefab for the pop-up
    private Dictionary<string, GameObject> activePopups = new Dictionary<string, GameObject>(); // Dictionary to store references to active pop-ups, using POI names as keys

    public void ActivatePopup(Dictionary<string, string> poiData)
    {
        string poiName = poiData.ContainsKey("name") ? poiData["name"] : "Unknown"; // Get the POI name from the poiData dictionary, or set it as "Unknown" if not found

        if (!activePopups.ContainsKey(poiName))
        {
            GameObject popupInstance = Instantiate(popupPrefab, transform); // Instantiate the pop-up prefab as a child of the manager
            activePopups.Add(poiName, popupInstance); // Add the pop-up reference to the activePopups dictionary

            PopUpContent popupContent = popupInstance.GetComponent<PopUpContent>(); // Get the PopUpContent component from the pop-up instance
            if (popupContent != null)
            {
                popupContent.SetPoiName(poiName); // Set the POI name in the PopUpContent component
            }
            else
            {
                Debug.LogWarning("PopUpContent component not found on the pop-up prefab.");
            }
        }

        GameObject activePopup = activePopups[poiName]; // Get the active pop-up instance
        activePopup.SetActive(true); // Set the pop-up to be active and visible
    }

    public bool IsPopupActive()
    {
        foreach (var kvp in activePopups)
        {
            if (kvp.Value.activeSelf) // Check if the pop-up is active
            {
                return true;
            }
        }
        return false; // Return false if no active pop-up is found
    }
}
