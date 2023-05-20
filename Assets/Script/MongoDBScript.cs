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
    private string url_test;
    private string responseText;

    IEnumerator GetRequest(string paramsURL, System.Func<string,bool> GetResult){
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

            GetResult(responseText);

            yield return responseText;
        }
    }

    public string CreateUrlBodyRequest(List<string> url_params_key_test, List<string> url_params_value){

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
        
        return paramsURL;
    }

}
