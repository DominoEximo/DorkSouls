using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject Player;
    public GameObject Camera;
    public GameObject DeathSign;

    // Start is called before the first frame update
    void Start()
    {
        DeathSign.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<Health>().HP <= 0)
        { 
            Player.GetComponent<PlayerMovement>().enabled = false;
            Camera.GetComponent<ThirdPersonCam>().enabled = false;
            DeathSign.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
