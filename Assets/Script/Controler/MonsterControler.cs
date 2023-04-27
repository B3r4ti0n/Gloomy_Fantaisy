using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControler : MonoBehaviour
{
    private Monster monster;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        monster = GetComponent<Monster>();
        animator = GetComponent<Animator>();
        animator.SetFloat("Action", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Logic of taking damage of monster
    public IEnumerator TakeDamage(int incommingDamage){
        monster.health = monster.health - incommingDamage; 
        yield return new WaitForSeconds(0.3f);
        //Trigger animation take damage
        animator.SetTrigger("TakeDamage");      
    }

    public void Die(){
        animator.SetTrigger("MonsterDie");
    }

    public string GetElementalWeekness(){
        return monster.weakness;
    }

    // Get Value for game logic
    public int GetHealth(){
        return monster.health;
    }
    public int GetMaxHealth(){
        return monster.maxHealth;
    }

    public int GetDamage(){
        return monster.damage;
    }

    public int GetSpeed(){
        return monster.speed;
    }
    //Trigger animation Dying of monster
    public void attackAnimation(){
        animator.SetTrigger("Attack");
    }

}
