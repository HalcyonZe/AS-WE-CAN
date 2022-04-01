using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{
    public List<GameObject> growthCycle;


    //�Ƿ�����ľ�赲
   public  bool haveTree=true;

    //�Ƿ�����ֲ״̬
  public  bool isPlan=false;
    
    bool seedling = false;
    bool sapling = false;
    bool mature = false;
    bool resulting = false;
    public bool dead = false;

    //ÿ�������׶���Ҫ��ʱ��
    public int growTimeInterval=5;
    float growTimer=0;

    bool isWatering=false;

    int resultTimes = 0;
   public GameObject resultPos;
   public GameObject bunana;
   public GameObject wood;



    private Player player;
    private bool isPlayerIn = false;
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
            if (isPlayerIn == true && Input.GetKeyDown(KeyCode.F))
            {
                //����
                if (player.takeFoodNum > 0 && seedling == false&&isPlan==false&&haveTree==false)
                {
                    player.takeFoodNum--;
                    seedling = true;
                    isPlan = true;

                    growthCycle[0].SetActive(true);
                }

                //��ˮ
                if (player.Watering())
                {
                    

                    //��ˮ��ʼ��ʱ�������ж����ڱ����ٴν�ˮ������ʱ��
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
                if (growTimer >= growTimeInterval||(resultTimes==3))
                {

                    if (resultTimes <= 2)
                    {
                        if(resultTimes==2)
                        {
                            Instantiate(bunana, resultPos.transform.position + new Vector3(-1, resultPos.transform.position.y,1), resultPos.transform.rotation);

                        }
                        else
                        {
                            Instantiate(bunana, resultPos.transform.position + new Vector3(resultTimes, resultPos.transform.position.y, resultTimes), resultPos.transform.rotation);

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
