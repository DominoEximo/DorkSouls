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
    public AudioSource Swing;

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
        bool heavyIndicator = Input.GetKey(KeyCode.LeftShift);


        

        if (Time.time - lastClickedTime > maxComboDelay)
        {
            numberOfClicks = 0;
            
            animator.SetBool("AttackS1", false);
            animator.SetBool("AttackS2", false);
            animator.SetBool("AttackS3", false);
            animator.SetBool("HeavyAttack1", false);
            animator.SetBool("HeavyAttack2", false);
            animator.SetBool("HeavyAttack3", false);
        }
        if (Time.time > nextFireTime)
        {

            switch (heavyIndicator)
            {
                case false:
                    if (Input.GetButtonDown("Fire1") && weaponEquip.Equipped)
                    {
                        animator.SetLayerWeight(animator.GetLayerIndex("Combo Layer"), 1f);
                        SwordHB.enabled = true;
                        if (attackCD <= 0)
                        {
                            Combo();
                        }
                        
                        
                        
                    }
                    else { attackCD -= Time.deltaTime; }
                    break;
                case true:
                    if (Input.GetButtonDown("Fire1") && weaponEquip.Equipped)
                    {
                        animator.SetLayerWeight(animator.GetLayerIndex("Combo Layer"), 1f);
                        SwordHB.enabled = true;
                        if (attackCD <= 0)
                        {
                            HeavyCombo();
                        }

                    }
                    else { attackCD -= Time.deltaTime; }
                    break;
                
            }
            

            


        }



    }

    #region Light_Attack
    private IEnumerator AttackWithSword1()
    {
        
        animator.SetBool("AttackS1", true);
        yield return new WaitForSeconds(0.44f);
        SwordHB.enabled = false;
        
        


    }
    private IEnumerator AttackWithSword2()
    {
        
        animator.SetBool("AttackS2", true);
        yield return new WaitForSeconds(0.44f);
        SwordHB.enabled = false;
        
        

    }
    private IEnumerator AttackWithSword3()
    {
        
        animator.SetBool("AttackS3", true);
        yield return new WaitForSeconds(0.44f);
        SwordHB.enabled = false;

        

    }
    #endregion



    #region Heavy_Attack

    private IEnumerator HeavyAttack1()
    {
        
        animator.SetBool("HeavyAttack1", true);
        yield return new WaitForSeconds(0.44f);
        SwordHB.enabled = false;
        

    }
    private IEnumerator HeavyAttack2()
    {
        
        animator.SetBool("HeavyAttack2", true);
        yield return new WaitForSeconds(0.44f);
        SwordHB.enabled = false;
        

    }
    private IEnumerator HeavyAttack3()
    {
        
        animator.SetBool("HeavyAttack3", true);
        yield return new WaitForSeconds(0.44f);
        SwordHB.enabled = false;
        

    }




    #endregion


    void Combo()
    {

        animator.SetLayerWeight(animator.GetLayerIndex("Combo Layer"), 1f);
        lastClickedTime = Time.time;
        numberOfClicks++;
        
        if (numberOfClicks == 1)
        {
            StartCoroutine(AttackWithSword1());
            Swing.Play();

        }
        numberOfClicks = Mathf.Clamp(numberOfClicks, 0, 3);
        
        
        if (numberOfClicks == 2 )
        {
            animator.SetBool("AttackS1", false);
            StartCoroutine(AttackWithSword2());
            Swing.Play();

        }

        
        if (numberOfClicks == 3)
        {
            animator.SetBool("AttackS2", false);
            StartCoroutine(AttackWithSword3());
            Swing.Play();
            attackCD = 2f;

        }
        


    }


    void HeavyCombo()
    {
        animator.SetLayerWeight(animator.GetLayerIndex("Combo Layer"), 1f);
        lastClickedTime = Time.time;
        numberOfClicks++;

        if (numberOfClicks == 1 )
        {
            StartCoroutine(HeavyAttack1());
            Swing.Play();
        }

        if (numberOfClicks == 2 )
        {
            animator.SetBool("HeavyAttack1", false);
            StartCoroutine(HeavyAttack2());
            Swing.Play();

        }

        if (numberOfClicks == 3 )
        {
            animator.SetBool("HeavyAttack2", false);
            StartCoroutine(HeavyAttack3());
            Swing.Play();
            attackCD = 2f;

        }
    }
}
