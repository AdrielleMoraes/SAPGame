using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{

    public Animator animator;
    public Transform attackpoint;
    public float attackrange;
    public LayerMask enemyLayer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
      //  Collider2D[] hitinfoenemies = Physics2D.OverlapCircle(attackpoint.position, attackrange, enemyLayer);

    }
}
