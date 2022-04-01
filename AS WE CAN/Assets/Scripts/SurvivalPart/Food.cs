using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private Player player;
 
    private bool isPlayerIn=false;

    public int addFoodNum = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType <Player>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerIn)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                player.takeFoodNum+=addFoodNum;
                

                Debug.Log("Oh year!!！增加持有食物数量 持有食物数量：" + player.takeFoodNum);

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
