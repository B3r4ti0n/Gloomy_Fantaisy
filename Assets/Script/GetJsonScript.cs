using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class GetJsonScript : MonoBehaviour
{
    [SerializeField]    private string url_test;
    [SerializeField]    private List<string> url_params_key_test;
    [SerializeField]    private List<string> url_params_value_test;

    public void DebugConnectionDatabase(){
        CreateUrlParamsObject(url_params_value_test);
    }

    // Get params with textfield and create paramsURL
    // Params:
    //  - url_params_key: List<string>
    //  - url_params_value: List<string>
    public string CreateUrlParamsObject(List<string> url_params_value){

        if (url_params_value == null){
            url_params_value = url_params_value_test;
        }

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
        return StartCoroutine(GetRequest(paramsURL)).ToString();
    }

    IEnumerator GetRequest(string paramsURL){

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(paramsURL);
        // Créer une requête POST avec l'URL cible
        UnityWebRequest request = UnityWebRequest.Put(url_test, "application/json");
    
        // Ajouter les en-têtes nécessaires
        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);

        // Créer un gestionnaire de téléchargement pour la réponse
        DownloadHandler downloadHandler = new DownloadHandlerBuffer();
        request.downloadHandler = downloadHandler;
        
        // Envoyer la requête
        yield return request.SendWebRequest();
 
        // Vérifier s'il y a une erreur
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            // Convertir les données de réponse en chaîne
            string responseText = System.Text.Encoding.UTF8.GetString(downloadHandler.data);

            // Utiliser la chaîne de réponse
            yield return responseText;
        }
    }

    
}
