using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnikiSoul : MonoBehaviour
{
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
        if (isPlayerIn)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                player.takeAnikiSoulNum++;


                Debug.Log("That's good!!???????л????? ?л꣺" + player.takeAnikiSoulNum);

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
