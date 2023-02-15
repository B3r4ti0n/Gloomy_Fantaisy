using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListener : MonoBehaviour
{
    [SerializeField]
    private GameControler gameControler;
    private Button attackButton;

    // Start is called before the first frame update
    void Start()
    {
        attackButton = GameObject.Find("AttackButton").GetComponent<Button>();
        attackButton.onClick.AddListener(gameControler.MakePlayerAttack);
    }

}
