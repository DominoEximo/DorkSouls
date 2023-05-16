using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{

    public float HP;
    private float InvincibleAmt;

    private Animator animator;
    public Slider HpBar;





    void Start()
    {
        HP = HP * PlayerPrefs.GetFloat("identicator");
        animator = GetComponentInChildren<Animator>();
        Debug.Log(PlayerPrefs.GetFloat("identicator"));
        SetMaxHealth(HP);
    }

    // Update is called once per frame
    void Update()
    {

        SetHealth(HP);

        if (InvincibleAmt > 0) 
        { 
            InvincibleAmt -= Time.deltaTime;
        }

        if (this.HP <= 0)
        {
            animator.SetBool("Dead", true);
        }

    }

    public void TakeDamage(float Amt, bool Bypass)
    {

        if (Bypass)
        {
            HP -= Amt;
        }
        else
        {
            if (InvincibleAmt <= 0)
            {
                HP -= Amt;
            }
        }
        
    }


    public void Invincible(float delay, float invincibleLength)
    {
        if (delay > 0)
        {
            StartCoroutine(StartInvincible(delay, invincibleLength));
        }
        else
        {
            InvincibleAmt = invincibleLength;

        }



    }
    IEnumerator StartInvincible(float dly, float invincLength)
    {
        yield return new WaitForSeconds(dly);
        InvincibleAmt = invincLength;
    }


    public void SetMaxHealth(float HP) 
    {

        HpBar.maxValue = HP;
        HpBar.value = HP;
    
    }

    public void SetHealth(float HP) 
    {

        HpBar.value = HP;

    }
}
