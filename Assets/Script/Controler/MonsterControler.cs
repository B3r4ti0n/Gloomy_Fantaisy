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

    public void TakeDamage(int incommingDamage){
        animator.SetTrigger("TakeDamage");     
        monster.health = monster.health - incommingDamage;  
        Debug.Log("monster health : " + GetHealth());
    }

    public void Die(){
        animator.SetTrigger("MonsterDie");
    }

    public string GetElementalWeekness(){
        return monster.weakness;
    }

    public int GetHealth(){
        return monster.health;
    }

}
