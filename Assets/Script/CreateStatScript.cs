using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class CreateStatScript : MonoBehaviour
{
    [SerializeField]
    Button validButton;

    [SerializeField]
    Text nameRace;

    [SerializeField] private string url_test;
    [SerializeField] private List<string> url_params_key_test;
    [SerializeField] private List<string> url_params_value_test;
    [SerializeField] private string url_update;
    [SerializeField] private List<string> url_params_key_update;
    [SerializeField] private List<string> url_params_value_update;
    [SerializeField] private string responseText = "";

    public static UserLogged userLogged = new UserLogged();
    // Start is called before the first frame update
    void Start()
    {
        validButton.onClick.AddListener(CreateStats);
    }

    // Update is called once per frame
    void CreateStats()
    {
        if (LoginScript.userLogged._id != null)
        {
            userLogged = LoginScript.userLogged;
        }else
        {
            userLogged = RegisterScript.userLogged;
        }

        MongoDBScript mongoDBScript = new MongoDBScript();

        string race = nameRace.text;
        string exp = "0";
        string gold = "0";
        string premium_gold = "0";

        //Params Url
        List<string> url_params_value = new List<string>();
        
        url_params_value.Add(race);
        url_params_value.Add(exp);
        url_params_value.Add(gold);
        url_params_value.Add(premium_gold);
        
            
        string testResult = mongoDBScript.CreateUrlBodyRequest(url_params_key_test,url_params_value);

        // Start function GetRequest to async method
        StartCoroutine(mongoDBScript.GetRequest(url_test,testResult,(result)=>{
            // Convert responseText string in object
            var responseJson = JsonUtility.FromJson<Stats>(result);
            CreateStatScript.userLogged.stats = responseJson;
            
            if(CreateStatScript.userLogged.ID_Stats == "") {
                CreateStatScript.userLogged.ID_Stats = CreateStatScript.userLogged.stats._id;
                url_params_value_update.Add(CreateStatScript.userLogged.name);
                url_params_value_update.Add(CreateStatScript.userLogged.email);
                url_params_value_update.Add(CreateStatScript.userLogged.ID_Stats);
                string updateResult = mongoDBScript.CreateUrlBodyRequest(url_params_key_update,url_params_value_update);
                StartCoroutine(mongoDBScript.GetRequestPatch(url_update,updateResult,()=>{
                    return false;
                }));
            }
            SceneManager.LoadScene("MapScene");

            return false;
        }));
    
    }
}
