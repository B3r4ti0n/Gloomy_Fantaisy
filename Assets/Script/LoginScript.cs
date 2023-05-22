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

        string name = usernameInputField.text;
        string password = passwordInputField.text;
        List<string> url_params_value = new List<string>();
        MongoDBScript mongoDBScript = new MongoDBScript();
        

        if (name != "" && password != ""){

            url_params_value.Add(name);
            url_params_value.Add(password);

            if (url_params_value == null){
                url_params_value = url_params_value_test;
            }

            string testResult = mongoDBScript.CreateUrlBodyRequest(url_params_key_test,url_params_value);

            StartCoroutine(mongoDBScript.GetRequest(url_test,testResult,(result)=>{
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
