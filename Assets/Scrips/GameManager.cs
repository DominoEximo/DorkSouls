using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject Player;
    public GameObject Camera;
    public GameObject DeathSign;
    public TextMeshProUGUI Win;
    

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
            Player.GetComponent<Attack>().enabled = false;
            Camera.GetComponent<ThirdPersonCam>().enabled = false;
            DeathSign.GetComponent<MeshRenderer>().enabled = true;
            StartCoroutine("Death_restart");
        }


        

        
            
        

    }

    IEnumerator Death_restart()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        UnityEngine.Cursor.lockState = CursorLockMode.None;
    }


    public void WinGameOver() 
    {
    
        Win.gameObject.SetActive(true);
        Player.GetComponent<WeaponEquip>().enabled = false;
        Player.GetComponent<PlayerMovement>().enabled = false;
        Camera.GetComponent<ThirdPersonCam>().enabled = false;
        Player.GetComponent<Block>().enabled = false;
        Player.GetComponent<Attack>().enabled = false;
        StartCoroutine(Death_restart());

    }

    
    
}
