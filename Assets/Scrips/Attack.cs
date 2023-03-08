using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private Animator animator;
    private WeaponEquip weaponEquip;
    private float attackCD = 1;
    public BoxCollider SwordHB;

    //Combo
    private static int numberOfClicks;
    private float nextFireTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        weaponEquip = GetComponent<WeaponEquip>();
        SwordHB.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && weaponEquip.Equipped && (attackCD <=0))
        {
            SwordHB.enabled = true;
            StartCoroutine(AttackWithSword1());
            
        }
        else
        {
            
            attackCD -= Time.deltaTime;
        }
        
    }

    private IEnumerator AttackWithSword1()
    {
        animator.SetBool("AttackS1", true);
        yield return new WaitForSeconds(0.44f);
        SwordHB.enabled = false;
        yield return new WaitForSeconds(0.36f);
        animator.SetBool("AttackS1", false);
        
    }
}
