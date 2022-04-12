using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : Bullets
{
    float timer=0;
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer>=10)
        {
            Destroy(gameObject);
        }
    }
   /*
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                other.GetComponent<PlayerSFM>().LifeChange(-1 * this.atk);
                ObjectPool.Instance.PushObject(gameObject);
                break;
            case "Block":
                ObjectPool.Instance.PushObject(gameObject);
                break;
          
        }
    }
    */
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                collision.gameObject.GetComponent<PlayerSFM>().LifeChange(-1 * this.atk);
                ObjectPool.Instance.PushObject(gameObject);
                break;
            case "Block":
                ObjectPool.Instance.PushObject(gameObject);
                break;
            default:
                {
                    Destroy(gameObject);
                }
                break;


        }

    }
}
