using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void SetHitBack()
    {
        gameObject.GetComponent<Animator>().SetBool("IsHit",false);
        player.useAxe = false;
       
    }    
}
