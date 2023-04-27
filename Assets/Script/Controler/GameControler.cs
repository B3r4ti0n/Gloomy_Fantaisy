using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameControler : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject canvasResult;
    [SerializeField]
    private TMP_Text resultText;

    private PlayerControler playerControler;
    [SerializeField]
    private GameObject monster;
    private MonsterControler monsterControler;
    private bool playerTurn;
    private bool playerRun;
    private bool playerIsAlive;
    [SerializeField]
    private Slider playerHealthBar;
    [SerializeField]
    private Slider monsterHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        playerIsAlive = true;
        playerRun = false;
        playerControler = player.GetComponent<PlayerControler>();
        monsterControler = monster.GetComponent<MonsterControler>();
        if (playerControler.GetSpeed() > monsterControler.GetSpeed())
        {
            playerTurn = true;
        }
        else if (playerControler.GetSpeed() == monsterControler.GetSpeed())
        {
            if (Random.Range(0, 1) == 0)
            {
                playerTurn = true;
            }
            else
            {
                playerTurn = false;
            }
        }
        else if (playerControler.GetSpeed() < monsterControler.GetSpeed())
        {
            playerTurn = false;
        }
        playerHealthBar.maxValue = playerControler.GetMaxHealth();
        playerHealthBar.value = playerControler.GetHealth();
        monsterHealthBar.maxValue = monsterControler.GetMaxHealth();
        monsterHealthBar.value = monsterControler.GetHealth();
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
            if (!playerTurn)
            {
                StartCoroutine(MakeMonsterAttack());
                playerTurn = !playerTurn;
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
        Weapon playerWeapon = playerControler.GetWeapon();
        if (playerTurn)
        {
            //Check if player use an elemental weakness of the monster
            if (monsterWeekness == playerWeapon.element)
            {
                StartCoroutine(monsterControler.TakeDamage(Random.Range(playerWeapon.MinDamage, playerWeapon.maxDamage) * 2));
            }
            else
            {
                StartCoroutine(monsterControler.TakeDamage(Random.Range(playerWeapon.MinDamage, playerWeapon.maxDamage)));
            }
            //Update life bar of monster
            monsterHealthBar.value = monsterControler.GetHealth();
            yield return new WaitForSeconds(1);
            playerTurn = !playerTurn;
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
        FinishBattle();
    }

}
