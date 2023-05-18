using UnityEngine;
using UnityEngine.EventSystems;

namespace Mapbox.Examples
{
    using Mapbox.Unity.MeshGeneration.Interfaces;
    using System.Collections.Generic;
    using UnityEngine.UI;

    public class PoiLabelTextSetter : MonoBehaviour, IFeaturePropertySettable
    {
        [SerializeField]
        Text _text;
        [SerializeField]
        Image _background;

        public void Set(Dictionary<string, object> props)
        {
            _text.text = "";

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
            RefreshBackground();
        }

        public void RefreshBackground()
        {
            RectTransform backgroundRect = _background.GetComponent<RectTransform>();
            LayoutRebuilder.ForceRebuildLayoutImmediate(backgroundRect);
        }

        void OnMouseDown()
        {
            Debug.Log("Clic détecté !");
            ClickTest();
        }

        void ClickTest()
        {
            Debug.Log(_text.text);
            PopUpManager popupManager = FindObjectOfType<PopUpManager>();
            if (popupManager != null)
            {
                Debug.Log("PopUpActiver");
                popupManager.ActivatePopup();
            }
            else
            {
                Debug.LogWarning("Le gestionnaire de pop-up n'a pas été trouvé dans la scène.");
            }
        }
    }
}
