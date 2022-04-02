using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{


  public  MovementState movementState;

    PlayerSFM playerSFM;
    
   public GameObject gun;
    public GameObject Z_Billy;
    float zShowTimer=0;
   
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
    public int food = 10;
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


    private float zTimer = 0;
    public  float zInterval = 20;

    public bool isAddPlantRusultBananaNum = false;

    public int VanNum=0;
    public int BananaManNum=0;

    public GameObject Banana_friend_FarmLand;
    public GameObject Banana_friend_PickAxe;
    public GameObject Van_friend_FarmLand;
    public GameObject Van_friend_Pickaxe;
    
    //0是农民1是矿工
    public int friendKind=0;

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
        zTimer += Time.deltaTime;
        Check_Z_Cd();
        //survival part
        survival();

     

        //当前生命值饱食度水分的增益与减少的限制
        Life_Food_Water_Limit();

        //放置用于生产的友军
        PlaceFriend();


        Z_billy();
    }



    void Z_billy()
    {
        

        //时间被重置说明Z被执行
        if(playerSFM.parameter.Z_Start==true)
        {
            Z_Billy.SetActive(true);
        }

        if(Z_Billy.activeSelf)
        {
            zShowTimer += Time.deltaTime;
        }
        if(zShowTimer>=5)
        {
            Z_Billy.SetActive(false);
            zShowTimer = 0;
            playerSFM.parameter.Z_Start = false;
        }
    }
    //检查计时是否大于语言大招的cd；
   void Check_Z_Cd()
    {
       
        if(zTimer>=zInterval)
        {
            playerSFM.parameter.canZ = true;  
        }
        else
        {
            playerSFM.parameter.canZ = false;
        }


        if(playerSFM.parameter.resetZtimer==true)
        {
            playerSFM.parameter.resetZtimer = false;
            playerSFM.parameter.canZ = false;
            
            zTimer = 0;
        }
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

  public  void  ShowAxe()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
          
            nowShow.SetActive(false);
            axe.SetActive(true);
            nowShow =axe;

        }
    }

 public   void ShowPickaxe()
    {
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            
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
            
            playerSFM.parameter.playerLife += 2;
            
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

    void PlaceFriend()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            //0和1分别是农民和矿工，确认职业后随机选择是van还是香蕉君
            int kind = 0;
            if (friendKind == 0)
            {
                kind = Random.Range(0, 2);
            }
            else if (friendKind == 1)
            {
                kind = Random.Range(2, 4);
            }


            if (kind == 0 && BananaManNum >= 1)
            {
                Instantiate(Banana_friend_FarmLand, gameObject.transform.position, gameObject.transform.rotation);
                BananaManNum--;
            }
            else if (kind == 1 && VanNum >= 1)
            {
                Instantiate(Van_friend_FarmLand, gameObject.transform.position, gameObject.transform.rotation);
                VanNum--;
            }
            else if (kind == 2 && BananaManNum >= 1)
            {
                Instantiate(Banana_friend_PickAxe, gameObject.transform.position, gameObject.transform.rotation);
                BananaManNum--;
            }
            else if (kind == 3 && VanNum >= 1)
            {
                Instantiate(Van_friend_Pickaxe, gameObject.transform.position, gameObject.transform.rotation);
                VanNum--;
            }

        }
    }
        

}
