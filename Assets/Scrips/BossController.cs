using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{


    public float lookRadius = 10f;
    public Transform Target;
    public BoxCollider WeaponHitBox;
    NavMeshAgent agent;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Target = PlayerManager.instance.player.transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(Target.position,transform.position);



        if (distance <= lookRadius)
        {



            if (distance <= agent.stoppingDistance)

            {
                animator.SetBool("inRange", true);
                animator.SetBool("isChasing", false);
                //Attack
                while (animator.GetBool("inRange") == true) { Attack(); }
                

                //FaceTowardsPlayer
                FaceTarget();


            }
            else 
            {
                animator.SetBool("isChasing", true);
                animator.SetBool("inRange", false);
                agent.SetDestination(Target.position);
                
            }
        }
        else { animator.SetBool("isChasing", false); }
        
    }

    void Attack() 
    {
        StartCoroutine(Attack1());
    }
    

    IEnumerator Attack1() 
    {
        animator.SetBool("Attack1", true);
        WeaponHitBox.enabled = true;
        yield return new WaitForSeconds(1f);
        WeaponHitBox.enabled = false;
        StartCoroutine(Attack2());
        
    }
    IEnumerator Attack2()
    {
        animator.SetBool("Attack2", true);
        WeaponHitBox.enabled = true;
        yield return new WaitForSeconds(1f);
        WeaponHitBox.enabled = false;
        StartCoroutine(Attack3());

    }
    IEnumerator Attack3()
    {
        animator.SetBool("Attack3", true);
        WeaponHitBox.enabled = true;
        yield return new WaitForSeconds(1f);
        WeaponHitBox.enabled = false;

    }

    void FaceTarget() 
    {
        Vector3 direction = (Target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
