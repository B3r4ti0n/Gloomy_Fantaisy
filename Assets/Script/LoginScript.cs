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

    // Get params with textfield and create paramsURL
    // Params:
    //  - url_params_key: List<string>
    //  - url_params_value: List<string>
    // Return: void
    public void OnLoginClick(){

        string username = usernameInputField.text;
        string password = passwordInputField.text;
        List<string> url_params_value = new List<string>();
        

        if (username != "" && password != ""){

            url_params_value.Add(username);
            url_params_value.Add(password);

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

        }else{
            // Else not password or pseudo given in input
            Debug.Log("Mot de passe ou pseudo incorrect");
        }
    }

    // Function send Request to users table with api 
    // Params:
    //  - paramsURL: String
    // Return: IEnumerator
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
            
            // Add your code on ligne 83 to ligne "yield return responseText;"
            SceneManager.LoadScene("MapScene");

            yield return responseText;
        }
    }
}
