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
    }
    public void MakePlayerAttack(){
            playerControler.Attack();
            monsterControler.TakeDamage();
    }

    public void MakeMonsterDie(){
        playerControler.Attack();
        monsterControler.Die();
    }

}
