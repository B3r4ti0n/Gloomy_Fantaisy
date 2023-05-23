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
    [SerializeField] private string url_test;
    [SerializeField] private List<string> url_params_key_test;
    [SerializeField] private List<string> url_params_value_test;

    public static UserLogged userLogged = new UserLogged();
    // Start is called before the first frame update
    void Start(){
        
        if (LoginScript.userLogged._id != null){
            userLogged = LoginScript.userLogged;
        }else{
            userLogged = CreateStatScript.userLogged;
        }

        string id = LoginScript.userLogged.ID_Stats;
        List<string> url_params_value = new List<string>();
        MongoDBScript mongoDBScript = new MongoDBScript();

        url_params_value.Add(id);

        string testResult = mongoDBScript.CreateUrlBodyRequest(url_params_key_test,url_params_value);
        StartCoroutine(mongoDBScript.GetRequest(url_test,testResult,(result)=>{
            // Convert result string to object
            var responseJson = JsonUtility.FromJson<Stats>(result);
            userLogged.stats = responseJson;

            //Change the text by the UserLogged data in LoginScript
            nameText.text = userLogged.name;
            lvlText.text = userLogged.stats.level.ToString();
            expText.text = userLogged.stats.exp.ToString();
            goldText.text = userLogged.stats.gold.ToString();
            pgoldText.text = userLogged.stats.premium_gold.ToString();

            if (userLogged.stats.race == "Orc")
            {
                avatarImg.texture = orcTexture;
            }else if (userLogged.stats.race == "Elf")
            {
                avatarImg.texture = elfTexture;
            }else
            {
                avatarImg.texture = undeadTexture;
            }
            //userLogged.stats.Race
            // avatarText.text = testImg;
            return false;
        }));

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
