using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class LoginScript : MonoBehaviour
{
    //Adress Api
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private string url_test;
    [SerializeField] private List<string> url_params_key_test;
    [SerializeField] private List<string> url_params_value_test;
    [SerializeField] private string responseText = "";

    //New user Logged
    public static UserLogged userLogged = new UserLogged();

    // Get params with textfield and create paramsURL
    // Params:
    //  - url_params_key: List<string>
    //  - url_params_value: List<string>
    // Return: void
    public void OnLoginClick(){

        string username = usernameInputField.text;
        string password = passwordInputField.text;
        string email    = "arcoucou77@gmail.com";
        string ID_Stats = "64527af131a271b2201ff0af";
        List<string> url_params_value = new List<string>();
        MongoDBScript mongoDBScript = new MongoDBScript();
        

        if (username != "" && password != ""){

            url_params_value.Add(username);
            url_params_value.Add(email);
            url_params_value.Add(password);
            url_params_value.Add(ID_Stats);

            if (url_params_value == null){
                url_params_value = url_params_value_test;
            }

            string testResult = mongoDBScript.CreateUrlBodyRequest(url_params_key_test,url_params_value);
            Debug.Log(testResult);

            StartCoroutine(mongoDBScript.GetRequest(testResult,(result)=>{
                // Convert responseText string in object
                var responseJson = JsonUtility.FromJson<UserLogged>(result);
                LoginScript.userLogged = responseJson;
                
                //If user have a race scene Map
                if (userLogged.ID_Stats != "")
                {
                    SceneManager.LoadScene("MapScene");
                    return true;
                }else{
                    SceneManager.LoadScene("ChooseCharaScene");
                    return false;
                }
            }));

            

        }else{
            // Else not password or pseudo given in input
            Debug.Log("Mot de passe ou pseudo incorrect");
        }
    }
}
