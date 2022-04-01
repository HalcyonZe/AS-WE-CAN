using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBananaTree : MonoBehaviour
{
    public GameObject wood;
    public GameObject woodPos;
    public Farm farm;

    float HP = 6;

    private Player player;
    float timer = 0;

    private bool isPlayerIn = false;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (isPlayerIn && timer > 0.8)
        {
            if (player.useAxe)
            {
                timer = 0;
               
                if (player.uesAxeID == 0)
                {
                    HP--;
                }
                if (player.uesAxeID == 1)
                {
                    HP -= 2;
                }
                if (player.uesAxeID == 2)
                {
                    HP -= 5;
                }
                
                print(HP);
                if (HP <= 0)
                {
                    if (player.uesAxeID == 0)
                    {
                        Instantiate(wood, woodPos.transform.position, woodPos.transform.rotation);
                       

                    }
                    if (player.uesAxeID == 1)
                    {
                        Instantiate(wood, woodPos.transform.position, woodPos.transform.rotation);
                        Instantiate(wood, woodPos.transform.position + new Vector3(1, 0, 1), woodPos.transform.rotation);
                       
                    }
                    if (player.uesAxeID == 2)
                    {
                        Instantiate(wood, woodPos.transform.position, woodPos.transform.rotation);
                        Instantiate(wood, woodPos.transform.position + new Vector3(1, 0, 1), woodPos.transform.rotation);
                        Instantiate(wood, woodPos.transform.position + new Vector3(-1, 0, 1), woodPos.transform.rotation);
                        Instantiate(wood, woodPos.transform.position + new Vector3(2, 0, 2), woodPos.transform.rotation);
                        Instantiate(wood, woodPos.transform.position + new Vector3(-2, 0, 2), woodPos.transform.rotation);
                        
                    }



                    farm.isPlan = false;
                    farm.dead = false;


                    HP = 3;
                    gameObject.SetActive(false);
                }
            }

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
