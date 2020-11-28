using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations: MonoBehaviour
    //All Enemy Animations are triggered from here
{
    private Animator cannibal_animator;
    private void Awake()
    {
        cannibal_animator = GetComponent<Animator>();
        
    }
    public void Walk(bool walking)
    {
        cannibal_animator.SetBool("Walk", walking);
    }
    public void Run(bool running)
    {
        cannibal_animator.SetBool("Run", running);
    }
    public void Attack()
    {
        cannibal_animator.SetTrigger("Attack");
    }
    public void dead()
    {
        cannibal_animator.SetTrigger("Dead");
    }
    

    
}
