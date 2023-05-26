using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToRegisterScript : MonoBehaviour
{
    public void GoToRegister(){
        SceneManager.LoadScene("RegisterScene");
    }
}
