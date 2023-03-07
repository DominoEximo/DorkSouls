using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEquip : MonoBehaviour
{

    private Animator animator;

    public GameObject sword;
    public GameObject shield;
    public Transform RHand;
    public Transform LHand;
    public Transform Back;
    bool Equipped;


    //Sword positions and rotations
    Vector3 swordEquipPos = new Vector3(9.8f, 10f, -46.8f);
    Vector3 swordEquipRot = new Vector3(-81.6f, 90f, 105.758f);
    Vector3 swordRestPos = new Vector3(-1.6f, -10.9f, 0);
    Vector3 swordRestRot = new Vector3(-81.6f, 90f, 0);

    //Shield positions and rotations
    Vector3 shieldEquipPos = new Vector3(16.0442f, -2.496703f, 3.32662f);
    Vector3 shieldEquipRot = new Vector3(-266.128f, 251.055f, 23.74001f);
    Vector3 shieldRestPos = new Vector3(-16.5f, -10.00002f, 0.3000059f);
    Vector3 shieldRestRot = new Vector3(-81.6f, 90f, 0);

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        Equipped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Equip") && !Equipped)
        {
            animator.SetBool("EquipS", true);
            StartCoroutine(EquipSword(sword, RHand, swordEquipPos, swordEquipRot));
            animator.SetBool("EquipSh", true);
            StartCoroutine(EquipShield(shield, LHand, shieldEquipPos, shieldEquipRot));


        }
        else if (Input.GetButtonDown("Equip") && Equipped)
        {
            animator.SetBool("EquipS", true);
            StartCoroutine(UnequipSword(sword, Back, swordRestPos, swordRestRot));
            animator.SetBool("EquipSh", true);
            StartCoroutine(UnequipShield(shield, Back, shieldRestPos, shieldRestRot));

        }

    }

    IEnumerator EquipSword(GameObject item, Transform container, Vector3 equipPos, Vector3 equipRot)
    {
        yield return new WaitForSeconds(0.3f);
        item.transform.SetParent(container, false);
        item.transform.localPosition = equipPos;
        item.transform.localEulerAngles = equipRot;

        Equipped = true;
        yield return new WaitForSeconds(1);
        animator.SetBool("EquipS", false);

    }
    IEnumerator UnequipSword(GameObject item, Transform container, Vector3 restPos, Vector3 restRot)
    {
        yield return new WaitForSeconds(0.3f);
        item.transform.SetParent(container, false);
        
        item.transform.localPosition = restPos;
        item.transform.localEulerAngles = restRot;

        Equipped = false;
        yield return new WaitForSeconds(1);
        animator.SetBool("EquipS", false);

    }
    IEnumerator EquipShield(GameObject item, Transform container, Vector3 equipPos, Vector3 equipRot)
    {
        yield return new WaitForSeconds(1f);
        item.transform.SetParent(container, false);
        item.transform.localPosition = equipPos;
        item.transform.localEulerAngles = equipRot;

        Equipped = true;
        //yield return new WaitForSeconds(1);
        animator.SetBool("EquipSh", false);

    }
    IEnumerator UnequipShield(GameObject item, Transform container, Vector3 restPos, Vector3 restRot)
    {
        yield return new WaitForSeconds(1f);
        item.transform.SetParent(container, false);
        
        item.transform.localPosition = restPos;
        item.transform.localEulerAngles = restRot;

        Equipped = false;
        //yield return new WaitForSeconds(1);
        animator.SetBool("EquipSh", false);
    }
}
