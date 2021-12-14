using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private PlayerMovement player;
    private bool isPlayerIn = false;

    public int addWaterNum = 1;
    public int addLifeNum = 1;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerIn)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                player.water += addWaterNum;
                player.life += addLifeNum;

                Debug.Log("That's good!!！增加水分和生命值" + " 生命：" + player.life + " 水分:" + player.water);

                Destroy(gameObject);
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
