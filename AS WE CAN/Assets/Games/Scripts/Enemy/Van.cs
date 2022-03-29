using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Van : Enemys
{
    private Vector3 dir = new Vector3(1,0,0);

    protected override void Attack()
    {
        //���ɷ���
        dir = Quaternion.AngleAxis(30,Vector3.up) * dir;
        dir.Normalize();
        //Debug.Log(dir);

        //�����ӵ�
        GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
        bullet.transform.position = this.transform.position;
        bullet.GetComponent<Bullets>().ShootBullet(dir, bulletSpeed, atk);
        
    }


}
