using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject impactEffect;
    private Playercontrol player;
    private void Start() {
        // player = GameObject.Find("Player").GetComponent<Playercontrol>();\
        player = FindObjectOfType<Playercontrol>();
    }
    private void OnCollisionEnter(Collision other) {
        GameObject impact = Instantiate(impactEffect, transform.position, Quaternion.identity);//Quaternion.identity    无转向效果
        Destroy(impact,2f);
        
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, 3f);    //3f为半径的球内的所有物体（有Collider的物体）
        foreach (Collider c in colliders)
        {
            if (c.gameObject.tag == "Player")
            {
               StartCoroutine(player.TakeDamage(10));
            }
        }
       Destroy(gameObject,2f);
    }
}
