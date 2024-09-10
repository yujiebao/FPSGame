using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float enemyHP = 100;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectilePoint;
   public void TakeDamage(float damageAmout)
    {
        enemyHP -= damageAmout;
        if(enemyHP <= 0)
        {
            animator.SetTrigger("Death");
            GetComponent<CapsuleCollider>().enabled = false;
        }
        else
        {
            animator.SetTrigger("Damage");
        }
    }   
    public void Shoot ()
    {
        Rigidbody rb = Instantiate(projectile,projectilePoint.position,quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 30f+transform.up*7f,ForceMode.Impulse);
        
    }
}
