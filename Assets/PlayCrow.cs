using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCrow : MonoBehaviour
{

    public AudioSource Crow;
    public Collider player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void OnTriggerEnter(Collider other) 
    {
        if (other == player) 
        {
            Crow.Play();
            Destroy(this);
        }
        
    }
}
