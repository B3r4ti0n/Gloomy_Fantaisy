using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MongoDBScript : MonoBehaviour
{
    IEnumerator GetRequest(string paramsURL, System.func<bool> fonction){
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
            LoginScript.userLogged = responseJson;
            
            //If user have a race scene Map
            if (userLogged.ID_Stats != "")
            {
                SceneManager.LoadScene("MapScene");
            }else{
                SceneManager.LoadScene("ChooseCharaScene");
            }

            yield return responseText;
        }
    }

    public string CreateUrlBodyRequest(array<string> url_params_key_test, array<string> url_params_value){

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
        
        return paramsURL
    }

}
