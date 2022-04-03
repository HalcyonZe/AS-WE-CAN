using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : Bullets
{
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Enemy":
                other.GetComponent<Enemys>().HpChange(-1 * this.atk);
                ObjectPool.Instance.PushObject(gameObject);
                break;
            case "Block":
                ObjectPool.Instance.PushObject(gameObject);
                break;
        }
    }
}
