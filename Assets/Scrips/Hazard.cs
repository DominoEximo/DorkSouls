using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hazard : MonoBehaviour
{

    [SerializeField] public float Damage;
    [SerializeField] public bool BypassInvincibility;
    public bool gotBlocked = false;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>())
        {
            try
            {
                if (other?.GetComponent<Block>().animator.GetBool("isBlocking") == true)
                {
                    other.GetComponent<Health>().TakeDamage(Damage / 4, BypassInvincibility);
                    gotBlocked = true;
                }
            }
            catch (NullReferenceException ex) 
            {
                Debug.Log("Cant block."); 
            }
            
            other.GetComponent<Health>().TakeDamage(Damage, BypassInvincibility);
            
            
        }
    }



    private void OnTriggerExit(Collider other) 
    {
        if (other.GetComponent<Health>()) { gotBlocked = false; }
    }
}
