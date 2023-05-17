using UnityEngine;
using UnityEngine.EventSystems;

namespace Mapbox.Examples
{
    using Mapbox.Unity.MeshGeneration.Interfaces;
    using System.Collections.Generic;
    using UnityEngine.UI;

    public class PoiLabelTextSetter : MonoBehaviour, IFeaturePropertySettable, IPointerClickHandler
    {
        [SerializeField]
        Text _text;
        [SerializeField]
        Image _background;

        public void Set(Dictionary<string, object> props)
        {
			Debug.Log("JE SUIS UN TESTTTT");
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

        public void OnPointerClick(PointerEventData eventData)
        {
            // Ajouter ici le code pour gérer le clic sur le texte du POI, par exemple pour ouvrir une fenêtre contextuelle avec des informations supplémentaires sur le POI.
            Debug.Log("Clic sur le POI : " + _text.text);
        }

        void OnMouseDown()
        {
            Debug.Log("Clic détecté !");
            ClickTest();
        }

        void ClickTest()
        {
            Debug.Log(_text.text);
        }
    }
}
