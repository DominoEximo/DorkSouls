using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{


    public float lookRadius = 10f;
    public Transform Target;
    public GameObject WeaponHitBox;
    public Health HP;
    public GameManager GM;
    NavMeshAgent agent;
    Animator animator;
    float comboCD = 0;
    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Target = PlayerManager.instance.player.transform;
        animator = GetComponent<Animator>();
        HP = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(Target.position,transform.position);

        Death();

        if (distance <= lookRadius && !isDead)
        {



            if (distance <= agent.stoppingDistance)

            {
                animator.SetBool("inRange", true);
                animator.SetBool("isChasing", false);
                //Attack
                if (WeaponHitBox.GetComponent<Hazard>().gotBlocked == true)
                {
                    StartCoroutine(Blocked());
                }
                if (comboCD <= 0)
                {
                    StartCoroutine(Attack1());
                    comboCD = 3.7f;
                    
                }
                else 
                {
                    Debug.Log(comboCD);
                    comboCD -= Time.deltaTime;
                }

                
                

                //FaceTowardsPlayer
                FaceTarget();


            }
            else 
            {
                animator.SetBool("isChasing", true);
                animator.SetBool("inRange", false);
                animator.SetBool("Attack1", false);
                animator.SetBool("Attack2", false);
                animator.SetBool("Attack3", false);
                comboCD = 0;
                agent.SetDestination(Target.position);
                
            }
        }
        else { animator.SetBool("isChasing", false); }
        
    }

    IEnumerator Blocked() 
    {
        agent.speed = 0;
        StopCoroutine(Attack1());
        StopCoroutine(Attack2());
        StopCoroutine(Attack3());
        animator.SetBool("gotBlocked", true);
        yield return new WaitForSeconds(2);
        animator.SetBool("gotBlocked", false);
        agent.speed = 3.5f;
    }

    void Attack() 
    {
        StartCoroutine(Attack1());
    }
    

    IEnumerator Attack1() 
    {
        animator.SetBool("Attack1", true);
        WeaponHitBox.GetComponent<BoxCollider>().enabled = true;
        yield return new WaitForSeconds(1f);
        WeaponHitBox.GetComponent<BoxCollider>().enabled = false;
        StartCoroutine(Attack2());
        
    }
    IEnumerator Attack2()
    {
        animator.SetBool("Attack2", true);
        animator.SetBool("Attack1", false);
        WeaponHitBox.GetComponent<BoxCollider>().enabled = true;
        yield return new WaitForSeconds(1f);
        WeaponHitBox.GetComponent<BoxCollider>().enabled = false;
        StartCoroutine(Attack3());

    }
    IEnumerator Attack3()
    {
        animator.SetBool("Attack3", true);
        animator.SetBool("Attack2", false);
        WeaponHitBox.GetComponent<BoxCollider>().enabled = true;
        yield return new WaitForSeconds(1f);
        WeaponHitBox.GetComponent<BoxCollider>().enabled = false;
        animator.SetBool("Attack3", false);

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


    void Death() 
    {
        if (HP.HP <= 0) 
        {
            StopCoroutine(Attack1());
            StopCoroutine(Attack2());
            StopCoroutine(Attack3());
            StopCoroutine(Blocked());
            isDead = true;
            agent.Stop();
            animator.SetBool("Dead", true);
            GM.WinGameOver();
            

            
        }
    }
}
