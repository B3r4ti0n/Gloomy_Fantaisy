using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class RegisterScript : MonoBehaviour
{
    //Adress Api
    [SerializeField] private string url_test = "http://localhost:8080/api/accounts/register";

    [SerializeField] private List<string> url_params_key_test;
    [SerializeField] private List<string> url_params_value_test;

    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField emailInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private string responseText = "";

    public static UserLogged userLogged = new UserLogged();

    public void OnRegisterClick(){

        string name = usernameInputField.text;
        string email = emailInputField.text;
        string password = passwordInputField.text;
        string ID_Stats = "";
        MongoDBScript mongoDBScript = new MongoDBScript();

        //Params Url
        List<string> url_params_value = new List<string>();
        
        url_params_value.Add(name);
        url_params_value.Add(email);
        url_params_value.Add(password);
        url_params_value.Add(ID_Stats);
        
        string testResult = mongoDBScript.CreateUrlBodyRequest(url_params_key_test,url_params_value);

        StartCoroutine(mongoDBScript.GetRequest(url_test,testResult,(result)=>{
            // Convert responseText string in object
            var responseJson = JsonUtility.FromJson<UserLogged>(result);
            LoginScript.userLogged = responseJson;
            
            SceneManager.LoadScene("ChooseCharaScene");
            return false;
        }));
    
    }
}
