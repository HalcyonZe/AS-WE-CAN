using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_UI : MonoBehaviour
{
    Player player;
    PlayerSFM playerSFM;

    public GameObject LiveUI;
    public GameObject DieUI;

    public Text UI_AnikiSoulNum;
    public Text UI_IronNum;
    public Text UI_StoneNum;
    public Text UI_WoodNum;

    public Text UI_FoodNum;
    public Text UI_WaterNum;

    public Text UI_VanNum;
    public Text UI_BananaManNum;


    public Text UI_LifeNum;
    public Text UI_BodyFoodNum;
    public Text UI_BodyWaterNum;
    public Text UI_ZNum;

    float timer=0;
 public   float waitDieTimer = 0;
    bool isPlayerDie=false;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        playerSFM = FindObjectOfType<PlayerSFM>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>=0.2)
        {
            UI_AnikiSoulNum.text ="*" +player.takeAnikiSoulNum.ToString();
            UI_IronNum.text = "*" + player.takeIronNum.ToString();
            UI_StoneNum.text = "*" + player.takeStoneNum.ToString();
            UI_WoodNum.text = "*" + player.takeWoodNum.ToString();

            UI_FoodNum.text = "*" + player.takeFoodNum.ToString();
            UI_WaterNum.text = "*" + player.takeWaterNum.ToString();


            UI_VanNum.text = player.VanNum.ToString()+ "*";
            UI_BananaManNum.text =   player.BananaManNum.ToString() + "*";

            UI_LifeNum.text = playerSFM.parameter.playerLife.ToString();
            UI_BodyFoodNum.text= player.food.ToString();
            UI_BodyWaterNum.text = player.water.ToString();

            if(player.zInterval>player.zTimer)
            {
                UI_ZNum.text = ((int)(player.zInterval - player.zTimer)).ToString();
            }
            else
            {
                UI_ZNum.text = "Z";
            }

            

            timer = 0;
        }
       
        
        DieUI_Function();
    }

    void DieUI_Function()
    {
        if (playerSFM.parameter.playerLife <= 0)
        {
            isPlayerDie = true;


        }
        if (isPlayerDie)
        {
            waitDieTimer += Time.deltaTime;
        }
        if (waitDieTimer >= 4)
        {
            LiveUI.SetActive(false);
            DieUI.SetActive(true);
        }
    }
  public  void ReStart()
    {
        SceneManager.LoadScene("HD-2D Test");
    }

  public  void ToMain()
    {
        SceneManager.LoadScene("HD-2D Test _Main");
    }
}
