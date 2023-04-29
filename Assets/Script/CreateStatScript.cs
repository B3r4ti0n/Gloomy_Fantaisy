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
        if (LoginScript.userLogged.id != 0)
        {
            userLogged = LoginScript.userLogged;
        }else
        {
            userLogged = RegisterScript.userLogged;
        }
        //Base stats on create account
        string id = ""+userLogged.id;
        string Race = nameRace.text;
        string ID_Level = "1";
        string Exp = "0";
        string Gold = "0";
        string Premium_gold = "0";
        string Health_Point = "100";
        string Offensive_value = "10";
        string Defensive_value = "10";
        string Intelligence_value = "10";
        string Speed_value = "10";
        string Mana_value = "100";

        //Params Url
        List<string> url_params_value = new List<string>();
        
        url_params_value.Add(id);
        url_params_value.Add(Race);
        url_params_value.Add(ID_Level);
        url_params_value.Add(Exp);
        url_params_value.Add(Gold);
        url_params_value.Add(Premium_gold);
        url_params_value.Add(Health_Point);
        url_params_value.Add(Offensive_value);
        url_params_value.Add(Defensive_value);
        url_params_value.Add(Intelligence_value);
        url_params_value.Add(Speed_value);
        url_params_value.Add(Mana_value);

        if (url_params_value == null){
            url_params_value = url_params_value_test;
        }

        // Create string with params Url
        string paramsURL = "{";
        if(url_params_key_test.Count == url_params_value.Count) {
            for(int nKey = 0; nKey <= url_params_key_test.Count-1; nKey++) {
                if (nKey < url_params_key_test.Count-1){
                    paramsURL+="\""+url_params_key_test[nKey]+"\":\""+url_params_value[nKey]+"\",";
                }else{
                    paramsURL+="\""+url_params_key_test[nKey]+"\":\""+url_params_value[nKey]+"\"";
                }
            }
        }
        paramsURL+="}";
        // Start function GetRequest to async method
        StartCoroutine(GetRequest(paramsURL));
    
    }

    IEnumerator GetRequest(string paramsURL){
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(paramsURL);

        // Create a Put request with url target
        UnityWebRequest request = UnityWebRequest.Put(url_test, "application/json");
        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);

        // Create a download handler to recover result request
        DownloadHandler downloadHandler = new DownloadHandlerBuffer();
        request.downloadHandler = downloadHandler;
        
        // Send request
        yield return request.SendWebRequest();
 
        // Check if request result done or error
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            // Convert DownloadHandler type in string UTF8
            responseText = System.Text.Encoding.UTF8.GetString(downloadHandler.data);

            // Convert responseText string in object
            var responseJson = JsonUtility.FromJson<UserLogged>(responseText);
            CreateStatScript.userLogged = responseJson;
            Debug.Log(CreateStatScript.userLogged.stats.Gold);
            Debug.Log(CreateStatScript.userLogged.stats.Race);
            
            SceneManager.LoadScene("MapScene");

            yield return responseText;
        }
    }
}
