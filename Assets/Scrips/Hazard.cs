using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{

    [SerializeField] public float Damage;
    [SerializeField] public bool BypassInvincibility;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>())
        {
            
            other.GetComponent<Health>().TakeDamage(Damage, BypassInvincibility);
            
            
        }
    }
}
