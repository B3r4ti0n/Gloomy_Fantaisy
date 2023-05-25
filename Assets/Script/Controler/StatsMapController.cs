using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsMapController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI lvlText;
    [SerializeField] private TextMeshProUGUI lvlUpText;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI pgoldText;
    [SerializeField] private RawImage avatarImg;
    [SerializeField] private Texture orcTexture;
    [SerializeField] private Texture elfTexture;
    [SerializeField] private Texture undeadTexture;
    [SerializeField] private GameObject orcObject;
    [SerializeField] private GameObject elfObject;
    [SerializeField] private GameObject undeadObject;
    [SerializeField] private string url_test;
    [SerializeField] private string url_getlvlUp;
    [SerializeField] public string url_update;
    [SerializeField] private List<string> url_params_key_test;
    [SerializeField] private List<string> url_params_key_lvlUp;
    [SerializeField] private List<string> url_params_value_test;
    [SerializeField] private List<string> url_params_key_update_lvlup;
    [SerializeField] private List<string> url_params_value_update_lvlup;
     private PopUpManager popupManager; 

    public static UserLogged userLogged = new UserLogged();
    public static LevelUp lvlUp = new LevelUp();
    // Start is called before the first frame update
    void Start(){
<<<<<<< HEAD

        popupManager = FindObjectOfType<PopUpManager>();

=======
    
>>>>>>> main
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
            pgoldText.text = userLogged.stats.gold_premium.ToString();

            if (userLogged.stats.race == "Orc")
            {
                avatarImg.texture = orcTexture;
                orcObject.SetActive(true);
            }else if (userLogged.stats.race == "Elf")
            {
                avatarImg.texture = elfTexture;
                elfObject.SetActive(true);
            }else if (userLogged.stats.race == "Undead")
            {
                avatarImg.texture = undeadTexture;
                undeadObject.SetActive(true);
            }else
            {
                Debug.Log("no race");
            }
            //userLogged.stats.Race
            // avatarText.text = testImg;
            List<string> url_params_lvlUp = new List<string>();
            string lvl = userLogged.stats.level.ToString();
            url_params_lvlUp.Add(lvl);

            string lvlUpResult = mongoDBScript.CreateUrlBodyRequest(url_params_key_lvlUp,url_params_lvlUp);
            StartCoroutine(mongoDBScript.GetRequest(url_getlvlUp,lvlUpResult,(result)=>{
                
                var responseJson = JsonUtility.FromJson<LevelUp>(result);
                lvlUp = responseJson;

                lvlUpText.text = lvlUp.exp.ToString();

                return false;
            }));
            return false;
        }));

    }

    public void Update()
    {
        goldText.text = userLogged.stats.gold.ToString();
        expText.text = userLogged.stats.exp.ToString();
        lvlUpText.text = lvlUp.exp.ToString();

        if(lvlUp.exp != 0){

            if(userLogged.stats.exp >= lvlUp.exp){
                userLogged.stats.level = userLogged.stats.level + 1;
                userLogged.stats.exp = userLogged.stats.exp - lvlUp.exp;
                userLogged.stats.level_point = userLogged.stats.level_point + lvlUp.level_point;

                popupManager.OpenLvlUpPopup();
            }
        }
        
    }
    // Update is called once per frame
    public void ActivatePopUp()
    {
        popupManager.OpenStatsPopup();
    }

    public void ClosePopUp()
    {
        popupManager.CloseStatsPopup();
    }

    public void CloseLvlUp()
    {
        popupManager.CloseLvlUpPopup();

        url_params_value_update_lvlup.Add(userLogged.stats._id.ToString());
        url_params_value_update_lvlup.Add(userLogged.stats.level.ToString());
        url_params_value_update_lvlup.Add(userLogged.stats.exp.ToString());
        url_params_value_update_lvlup.Add(userLogged.stats.gold.ToString());
        url_params_value_update_lvlup.Add(userLogged.stats.level_point.ToString());

        MongoDBScript mongoDBScript = new MongoDBScript();
        Debug.Log(url_params_value_update_lvlup);
        
        string updateResult = mongoDBScript.CreateUrlBodyRequest(url_params_key_update_lvlup,url_params_value_update_lvlup);
        
        StartCoroutine(mongoDBScript.GetRequestPatch(url_update,updateResult,()=>{

            List<string> url_params_lvlUp = new List<string>();
            string lvl = userLogged.stats.level.ToString();
            url_params_lvlUp.Add(lvl);

            string lvlUpResult = mongoDBScript.CreateUrlBodyRequest(url_params_key_lvlUp,url_params_lvlUp);
            Debug.Log(lvlUpResult);
            StartCoroutine(mongoDBScript.GetRequest(url_getlvlUp,lvlUpResult,(result)=>{
                Debug.Log(result);
                var responseJson = JsonUtility.FromJson<LevelUp>(result);
                lvlUp = responseJson;

                lvlUpText.text = lvlUp.exp.ToString();

                Debug.Log(lvlUp.exp);
                url_params_lvlUp.Clear();
                return false;
            }));
            url_params_value_update_lvlup.Clear();
            return false;
        }));
    }
}
