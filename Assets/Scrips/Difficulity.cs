using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulity : MonoBehaviour
{

    public void setDifficulityToHard() { PlayerPrefs.SetFloat("identicator", 2f); }

    public void setDifficulityToNormal() { PlayerPrefs.SetFloat("identicator", 1f); }


}
