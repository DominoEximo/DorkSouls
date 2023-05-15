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
            agent.SetDestination(Target.position);
            animator.SetBool("isChasing", true);
            

            if (distance <= agent.stoppingDistance) 
                
            {
                //Attack


                //FaceTowardsPlayer
                FaceTarget();
                agent.SetDestination(transform.position);
                
            }
        }
        else { animator.SetBool("isChasing", false); }
        
    }


    void basicCombo() 
    {
        animator.SetBool("Attack1", true);



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
