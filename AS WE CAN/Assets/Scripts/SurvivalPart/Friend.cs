using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : MonoBehaviour
{
    public bool isBanana;
    public bool isVan;
    private bool isPlayerIn = false;
    private Player player;

    public bool isFriend_FarmLand;
    public GameObject bucket;
    public GameObject Zax;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isFriend_FarmLand)
        {
            timer += Time.deltaTime;
            if (timer > 6)
            {
                bucket.SetActive(true);
                Zax.SetActive(false);
            }
            if (timer > 10)
            {
                bucket.SetActive(false);
                Zax.SetActive(true);
                timer = 0;
            }
        }
        



        if(isPlayerIn==true&&Input.GetKeyDown(KeyCode.Q))
        {
            if(isBanana==true)
            {
                player.BananaManNum++;
            }
            if(isVan==true)
            {
                player.VanNum++;
            }
         

            Destroy(gameObject);
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
