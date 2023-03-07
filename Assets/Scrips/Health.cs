using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public float HP;
    private float InvincibleAmt;

    // Update is called once per frame
    void Update()
    {
        if (InvincibleAmt > 0) 
        { 
            InvincibleAmt -= Time.deltaTime;
        }

    }

    public void TakeDamage(float Amt, bool Bypass)
    {

        if (Bypass)
        {
            HP -= Amt;
            Debug.Log("Bypass Damage");
        }
        else
        {
            if (InvincibleAmt <= 0)
            {
                HP -= Amt;
                Debug.Log("Taken Damage");
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
        Debug.Log("Invincible");
        InvincibleAmt = invincLength;
    }
}
