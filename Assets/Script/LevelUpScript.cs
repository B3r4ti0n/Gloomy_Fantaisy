using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpScript : MonoBehaviour
{
    [SerializeField]
    public Text levelText; // Reference to the farm button

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = StatsMapController.userLogged.stats.level.ToString();
    }
}
