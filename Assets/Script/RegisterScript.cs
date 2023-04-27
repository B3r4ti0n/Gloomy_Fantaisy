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

        //Params Url
        List<string> url_params_value = new List<string>();
        
        url_params_value.Add(name);
        url_params_value.Add(email);
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
        Debug.Log(paramsURL);
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
            RegisterScript.userLogged = responseJson;
            Debug.Log(RegisterScript.userLogged.stats.Race);
            
            SceneManager.LoadScene("ChooseCharaScene");

            yield return responseText;
        }
    }
}
