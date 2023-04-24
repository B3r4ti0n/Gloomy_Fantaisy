using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControler : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private PlayerControler playerControler;
    [SerializeField]
    private GameObject monster;
    private MonsterControler monsterControler;
    // Start is called before the first frame update
    void Start()
    {
        playerControler = player.GetComponent<PlayerControler>();
        monsterControler = monster.GetComponent<MonsterControler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(monsterControler.GetHealth() <= 0){
            MakeMonsterDie();
        }
    }
    public void MakePlayerAttack(){
        string monsterWeekness = monsterControler.GetElementalWeekness();
        Weapon playerWeapon = playerControler.GetWeapon();
        if(monsterWeekness == playerWeapon.element){
            monsterControler.TakeDamage(playerWeapon.damage * 2);
        }else{
            monsterControler.TakeDamage(playerWeapon.damage);
        }
    }

    public void MakeMonsterDie(){
        monsterControler.Die();
    }

}
