using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : Bullets
{
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                other.GetComponent<PlayerSFM>().LifeChange(-1 * this.atk);
                ObjectPool.Instance.PushObject(gameObject);
                break;
        }
    }
}
