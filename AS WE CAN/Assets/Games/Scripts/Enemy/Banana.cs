using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : Enemys
{
    private Vector3 dir = new Vector3(1, 0, 0);
    protected override void Attack()
    {
        for (int i = 0; i < 8; i++)
        {
            //生成方向
            dir = Quaternion.AngleAxis(45, Vector3.up) * dir;
            dir.Normalize();
            //生成子弹
            GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
            bullet.transform.position = this.transform.position;
            bullet.GetComponent<Bullets>().ShootBullet(dir, bulletSpeed, atk);
        }              
    }
}
