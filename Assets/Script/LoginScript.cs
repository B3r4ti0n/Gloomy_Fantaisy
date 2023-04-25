using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoginScript : MonoBehaviour
{
    //Adress Api
    [SerializeField] private string authentificationEndpoint = "http://127.0.0.1:8080/api/accounts/login";
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;

    public void OnLoginClick(){
        string username = usernameInputField.text;
        string password = passwordInputField.text;

        StartCoroutine(GetUserByNamePassword(username,password));
    }

    IEnumerator GetUserByNamePassword(string username,string password){
        
        if (username != "" && password != ""){
            List<string> url_params_value = new List<string>();
            url_params_value.Add(username);
            url_params_value.Add(password);
            // Récupère une référence à l'objet JsonRequester dans la scène
            GetJsonScript getJsonScript = GameObject.Find("BtnSignIn").GetComponent<GetJsonScript>();

            string result = getJsonScript.CreateUrlParamsObject(url_params_value);
            Debug.Log(result);
            yield return ""+result;
        }else{
            Debug.Log("mot de passe ou pseudo incorrect");
        }

    }

}
