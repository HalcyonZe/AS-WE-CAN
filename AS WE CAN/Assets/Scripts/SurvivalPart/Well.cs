using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Well : MonoBehaviour
{

    private bool isPlayerIn = false;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerIn == true && Input.GetKeyDown(KeyCode.F))
        {
            if( player.buckets.activeSelf)
            {
                if (player.bucktsWater < 2)
                {
                    player.bucktsWater++;
                }

                player.ChangeBucktsSprites();
            }

            player.water = player.maxWater;
            print("ºÈ¹»ÁË:"+player.water);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isPlayerIn = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isPlayerIn = false;
        }
    }
}
