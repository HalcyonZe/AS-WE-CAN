using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{

    protected int atk;
    protected Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    //×Óµ¯³õÊ¼»¯
    public void ShootBullet(Vector3 dir, float speed, int atk)
    {
        this.rigidbody.velocity = dir * speed;
        this.atk = atk;
    }

    /*private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "Player":
                other.GetComponent<PlayerSFM>().LifeChange(-1 * this.atk);
                ObjectPool.Instance.PushObject(gameObject);
                break;
            case "Enemy":
                other.GetComponent<Enemys>().HpChange(-1 * this.atk);
                ObjectPool.Instance.PushObject(gameObject);
                break;
        }
        
    }*/

}
