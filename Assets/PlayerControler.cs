using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
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

    public void Attack(){
        animator.SetTrigger("Attack");
    }
}
