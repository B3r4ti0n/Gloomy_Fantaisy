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

    public static UserLogged userLogged = new UserLogged();
    // Start is called before the first frame update
    void Start()
{
    Debug.Log(CreateStatScript.userLogged.name);
    Debug.Log(CreateStatScript.userLogged.stats.Race);
    if (LoginScript.userLogged.id != 0)
    {
        userLogged = LoginScript.userLogged;
    }else
    {
        userLogged = CreateStatScript.userLogged;
    }
    //Change the text by the UserLogged data in LoginScript
    nameText.text = userLogged.name;
    lvlText.text = userLogged.stats.ID_Level.ToString();
    expText.text = userLogged.stats.Exp.ToString();
    goldText.text = userLogged.stats.Gold.ToString();
    pgoldText.text = userLogged.stats.Premium_gold.ToString();
    avatarText.text = userLogged.stats.Race;

}
    // Update is called once per frame
    void Update()
    {
        
    }
}
