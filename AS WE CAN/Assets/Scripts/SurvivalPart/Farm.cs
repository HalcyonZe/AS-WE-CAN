using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{
    public List<GameObject> growthCycle;


    //是否有树木阻挡
   public  bool haveTree=true;

    //是否处于种植状态
  public  bool isPlan=false;
    
    bool seedling = false;
    bool sapling = false;
    bool mature = false;
    bool resulting = false;
    public bool dead = false;

    //每个生长阶段需要的时间
    public int growTimeInterval=5;
    float growTimer=0;

    bool isWatering=false;

    int resultTimes = 0;
   public GameObject resultPos;
   public GameObject bunana;
   public GameObject wood;



    private Player player;
    private bool isPlayerIn = false;


    bool isFriendIn = false;
    int friendSkill = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        growTimer += Time.deltaTime;

        if(dead==false)
        {
            if ((isPlayerIn == true && Input.GetKeyDown(KeyCode.F))||isFriendIn)
            {
                //播种
                if (((player.takeFoodNum > 0)||isFriendIn) && seedling == false&&isPlan==false&&haveTree==false)
                {
                    player.takeFoodNum--;
                    seedling = true;
                    isPlan = true;

                    growthCycle[0].SetActive(true);

                    growTimer = 0;
                }

                //浇水
                if (isFriendIn||player.Watering())
                {
                    

                    //浇水后开始计时生长，判断用于避免再次浇水后重置时间
                    if(isWatering==false)
                    {
                        growTimer = 0;
                    }

                    isWatering = true;

                }
            }



            if (seedling == true && isWatering)
            {
                if (growTimer >= growTimeInterval)
                {
                    seedling = false;
                    sapling = true;

                    growthCycle[0].SetActive(false);
                    growthCycle[1].SetActive(true);

                    growTimer = 0;

                    isWatering = false;
                }

            }
            else if (sapling == true && isWatering)
            {
                if (growTimer >= growTimeInterval)
                {
                    sapling = false;
                    mature = true;

                    growthCycle[1].SetActive(false);
                    growthCycle[2].SetActive(true);

                    growTimer = 0;
                }

            }
            else if (mature == true && isWatering)
            {
                if (growTimer >= growTimeInterval)
                {
                    mature = false;
                    resulting = true;

                    growthCycle[2].SetActive(false);
                    growthCycle[3].SetActive(true);

                    growTimer = 0;
                }

            }

            else if (resulting == true)
            {
                if (growTimer >= growTimeInterval||(resultTimes==3)|| (resultTimes == 6))
                {

                    if (resultTimes <= 2&&(player.isAddPlantRusultBananaNum==false||friendSkill==0))
                    {
                        if(resultTimes==2)
                        {
                          Instantiate(bunana, resultPos.transform.position + new Vector3(Random.Range(-2.1f, 2.1f), 0, Random.Range(-2.1f, 2.1f)), resultPos.transform.rotation);
                           


                        }
                        else
                        {
                            Instantiate(bunana, resultPos.transform.position + new Vector3(resultTimes+ Random.Range(-0.5f, 1f), 0, Random.Range(-2.1f, 2.1f)), resultPos.transform.rotation);
                           
                        }
                        growTimer = 0;
                        resultTimes++;

                        
                    }

                    else if(resultTimes <= 5&&(player.isAddPlantRusultBananaNum==true||friendSkill==1))
                    {
                        if (resultTimes == 2)
                        {
                            Instantiate(bunana, resultPos.transform.position + new Vector3(Random.Range(-2.1f, 2.1f), 0, Random.Range(-2.1f, 2.1f)), resultPos.transform.rotation);



                        }
                        else if(resultTimes>2)
                        {
                            Instantiate(bunana, resultPos.transform.position + new Vector3(Random.Range(-2.1f, 2.1f), 0, Random.Range(-1, 1.5f)), resultPos.transform.rotation);

                        }
                        else
                        {
                            Instantiate(bunana, resultPos.transform.position + new Vector3(Random.Range(-2.1f, 2.1f), 0, resultTimes + Random.Range(-0.5f, 1f)), resultPos.transform.rotation);

                        }
                        growTimer = 0;
                        resultTimes++;
                    }
                    else
                    {
                        resulting = false;

                        growthCycle[3].SetActive(false);
                        growthCycle[4].SetActive(true);

                        resultTimes = 0;

                        dead = true;

                        friendSkill = 1;
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
}
