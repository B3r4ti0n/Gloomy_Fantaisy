using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsMapController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI lvlText;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI pgoldText;
    [SerializeField] private TextMeshProUGUI avatarText;
    // Start is called before the first frame update
    void Start()
{
    //Change the text by the UserLogged data in LoginScript
    nameText.text = LoginScript.userLogged.name;
    lvlText.text = LoginScript.userLogged.stats.ID_Level.ToString();
    expText.text = LoginScript.userLogged.stats.Exp.ToString();
    goldText.text = LoginScript.userLogged.stats.Gold.ToString();
    pgoldText.text = LoginScript.userLogged.stats.Premium_gold.ToString();
    avatarText.text = LoginScript.userLogged.stats.Race;

}
    // Update is called once per frame
    void Update()
    {
        
    }
}
