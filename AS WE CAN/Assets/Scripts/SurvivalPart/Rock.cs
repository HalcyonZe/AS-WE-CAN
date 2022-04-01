using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public GameObject stone;
    public GameObject stonePos;
    private Player player;
    float timer = 0;
    private bool isPlayerIn = false;
    float HP = 10;
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
            if (player.usePickaxe)
            {
                timer = 0;

                if (player.usePiceAxeID == 0)
                {
                    HP--;
                }
                else if (player.usePiceAxeID == 1)
                {
                    HP -= 2;
                }
                else if (player.usePiceAxeID == 2)
                {
                    HP -= 5;
                }
                
                print(HP);
                if (HP <= 0)
                {
                    if (player.usePiceAxeID == 0)
                    {
                        Instantiate(stone, stonePos.transform.position, stonePos.transform.rotation);
                        Instantiate(stone, stonePos.transform.position + new Vector3(1, 0, 1), stonePos.transform.rotation);
                        HP = 10;

                    }
                    if (player.usePiceAxeID == 1)
                    {
                        Instantiate(stone, stonePos.transform.position, stonePos.transform.rotation);
                        Instantiate(stone, stonePos.transform.position + new Vector3(1, 0, 1), stonePos.transform.rotation);
                        Instantiate(stone, stonePos.transform.position + new Vector3(-1, 0, 1), stonePos.transform.rotation);
                        HP = 10;
                    }
                    if (player.usePiceAxeID == 2)
                    {
                        Instantiate(stone, stonePos.transform.position, stonePos.transform.rotation);
                        Instantiate(stone, stonePos.transform.position + new Vector3(1, 0, 1), stonePos.transform.rotation);
                        Instantiate(stone, stonePos.transform.position + new Vector3(-1, 0, 1), stonePos.transform.rotation);
                        Instantiate(stone, stonePos.transform.position + new Vector3(2, 0, 2), stonePos.transform.rotation);
                        Instantiate(stone, stonePos.transform.position + new Vector3(-2, 0, 2), stonePos.transform.rotation);
                        Instantiate(stone, stonePos.transform.position + new Vector3(1, 0, 0), stonePos.transform.rotation);
                        HP = 10;
                    }

                    

                   
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
