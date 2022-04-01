using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{




    PlayerSFM playerSFM;
    
   public GameObject gun;
   
    public GameObject axe;
    public List<GameObject> axeList;
   public bool useAxe = false;
    public int uesAxeID=0;

    public GameObject pickaxe;
    public List<GameObject> pickaxeList;
    public bool usePickaxe = false;
    public int usePiceAxeID = 0;

   //buckets part
    public GameObject buckets;
    public List<GameObject> bucketsList;
    public int bucktsWater=0;
    

    public GameObject nowShow;
    
    

   public int water = 10;
    public int maxWater = 10;
    private int food = 10;
    public int maxFood = 10;
    public float hungerInterval = 10;
     float hungerClock;
    public float thirstyInterval = 10;
     float thirstyClock;

    //携带的物资数
    public int takeFoodNum=0;
    public int takeWaterNum=0;
    public int takeWoodNum = 0;
    public int takeIronNum = 0;
    public int takeAnikiSoulNum = 0;
    public int takeStoneNum = 0;


    void Start()
    {
        water = maxWater;
        food  = maxFood;

        playerSFM = gameObject.GetComponent<PlayerSFM>();

        nowShow = gun;
       
        hungerClock = hungerInterval;
        thirstyClock = thirstyInterval;
    }

    void Update()
    {

        //survival part
        survival();

     

        //当前生命值饱食度水分的增益与减少的限制
        Life_Food_Water_Limit();
    }


    //改变桶的图片
  public  void ChangeBucktsSprites()
    {
        if (bucktsWater == 0)
        {
            bucketsList[0].SetActive(true);
            bucketsList[1].SetActive(false);
            bucketsList[2].SetActive(false);
        }
        if (bucktsWater == 1)
        {
            bucketsList[0].SetActive(false);
            bucketsList[1].SetActive(true);
            bucketsList[2].SetActive(false);
        }
        if (bucktsWater == 2)
        {
            bucketsList[0].SetActive(false);
            bucketsList[1].SetActive(false);
            bucketsList[2].SetActive(true);
        }
    }

    //浇水
    public bool Watering()
    {
        
        if((bucktsWater>0)&&buckets.activeSelf)
        {
            bucktsWater--;
            ChangeBucktsSprites();
            return true;
        }
        
        return false;
    }

    void Life_Food_Water_Limit()
    {
        if (playerSFM.parameter.playerLife >= playerSFM.parameter.MaxLife)
        {
            playerSFM.parameter.playerLife = playerSFM.parameter.MaxLife;
        }
        if(playerSFM.parameter.playerLife < 0)
        {
            playerSFM.parameter.playerLife = 0;
        }
        if(food<0)
        {
            food = 0;
        }
        if(food>maxFood)
        {
            food = maxFood;
        }
        if(water<0)
        {
            water = 0;
        }
        if(water>maxWater)
        {
            water = maxWater;
        }
    }
    void survival()
    {
        
        Hunger();

        Thisty();

        EatFood();

        DrinkWater();


        //changeTool
        ShowBuckets();
        ShowGun();
        ShowAxe();
        ShowPickaxe();

        //useTool
        UseAxe();
        UsePickaxe();
    }
    void ShowBuckets()
    {
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            nowShow.SetActive(false);
            buckets.SetActive(true);
            nowShow = buckets;
            
        }
    }
    void ShowGun()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            nowShow.SetActive(false);
            gun.SetActive(true);
            nowShow = gun;

        }
    }

    void  ShowAxe()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            print("5");
            nowShow.SetActive(false);
            axe.SetActive(true);
            nowShow =axe;

        }
    }

    void ShowPickaxe()
    {
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            print("6");
            nowShow.SetActive(false);
            pickaxe.SetActive(true);
            nowShow = pickaxe;

        }
    }

    void Hunger()
    {
        hungerClock -= Time.deltaTime;
        if (hungerClock <= 0)
        {
            hungerClock = hungerInterval;
            food--;
            //Debug.Log("Food:"+food);
            if (food <= 0)
            {
                food = 0;
                playerSFM.LifeChange(-1);
                Debug.Log("你饿坏了！生命减一！" + "生命：" + playerSFM.parameter.playerLife);
            }

        }
    }

    void Thisty()
    {
        thirstyClock -= Time.deltaTime;
        if (thirstyClock <= 0)
        {
            thirstyClock = thirstyInterval;
            water--;

            //Debug.Log("Water:"+water);
            if (water <= 0)
            {
                water = 0;
                playerSFM.LifeChange(-1);
                Debug.Log("你太渴了！生命减一！" + "生命：" + playerSFM.parameter.playerLife);
            }

        }
    }

    void EatFood()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)&&takeFoodNum>0)
        {
            food += 2;
            takeFoodNum--;
            print("吃了:" + food);
        }
      
    }
    void DrinkWater()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && takeWaterNum > 0)
        {
            water += 2;
            takeWaterNum--;
            print("喝了:"+water);
        }
        
    }


    void UseAxe()
    {
        if(axe.activeSelf)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if (axeList[2].activeSelf)
                {
                    axeList[2].GetComponent<Animator>().SetBool("IsHit", true);
                    
                    useAxe = true;
                    uesAxeID = 2;
                }
                else if (axeList[1].activeSelf)
                {
                    axeList[1].GetComponent<Animator>().SetBool("IsHit", true);
                    useAxe = true;
                    uesAxeID = 1;
                }
                else if (axeList[0].activeSelf)
                {
                    axeList[0].GetComponent<Animator>().SetBool("IsHit", true);
                    useAxe = true;
                   
                    uesAxeID = 0;
                }
            }
         
        }
    }

    void UsePickaxe()
    {
        if (pickaxe.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (pickaxeList[2].activeSelf)
                {
                    pickaxeList[2].GetComponent<Animator>().SetBool("IsHit", true);

                    usePickaxe = true;
                    usePiceAxeID = 2;
                }
                else if (pickaxeList[1].activeSelf)
                {
                    pickaxeList[1].GetComponent<Animator>().SetBool("IsHit", true);
                    usePickaxe = true;
                    usePiceAxeID = 1;
                }
                else if (pickaxeList[0].activeSelf)
                {
                    pickaxeList[0].GetComponent<Animator>().SetBool("IsHit", true);
                    usePickaxe = true;

                    usePiceAxeID = 0;
                }
            }

        }
    }

}
