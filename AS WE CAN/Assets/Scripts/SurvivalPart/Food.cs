using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private PlayerMovement player;
    private bool isPlayerIn=false;

    public int addFoodNum = 1;
    public int addLifeNum = 1;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType <PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerIn)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                player.food+=addFoodNum;
                player.life += addLifeNum;

                Debug.Log("Oh year!!！增加饱食度和生命值" + " 生命：" + player.life + " 饱食度:" + player.food);

                Destroy(gameObject);
            }
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       if( other.tag=="Player")
        {
            isPlayerIn = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="Player")
        {
            isPlayerIn = false;
        }
    }
}
