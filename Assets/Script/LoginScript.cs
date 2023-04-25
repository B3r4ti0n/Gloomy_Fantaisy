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
        StartCoroutine(Upload());
    }
    
    IEnumerator Upload()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;

        WWWForm form = new WWWForm();
        form.AddField("name", username);
        form.AddField("password", password);

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("name="+username+"&pasword="+password));

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:8080/api/accounts/login", formData))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                SceneManager.LoadScene("MapScene");
                Debug.Log("Form upload complete!");
            }
        }
    }

}
