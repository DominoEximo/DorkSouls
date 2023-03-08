using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private Animator animator;
    private WeaponEquip weaponEquip;
    private float attackCD = 1;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        weaponEquip = GetComponent<WeaponEquip>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && weaponEquip.Equipped && (attackCD <=0))
        {
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
        yield return new WaitForSeconds(0.8f);
        animator.SetBool("AttackS1", false);
    }
}
