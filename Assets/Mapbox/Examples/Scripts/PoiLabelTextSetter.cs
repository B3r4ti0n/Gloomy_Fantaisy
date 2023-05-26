using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mapbox.Examples
{
    using Mapbox.Unity.MeshGeneration.Interfaces;
    using System.Collections.Generic;
    using UnityEngine.UI;

    public class PoiLabelTextSetter : MonoBehaviour, IFeaturePropertySettable
    {
        [SerializeField]
        Text _text; // Reference to the Text component for displaying the label
        [SerializeField]
        Image _background; // Reference to the Image component for the background

        [SerializeField]
        GameObject prefabMap;

        private Dictionary<string, string> poiData; // Stores the data associated with the point of interest

        private bool isPointerDown = false;
        private float pointerDownTime = 0f;
        private float clickDelay = 0.1f;

        public void Set(Dictionary<string, object> props)
        {
            _text.text = ""; // Clear the label text

            // Check if the properties dictionary contains certain keys and set the label text accordingly
            if (props.ContainsKey("name"))
            {
                _text.text = props["name"].ToString();
            }
            else if (props.ContainsKey("house_num"))
            {
                _text.text = props["house_num"].ToString();
            }
            else if (props.ContainsKey("type"))
            {
                _text.text = props["type"].ToString();
            }

            // Convert the properties to a dictionary of string-string pairs
            poiData = new Dictionary<string, string>();
            foreach (var prop in props)
            {
                poiData[prop.Key] = prop.Value.ToString();
            }

            RefreshBackground(); // Refresh the background layout
        }
public void RefreshBackground()
        {
            RectTransform backgroundRect = _background.GetComponent<RectTransform>();
            LayoutRebuilder.ForceRebuildLayoutImmediate(backgroundRect); // Force the immediate rebuild of the layout for the background
        }

        void OnMouseDown()
        {
            isPointerDown = true;
            pointerDownTime = Time.time;
        }

        void OnMouseUp()
        {
            isPointerDown = false;

            if (Time.time - pointerDownTime <= clickDelay)
            {
                if (prefabMap.name == "LabelIcon")
                {
                    ClickTest();
                }
                else if (prefabMap.name == "SkeletonWarrior")
                {
                    SceneManager.LoadScene("PVEBattleScene");
                }
                else
                {
                    Debug.Log("no prefab");
                }
            }
        }

        void ClickTest()
        {
            Debug.Log(_text.text); // Log the label text
            PopUpManager popupManager = FindObjectOfType<PopUpManager>(); // Find the PopUpManager component in the scene
            if (popupManager != null)
            {
                if (!popupManager.IsPopupActive()) // Check if a pop-up is already active
                {
                    Debug.Log("Pop-up activated");
                    popupManager.ActivatePopup(poiData); // Activate the pop-up and pass the poiData
                }
                else
                {
                    Debug.Log("A pop-up is already active.");
                }
            }
            else
            {
                Debug.LogWarning("Pop-up manager not found in the scene.");
            }
        }
    }
}