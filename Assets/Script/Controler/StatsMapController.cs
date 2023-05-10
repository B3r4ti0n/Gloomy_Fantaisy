using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsMapController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI lvlText;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI pgoldText;
    [SerializeField] private RawImage avatarImg;
    [SerializeField] private Texture orcTexture;
    [SerializeField] private Texture elfTexture;
    [SerializeField] private Texture undeadTexture;

    public static UserLogged userLogged = new UserLogged();
    // Start is called before the first frame update
    void Start()
{
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
    if (userLogged.stats.Race == "Orc")
    {
        avatarImg.texture = orcTexture;
    }else if (userLogged.stats.Race == "Elf")
    {
        avatarImg.texture = elfTexture;
    }else
    {
        avatarImg.texture = undeadTexture;
    }
    //userLogged.stats.Race
    // avatarText.text = testImg;

}
    // Update is called once per frame
    void Update()
    {
        
    }
}
