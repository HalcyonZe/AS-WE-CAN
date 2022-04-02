using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public GameObject wood;
    public GameObject woodPos;
    public Farm farm;

   public float HP = 10;

    private Player player;
    float timer = 0;
  
    private bool isPlayerIn = false;

    bool isFriendIn = false;   
    float friendDemage = 0.75f;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(isPlayerIn&&timer>0.8)
        {
            if(player.useAxe)
            {
                timer = 0;

                if (player.uesAxeID == 0)
                {
                    HP--;
                }
                else  if (player.uesAxeID == 1)
                {
                    HP -= 2;
                }
                else   if (player.uesAxeID == 2)
                {
                    HP -= 5;
                }
                gameObject.GetComponent<Animator>().SetBool("IsHit", true);
                print(HP);
                if (HP<=0)
                {
                    if(player.uesAxeID==0)
                    {
                        Instantiate(wood, woodPos.transform.position, woodPos.transform.rotation);
                        Instantiate(wood, woodPos.transform.position + new Vector3(Random.Range(-2.1f, 2.1f), 0, Random.Range(-2.1f, 2.1f)), woodPos.transform.rotation);
                        
                    }
                    if (player.uesAxeID == 1)
                    {
                        Instantiate(wood, woodPos.transform.position, woodPos.transform.rotation);
                        Instantiate(wood, woodPos.transform.position + new Vector3(Random.Range(-2.1f, 2.1f), 0, Random.Range(-2.1f, 2.1f)), woodPos.transform.rotation);
                        Instantiate(wood, woodPos.transform.position + new Vector3(Random.Range(-2.1f, 2.1f), 0, Random.Range(-2.1f, 2.1f)), woodPos.transform.rotation);
                    }
                    if (player.uesAxeID == 2)
                    {
                        Instantiate(wood, woodPos.transform.position, woodPos.transform.rotation);
                        Instantiate(wood, woodPos.transform.position + new Vector3(1, 0, Random.Range(-2.1f, 2.1f)), woodPos.transform.rotation);
                        Instantiate(wood, woodPos.transform.position + new Vector3(-1, 0, Random.Range(-2.1f, 2.1f)), woodPos.transform.rotation);
                        Instantiate(wood, woodPos.transform.position + new Vector3(Random.Range(-2.1f, 2.1f), 0, Random.Range(-2.1f, 2.1f)), woodPos.transform.rotation);
                        Instantiate(wood, woodPos.transform.position + new Vector3(-2, 0, Random.Range(-2.1f, 2.1f)), woodPos.transform.rotation);
                        Instantiate(wood, woodPos.transform.position + new Vector3(Random.Range(-2.1f, 2.1f), 0, Random.Range(-2.1f, 2.1f)), woodPos.transform.rotation);
                    }

                    farm.haveTree = false;

                    Destroy(gameObject);
                }
            }
            
        }

        if (isFriendIn)
        {
            HP -= friendDemage * Time.deltaTime;


         
                gameObject.GetComponent<Animator>().SetBool("IsHit", true);
                
                if (HP <= 0)
                {
                        Instantiate(wood, woodPos.transform.position, woodPos.transform.rotation);
                        Instantiate(wood, woodPos.transform.position + new Vector3(1, 0, Random.Range(-2.1f, 2.1f)), woodPos.transform.rotation);
                        Instantiate(wood, woodPos.transform.position + new Vector3(-1, 0, Random.Range(-2.1f, 2.1f)), woodPos.transform.rotation);
                        Instantiate(wood, woodPos.transform.position + new Vector3(Random.Range(-2.1f, 2.1f), 0, Random.Range(-2.1f, 2.1f)), woodPos.transform.rotation);
                        Instantiate(wood, woodPos.transform.position + new Vector3(-2, 0, Random.Range(-2.1f, 2.1f)), woodPos.transform.rotation);
                        Instantiate(wood, woodPos.transform.position + new Vector3(Random.Range(-2.1f, 2.1f), 0, Random.Range(-2.1f, 2.1f)), woodPos.transform.rotation);
                    

                    farm.haveTree = false;

                    Destroy(gameObject);
                }
            

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isPlayerIn = true;

            //设置此时要放置的类型是农民
            player.friendKind = 0;
        }
        if (other.tag == "Friend_Farmland")
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

        if (other.tag == "Friend_Farmland")
        {
            isFriendIn = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Friend_Farmland")
        {
            isFriendIn = true;
        }
        else
        {
            isFriendIn = false;
        }
    }


    void getAnimBack()
    {
        gameObject.GetComponent<Animator>().SetBool("IsHit", false);
    }
}
