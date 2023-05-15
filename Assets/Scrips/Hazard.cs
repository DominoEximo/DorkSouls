using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hazard : MonoBehaviour
{
    public GameObject player;
    public AudioSource GSHit;
    [SerializeField] public float Damage;
    [SerializeField] public bool BypassInvincibility;
    public bool gotBlocked = false;
    public BoxCollider hitBox;


    void Update() 
    {

        gotBlocked = false;
        
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>())
        {
            if (other == player.GetComponent<CapsuleCollider>() && gotBlocked == false) { GSHit.Play(); }
            try
            {
                if (other?.GetComponent<Block>().animator.GetBool("isBlocking") == true)
                {
                    other.GetComponent<Health>().TakeDamage(Damage / 4, BypassInvincibility);
                    gotBlocked = true;
                    hitBox.enabled = false;
                }
            }
            catch (NullReferenceException ex) 
            {
                Debug.Log("Cant block."); 
            }
            
            other.GetComponent<Health>().TakeDamage(Damage, BypassInvincibility);
            hitBox.enabled = false;


        }
    }



    private void OnTriggerExit(Collider other) 
    {
        if (other.GetComponent<Health>()) { gotBlocked = false; }
    }
}
