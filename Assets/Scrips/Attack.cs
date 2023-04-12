using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private Animator animator;
    private WeaponEquip weaponEquip;
    private float attackCD = 2f;
    public BoxCollider SwordHB;

    //Combo
    private static int numberOfClicks = 0;
    private float nextFireTime = 0f;
    float lastClickedTime = 0;
    float maxComboDelay = 1;

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
        

        
        if (Time.time - lastClickedTime > maxComboDelay)
        {
            numberOfClicks = 0;
            animator.SetLayerWeight(animator.GetLayerIndex("Combo Layer"), 0);
            animator.SetBool("AttackS1", false);
            animator.SetBool("AttackS2", false);
            animator.SetBool("AttackS3", false);
        }
        if (Time.time > nextFireTime)
        {
            if (Input.GetButtonDown("Fire1") && weaponEquip.Equipped)
            {
                
                SwordHB.enabled = true;
                Combo();
                
            }

           
        }

        
        
    }

    private IEnumerator AttackWithSword1()
    {
        animator.SetBool("AttackS1", true);
        yield return new WaitForSeconds(0.44f);
        SwordHB.enabled = false;
        //yield return new WaitForSeconds(0.36f);
        //animator.SetBool("AttackS1", false);
        
        
    }
    private IEnumerator AttackWithSword2()
    {
        animator.SetBool("AttackS2", true);
        yield return new WaitForSeconds(0.44f);
        SwordHB.enabled = false;
        //yield return new WaitForSeconds(0.36f);
        //animator.SetBool("AttackS2", false);

    }
    private IEnumerator AttackWithSword3()
    {
        animator.SetBool("AttackS3", true);
        yield return new WaitForSeconds(0.44f);
        SwordHB.enabled = false;
        //yield return new WaitForSeconds(0.36f);
        //animator.SetBool("AttackS3", false);

    }


    void Combo()
    {

        animator.SetLayerWeight(animator.GetLayerIndex("Combo Layer"), 1f);
        lastClickedTime = Time.time;
        numberOfClicks++;
        if (numberOfClicks == 1)
        {
            StartCoroutine(AttackWithSword1());
            
        }
        numberOfClicks = Mathf.Clamp(numberOfClicks, 0, 3);
        
        if (numberOfClicks == 2 )
        {
            animator.SetBool("AttackS1", false);
            StartCoroutine(AttackWithSword2());
            
        }
        if (numberOfClicks == 3)
        {
            animator.SetBool("AttackS2", false);
            StartCoroutine(AttackWithSword3());
            
        }
        


    }
}
