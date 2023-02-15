using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class LoginScript : MonoBehaviour
{
    //Adress Api
    [SerializeField] private string authentificationEndpoint = "http...";

    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;

    public void OnLoginClick(){
        StartCoroutine(TryLogin());
    }
    private IEnumerator TryLogin(){
        string username = usernameInputField.text;
        string password = passwordInputField.text;

        UnityWebRequest request = UnityWebRequest.Get($"{authentificationEndpoint}?username={username}&password{password}");
        var handler = request.SendWebRequest();

        float startTime = 0.0f;
        while(!handler.isDone){

            startTime += Time.deltaTime;

            if (startTime > 10.0f){
                break;
            }

            yield return null;
        }
        if (request.result == UnityWebRequest.Result.Success){
            Debug.Log(request.downloadHandler.text);
        }else{
            Debug.Log("Unable to connect...");
        }

        Debug.Log($"{username}:{password}");

        yield return null;
    }

}
