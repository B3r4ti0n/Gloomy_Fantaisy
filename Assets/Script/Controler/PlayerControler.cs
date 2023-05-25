using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private Player player;
    private Weapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponent<Weapon>();
        animator = GetComponent<Animator>();
        animator.SetFloat("Action", 0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Use for Tests
    public void SetPlayer(Player newPlayer){
        player = newPlayer;
    }

    public void SetMaxhealth(int maxHealth){
        player.SetMaxHealth(maxHealth);
    }
    public Weapon GetWeapon(){
        animator.SetTrigger("Attack");
        return weapon;
    }

    // Call a change for value of Health of Player
    public void TakeDamage(int incommingDamage){
        player.ChangeHealth(player.GetHealth() - incommingDamage);
        animator.SetTrigger("TakeDamage");
    }

    // Get Value for game logic
    public int GetHealth(){
        return player.GetHealth();
    }
    public int GetMaxHealth(){
        return player.GetMaxHealth();
    }

    public int GetSpeed(){
        return player.GetSpeed();
    }

    public void SetSpeed(int speed){
        player.SetSpeed(speed);
    }

    //Trigger animation of Dying
    public void Die(){
        animator.SetTrigger("PlayerDie");
    }
}
