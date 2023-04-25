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

    // Start script on initialization of button sign-in and on click button sign-in
    // Params: null
    // Result: void
    public void Start() {
        
    }
    
    public void test(){
        GetObjectWithParams(url_params_key_test,url_params_value_test);
    }
    // Get params with textfield and create paramsURL
    // Params:
    //  -List
    public void GetObjectWithParams(List<string> url_params_key, List<string> url_params_value){
        string paramsURL = "{";

        if(url_params_key.Count == url_params_value.Count) {
            for(int nKey = 0; nKey <= url_params_key.Count-1; nKey++) {
                if (nKey < url_params_key.Count-1){
                    paramsURL+="\""+url_params_key[nKey]+"\":\""+url_params_value[nKey]+"\",";
                }else{
                    paramsURL+="\""+url_params_key[nKey]+"\":\""+url_params_value[nKey]+"\"";
                }
            }
        }
        paramsURL+="}";
        Debug.Log(paramsURL);
        StartCoroutine(GetRequest(paramsURL));
    }

    IEnumerator GetRequest(string paramsURL){

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(paramsURL);
        Debug.Log(bodyRaw);
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
            Debug.Log(responseText);
        }
    }

    
}
