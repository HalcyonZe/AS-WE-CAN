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
    public float HP = 10;

    bool isFriendIn=false;
    int friendSkill = 0;
    float friendDemage=0.2f;
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
                
                
                if (HP <= 0)
                {
                    if (player.usePiceAxeID == 0)
                    {
                        Instantiate(stone, stonePos.transform.position, stonePos.transform.rotation);
                        Instantiate(stone, stonePos.transform.position + new Vector3(1, 0, Random.Range(-2.1f, 2.1f)), stonePos.transform.rotation);
                        HP = 10;

                    }
                   else if (player.usePiceAxeID == 1)
                    {
                        Instantiate(stone, stonePos.transform.position, stonePos.transform.rotation);
                        Instantiate(stone, stonePos.transform.position + new Vector3(Random.Range(-2.1f, 2.1f), 0, 1), stonePos.transform.rotation);
                        Instantiate(stone, stonePos.transform.position + new Vector3(Random.Range(-2.1f, 2.1f), 0, Random.Range(-2.1f, 2.1f)), stonePos.transform.rotation);
                        HP = 10;
                    }
                   else   if (player.usePiceAxeID == 2)
                    {
                        Instantiate(stone, stonePos.transform.position, stonePos.transform.rotation);
                        Instantiate(stone, stonePos.transform.position + new Vector3(Random.Range(-2.1f, 2.1f), 0, Random.Range(-2.1f, 2.1f)), stonePos.transform.rotation);
                        Instantiate(stone, stonePos.transform.position + new Vector3(Random.Range(-2.1f, 2.1f), 0, Random.Range(-2.1f, 2.1f)), stonePos.transform.rotation);
                        Instantiate(stone, stonePos.transform.position + new Vector3(Random.Range(-2.1f, 2.1f), 0, Random.Range(-2.1f, 2.1f)), stonePos.transform.rotation);
                        Instantiate(stone, stonePos.transform.position + new Vector3(Random.Range(-2.1f, 2.1f), 0, Random.Range(-2.1f, 2.1f)), stonePos.transform.rotation);
                        Instantiate(stone, stonePos.transform.position + new Vector3(Random.Range(-2.1f, 2.1f), 0, 0), stonePos.transform.rotation);
                        HP = 10;
                    }

                    

                   
                }
            }

        }
        if (isFriendIn)
        {



            HP -= friendDemage*Time.deltaTime;

                
                if (HP <= 0)
                {
                    if (friendSkill == 0)
                    {
                        Instantiate(stone, stonePos.transform.position, stonePos.transform.rotation);
                        Instantiate(stone, stonePos.transform.position + new Vector3(Random.Range(-2.1f, 2.1f), 0, 1), stonePos.transform.rotation);
                        HP = 10;

                        friendSkill++;

                        friendDemage = 0.5f;

                    }
                  else  if (friendSkill == 1)
                    {
                        Instantiate(stone, stonePos.transform.position, stonePos.transform.rotation);
                        Instantiate(stone, stonePos.transform.position + new Vector3(1, 0, Random.Range(-2.1f, 2.1f)), stonePos.transform.rotation);
                        Instantiate(stone, stonePos.transform.position + new Vector3(Random.Range(-2.1f, 2.1f), 0, 1), stonePos.transform.rotation);
                        HP = 10;

                        friendSkill++;

                        friendDemage = 0.75f;
                }
                 else   if (friendSkill == 2)
                    {
                        Instantiate(stone, stonePos.transform.position, stonePos.transform.rotation);
                        Instantiate(stone, stonePos.transform.position + new Vector3(Random.Range(-2.1f, 2.1f), 0, Random.Range(-2.1f, 2.1f)), stonePos.transform.rotation);
                        Instantiate(stone, stonePos.transform.position + new Vector3(Random.Range(-2.1f, 2.1f), 0, Random.Range(-2.1f, 2.1f)), stonePos.transform.rotation);
                        Instantiate(stone, stonePos.transform.position + new Vector3(Random.Range(-2.1f, 2.1f), 0, Random.Range(-2.1f, 2.1f)), stonePos.transform.rotation);
                        Instantiate(stone, stonePos.transform.position + new Vector3(Random.Range(-2.1f, 2.1f), 0, Random.Range(-2.1f, 2.1f)), stonePos.transform.rotation);
                        Instantiate(stone, stonePos.transform.position + new Vector3(Random.Range(-2.1f, 2.1f), 0, 0), stonePos.transform.rotation);
                        HP = 10;
                        friendDemage = 1;
                }




                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isPlayerIn = true;

            //设置此时要放置的类型是矿工
            player.friendKind = 1;
        }
        if(other.tag=="Friend_Pickaxe")
        {
            isFriendIn = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isPlayerIn = false;
        }

        if (other.tag == "Friend_Pickaxe")
        {
            isFriendIn = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Friend_Pickaxe")
        {
            isFriendIn = true;
        }
        else
        {
            isFriendIn = false;
        }
    }
}
