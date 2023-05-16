using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HazardToBoss : MonoBehaviour
{
    public GameObject player;
    public AudioSource GSHit;
    [SerializeField] public float Damage;
    [SerializeField] public bool BypassInvincibility;
    public bool gotBlocked = false;
    public BoxCollider hitBox;
    public Block playerBlock;


    void Update() 
    {

        
        
    }

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BossHealth>())
        {
            
            try
            {
                
                if (playerBlock.animator.GetBool("isBlocking") == true)
                {
                    
                    other.GetComponent<Health>().TakeDamage(Damage / 4, BypassInvincibility);
                    gotBlocked = true;
                    Debug.Log(gotBlocked);
                    hitBox.enabled = false;
                    
                }
                else 
                {
                    if (other == player.GetComponent<CapsuleCollider>() && gotBlocked == false) { GSHit.Play(); }
                    other.GetComponent<BossHealth>().TakeDamage(Damage, BypassInvincibility);
                    hitBox.enabled = false;
                }
            }
            catch (NullReferenceException ex) { Debug.Log("Exc"); }
            
            



            

        }
    }
    



    void OnTriggerExit(Collider other) 
    {
        if (other.GetComponent<BossHealth>()) { gotBlocked = false; }
    }

}

