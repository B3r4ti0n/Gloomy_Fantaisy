using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private Animator animator;
    private Player player;
    private Weapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        weapon = GetComponent<Weapon>();
        animator = GetComponent<Animator>();
        animator.SetFloat("Action", 0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Weapon GetWeapon(){
        animator.SetTrigger("Attack");
        return weapon;
    }

    public void GetDamage(int incommingDamage){
        player.health = player.health - incommingDamage;
    }
}
