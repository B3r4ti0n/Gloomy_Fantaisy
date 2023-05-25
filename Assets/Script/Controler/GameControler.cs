using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControler : MonoBehaviour
{
    [SerializeField]
    private GameObject FemaleAvatar;
    [SerializeField]
    private GameObject MaleAvatar;
    [SerializeField]
    private GameObject canvasResult;
    [SerializeField]
    private TMP_Text resultText;
    [SerializeField]
    private TMP_Text rewardText;
    private PlayerControler playerControler;
    [SerializeField]
    private GameObject monster;
    private MonsterControler monsterControler;
    private bool playerTurn;
    private bool playerRun;
    private bool monsterTurn;
    private bool playerIsAlive;
    [SerializeField]
    private Slider playerHealthBar;
    [SerializeField]
    private Slider monsterHealthBar;

    public static UserLogged userLogged = new UserLogged();



    [SerializeField] private string url_update;
    [SerializeField] private List<string> url_params_key_update;
    [SerializeField] private List<string> url_params_value_update;

    private int rewardGold;
    private int rewardExp;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(StatsMapController.userLogged.name);
        userLogged = StatsMapController.userLogged;
        if (StatsMapController.userLogged.stats.race == "Orc")
        {
            MaleAvatar.SetActive(true);
            playerControler = MaleAvatar.GetComponent<PlayerControler>();
        }
        else
        {
            FemaleAvatar.SetActive(true);
            playerControler = FemaleAvatar.GetComponent<PlayerControler>();
        }
        playerControler.SetMaxhealth(userLogged.stats.health_point);
        playerControler.SetSpeed(userLogged.stats.speed_value);
        playerIsAlive = true;
        playerRun = false;
        monsterTurn = false;
        monsterControler = monster.GetComponent<MonsterControler>();
        if (playerControler.GetSpeed() > monsterControler.GetSpeed())
        {
            playerTurn = true;
            monsterTurn = false;
        }
        else if (playerControler.GetSpeed() == monsterControler.GetSpeed())
        {
            if (Random.Range(0, 1) == 0)
            {
                playerTurn = true;
                monsterTurn = false;
            }
            else
            {
                playerTurn = false;
                monsterTurn = true;
            }
        }
        else if (playerControler.GetSpeed() < monsterControler.GetSpeed())
        {
            playerTurn = false;
            monsterTurn = true;
        }
        playerHealthBar.maxValue = playerControler.GetMaxHealth();
        playerHealthBar.value = playerControler.GetMaxHealth();
        monsterHealthBar.maxValue = monsterControler.GetMaxHealth();
        monsterHealthBar.value = monsterControler.GetMaxHealth();
        rewardGold = Random.Range(30, 70);
        rewardExp = Random.Range(30, 70);
    }

    // Update is called once per frame
    // Game Logic
    void Update()
    {
        // Check life point of monster and player
        if (monsterControler.GetHealth() <= 0)
        {
            //Kill monster and trigger Victory menu
            StartCoroutine(MakeMonsterDie());
            StartCoroutine(FinishBattle());
        }
        else
        {
            //Let's monster attack while it's not player turn
            if (!playerTurn && monsterTurn)
            {
                StartCoroutine(MakeMonsterAttack());
                monsterTurn = !monsterTurn;
            }
        }
        if (playerControler.GetHealth() <= 0)
        {
            //Kill player and set Defead menu
            playerIsAlive = false;
            StartCoroutine(MakePlayerDie());
            StartCoroutine(FinishBattle());
        }


    }

    //Call for start attack of player
    public void PlayerAttack()
    {
        StartCoroutine(MakePlayerAttack());
    }
    //Logic of player attack
    IEnumerator MakePlayerAttack()
    {
        string monsterWeekness = monsterControler.GetElementalWeekness();
        if (playerTurn && !monsterTurn)
        {
            Weapon playerWeapon = playerControler.GetWeapon();
            int minDamage = playerWeapon.MinDamage + (playerWeapon.MinDamage * (userLogged.stats.offensive_value / 100));
            int maxDamage = playerWeapon.maxDamage + (playerWeapon.maxDamage * (userLogged.stats.offensive_value / 100));
            //Check if player use an elemental weakness of the monster
            if (monsterWeekness == playerWeapon.element)
            {
                StartCoroutine(monsterControler.TakeDamage(Random.Range(minDamage, maxDamage) * 2));
            }
            else
            {
                StartCoroutine(monsterControler.TakeDamage(Random.Range(minDamage, maxDamage)));
            }
            //Update life bar of monster
            monsterHealthBar.value = monsterControler.GetHealth();
            playerTurn = !playerTurn;
            yield return new WaitForSeconds(1);
            monsterTurn = !monsterTurn;
        }
    }
    // Logic of monster attack
    IEnumerator MakeMonsterAttack()
    {
        monsterControler.attackAnimation();
        yield return new WaitForSeconds(0.5f);
        // Call logic of Player for taking Damage
        playerControler.TakeDamage(monsterControler.GetDamage());
        //Update player life bar
        playerHealthBar.value = playerControler.GetHealth();
        yield return new WaitForSeconds(0.5f);
        playerTurn = !playerTurn;
    }
    //Launch animation dying for monster
    IEnumerator MakeMonsterDie()
    {
        monsterControler.Die();
        yield return new WaitForSeconds(2);
    }
    //Launch animation dying for player
    IEnumerator MakePlayerDie()
    {
        playerControler.Die();
        yield return new WaitForSeconds(2);
    }
    //Choose the menu for finish
    IEnumerator FinishBattle()
    {
        yield return new WaitForSeconds(1.5f);
        if (playerRun)
        {
            resultText.text = "You run away !";

        }
        else if (playerIsAlive)
        {
            resultText.text = "You have triumphed !";
            rewardText.text = "You gain " + rewardGold + " G, and " + rewardExp + " exp.";
        }
        else
        {
            resultText.text = "Tou have been defeated !";
        }

        canvasResult.SetActive(true);
    }

    //Make player Run Away
    public void MakeRunAwayPlayer()
    {
        playerRun = true;
        StartCoroutine(FinishBattle());
    }

    public void quitBattle()
    {
        if( playerIsAlive && !playerRun){
            userLogged.stats.gold = userLogged.stats.gold + rewardGold;
            userLogged.stats.exp = userLogged.stats.exp + rewardExp;
        }
        url_params_value_update.Add(userLogged.stats._id);
        url_params_value_update.Add(userLogged.stats.gold.ToString());
        url_params_value_update.Add(userLogged.stats.exp.ToString());
        MongoDBScript mongoDBScript = new MongoDBScript();
        string updateResult = mongoDBScript.CreateUrlBodyRequest(url_params_key_update, url_params_value_update);
        StartCoroutine(mongoDBScript.GetRequestPatch(url_update, updateResult, () =>
        {
            SceneManager.LoadScene("MapScene");
            return false;
        }));
        
    }

}
