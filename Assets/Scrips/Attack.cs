using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private Animator animator;
    private WeaponEquip weaponEquip;
    private float attackCD = 2f;
    public BoxCollider SwordHB;
    public GameManager GM;
    public float bombCD = 5f;
    public GameObject bomb;
    public Transform player;

    //Combo
    public static int numberOfClicks = 0;
    private float nextFireTime = 0f;
    float lastClickedTime = 0;
    float maxComboDelay = 2f;
    public AudioSource Swing;
    bool isAttacking = false;
    public HazardToBoss Weapon;

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

        if (Input.GetKeyDown(KeyCode.E) && bombCD <= 0 && GM.bombCounter > 0)
        {
            bombPlant();
        }
        else { bombCD -= Time.deltaTime; }

        
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
                        Weapon.Damage = 20;
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
                        Weapon.Damage = 80;
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
        isAttacking = true;
        animator.SetBool("AttackS1", true);
        Swing.Play();
        yield return new WaitForSeconds(1f);

        SwordHB.enabled = false;
        isAttacking = false;
        


    }
    private IEnumerator AttackWithSword2()
    {
        isAttacking = true;
        animator.SetBool("AttackS2", true);
        Swing.Play();
        yield return new WaitForSeconds(1f);
        SwordHB.enabled = false;
        isAttacking = false;


    }
    private IEnumerator AttackWithSword3()
    {
        isAttacking = true;
        animator.SetBool("AttackS3", true);
        Swing.Play();
        yield return new WaitForSeconds(1f);
        SwordHB.enabled = false;
        isAttacking = false;


    }
    #endregion



    #region Heavy_Attack

    private IEnumerator HeavyAttack1()
    {
        isAttacking = true;
        animator.SetBool("HeavyAttack1", true);
        Swing.Play();
        yield return new WaitForSeconds(1f);
        SwordHB.enabled = false;
        isAttacking = false;

    }
    private IEnumerator HeavyAttack2()
    {
        isAttacking = true;
        animator.SetBool("HeavyAttack2", true);
        Swing.Play();
        yield return new WaitForSeconds(1f);
        SwordHB.enabled = false;
        isAttacking = false;

    }
    private IEnumerator HeavyAttack3()
    {
        isAttacking = true;
        animator.SetBool("HeavyAttack3", true);
        Swing.Play();
        yield return new WaitForSeconds(1f);
        SwordHB.enabled = false;
        isAttacking = false;

    }




    #endregion


    #region Bomb

    void bombPlant() 
    {
        Instantiate(bomb, player.position, new Quaternion(0.210447073f, -0.23880139f, -0.685028732f, 0.655302644f));
        bombCD = 5f;
        GM.bombCounter -= 1;
    }

    #endregion

    void Combo()
    {
        if (!isAttacking) 
        
        {
            animator.SetLayerWeight(animator.GetLayerIndex("Combo Layer"), 1f);
            lastClickedTime = Time.time;
            numberOfClicks++;
            SwordHB.enabled = true;

            if (numberOfClicks == 1)
            {
                StartCoroutine(AttackWithSword1());
                
            }
            numberOfClicks = Mathf.Clamp(numberOfClicks, 0, 3);


            if (numberOfClicks == 2)
            {
                animator.SetBool("AttackS1", false);
                StartCoroutine(AttackWithSword2());
                

            }


            if (numberOfClicks == 3)
            {
                animator.SetBool("AttackS2", false);
                StartCoroutine(AttackWithSword3());
                attackCD = 2f;

            }

        }
        
        


    }


    void HeavyCombo()
    {
        if (!isAttacking) 
        {
            animator.SetLayerWeight(animator.GetLayerIndex("Combo Layer"), 1f);
            lastClickedTime = Time.time;
            numberOfClicks++;
            SwordHB.enabled = true;

            if (numberOfClicks == 1)
            {
                StartCoroutine(HeavyAttack1());
                
            }

            if (numberOfClicks == 2)
            {
                animator.SetBool("HeavyAttack1", false);
                StartCoroutine(HeavyAttack2());

            }

            if (numberOfClicks == 3)
            {
                animator.SetBool("HeavyAttack2", false);
                StartCoroutine(HeavyAttack3());
                attackCD = 2f;

            }
        }
        
    }
}
