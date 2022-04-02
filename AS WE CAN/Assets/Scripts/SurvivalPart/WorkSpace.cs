using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkSpace : MonoBehaviour
{
    public List<Button> buttons;
    private bool isPlayerIn = false;
    private Player player;
    public GameObject canvas;
    bool open=false;

    float timer=0;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.F)&&isPlayerIn)
        {
            if(open == false)
            {
                canvas.SetActive(true);
                open = true;

                Check();

                timer = 0;
            }
            else if (timer > 0.2)
            {
                canvas.SetActive(false);
                open = false;
                timer = 0;

                //debug
                player.useAxe = false;
                player.usePickaxe = false;
            }

        }
       
    }

    //检查是否满足制作要求
    void Check()
    {
        CheckButton_IronAxe();
        CheckButton_AnikiAxe();
        CheckButton_IronPickaxe();
        CheckButton_AnikiPickaxe();
        CheckButton_TakeWater();
        CheckButton_TakeAnikiSoul();
    }

    void CheckButton_IronAxe()
    {
        if(player.takeIronNum>=50&&player.takeWaterNum>=10&&player.uesAxeID==0)
        {
            buttons[0].interactable = true;
        }
        else
        {
            buttons[0].interactable = false;
        }
    }
    void CheckButton_AnikiAxe()
    {
        if (player.takeAnikiSoulNum >= 100 && player.takeWaterNum >= 20&&player.uesAxeID==1)
        {
            buttons[1].interactable = true;
        }
        else
        {
            buttons[1].interactable = false;
        }
    }
    void CheckButton_IronPickaxe()
    {
        if (player.takeIronNum >= 30 && player.takeWaterNum >= 5&&player.usePiceAxeID==0)
        {
            buttons[2].interactable = true;
        }
        else
        {
            buttons[2].interactable = false;
        }
    }

    void CheckButton_AnikiPickaxe()
    {
        if (player.takeAnikiSoulNum >= 60 && player.takeWaterNum >= 10&&player.usePiceAxeID==1)
        {
            buttons[3].interactable = true;
        }
        else
        {
            buttons[3].interactable = false;
        }
    }
    void CheckButton_TakeWater()
    {
        if (player.takeWoodNum >= 2 && player.takeFoodNum>= 2)
        {
            buttons[4].interactable = true;
        }
        else
        {
            buttons[4].interactable = false;
        }
    }

    void CheckButton_TakeAnikiSoul()
    {
        if (player.takeIronNum >= 10&& player.takeFoodNum >= 5&&player.takeStoneNum>=10)
        {
            buttons[5].interactable = true;
        }
        else
        {
            buttons[5].interactable = false;
        }
    }
   public void MakeIronAxe()
    {
        //结算消费
        player.takeIronNum -= 50;
        player.takeWaterNum -= 10;

        //获得道具
        player.uesAxeID = 1;
;       player.axeList[0].SetActive(false);
        player.axeList[1].SetActive(true);
        
        //切换到道具
        player.nowShow.SetActive(false);
        player.axe.SetActive(true);
        player.nowShow =player.axe;

        //更新菜单
        Check();

    }
   public void MakeAnikiAxe()
    {
        player.takeAnikiSoulNum -= 100;
        player.takeWaterNum -= 20;

        player.uesAxeID = 2;
        player.axeList[1].SetActive(false);
        player.axeList[2].SetActive(true);

        player.nowShow.SetActive(false);
        player.axe.SetActive(true);
        player.nowShow = player.axe;

        Check();

    }
 public   void MakeIronPickaxe()
    {
        player.takeIronNum -= 30;
        player.takeWaterNum -= 5;

        player.usePiceAxeID = 1;
        player.pickaxeList[0].SetActive(false);
        player.pickaxeList[1].SetActive(true);

        player.nowShow.SetActive(false);
        player.pickaxe.SetActive(true);
        player.nowShow = player.pickaxe;

        Check();
    }

 public   void MakeAnikiPickaxe()
    {
        player.takeAnikiSoulNum -= 60;
        player.takeWaterNum -= 10;

        player.usePiceAxeID = 2;
        player.pickaxeList[1].SetActive(false);
        player.pickaxeList[2].SetActive(true);

        player.nowShow.SetActive(false);
        player.pickaxe.SetActive(true);
        player.nowShow = player.pickaxe;

        Check();
    }
   public void MakeTakeWater()
    {
        player.takeWoodNum -= 2;
        player.takeFoodNum -= 2;

        player.takeWaterNum++;

        Check();
    }
 public   void MakeTakeAnikiSoul()
    {
        player.takeIronNum -= 10;
        player.takeFoodNum -= 5;
        player.takeStoneNum -= 10;

        player.takeAnikiSoulNum++;

        Check();
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
            canvas.SetActive(false);

            //debug
            player.useAxe = false;
            player.usePickaxe = false;
        }
    }
}
