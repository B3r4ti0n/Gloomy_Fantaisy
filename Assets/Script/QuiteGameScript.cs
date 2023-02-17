using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuiteGameScript : MonoBehaviour
{
    public void OnClickQuitGame () {
            SceneManager.LoadScene("PVEBattleScene");
    }
}
