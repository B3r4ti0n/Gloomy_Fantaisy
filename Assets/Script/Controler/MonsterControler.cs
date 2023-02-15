using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControler : MonoBehaviour
{
   private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("Action", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(){
        animator.SetTrigger("TakeDamage");       
    }

    public void Die(){
        animator.SetTrigger("MonsterDie");
    }

}
