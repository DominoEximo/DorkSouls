using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{


    public float lookRadius = 10f;
    public Transform Target;
    public GameObject WeaponHitBox;
    public GameObject HpBar;
    public GameObject FogWall;
    public BossHealth HP;
    public GameManager GM;
    public GameObject BlockSound;
    NavMeshAgent agent;
    Animator animator;
    float comboCD = 0;
    bool isDead = false;
    public bool secondPhase;
    bool musicOn = false;
    AudioSource bossMusic;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Target = PlayerManager.instance.player.transform;
        animator = GetComponent<Animator>();
        HP = GetComponent<BossHealth>();
        HpBar.SetActive(false);
        FogWall.SetActive(false);
        BlockSound.SetActive(false);
        bossMusic = GetComponent<AudioSource>();
        bossMusic.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(Target.position,transform.position);

        Death();

        if (distance <= lookRadius && !isDead)
        {
            bossMusic.enabled = true;
            
            HpBar.SetActive(true);
            FogWall.SetActive(true);


            if (distance <= agent.stoppingDistance)

            {
                animator.SetBool("inRange", true);
                animator.SetBool("isChasing", false);
                //Attack
                if (WeaponHitBox.GetComponent<Hazard>().gotBlocked == true)
                {

                    BlockSound.SetActive(true);
                    StartCoroutine(Blocked());
                    
                }
                if (comboCD <= 0)
                {
                    StartCoroutine(Attack1());
                    comboCD = 3.7f;
                    
                }
                else 
                {
                    
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
        yield return new WaitForSeconds(1.2f);
        BlockSound.SetActive(false);
        animator.SetBool("gotBlocked", false);
        WeaponHitBox.GetComponent<Hazard>().gotBlocked = false;
        agent.speed = 3.5f;
    }

    void Attack() 
    {
        StartCoroutine(Attack1());
    }
    

    IEnumerator Attack1() 
    {
        agent.speed = 0;
        animator.SetBool("Attack1", true);
        WeaponHitBox.GetComponent<BoxCollider>().enabled = true;
        yield return new WaitForSeconds(1f);
        
        agent.speed = 3.5f;
        StartCoroutine(Attack2());
        
    }
    IEnumerator Attack2()
    {
        agent.speed = 0;
        animator.SetBool("Attack2", true);
        animator.SetBool("Attack1", false);
        WeaponHitBox.GetComponent<BoxCollider>().enabled = true;
        yield return new WaitForSeconds(1f);
        
        agent.speed = 3.5f;
        StartCoroutine(Attack3());

    }
    IEnumerator Attack3()
    {
        agent.speed = 0;
        animator.SetBool("Attack3", true);
        animator.SetBool("Attack2", false);
        WeaponHitBox.GetComponent<BoxCollider>().enabled = true;
        yield return new WaitForSeconds(1f);

        animator.SetBool("Attack3", false);
        agent.speed = 3.5f;

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
            bossMusic.enabled = false;
            StopCoroutine(Attack1());
            StopCoroutine(Attack2());
            StopCoroutine(Attack3());
            StopCoroutine(Blocked());
            isDead = true;
            agent.isStopped = true;
            animator.SetBool("Dead", true);
            GM.WinGameOver();
            

            
        }
    }
}
