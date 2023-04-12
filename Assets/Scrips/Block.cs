using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    private Animator animator;
    private WeaponEquip weaponEquip;
    public BoxCollider ShieldHB;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        weaponEquip = GetComponent<WeaponEquip>();
        ShieldHB.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
        animator.SetBool("isBlocking", false);
        ShieldHB.enabled = false;

        if (Input.GetKey(KeyCode.Mouse1) && weaponEquip.Equipped)
        {
            animator.SetLayerWeight(animator.GetLayerIndex("Combo Layer"), 1f);
            ShieldHB.enabled = true;
            Blocking();
        }
        
    }


    void Blocking()
    {
        animator.SetBool("isBlocking", true);
        

    }
}
