using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public AK ak;
    public List<Button> buttons;
    private bool isPlayerIn = false;
    private Player player;
    PlayerSFM playerSFM;
    public GameObject canvas;
    bool open = false;
    float timer = 0;
   

    bool once_AddLife=true;
    bool once_AddMaxFood = true;
    bool once_AddMaxWater = true;
    bool once_LoseShootInterval = true;
    bool once_LoseBigMoveInterval = true;
    bool once_AddPlantRusultBananaNum = true;

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

        if (Input.GetKeyDown(KeyCode.F) && isPlayerIn)
        {
            if (open == false)
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
            }

        }
    }


    void Check()
    {
        if(player.takeAnikiSoulNum>=100&&once_AddLife)
        {
            buttons[0].interactable = true;

        }
        else
        {
            buttons[0].interactable = false;
        }

        if (player.takeAnikiSoulNum >= 50&&once_AddMaxFood)
        {
            buttons[1].interactable = true;

        }
        else
        {
            buttons[1].interactable = false;
        }

        if (player.takeAnikiSoulNum >= 25&&once_AddMaxWater)
        {
            buttons[2].interactable = true;

        }
        else
        {
            buttons[2].interactable = false;
        }

        if (player.takeAnikiSoulNum >= 150&&once_LoseShootInterval)
        {
            buttons[3].interactable = true;

        }
        else
        {
            buttons[3].interactable = false;
        }

        if (player.takeAnikiSoulNum >= 500&&once_LoseBigMoveInterval)
        {
            buttons[4].interactable = true;

        }
        else
        {
            buttons[4].interactable = false;
        }

        if (player.takeAnikiSoulNum >= 100&&once_AddPlantRusultBananaNum)
        {
            buttons[5].interactable = true;

        }
        else
        {
            buttons[5].interactable = false;
        }
    }

    public void ButtonFunction_AddLife()
    {
        player.takeAnikiSoulNum -= 100;

        playerSFM.parameter.MaxLife += (int)(playerSFM.parameter.MaxLife * 0.5);
        playerSFM.parameter.playerLife = playerSFM.parameter.MaxLife;
        
        once_AddLife = false;

        Check();
    }

    public void ButtonFunction_AddMaxFood()
    {
        player.takeAnikiSoulNum -= 50;

        player.maxFood += (int)(player.maxFood * 0.5f);
        player.food = player.maxFood;

        once_AddMaxFood = false;

        Check();
    }

    public void ButtonFunction_AddMaxWater()
    {
        player.takeAnikiSoulNum -= 25;

        player.maxWater += (int)(player.maxWater * 0.5f);
        player.water = player.maxWater;

        once_AddMaxWater = false;

        Check();
    }

    public void ButtonFunction_LoseShootInterval()
    {
        player.takeAnikiSoulNum -= 150;

        ak.fire_interval=0.2f;

        once_LoseShootInterval = false;

        Check();
    }

    public void ButtonFunction_LoseBigMoveInterval()
    {
        player.takeAnikiSoulNum -= 500;

        player.zInterval = 10;

        once_LoseBigMoveInterval = false;

        Check();
    }

    public void ButtonFunction_AddPlantRusultBananaNum()
    {
        player.takeAnikiSoulNum -= 100;

        player.isAddPlantRusultBananaNum = true;

        once_AddPlantRusultBananaNum = false;

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
        }
    }
}
