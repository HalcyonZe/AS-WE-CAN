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
                if (other.GetComponent<Enemys>().m_Hp > 0)
                {
                    other.GetComponent<Enemys>().EnemyToHurt(this.Bulletdir);
                }
                ObjectPool.Instance.PushObject(gameObject);
                break;
            case "Block":
                ObjectPool.Instance.PushObject(gameObject);
                break;
        }
    }
}
