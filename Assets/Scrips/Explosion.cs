using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    bool bossInRadius = false;
    bool playerInRadius  = true;
    public GameObject player;
    public GameObject boss;
    public ParticleSystem effect;

    float Tic = 5f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boss = GameObject.FindGameObjectWithTag("Boss");
        effect = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Tic -= Time.deltaTime;
        if (Tic <= 0 && playerInRadius)
        {
            Debug.Log("Took damage");
            player.GetComponent<Health>().TakeDamage(150, true);
            effect.Play();
            Destroy(gameObject);
        }
        

        if (Tic <= 0 && bossInRadius)
        {
            boss.GetComponent<BossHealth>().TakeDamage(150, true);
            effect.Play();
            Destroy(gameObject);
        }

        if (Tic <= 0) 
        {
            effect.Play();
            Destroy(gameObject); 
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BossHealth>()) { bossInRadius = true; }

        if (other.GetComponent<Health>()) { playerInRadius = true; }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<BossHealth>()) { bossInRadius = false; }

        if (other.GetComponent<Health>()) { playerInRadius = false; }

    }

}
